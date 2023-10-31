using System.Text;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using ClosedXML.Excel;
using WebVisit.Models;
using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class CardController: BaseController
    {
        // private Repository<Company>? CompanyData { get; set; }
        //인원출입증 
        private Repository<CardIssueApply>? CardIssueApplyData { get; set; }
        private Repository<CardIssueApplyHistory>? CardIssueApplyHistoryData { get; set; }        
        //차량출입증
        private Repository<CarCardIssueApply>? CarCardIssueApplyData { get; set; }
        private Repository<CarCardIssueApplyHistory>? CarCardIssueApplyHistoryData { get; set; }
        //임시증         
        private Repository<TempCardIssueApply>? TempCardIssueApplyData { get; set; }
        private Repository<TempCardIssueApplyHistory>? TempCardIssueApplyHistoryData { get; set; }
        
        private Repository<ViewCardPerson>? ViewCardPersonData { get; set; }

        public CardController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer){}

        protected override void Init(){
            if (DbSVISIT != null) {
                CompanyData ??= new Repository<Company>(DbSVISIT);
                CardIssueApplyData ??= new Repository<CardIssueApply>(DbSVISIT);
                CardIssueApplyHistoryData ??= new Repository<CardIssueApplyHistory>(DbSVISIT);
                CarCardIssueApplyData ??= new Repository<CarCardIssueApply>(DbSVISIT);
                CarCardIssueApplyHistoryData ??= new Repository<CarCardIssueApplyHistory>(DbSVISIT);
                TempCardIssueApplyData ??= new Repository<TempCardIssueApply>(DbSVISIT);
                TempCardIssueApplyHistoryData ??= new Repository<TempCardIssueApplyHistory>(DbSVISIT);
            }
            if (DbPASSPORT != null){
                ViewCardPersonData ??= new Repository<ViewCardPerson>(DbPASSPORT);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public IActionResult Index() {
            return RedirectToAction("List");
        }
        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcardtypeid?}/{searchcardissuestatus?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult List (CardGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var my = GetMe();
            // bool accessible = IsMaster() || IsGeneralManager() || IsHR() || IsESH() || IsSecurity() || IsPartnerNonresidentManager() ;
            // if (accessible == false) {
            //     return View("_Inaccessible");
            // }
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsHR() ||  IsESH() || IsPartnerNonresidentManager();

            CardGridData filterhValue = (CardGridData) FilterGridData(values);
            var m = GetViewCardPersonList(filterhValue);

            var options2 = new QueryOptions<Company>
            {
                Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
            };
            // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            // PersonType 회원구분: 임직원(0)/상주직원(1)/비상주관리자(3)/비상주직원(4)/방문객(5)
            // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
            // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
            CardListViewModel vm =new(){
                ViewCardPeople = m.a,
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType), // 출입증구분 0: 일반, 1: 방문증 
                CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType), 
                CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus), // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정               
                CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
                CurrentRoute = values,
                SearchRoute=filterhValue,
                TotalPages = values.GetTotalPages(m.b),
                TotalCnt = m.b,
            };
            if (CompanyData != null){
                vm.Companies = CompanyData.List(options2);
                // var options = new QueryOptions<CardIssueApply>
                // {
                //     PageNumber = values.PageNumber,
                //     PageSize = values.PageSize,
                //     OrderByDirection = values.SortDirection,
                //     Where = a => a.CardApplyStatus == 1 && a.IsDelete == "N",
                // };
                // CardIssueApplys = CardIssueApplyData.List(options),
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcardtypeid?}/{searchcardissuestatus?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult? ExcelDownload(CardGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            // PersonDTO my = GetMe();
            bool accessible = IsMaster() || IsGeneralManager() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }

            DataTable dt = new(ExTitle);                   
            // 사업장	출입증번호	구분	회사명	사번	성명	재발행차수	발급	유효기간
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Access CardNo"]), new DataColumn(@Localizer["Classify"]),
                                        new DataColumn(@Localizer["Company Name"]), new DataColumn(@Localizer["Sabun"]), new DataColumn(@Localizer["Name"]),
                                        new DataColumn(@Localizer["Reissue Number"]),new DataColumn(@Localizer["Issue"]),new DataColumn(@Localizer["Valid Period"]), });
            
            CardGridData filterhValue = (CardGridData) FilterGridData(values);
            var v = GetViewCardPersonList(filterhValue, true);
            if (v != null){
                List<CommonCode> CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType); // 출입증구분 0: 일반, 1: 방문증 
                List< CommonCode > CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus); // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정               
                // List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                // List< CommonCode > CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType);
                // List<CommonCode> CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType);
                // List<CommonCode> CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus);
                foreach(ViewCardPerson viewCardPerson in v.a){
                        var location = viewCardPerson.PersonUser2;
                        var cardTypeName = "";
                        var cardIssueStatusName = "";
                        var cardValidDate = "";
                        if (viewCardPerson.CardType >=0 && CodeCardType != null) {
                            foreach(var m in CodeCardType) {
                                if (m.SubCodeID != null && m.SubCodeID.Equals(viewCardPerson.CardType)) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        cardTypeName = m.CodeNameKor;
                                    }else {
                                        cardTypeName = m.CodeNameEng;
                                    }
                                }
                            }
                        }
                        if (viewCardPerson?.CardStatusId != null && CodeCardIssueStatus != null) {
                            foreach(var m in CodeCardIssueStatus) {
                                if (m.SubCodeID != null && m.SubCodeID.Equals(viewCardPerson.CardStatusId)) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        cardIssueStatusName = m.CodeNameKor;
                                    }else {
                                        cardIssueStatusName = m.CodeNameEng;
                                    }
                                }
                            }
                        }
                        if(viewCardPerson?.ValidDate != null){
                            cardValidDate=string.Format("{0:yyyy-MM-dd}", viewCardPerson.ValidDate);
                        }
                    dt.Rows.Add(location, viewCardPerson?.CardNo, cardTypeName, "DB HiTek", viewCardPerson?.Sabun, viewCardPerson?.Name, viewCardPerson?.ReIssueCnt, cardIssueStatusName, cardValidDate);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            }else {
                WriteLog("ExportImport is null");
                return new EmptyResult();            
            }
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcardtypeid?}/{searchcardissuestatus?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult Detail (int PID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("PID:" + PID);

            // var cardIssueApply = CardIssueApplyData.Get(new QueryOptions<CardIssueApply>{
            //     Where = a => a.CardIssueApplyID == cardIssueApplyID,
            // }) ?? new CardIssueApply();
            // WriteLog("cardIssueApply:" + Dump(cardIssueApply));

            var vcp = GetViewCardPersonByPID(PID);

            // var person = GetPerson(vcp.Sabun??"");
            Person person = new();
            var v = GetUnionPerson(null, vcp.Sabun??"");
            var companyName = "";
            if(v != null && v.a.Count > 0){
                person = v.a[0];
                if (person.CompanyID != null){
                    companyName = GetCompanyName(person.CompanyID??0);
                }
                if (person.ImageData != null && person.ImageDataHash != null) {
                    var base64 = Convert.ToBase64String(person.ImageDataHash);
                    ViewBag.img1= String.Format("data:image/gif;base64,{0}", base64);
                }
            }
            WriteLog("person:" + Dump(person));
            
            CardIssueApplyDetailViewModel vm = new(){ 
                // CardIssueApply = cardIssueApply,
                ViewCardPerson = vcp,
                Person = person,
                CompanyName = companyName,
                CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType), // 출입증구분 0: 일반, 1: 방문증 
                CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus), // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus),
            };
            return View(vm);
        }
        
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcardtypeid?}/{searchcardissuestatus?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult Detail(string mode, int cardIssueApplyID, 
            int CardID, string CardNo, int ReissueNo, int CardTypeID, int CardIssueStatus, string CardName, string CardValidDate, 
            string Location, int CompanyID, string CompanyName, int PersonTypeID,
            string Sabun, string Name, string OrgID, string OrgNameKor, string OrgNameEng, int GradeID, string GradeName, string Mobile, string Tel
        )
        { 
            WriteLog("mode: "+mode+", cardIssueApplyID: "+cardIssueApplyID+", CardNo: "+CardNo+", ReissueNo: "+ReissueNo+", CardTypeID: "+CardTypeID+", CardIssueStatus: "+CardIssueStatus+", CardName: "+CardName+", CardValidDate: "+CardValidDate);
            WriteLog("mode: "+mode+", Location: "+Location+", CompanyID: "+CompanyID+", CompanyName: "+CompanyName+", PersonTypeID: "+PersonTypeID+", Sabun: "+Sabun+", Name: "+Name);
            WriteLog("mode: "+mode+", OrgID: "+OrgID+", OrgNameKor: "+OrgNameKor+", OrgNameEng: "+OrgNameEng+", GradeID: "+GradeID+", GradeName: "+GradeName+", Mobile: "+Mobile+", Tel: "+Tel);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();
            if (mode.Equals("E")){ // 출입증신청 정보 수정 
                if(cardIssueApplyID > 0)
                {
                    // 1. 출입증신청 수정 
                    var orgObj = GetCardIssueApply(cardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    
                    newObj.Location = Location; // 사업장구분
                    newObj.CompanyID = CompanyID; // 업체ID
                    newObj.CompanyName = CompanyName; // 회사명
                    newObj.PersonTypeID = PersonTypeID; // 회원구분

                    newObj.Sabun = Sabun; // 신청자 
                    newObj.Name = Name; 
                    newObj.OrgID = OrgID;
                    newObj.OrgNameKor = OrgNameKor;
                    newObj.OrgNameEng = OrgNameEng;
                    newObj.GradeID = GradeID; // 직급ID
                    newObj.GradeName = GradeName; // 직급명
                    newObj.Mobile = Mobile; // 휴대전화
                    newObj.Tel = Tel; // 자택전화

                    // TermsPrivarcyFileData = TermsPrivarcyFileData; // 개인정보활용동의서(첨부파일)
                    // TermsPrivarcyFileDataHash = TermsPrivarcyFileDataHash; // 개인정보활용동의서(첨부파일Hash)

                    // ContactSabun = ContactSabun, // 담당자
                    // ContactName = ContactName,
                    // ContactOrgID = ContactOrgID,
                    // ContactOrgNameKor = ContactOrgNameKor,
                    // ContactOrgNameEng = ContactOrgNameEng,   

                    // CardIssueType = CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                    // ApplyReason = ApplyReason, // 출입증신청사유                    

                    newObj.CardID = CardID; // 출입증ID- S1ACCESS 체크 
                    newObj.CardNo = CardNo; // 출입증번호- S1ACCESS 체크 
                    newObj.CardTypeID = CardTypeID; // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                    newObj.CardName = CardName; // 출입증명- S1ACCESS 체크 
                    // newObj.CardApplyStatus = 0, // 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    newObj.CardIssueStatus = CardIssueStatus; //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                    newObj.CardValidDate = Convert.ToDateTime(CardValidDate); //출입증유효기간
                    newObj.ReissueNo = ReissueNo; // 재발급차수- S1ACCESS 체크 

                    newObj.UpdateDate = DateTime.Now;
                    CardIssueApplyData.Update(newObj);
                    CardIssueApplyData.Save();

                    // 1.1 출입증신청히스토리  => 발급상태 변경에 대한 히스토리 등록시 주석 해제
                    // CardIssueApplyHistory cardIssueApplyHistory = new()
                    // {
                    //     CardIssueApplyID = orgObj.CardIssueApplyID, //출입증신청ID
                    //     Location = Location, // 사업장구분
                    //     CompanyID = CompanyID, // 업체ID
                    //     CompanyName = CompanyName, // 회사명
                    //     PersonTypeID = PersonTypeID, // 회원구분

                    //     Sabun = Sabun, // 신청자 
                    //     Name = Name, 
                    //     OrgID = OrgID,
                    //     OrgNameKor = OrgNameKor,
                    //     OrgNameEng = OrgNameEng,
                    //     GradeID = GradeID, // 직급ID
                    //     GradeName = GradeName, // 직급명
                    //     Mobile = Mobile, // 휴대전화
                    //     Tel = Tel, // 자택전화

                    //     // TermsPrivarcyFileData = TermsPrivarcyFileData, // 개인정보활용동의서(첨부파일)
                    //     // TermsPrivarcyFileDataHash = TermsPrivarcyFileDataHash, // 개인정보활용동의서(첨부파일Hash)

                    //     // ContactSabun = orgObj.ContactSabun, // 담당자
                    //     // ContactName = orgObj.ContactName,
                    //     // ContactOrgID = orgObj.ContactOrgID,
                    //     // ContactOrgNameKor = orgObj.ContactOrgNameKor,
                    //     // ContactOrgNameEng = orgObj.ContactOrgNameEng,   

                    //     // CardIssueType = orgObj.CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                    //     // ApplyReason = orgObj.ApplyReason, // 출입증신청사유
                        
                    //     CardID = CardID, // 출입증ID- S1ACCESS 체크 
                    //     CardNo = CardNo, // 출입증번호- S1ACCESS 체크                         
                    //     CardTypeID = CardTypeID, // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                    //     CardName = CardName, // 출입증명- S1ACCESS 체크 
                    //     // CardApplyStatus = CardApplyStatus, // 출입증신청상태: 승인대기(0), 승인완료(1), 반려(2) 
                    //     CardIssueStatus = CardIssueStatus, //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                    //     CardValidDate = Convert.ToDateTime(CardValidDate), //출입증유효기간
                    //     ReissueNo = ReissueNo, // 재발급차수- S1ACCESS 체크                         
                        
                    //     // Memo = Memo, // 처리사유 

                    //     UpdateSabun = my.Sabun,//등록자
                    //     UpdateName = my.Name,
                    //     UpdateOrgID = my.OrgID,
                    //     UpdateOrgNameKor = my.OrgNameKor,
                    //     UpdateOrgNameEng = my.OrgNameEng,
                    //     InsertUpdateDate = DateTime.Now
                    // };
                    // CardIssueApplyHistoryData.Insert(cardIssueApplyHistory);
                    // CardIssueApplyHistoryData.Save();

                } 
            }

            return RedirectToAction("Detail", new { cardIssueApplyID, culture=GetLang()});
        }
        // {searchcardissuestatus?}/
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcartypeid?}/{searchcarno?}/{searchname?}/{searchsabun?}")]
        public IActionResult CarList (CarCardGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var my = GetMe();
            // bool accessible = IsMaster() || IsGeneralManager() || IsHR() || IsESH() || IsSecurity() || IsPartnerNonresidentManager();
            // if (accessible == false) {
            //     return View("_Inaccessible");
            // }
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsHR() ||  IsESH() || IsPartnerNonresidentManager();

            CarCardListViewModel vm =new();
            if(CarCardIssueApplyData != null){
                CarCardGridData filterhValue = (CarCardGridData) FilterGridData(values);
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
                // CodeCardIssueStatus 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정 
                // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
                // CodeCarType 차량구분코드  : 승용(0)/영업(1)
                // CodeCardType 출입증구분 0: 일반, 1: 방문증 
                vm =new(){
                    CarCardIssueApplys = GetCarList(filterhValue),
                    // CarCardIssueApplys = CarCardIssueApplyData.List(options),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType),
                    CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus), 
                    CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType), 
                    CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType),
                    CurrentRoute = values,
                    SearchRoute=filterhValue,
                    TotalPages = values.GetTotalPages(CarCardIssueApplyData.Count),
                    TotalCnt = CarCardIssueApplyData.Count,
                };
            }
            return View(vm);
        }
        
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcartypeid?}/{searchcarno?}/{searchname?}/{searchsabun?}")]
        public IActionResult? CarExcelDownload(CarCardGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            bool accessible = IsMaster() || IsGeneralManager() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return default;
            }
            DataTable dt = new(ExTitle);
            // 회사명	사업장	차량번호	구분	차량구분	성명	부서명	발급	유효기간
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Company Name"]), new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Car No"]),
                                        new DataColumn(@Localizer["Classify"]), new DataColumn(@Localizer["Car Classify"]), new DataColumn(@Localizer["Name"]),
                                        new DataColumn(@Localizer["Department Name"]),new DataColumn(@Localizer["Issue"]),new DataColumn(@Localizer["Valid Period"]), });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            CarCardGridData filterhValue = (CarCardGridData) FilterGridData(values);
            
            var v = GetCarList(filterhValue);
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            List<CommonCode> CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType);
            List<CommonCode> CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType);
            List<CommonCode> CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType); 
            if(v != null){
                string location = string.Empty; 
                string cardValidDate="";
                string carType = string.Empty;  
                string cardType = string.Empty;  
                string cardIssueType = string.Empty;  

                foreach(var carCardIssueApply in v){
                    location = "";
                    cardValidDate="";
                    carType = "";
                    cardType = "";
                    cardIssueType = "";

                    if (carCardIssueApply.Location != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(carCardIssueApply.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (carCardIssueApply.CardTypeID >= 0 && CodeCardType != null) {
                        foreach(var m in CodeCardType) {
                            if (m.SubCodeID == carCardIssueApply.CardTypeID) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    cardType = m.CodeNameKor;
                                }else {
                                    cardType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (carCardIssueApply.CarTypeCodeID >= 0 && CodeCarType != null) {
                        foreach(var m in CodeCarType) {
                            if (m.SubCodeID == carCardIssueApply.CarTypeCodeID) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    carType = m.CodeNameKor;
                                }else {
                                    carType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (carCardIssueApply.CardIssueType >= 0 && CodeCardIssueType != null) {
                        foreach(var m in CodeCardIssueType) {
                            if (m.SubCodeID == carCardIssueApply.CardIssueType) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    cardIssueType = m.CodeNameKor;
                                }else {
                                    cardIssueType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }                
                    if(carCardIssueApply.CardValidDate  != null){
                        cardValidDate =string.Format("{0:yyyy-MM-dd}", carCardIssueApply.CardValidDate );
                    }
                    // 회사명	사업장	차량번호	구분	차량구분	성명	부서명	발급	유효기간
                    dt.Rows.Add(carCardIssueApply.CompanyName, location, carCardIssueApply.CarNo, cardType, carType, carCardIssueApply.Name, CultureInfo.CurrentCulture.ToString().Equals("ko") ? carCardIssueApply.OrgNameKor : carCardIssueApply.OrgNameEng, cardIssueType, cardValidDate );
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");                
            } else {
                WriteLog("Visit is null");
                return null;
            }           
        }
        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcartypeid?}/{searchcarno?}/{searchname?}/{searchsabun?}")]
        public IActionResult CarDetail (int carCardIssueApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("carCardIssueApplyID:" + carCardIssueApplyID + ", slug" + slug);

            var carCardIssueApply = CarCardIssueApplyData.Get(new QueryOptions<CarCardIssueApply>{
                Where = a => a.CarCardIssueApplyID == carCardIssueApplyID,
            }) ?? new CarCardIssueApply();
            WriteLog("carCardIssueApply:" + Dump(carCardIssueApply));

            var person = GetPerson(carCardIssueApply.Sabun);
            WriteLog("person:" + Dump(person));
            
            CarCardIssueApplyDetailViewModel vm = new(){ 
                CarCardIssueApply = carCardIssueApply,
                Person = person, 
                CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType),
                CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType), // 출입증구분 0: 일반, 1: 방문증 
                CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus), // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcartypeid?}/{searchcarno?}/{searchname?}/{searchsabun?}")]
        public IActionResult CarDetail(string mode, int carCardIssueApplyID, 
            int CardID, string CardNo, int ReissueNo, int CardTypeID, int CardIssueStatus, string CardName, string CardValidDate, 
            string Location, int CompanyID, string CompanyName, int PersonTypeID,
            string Sabun, string Name, string OrgID, string OrgNameKor, string OrgNameEng, int GradeID, string GradeName, string Mobile, string Tel,
            string CarNo, int CarTypeCodeID, string CarModel
        )
        { 
            WriteLog("mode: "+mode+", carCardIssueApplyID: "+carCardIssueApplyID+", CardNo: "+CardNo+", ReissueNo: "+ReissueNo+", CardTypeID: "+CardTypeID+", CardIssueStatus: "+CardIssueStatus+", CardName: "+CardName+", CardValidDate: "+CardValidDate);
            WriteLog("mode: "+mode+", Location: "+Location+", CompanyID: "+CompanyID+", CompanyName: "+CompanyName+", PersonTypeID: "+PersonTypeID+", Sabun: "+Sabun+", Name: "+Name);
            WriteLog("mode: "+mode+", OrgID: "+OrgID+", OrgNameKor: "+OrgNameKor+", OrgNameEng: "+OrgNameEng+", GradeID: "+GradeID+", GradeName: "+GradeName+", Mobile: "+Mobile+", Tel: "+Tel);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();
            if (mode.Equals("E")){ // 출입증신청 정보 수정 
                if(carCardIssueApplyID > 0)
                {
                    // 1. 출입증신청 수정 
                    var orgObj = GetCarCardIssueApply(carCardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    
                    newObj.Location = Location; // 사업장구분
                    newObj.CompanyID = CompanyID; // 업체ID
                    newObj.CompanyName = CompanyName; // 회사명
                    newObj.PersonTypeID = PersonTypeID; // 회원구분

                    newObj.Sabun = Sabun; // 신청자 
                    newObj.Name = Name; 
                    newObj.OrgID = OrgID;
                    newObj.OrgNameKor = OrgNameKor;
                    newObj.OrgNameEng = OrgNameEng;
                    newObj.GradeID = GradeID; // 직급ID
                    newObj.GradeName = GradeName; // 직급명
                    newObj.Mobile = Mobile; // 휴대전화
                    newObj.Tel = Tel; // 자택전화

                    // newObj.TermsPrivarcyFileData = TermsPrivarcyFileData; // 개인정보활용동의서(첨부파일)
                    // newObj.TermsPrivarcyFileDataHash = TermsPrivarcyFileDataHash; // 개인정보활용동의서(첨부파일Hash)

                    // newObj.ContactSabun = ContactSabun, // 담당자
                    // newObj.ContactName = ContactName,
                    // newObj.ContactOrgID = ContactOrgID,
                    // newObj.ContactOrgNameKor = ContactOrgNameKor,
                    // newObj.ContactOrgNameEng = ContactOrgNameEng,   

                    // newObj.CardIssueType = CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                    // newObj.ApplyReason = ApplyReason, // 출입증신청사유 
                    
                    newObj.CarNo = CarNo; // 차량번호
                    newObj.CarTypeCodeID = CarTypeCodeID; // 차량구분코드ID
                    // newObj.CarModel = CarModel; // 차량모델
                    // newObj.CarLIcenseFileData = CarLIcenseFileData; // 차량등록증(첨부파일)
                    // newObj.CarLIcenseFileDataHash = CarLIcenseFileDataHash; // 차량등록증(첨부파일Hash)
                                       

                    newObj.CardID = CardID; // 출입증ID- S1ACCESS 체크 
                    newObj.CardNo = CardNo; // 출입증번호- S1ACCESS 체크 
                    newObj.CardTypeID = CardTypeID; // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                    newObj.CardName = CardName; // 출입증명- S1ACCESS 체크 
                    // newObj.CardApplyStatus = 0, // 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    newObj.CardIssueStatus = CardIssueStatus; //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                    newObj.CardValidDate = Convert.ToDateTime(CardValidDate); //출입증유효기간
                    newObj.ReissueNo = ReissueNo; // 재발급차수- S1ACCESS 체크 

                    newObj.UpdateDate = DateTime.Now;
                    CarCardIssueApplyData.Update(newObj);
                    CarCardIssueApplyData.Save();
                    WriteLog("CarCardIssueApplyData: "+Dump(CarCardIssueApplyData));

                    // 1.1 출입증신청히스토리 입력 => 발급상태 변경에 대한 히스토리 등록시 주석 해제
                    // CarCardIssueApplyHistory carCardIssueApplyHistory = new()
                    // {
                    //     CarCardIssueApplyID = orgObj.CarCardIssueApplyID, //출입증신청ID
                    //     Location = Location, // 사업장구분
                    //     CompanyID = CompanyID, // 업체ID
                    //     CompanyName = CompanyName, // 회사명
                    //     PersonTypeID = PersonTypeID, // 회원구분

                    //     Sabun = Sabun, // 신청자 
                    //     Name = Name, 
                    //     OrgID = OrgID,
                    //     OrgNameKor = OrgNameKor,
                    //     OrgNameEng = OrgNameEng,
                    //     GradeID = GradeID, // 직급ID
                    //     GradeName = GradeName, // 직급명
                    //     Mobile = Mobile, // 휴대전화
                    //     Tel = Tel, // 자택전화

                    //     // TermsPrivarcyFileData = TermsPrivarcyFileData, // 개인정보활용동의서(첨부파일)
                    //     // TermsPrivarcyFileDataHash = TermsPrivarcyFileDataHash, // 개인정보활용동의서(첨부파일Hash)

                    //     // ContactSabun = orgObj.ContactSabun, // 담당자
                    //     // ContactName = orgObj.ContactName,
                    //     // ContactOrgID = orgObj.ContactOrgID,
                    //     // ContactOrgNameKor = orgObj.ContactOrgNameKor,
                    //     // ContactOrgNameEng = orgObj.ContactOrgNameEng,   

                    //     // CardIssueType = orgObj.CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                    //     // ApplyReason = orgObj.ApplyReason, // 출입증신청사유
                        
                    //     CarNo = CarNo, // 차량번호
                    //     CarTypeCodeID = CarTypeCodeID, // 차량구분코드ID
                    //     // CarModel = CarModel, // 차량모델
                    //     // CarLIcenseFileData = orgObj.CarLIcenseFileData, // 차량등록증(첨부파일)
                    //     // CarLIcenseFileDataHash = orgObj.CarLIcenseFileDataHash, // 차량등록증(첨부파일Hash)

                    //     CardID = CardID, // 출입증ID- S1ACCESS 체크 
                    //     CardNo = CardNo, // 출입증번호- S1ACCESS 체크 
                    //     CardTypeID = CardTypeID, // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                    //     // CardApplyStatus = CardApplyStatus, // 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    //     CardIssueStatus = CardIssueStatus, // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정   
                    //     CardValidDate = Convert.ToDateTime(CardValidDate), //출입증유효기간                 
                    //     // ReissueNo = ReissueNo, // 재발급차수- S1ACCESS 체크 


                    //     UpdateSabun = my.Sabun,//등록자
                    //     UpdateName = my.Name,
                    //     UpdateOrgID = my.OrgID,
                    //     UpdateOrgNameKor = my.OrgNameKor,
                    //     UpdateOrgNameEng = my.OrgNameEng,
                    //     InsertUpdateDate = DateTime.Now
                    // };
                    // CarCardIssueApplyHistoryData.Insert(carCardIssueApplyHistory);
                    // CarCardIssueApplyHistoryData.Save();

                } 
            }

            return RedirectToAction("CarDetail", new { carCardIssueApplyID, culture=GetLang()});
        }
        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult TempList (TempCardIssueApplyGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            TempCardIssueApplyListViewModel vm =new();
            if(TempCardIssueApplyData != null){
                TempCardIssueApplyGridData filterhValue = (TempCardIssueApplyGridData) FilterGridData(values);              
                vm =new(){
                    TempCardIssueApply=GetTempCardIssueApplyList(filterhValue),
                    // TempCardIssueApply = TempCardIssueApplyData.List(options),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CurrentRoute = values,
                    SearchRoute=filterhValue,
                    TotalPages = values.GetTotalPages(TempCardIssueApplyData.Count),
                    TotalCnt = TempCardIssueApplyData.Count,
                };
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult? TempExcelDownload(TempCardIssueApplyGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);          
            // 회사	부서명	성명	발급일시	임시증번호	상태	회수일시
            //2023-08-11 회사명 삭제 new DataColumn(@Localizer["Company Name"]), 
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Name"]),
                                        new DataColumn(@Localizer["Issue DateTime"]), new DataColumn(@Localizer["Access CardNo"]),
                                        new DataColumn(@Localizer["Status"]),new DataColumn(@Localizer["Collection DateTime"]), });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            TempCardIssueApplyGridData filterhValue = (TempCardIssueApplyGridData) FilterGridData(values);
            
            var v = GetTempCardIssueApplyList(filterhValue);
            List<CommonCode> CodeTempCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.TempCardIssueStatus);
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            if(v != null){
                string location = string.Empty;
                string tempCardIssueStatus = string.Empty;  
                string issueDate="";
                string returnDate="";              

                foreach(var tempCardIssueApply in v){
                    location = "";
                    tempCardIssueStatus = "";

                    if (tempCardIssueApply.Location != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(tempCardIssueApply.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (tempCardIssueApply.TempCardIssueStatus >= 0 && CodeTempCardIssueStatus != null) {
                        foreach(var m in CodeTempCardIssueStatus) {
                            if (m.SubCodeID == tempCardIssueApply.TempCardIssueStatus) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    tempCardIssueStatus = m.CodeNameKor;
                                }else {
                                    tempCardIssueStatus = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if(tempCardIssueApply.InsertDate != null){
                        issueDate=string.Format("{0:yyyy-MM-dd HH:mm}", tempCardIssueApply.InsertDate);
                    }
                    if(tempCardIssueApply.ReturnDate != null){
                        returnDate=string.Format("{0:yyyy-MM-dd HH:mm}", tempCardIssueApply.ReturnDate);
                    }
                    dt.Rows.Add(location, CultureInfo.CurrentCulture.ToString().Equals("ko") ? tempCardIssueApply.OrgNameKor : tempCardIssueApply.OrgNameEng, tempCardIssueApply.Name, issueDate, tempCardIssueApply.CardNo, tempCardIssueStatus, returnDate);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");                
            } else {
                WriteLog("Visit is null");
                return null;
            }          
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public async Task<IActionResult> TempListAsync (TempCardIssueApplyGridData values, int tempCardIssueApplyID, int TempCardIssueStatus, string mode = "")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();

            if(mode.Equals("ETempCardIssueStatus")){ // 임시카드 회수

                if (tempCardIssueApplyID > 0)
                {
                    // 1. 임시카드 상태 수정 
                    var orgObj = GetTempCardIssueApply(tempCardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.TempCardIssueStatus = TempCardIssueStatus; // 임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                    if (TempCardIssueStatus == 1)
                    {
                        newObj.ReturnDate = DateTime.Now;
                    }
                    newObj.UpdateDate = DateTime.Now;
                    TempCardIssueApplyData.Update(newObj);
                    TempCardIssueApplyData.Save();

                    // 1.1 반입반출히스토리 입력 (결재상태, 확인수량, 의견)
                    // 확인수량, 의견은 ExportImportHistory 만 에 입력                 
                    TempCardIssueApplyHistory tempCardIssueApplyHistory = new()
                    {
                        TempCardIssueApplyID = newObj.TempCardIssueApplyID, //임시증발급ID
                        Location = newObj.Location, //사업장구분
                        CompanyID = newObj.CompanyID, //업체ID
                        CompanyName = newObj.CompanyName, //회사명
                        PersonTypeID = newObj.PersonTypeID, //회원구분
                        Sabun = newObj.Sabun, //회원사번
                        Name = newObj.Name, //회원이름
                        OrgID = newObj.OrgID, //부서ID
                        OrgNameKor = newObj.OrgNameKor, //부서명Kor
                        OrgNameEng = newObj.OrgNameEng, //부서명Eng
                        GradeID = newObj.GradeID, //직급ID
                        GradeName = newObj.GradeName, //직급명
                        Mobile = newObj.Mobile, //휴대전화
                        Tel = newObj.Tel, //자택전화
                        // CardID = newObj.CardID, //출입증ID
                        CardNo = newObj.CardNo, //출입증번호
                        CardTypeID = newObj.CardTypeID, //출입증구분ID: 0-일반(사원증, 상주사원증, 비상주사원증), 1-방문증(일반방문증/공사출입증)
                        CardName = newObj.CardName, //출입증명
                        TempCardIssueStatus = TempCardIssueStatus, //임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                        ReturnDate = newObj.ReturnDate, //회수일시
                        CardValidDate = newObj.CardValidDate, //출입증유효기간
                        ReissueNo = newObj.ReissueNo, //재발급차수

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    TempCardIssueApplyHistoryData.Insert(tempCardIssueApplyHistory);
                    TempCardIssueApplyHistoryData.Save();
                    WriteLog("tempCardIssueApplyHistory: " + Dump(tempCardIssueApplyHistory));

                    if (DbPASSPORT != null)
                    {
                        // 임시카드 회수
                        StringBuilder sb = new();
                        sb.Append("PROCI_CARD_TEMP @PageType='U', @RStatus='N', @TCardNo='");
                        sb.Append(newObj.CardNo);
                        sb.Append("' , @Sabun='");
                        sb.Append(newObj.Sabun);
                        sb.Append("', @UpdateSabun='");
                        sb.Append(my.Sabun);
                        sb.Append("', @IP='");
                        sb.Append(GetUserIP());
                        sb.Append("'");
                        WriteLog("sp query:" + sb.ToString());
                        var fs1 = FormattableStringFactory.Create(sb.ToString());
                        try
                        {
                            await DbPASSPORT.Database.ExecuteSqlAsync(fs1);
                        }
                        catch (Exception e)
                        {
                            WriteError(e.ToString());
                        }
                    }
                }
            }            
            
            // var options = new QueryOptions<TempCardIssueApply>
            // {
            //     // Includes = "",
            //     PageNumber = values.PageNumber,
            //     PageSize = values.PageSize,
            //     OrderByDirection = values.SortDirection,
            //     Where = a => a.IsDelete == "N",
            // };
            // TempCardIssueApplyListViewModel vm =new(){
            //     TempCardIssueApply = TempCardIssueApplyData.List(options),
            //     CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
            //     CurrentRoute = values,
            //     TotalPages = values.GetTotalPages(TempCardIssueApplyData.Count),
            //     TotalCnt = TempCardIssueApplyData.Count,
            // };
            return RedirectToAction("TempList", new { culture = GetLang() });
            // return View(vm);
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult TempReg ()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            TempCardIssueApplyRegViewModel vm =new(){

            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public async Task<IActionResult> TempRegAsync(string mode, string Location, int CompanyID, string CompanyName, int PersonTypeID, 
            string Sabun, string Name, string OrgID, string OrgNameKor, string OrgNameEng, int GradeID, string GradeName, 
            string Mobile, string Tel, 
            int CardID, string CardNo, int CardTypeID, string CardName, int TempCardIssueStatus, string ReturnDate, string CompanyTel, int ReissueNo
        )
        { 

            WriteLog("mode: "+mode+", Location: "+Location+", CompanyID: "+CompanyID+", CompanyName: "+CompanyName+", PersonTypeID: "+PersonTypeID);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("A")) // 임시증발급 추가
            {
                PersonDTO my = GetMe();
                // 1. 임시증발급 (TempCardIssueApply) 정보 입력 
                TempCardIssueApply tempCardIssueApply = new()
                { 
                    Location = Location, //사업장구분
                    CompanyID = CompanyID, //업체ID
                    CompanyName = CompanyName, //회사명
                    PersonTypeID = PersonTypeID, //회원구분
                    Sabun = Sabun, //회원사번
                    Name = Name, //회원이름
                    OrgID = OrgID, //부서ID
                    OrgNameKor = OrgNameKor, //부서명Kor
                    OrgNameEng = OrgNameEng, //부서명Eng
                    GradeID = GradeID, //직급ID
                    GradeName = GradeName, //직급명
                    Mobile = Mobile, //휴대전화
                    Tel = Tel, //자택전화
                    // CardID = CardID, //출입증ID
                    CardNo = CardNo, //출입증번호
                    CardTypeID = 0, //출입증구분ID: 0-일반(사원증, 상주사원증, 비상주사원증), 1-방문증(일반방문증/공사출입증)
                    CardName = CardName, //출입증명
                    TempCardIssueStatus = 0, //임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                    // ReturnDate = ReturnDate, //회수일시
                    // CardValidDate = CardValidDate, //출입증유효기간
                    // ReissueNo = ReissueNo, //재발급차수

                    InsertSabun = my.Sabun,//등록자
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now
                };
                TempCardIssueApplyData.Insert(tempCardIssueApply);
                TempCardIssueApplyData.Save();
                WriteLog("tempCardIssueApply: "+Dump(tempCardIssueApply));

                // 1.1 임시증발급 히스토리 (TempCardIssueApplyHistory) 정보 입력 
                TempCardIssueApplyHistory tempCardIssueApplyHistory = new()
                { 
                    TempCardIssueApplyID = tempCardIssueApply.TempCardIssueApplyID, //임시증발급ID
                    Location = Location, //사업장구분
                    CompanyID = CompanyID, //업체ID
                    CompanyName = CompanyName, //회사명
                    PersonTypeID = PersonTypeID, //회원구분
                    Sabun = Sabun, //회원사번
                    Name = Name, //회원이름
                    OrgID = OrgID, //부서ID
                    OrgNameKor = OrgNameKor, //부서명Kor
                    OrgNameEng = OrgNameEng, //부서명Eng
                    GradeID = GradeID, //직급ID
                    GradeName = GradeName, //직급명
                    Mobile = Mobile, //휴대전화
                    Tel = Tel, //자택전화
                    // CardID = CardID, //출입증ID
                    CardNo = CardNo, //출입증번호
                    CardTypeID = 0, //출입증구분ID: 0-일반(사원증, 상주사원증, 비상주사원증), 1-방문증(일반방문증/공사출입증)
                    CardName = CardName, //출입증명
                    TempCardIssueStatus = 0, //임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                    // ReturnDate = ReturnDate, //회수일시
                    // CardValidDate = CardValidDate, //출입증유효기간
                    // ReissueNo = ReissueNo, //재발급차수

                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = DateTime.Now
                };
                TempCardIssueApplyHistoryData.Insert(tempCardIssueApplyHistory);
                TempCardIssueApplyHistoryData.Save();
                WriteLog("tempCardIssueApplyHistory: "+Dump(tempCardIssueApplyHistory));

                if (DbPASSPORT != null){
                    // var companyName = GetCompanyName(person.CompanyID??0);
                    StringBuilder sb = new();
                    sb.Append("PROCI_CARD_TEMP @PageType='U', @RStatus='R', @TCardNo='");
                    sb.Append(CardNo);
                    sb.Append("' , @Sabun='");
                    sb.Append(Sabun);
                    sb.Append("', @UpdateSabun='"); 
                    sb.Append(my.Sabun);
                    sb.Append("', @IP='"); 
                    sb.Append(GetUserIP());
                    sb.Append("'");
                    WriteLog("sp query:"+sb.ToString());
                    var fs1 = FormattableStringFactory.Create(sb.ToString());
                    try{
                        await DbPASSPORT.Database.ExecuteSqlAsync(fs1);
                    }catch(Exception e){
                        WriteError(e.ToString());
                    }
                }                
            }
            return RedirectToAction("TempList", new { culture=GetLang() });   
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult TempDetail (int tempCardIssueApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("tempCardIssueApplyID:" + tempCardIssueApplyID + ", slug" + slug);

            var tempCardIssueApply = TempCardIssueApplyData.Get(new QueryOptions<TempCardIssueApply>{
                Where = a => a.TempCardIssueApplyID == tempCardIssueApplyID,
            }) ?? new TempCardIssueApply();

            TempCardIssueApplyDetailViewModel vm = new(){
                TempCardIssueApply = tempCardIssueApply,
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                
                // TempCardIssueStatus 임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                CodeTempCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.TempCardIssueStatus),
            };

            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")]
        public IActionResult TempDetail(string mode, int tempCardIssueApplyID, int TempCardIssueStatus)
        { 
            WriteLog("mode: "+mode+", tempCardIssueApplyID: "+tempCardIssueApplyID+", TempCardIssueStatus: "+TempCardIssueStatus);
            
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            if(mode.Equals("ETempCardIssueStatus")){ // 임시카드 회수

                if(tempCardIssueApplyID > 0)
                {
                    // 1. 임시카드 상태 수정 
                    var orgObj = GetTempCardIssueApply(tempCardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.TempCardIssueStatus = TempCardIssueStatus; // 임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                    if(TempCardIssueStatus == 1){
                        newObj.ReturnDate = DateTime.Now;
                    }
                    newObj.UpdateDate = DateTime.Now;
                    TempCardIssueApplyData.Update(newObj);
                    TempCardIssueApplyData.Save();

                    // 1.1 반입반출히스토리 입력 (결재상태, 확인수량, 의견)
                    // 확인수량, 의견은 ExportImportHistory 만 에 입력                 
                    TempCardIssueApplyHistory tempCardIssueApplyHistory = new()
                    { 
                        TempCardIssueApplyID = newObj.TempCardIssueApplyID, //임시증발급ID
                        Location = newObj.Location, //사업장구분
                        CompanyID = newObj.CompanyID, //업체ID
                        CompanyName = newObj.CompanyName, //회사명
                        PersonTypeID = newObj.PersonTypeID, //회원구분
                        Sabun = newObj.Sabun, //회원사번
                        Name = newObj.Name, //회원이름
                        OrgID = newObj.OrgID, //부서ID
                        OrgNameKor = newObj.OrgNameKor, //부서명Kor
                        OrgNameEng = newObj.OrgNameEng, //부서명Eng
                        GradeID = newObj.GradeID, //직급ID
                        GradeName = newObj.GradeName, //직급명
                        Mobile = newObj.Mobile, //휴대전화
                        Tel = newObj.Tel, //자택전화
                        // CardID = newObj.CardID, //출입증ID
                        CardNo = newObj.CardNo, //출입증번호
                        CardTypeID = newObj.CardTypeID, //출입증구분ID: 0-일반(사원증, 상주사원증, 비상주사원증), 1-방문증(일반방문증/공사출입증)
                        CardName = newObj.CardName, //출입증명
                        TempCardIssueStatus = TempCardIssueStatus, //임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
                        ReturnDate = newObj.ReturnDate, //회수일시
                        CardValidDate = newObj.CardValidDate, //출입증유효기간
                        ReissueNo = newObj.ReissueNo, //재발급차수
                        
                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    TempCardIssueApplyHistoryData.Insert(tempCardIssueApplyHistory);
                    TempCardIssueApplyHistoryData.Save();
                    WriteLog("tempCardIssueApplyHistory: "+Dump(tempCardIssueApplyHistory));
                }
            }
            return RedirectToAction("TempDetail", new { tempCardIssueApplyID, culture=GetLang()});
        }

        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
        public IActionResult ApplyList (CardIssueApplyGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            ViewBag.Readable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            CardIssueApplyListViewModel vm =new();
            if(CardIssueApplyData != null){
                CardIssueApplyGridData filterhValue = (CardIssueApplyGridData) FilterGridData(values);  
                var options2 = new QueryOptions<Company>
                {
                    Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
                };

                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                // PersonType 회원구분: 임직원(0)/상주직원(1)/비상주관리자(3)/비상주직원(4)/방문객(5)
                // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
                // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                vm =new(){
                    // CardIssueApplys = CardIssueApplyData.List(options),
                    CardIssueApplys = GetApplyList(filterhValue),
                    Companies = CompanyData.List(options2),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                    CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType),                
                    CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
                    CurrentRoute = values,
                    SearchRoute=filterhValue,
                    TotalPages = values.GetTotalPages(CardIssueApplyData.Count),
                    TotalCnt = CardIssueApplyData.Count,
                };
                // CardIssueApplyListViewModel vm =new(){
                //     CardIssueApplys = CardIssueApplyData.List(options),
                //     Companies = CompanyData.List(options2),
                //     CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                //     CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                //     CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType),                
                //     CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
                //     CurrentRoute = values,
                //     SearchRoute=filterhValue,
                //     TotalPages = values.GetTotalPages(CardIssueApplyData.Count),
                //     TotalCnt = CardIssueApplyData.Count,
                // };
            }
            return View(vm);
        }


        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
        public IActionResult? ApplyExcelDownload(CardIssueApplyGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            // 신청일자	사업장	회사명	사원구분	발급구분	신청사유	신청상태
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn(@Localizer["Apply Date"]), new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]),
                                        new DataColumn(@Localizer["Employee Classify"]), new DataColumn(@Localizer["Issue Classify"]), new DataColumn(@Localizer["Application Reason"]),
                                        new DataColumn(@Localizer["Apply Status"]), });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            CardIssueApplyGridData filterhValue = (CardIssueApplyGridData) FilterGridData(values);
            var v = GetApplyList(filterhValue);
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            List<CommonCode> CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType);
            List<CommonCode> CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType); 
            List<CommonCode> CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus); 
            if(v != null){
                string location = string.Empty; 
                string insertDate="";
                string personType = "";
                string cardIssueType = string.Empty;
                string cardApplyStatus = string.Empty;

                foreach(var cardIssueApply in v){
                    location = "";
                    insertDate="";
                    personType = "";
                    cardIssueType = "";
                    cardApplyStatus = "";

                    if (cardIssueApply.Location != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(cardIssueApply.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (cardIssueApply.PersonTypeID >= 0 && CodePersonType != null) {
                        foreach(var m in CodePersonType) {
                            if (m.SubCodeID == cardIssueApply.PersonTypeID) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    personType = m.CodeNameKor;
                                }else {
                                    personType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (cardIssueApply.CardIssueType >= 0 && CodeCardIssueType != null) {
                        foreach(var m in CodeCardIssueType) {
                            if (m.SubCodeID == cardIssueApply.CardIssueType) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    cardIssueType = m.CodeNameKor;
                                }else {
                                    cardIssueType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (cardIssueApply.CardApplyStatus >= 0 && CodeCardApplyStatus != null) {
                        foreach(var m in CodeCardApplyStatus) {
                            if (m.SubCodeID == cardIssueApply.CardApplyStatus) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    cardApplyStatus = m.CodeNameKor;
                                }else {
                                    cardApplyStatus = m.CodeNameEng??"";
                                }
                            }
                        }
                    }                    
                    if(cardIssueApply.InsertDate != null){
                        insertDate=string.Format("{0:yyyy-MM-dd}", cardIssueApply.InsertDate);
                    }
                    dt.Rows.Add(insertDate, location, cardIssueApply.CompanyName, personType, cardIssueType, cardIssueApply.ApplyReason, cardApplyStatus);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");                
            } else {
                WriteLog("Visit is null");
                return null;
            }          
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
        public IActionResult ApplyReg ()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsPartner = IsPartnerNonresidentManager();
            ViewBag.IsEmployee = IsEmployee();
            ViewBag.my = my;
            WriteLog("my:"+Dump(my));
            // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            List<ViewDuty> contactPersonList = GetViewDudies(null);
            WriteLog("contactPersonList:"+Dump(contactPersonList));
            // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
            CardIssueApplyRegViewModel vm =new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType),
                ContactPersonList = contactPersonList
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
        public async Task<IActionResult> ApplyReg(string mode,  CardIssueApply cardIssueApply){ 
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            if (mode.Equals("A") && CardIssueApplyData != null && CardIssueApplyHistoryData != null) // 출입증 신청 추가
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                // 1. 출입증신청 (CardIssueApply) 정보 입력 
                cardIssueApply.CardTypeID = 0; // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증)  // to-do: 차량 출입증 확인 필요 
                cardIssueApply.CardApplyStatus = 0; // 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                cardIssueApply.CardIssueStatus = 1; //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                cardIssueApply.InsertSabun = my.Sabun;//등록자
                cardIssueApply.InsertName = my.Name;
                cardIssueApply.InsertOrgID = my.OrgID;
                cardIssueApply.InsertOrgNameKor = my.OrgNameKor;
                cardIssueApply.InsertOrgNameEng = my.OrgNameEng;
                cardIssueApply.InsertDate = today;
                if(cardIssueApply.CompanyName == null){
                    cardIssueApply.CompanyName = "";
                    cardIssueApply.CompanyID = -1;
                }
                if(cardIssueApply.FormFile != null && cardIssueApply.FormFile.Count > 0){
                    FileData fileData = await GetFileData(cardIssueApply.FormFile[0]);
                    cardIssueApply.TermsPrivarcyFileDataHash = fileData.Hash;
                    if (!String.IsNullOrEmpty(fileData.Meta)) {
                        cardIssueApply.TermsPrivarcyFileData = fileData.Meta;
                    }
                }
                // CardID = CardID, // 출입증ID- S1ACCESS 체크 
                // CardNo = CardNo, // 출입증번호- S1ACCESS 체크 
                // CardName = CardName, // 출입증명- S1ACCESS 체크 
                // CardValidDate = CardValidDate, //출입증유효기간
                // ReissueNo = ReissueNo, // 재발급차수- S1ACCESS 체크 
                CardIssueApplyData.Insert(cardIssueApply);
                CardIssueApplyData.Save();
                WriteLog("cardIssueApply: "+Dump(cardIssueApply));

                // 1.1 출입증신청히스토리 (CardIssueApplyHistory)  입력 
                CardIssueApplyHistory cardIssueApplyHistory = new()
                {
                    CardIssueApplyID = cardIssueApply.CardIssueApplyID, //출입증신청ID
                    Location = cardIssueApply.Location, // 사업장구분
                    CompanyID = cardIssueApply.CompanyID, // 업체ID
                    CompanyName = cardIssueApply.CompanyName, // 회사명
                    PersonTypeID = cardIssueApply.PersonTypeID, // 회원구분

                    Sabun = cardIssueApply.Sabun, // 신청자 
                    Name = cardIssueApply.Name, 
                    OrgID = cardIssueApply.OrgID,
                    OrgNameKor = cardIssueApply.OrgNameKor,
                    OrgNameEng = cardIssueApply.OrgNameEng,
                    GradeID = cardIssueApply.GradeID, // 직급ID
                    GradeName = cardIssueApply.GradeName, // 직급명
                    Mobile = cardIssueApply.Mobile, // 휴대전화
                    Tel = cardIssueApply.Tel, // 자택전화

                    // TermsPrivarcyFileData = cardIssueApply.TermsPrivarcyFileData, // 개인정보활용동의서(첨부파일)
                    // TermsPrivarcyFileDataHash = cardIssueApply.TermsPrivarcyFileDataHash, // 개인정보활용동의서(첨부파일Hash)

                    ContactSabun = cardIssueApply.ContactSabun, // 담당자
                    ContactName = cardIssueApply.ContactName,
                    ContactOrgID = cardIssueApply.ContactOrgID,
                    ContactOrgNameKor = cardIssueApply.ContactOrgNameKor,
                    ContactOrgNameEng = cardIssueApply.ContactOrgNameEng,   

                    CardIssueType = cardIssueApply.CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                    ApplyReason = cardIssueApply.ApplyReason, // 출입증신청사유

                    // CardID = cardIssueApply.CardID, // 출입증ID- S1ACCESS 체크 
                    // CardNo = cardIssueApply.CardNo, // 출입증번호- S1ACCESS 체크 
                    CardTypeID = cardIssueApply.CardTypeID, // 출입증구분 - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                    // CardName = CardName, // 출입증명- S1ACCESS 체크 
                    CardApplyStatus = cardIssueApply.CardApplyStatus, // 출입증신청상태: 승인대기(0), 승인완료(1), 반려(2) 
                    CardIssueStatus = cardIssueApply.CardIssueStatus, //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                    // CardValidDate = cardIssueApply.CardValidDate, //출입증유효기간
                    // ReissueNo = cardIssueApply.ReissueNo, // 재발급차수- S1ACCESS 체크 

                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = today
                };
                CardIssueApplyHistoryData.Insert(cardIssueApplyHistory);
                CardIssueApplyHistoryData.Save();

            }

            return RedirectToAction("ApplyList", new { culture=GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
        public IActionResult ApplyDetail (int cardIssueApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("cardIssueApplyID:" + cardIssueApplyID + ", slug" + slug);
            ViewBag.IsEditable = IsMaster() || IsHR();
            var cardIssueApply = CardIssueApplyData.Get(new QueryOptions<CardIssueApply>{
                Where = a => a.CardIssueApplyID == cardIssueApplyID,
            }) ?? new CardIssueApply();

            var options = new QueryOptions<CardIssueApplyHistory>
            {
                Where = a => a.CardIssueApplyID == cardIssueApplyID,
            };
            // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
            CardIssueApplyDetailViewModel vm = new(){ 
                CardIssueApply = cardIssueApply,
                CardIssueApplyHistory = CardIssueApplyHistoryData.List(options),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),                
                CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType),
                CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
        public IActionResult ApplyDetail(string mode, int cardIssueApplyID,
            int CardApplyStatus, string Memo, string ApplyReason)
        { 
            WriteLog("mode: "+mode+", cardIssueApplyID: "+cardIssueApplyID+", CardApplyStatus: "+CardApplyStatus+", Memo: "+Memo);
            WriteLog("mode: "+mode+", ApplyReason: "+ApplyReason);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();
            if(mode.Equals("ECardApplyStatus")){ // 출입증신청 상태 수정

                if(cardIssueApplyID > 0)
                {                
                    // 1. 출입증신청 (CardIssueApply) 상태 수정 
                    var orgObj = GetCardIssueApply(cardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.CardApplyStatus = CardApplyStatus; // 출입증신청상태 CardApplyStatus : 승인대기(0)/승인완료(1)/반려(2)
                    newObj.UpdateDate = DateTime.Now;
                    CardIssueApplyData.Update(newObj);
                    CardIssueApplyData.Save();

                    // 1.1 출입증 진행상태 처리사유 와 함께 입력
                    // 처리사유 는 CardIssueApplyHistory 만 에 입력 
                    CardIssueApplyHistory cardIssueApplyHistory = new()
                    {
                        CardIssueApplyID = orgObj.CardIssueApplyID, //출입증신청ID
                        Location = orgObj.Location, // 사업장구분
                        CompanyID = orgObj.CompanyID, // 업체ID
                        CompanyName = orgObj.CompanyName, // 회사명
                        PersonTypeID = orgObj.PersonTypeID, // 회원구분

                        Sabun = orgObj.Sabun, // 신청자 
                        Name = orgObj.Name, 
                        OrgID = orgObj.OrgID,
                        OrgNameKor = orgObj.OrgNameKor,
                        OrgNameEng = orgObj.OrgNameEng,
                        GradeID = orgObj.GradeID, // 직급ID
                        GradeName = orgObj.GradeName, // 직급명
                        Mobile = orgObj.Mobile, // 휴대전화
                        Tel = orgObj.Tel, // 자택전화

                        TermsPrivarcyFileData = orgObj.TermsPrivarcyFileData, // 개인정보활용동의서(첨부파일)
                        TermsPrivarcyFileDataHash = orgObj.TermsPrivarcyFileDataHash, // 개인정보활용동의서(첨부파일Hash)

                        ContactSabun = orgObj.ContactSabun, // 담당자
                        ContactName = orgObj.ContactName,
                        ContactOrgID = orgObj.ContactOrgID,
                        ContactOrgNameKor = orgObj.ContactOrgNameKor,
                        ContactOrgNameEng = orgObj.ContactOrgNameEng,   

                        CardIssueType = orgObj.CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                        ApplyReason = orgObj.ApplyReason, // 출입증신청사유
                        
                        CardID = orgObj.CardID, // 출입증ID- S1ACCESS 체크 
                        CardNo = orgObj.CardNo, // 출입증번호- S1ACCESS 체크                         
                        CardTypeID = orgObj.CardTypeID, // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                        CardName = orgObj.CardName, // 출입증명- S1ACCESS 체크 
                        CardApplyStatus = CardApplyStatus, // 출입증신청상태: 승인대기(0), 승인완료(1), 반려(2) 
                        CardIssueStatus = orgObj.CardIssueStatus, //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                        CardValidDate = orgObj.CardValidDate, //출입증유효기간
                        ReissueNo = orgObj.ReissueNo, // 재발급차수- S1ACCESS 체크                         
                        
                        Memo = Memo, // 처리사유 

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    CardIssueApplyHistoryData.Insert(cardIssueApplyHistory);
                    CardIssueApplyHistoryData.Save();
                }
            }else if (mode.Equals("E")){ // 출입증신청 정보 수정 
                if(cardIssueApplyID > 0)
                {
                    // 1. 출입증신청 출입증신청사유 수정 
                    var orgObj = GetCardIssueApply(cardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.ApplyReason = ApplyReason; // 출입증신청사유
                    newObj.UpdateDate = DateTime.Now;
                    CardIssueApplyData.Update(newObj);
                    CardIssueApplyData.Save();

                    // 1.1 출입증신청히스토리 입력: 출입증신청사유만 수정시에는 히스토리 입력 안함. 

                } 
            }
            return RedirectToAction("ApplyDetail", new { cardIssueApplyID, culture=GetLang()});
        }

        [HttpPost]
        public FileResult? DownloadCardIssueApply(string CardIssueApplyID, string FileIdx){
            if (IsAccess() == false) {
                return null;
            }
            WriteLog("start download> CardIssueApplyID:"+CardIssueApplyID+" , FileIdx:" + FileIdx);
            if (!string.IsNullOrEmpty(CardIssueApplyID) && !string.IsNullOrEmpty(FileIdx) && !CardIssueApplyID.Equals("0")) 
            {
                int id = int.Parse(CardIssueApplyID);
                int fileIdx = int.Parse(FileIdx);
                CardIssueApply? cardIssueApply = CardIssueApplyData?.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (cardIssueApply!= null)
                { 
                    WriteLog("cardIssueApply:"+Dump(cardIssueApply));
                    if (fileIdx == 0) {
                        if (cardIssueApply.TermsPrivarcyFileDataHash != null && cardIssueApply.TermsPrivarcyFileData != null) {
                            fileData = cardIssueApply.TermsPrivarcyFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(cardIssueApply.TermsPrivarcyFileData);
                        } else {
                            return null;
                        }
                    }
                    WriteLog("fileIdx:" + fileIdx + ", meta:"+Dump(meta));
                    if (fileData != null && meta != null && !String.IsNullOrEmpty(meta.ContentType)) {
                        WriteLog("meta.ContentType:" + meta.ContentType);
                        // 이미지의 경우 meta.FileName 을 주지 않으면 부라우저에서 열림
                        return File(fileData, meta.ContentType, meta.FileName);
                    }
                }
            }
            return null;
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
        public IActionResult CarApplyList (CarCardIssueApplyGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployee() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            ViewBag.Readable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            CarCardIssueApplyListViewModel vm =new();
            if(CarCardIssueApplyData != null){
                CarCardIssueApplyGridData filterhValue = (CarCardIssueApplyGridData) FilterGridData(values);
                var options2 = new QueryOptions<Company>{
                    Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
                };
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                // PersonType 회원구분: 임직원(0)/상주직원(1)/비상주관리자(3)/비상주직원(4)/방문객(5)
                // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
                // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                vm =new(){
                    CarCardIssueApplys = GetCarApplyList(filterhValue),
                    // CarCardIssueApplys = CarCardIssueApplyData.List(options),
                    Companies = CompanyData.List(options2),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                    CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType),                
                    CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
                    CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType),
                    CurrentRoute = values,
                    SearchRoute=filterhValue,
                    TotalPages = values.GetTotalPages(CarCardIssueApplyData.Count),
                    TotalCnt = CarCardIssueApplyData.Count,
                };

            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
        public IActionResult? CarApplyExcelDownload(CarCardIssueApplyGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);          
            // 신청일자	사업장	회사명	차량구분	발급구분	신청사유	신청상태
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn(@Localizer["Apply Date"]), new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]), new DataColumn(@Localizer["Car Classify"]),
                                        new DataColumn(@Localizer["Issue Classify"]), new DataColumn(@Localizer["Application Reason"]), new DataColumn(@Localizer["Apply Status"]), });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            CarCardIssueApplyGridData filterhValue = (CarCardIssueApplyGridData) FilterGridData(values);
            
            var v = GetCarApplyList(filterhValue);
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            List<CommonCode> CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType);
            List<CommonCode> CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType); 
            List<CommonCode> CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus); 
            if(v != null){
                string location = string.Empty; 
                string insertDate="";
                string carType = string.Empty;  
                string cardIssueType = string.Empty;  
                string cardApplyStatus = string.Empty;  

                foreach(var carCardIssueApply in v){
                    location = "";
                    insertDate="";
                    carType = "";
                    cardIssueType = "";
                    cardApplyStatus = "";

                    if (carCardIssueApply.Location != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(carCardIssueApply.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (carCardIssueApply.CarTypeCodeID >= 0 && CodeCarType != null) {
                        foreach(var m in CodeCarType) {
                            if (m.SubCodeID == carCardIssueApply.CarTypeCodeID) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    carType = m.CodeNameKor;
                                }else {
                                    carType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (carCardIssueApply.CardIssueType >= 0 && CodeCardIssueType != null) {
                        foreach(var m in CodeCardIssueType) {
                            if (m.SubCodeID == carCardIssueApply.CardIssueType) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    cardIssueType = m.CodeNameKor;
                                }else {
                                    cardIssueType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (carCardIssueApply.CardApplyStatus >= 0 && CodeCardApplyStatus != null) {
                        foreach(var m in CodeCardApplyStatus) {
                            if (m.SubCodeID == carCardIssueApply.CardApplyStatus) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    cardApplyStatus = m.CodeNameKor;
                                }else {
                                    cardApplyStatus = m.CodeNameEng??"";
                                }
                            }
                        }
                    }                    
                    if(carCardIssueApply.InsertDate != null){
                        insertDate=string.Format("{0:yyyy-MM-dd}", carCardIssueApply.InsertDate);
                    }
                    dt.Rows.Add(insertDate, location, carCardIssueApply.CompanyName, carType, cardIssueType, carCardIssueApply.ApplyReason, cardApplyStatus);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");                
            } else {
                WriteLog("Visit is null");
                return null;
            }
        }
        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
        public IActionResult CarApplyReg ()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsPartner = IsPartnerNonresidentManager();
            ViewBag.IsEmployee = IsEmployee();
            ViewBag.my = my;

            // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            List<ViewDuty> contactPersonList = GetViewDudies(null);
            WriteLog("contactPersonList:"+Dump(contactPersonList));
            // CardIssueType 출입증발급구분: 신규(0)/재발급(1)
            CarCardIssueApplyRegViewModel vm =new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType), 
                CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType),                  
                ContactPersonList = contactPersonList
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
        public async Task<IActionResult> CarApplyReg(string mode,  CarCardIssueApply carCardIssueApply){
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            // if (values != null && values.ToDictionary() != null){
            //     var dict = values.ToDictionary();
            //     dict.Add("culture", GetLang());
            //     ViewBag.prevDic = dict;
            // }
            
            WriteLog("mode: "+mode+ "carCardIssueApply: " +Dump(carCardIssueApply));
            if (CarCardIssueApplyData != null && CarCardIssueApplyHistoryData != null && mode.Equals("A")) // 출입증 신청 추가
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                // 1. 차량출입증신청 (CarCardIssueApply) 정보 입력 
                carCardIssueApply.CardApplyStatus = 0; // 출입증신청상태
                carCardIssueApply.CardIssueStatus = 1; //출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                carCardIssueApply.InsertSabun = my.Sabun;//등록자 
                carCardIssueApply.InsertName = my.Name;
                carCardIssueApply.InsertOrgID = my.OrgID;
                carCardIssueApply.InsertOrgNameKor = my.OrgNameKor;
                carCardIssueApply.InsertOrgNameEng = my.OrgNameEng;
                carCardIssueApply.InsertDate = today;
                if(carCardIssueApply.FormFile != null && carCardIssueApply.FormFile.Count > 0){
                    FileData fileData = await GetFileData(carCardIssueApply.FormFile[0]);
                    carCardIssueApply.CarLIcenseFileDataHash = fileData.Hash;
                    if (!String.IsNullOrEmpty(fileData.Meta)) {
                        carCardIssueApply.CarLIcenseFileData = fileData.Meta;
                    }
                }
                if(carCardIssueApply.FormFile2 != null && carCardIssueApply.FormFile2.Count > 0){
                    FileData fileData = await GetFileData(carCardIssueApply.FormFile2[0]);
                    carCardIssueApply.TermsPrivarcyFileDataHash = fileData.Hash;
                    if (!String.IsNullOrEmpty(fileData.Meta)) {
                        carCardIssueApply.TermsPrivarcyFileData = fileData.Meta;
                    }
                }
                // CardID = CardID, // 출입증ID- S1ACCESS 체크 
                // CardNo = CardNo, // 출입증번호- S1ACCESS 체크 
                // CardTypeID = 0, // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                // CardName = CardName, // 출입증명- S1ACCESS 체크
                // CardValidDate = CardValidDate, //출입증유효기간
                // ReissueNo = ReissueNo, // 재발급차수- S1ACCESS 체크
                CarCardIssueApplyData.Insert(carCardIssueApply);
                CarCardIssueApplyData.Save();
                WriteLog("carCardIssueApply: "+Dump(carCardIssueApply));
                // 1.1 차량출입증신청히스토리 (CarCardIssueApplyHistory)  입력 
                CarCardIssueApplyHistory carCardIssueApplyHistory = new(){
                    CarCardIssueApplyID = carCardIssueApply.CarCardIssueApplyID, //출입증신청ID
                    Location = carCardIssueApply.Location, // 사업장구분
                    CompanyID = carCardIssueApply.CompanyID, // 업체ID
                    CompanyName = carCardIssueApply.CompanyName, // 회사명
                    PersonTypeID = carCardIssueApply.PersonTypeID, // 회원구분
                    Sabun = carCardIssueApply.Sabun, // 신청자 
                    Name = carCardIssueApply.Name, 
                    OrgID = carCardIssueApply.OrgID,
                    OrgNameKor = carCardIssueApply.OrgNameKor,
                    OrgNameEng = carCardIssueApply.OrgNameEng,
                    GradeID = carCardIssueApply.GradeID, // 직급ID
                    GradeName = carCardIssueApply.GradeName, // 직급명
                    Mobile = carCardIssueApply.Mobile, // 휴대전화
                    Tel = carCardIssueApply.Tel, // 자택전화
                    // TermsPrivarcyFileData = carCardIssueApply.TermsPrivarcyFileData, // 개인정보활용동의서(첨부파일)
                    // TermsPrivarcyFileDataHash = carCardIssueApply.TermsPrivarcyFileDataHash, // 개인정보활용동의서(첨부파일Hash)
                    ContactSabun = carCardIssueApply.ContactSabun, // 담당자
                    ContactName = carCardIssueApply.ContactName,
                    ContactOrgID = carCardIssueApply.ContactOrgID,
                    ContactOrgNameKor = carCardIssueApply.ContactOrgNameKor,
                    ContactOrgNameEng = carCardIssueApply.ContactOrgNameEng,   
                    CardIssueType = carCardIssueApply.CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                    ApplyReason = carCardIssueApply.ApplyReason, // 출입증신청사유
                    CarNo = carCardIssueApply.CarNo, // 차량번호
                    CarTypeCodeID = carCardIssueApply.CarTypeCodeID, // 차량구분코드ID
                    CarModel = carCardIssueApply.CarModel, // 차량모델
                    // CarLIcenseFileData = carCardIssueApply.CarLIcenseFileData, // 차량등록증(첨부파일)
                    // CarLIcenseFileDataHash = carCardIssueApply.CarLIcenseFileDataHash, // 차량등록증(첨부파일Hash)
                    // CardID = carCardIssueApply.CardID, // 출입증ID- S1ACCESS 체크 
                    // CardNo = carCardIssueApply.CardNo, // 출입증번호- S1ACCESS 체크 
                    // CardTypeID = carCardIssueApply.CardTypeID, // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                    // CardName = carCardIssueApply.CardName, // 출입증명- S1ACCESS 체크
                    CardApplyStatus = carCardIssueApply.CardApplyStatus, // 출입증신청상태
                    CardIssueStatus = carCardIssueApply.CardIssueStatus, // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                    // CardValidDate = carCardIssueApply.CardValidDate, //출입증유효기간
                    // ReissueNo = carCardIssueApply.ReissueNo, // 재발급차수- S1ACCESS 체크 
                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = today
                };
                CarCardIssueApplyHistoryData.Insert(carCardIssueApplyHistory);
                CarCardIssueApplyHistoryData.Save();
            }
            return RedirectToAction("CarApplyList", new { culture=GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
        public IActionResult CarApplyDetail (int carCardIssueApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            ViewBag.IsEditable = IsMaster() || IsHR();            
            WriteLog("carCardIssueApplyID:" + carCardIssueApplyID + ", slug" + slug);

            // if (values != null && values.ToDictionary() != null){
            //     var dict = values.ToDictionary();
            //     dict.Add("culture", GetLang());
            //     ViewBag.prevDic = dict;
            // }

            var carCardIssueApply = CarCardIssueApplyData.Get(new QueryOptions<CarCardIssueApply>{
                Where = a => a.CarCardIssueApplyID == carCardIssueApplyID,
            }) ?? new CarCardIssueApply();

            var options = new QueryOptions<CarCardIssueApplyHistory>
            {
                Where = a => a.CarCardIssueApplyID == carCardIssueApplyID,
            };
            // CodeCardApplyStatus 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
            CarCardIssueApplyDetailViewModel vm = new(){ 
                CarCardIssueApply = carCardIssueApply,
                CarCardIssueApplyHistory = CarCardIssueApplyHistoryData.List(options),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),                
                CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType),
                CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
                CodeCarType = GetCommonCodes((int)VisitEnum.CommonCode.CarType),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
        public IActionResult CarApplyDetail(string mode, int carCardIssueApplyID,
            int CardApplyStatus, string Memo, string ApplyReason)
        { 
            WriteLog("mode: "+mode+", carCardIssueApplyID: "+carCardIssueApplyID+", CardApplyStatus: "+CardApplyStatus+", Memo: "+Memo);
            WriteLog("mode: "+mode+", ApplyReason: "+ApplyReason);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();

            if(mode.Equals("ECardApplyStatus")){ // 출입증신청 상태 수정

                if(carCardIssueApplyID > 0)
                {                
                    // 1. 출입증신청 (CardIssueApply) 상태 수정 
                    var orgObj = GetCarCardIssueApply(carCardIssueApplyID, true);
                    var newObj = orgObj.Clone();                    
                    newObj.CardApplyStatus = CardApplyStatus; // 출입증신청상태 CardApplyStatus : 승인대기(0)/승인완료(1)/반려(2)
                    newObj.UpdateDate = DateTime.Now;
                    CarCardIssueApplyData.Update(newObj);
                    CarCardIssueApplyData.Save();

                    if(CardApplyStatus == 1){
                        // 승인처리할때 회원(Person)의 차량등록대수(CarRegCnt) 증가 시키기 CarRegCnt +1  
                        if(!string.IsNullOrEmpty(newObj.Sabun)){
                            var orgObj2 = GetPersonBySabun(newObj.Sabun, true);
                            var newObj2 = orgObj2;
                            newObj2.CarRegCnt = orgObj2.CarRegCnt + 1;
                            if (PersonData != null && orgObj2 != null){
                                PersonData.Update(newObj2);
                                PersonData.Save();
                            }
                        }
                    }

                    // 1.1 출입증 진행상태 처리사유 와 함께 입력
                    // 처리사유 는 CarCardIssueApplyHistory 만 에 입력 
                    CarCardIssueApplyHistory carCardIssueApplyHistory = new()
                    {

                        CarCardIssueApplyID = orgObj.CarCardIssueApplyID, //출입증신청ID
                        Location = orgObj.Location, // 사업장구분
                        CompanyID = orgObj.CompanyID, // 업체ID
                        CompanyName = orgObj.CompanyName, // 회사명
                        PersonTypeID = orgObj.PersonTypeID, // 회원구분

                        Sabun = orgObj.Sabun, // 신청자 
                        Name = orgObj.Name, 
                        OrgID = orgObj.OrgID,
                        OrgNameKor = orgObj.OrgNameKor,
                        OrgNameEng = orgObj.OrgNameEng,
                        GradeID = orgObj.GradeID, // 직급ID
                        GradeName = orgObj.GradeName, // 직급명
                        Mobile = orgObj.Mobile, // 휴대전화
                        Tel = orgObj.Tel, // 자택전화

                        TermsPrivarcyFileData = orgObj.TermsPrivarcyFileData, // 개인정보활용동의서(첨부파일)
                        TermsPrivarcyFileDataHash = orgObj.TermsPrivarcyFileDataHash, // 개인정보활용동의서(첨부파일Hash)

                        ContactSabun = orgObj.ContactSabun, // 담당자
                        ContactName = orgObj.ContactName,
                        ContactOrgID = orgObj.ContactOrgID,
                        ContactOrgNameKor = orgObj.ContactOrgNameKor,
                        ContactOrgNameEng = orgObj.ContactOrgNameEng,   

                        CardIssueType = orgObj.CardIssueType, // 출입증발급구분: 신규(0)/재발급(1)
                        ApplyReason = orgObj.ApplyReason, // 출입증신청사유
                        
                        CarNo = orgObj.CarNo, // 차량번호
                        CarTypeCodeID = orgObj.CarTypeCodeID, // 차량구분코드ID
                        CarModel = orgObj.CarModel, // 차량모델
                        // CarLIcenseFileData = orgObj.CarLIcenseFileData, // 차량등록증(첨부파일)
                        // CarLIcenseFileDataHash = orgObj.CarLIcenseFileDataHash, // 차량등록증(첨부파일Hash)

                        // CardID = orgObj.CardID, // 출입증ID- S1ACCESS 체크 
                        // CardNo = orgObj.CardNo, // 출입증번호- S1ACCESS 체크 
                        // CardTypeID = orgObj.CardTypeID, // 출입증구분ID - S1ACCESS 체크 : 0-일반(사원증, 상주사원증, 비상주사원증), 1- 방문증(일반방문증, 공사출입증) / to-do: 차량 출입증 확인 필요 
                        CardApplyStatus = CardApplyStatus, // 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                        CardIssueStatus = orgObj.CardIssueStatus, // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정   
                        // CardValidDate = orgObj.CardValidDate, //출입증유효기간                     
                        // ReissueNo = orgObj.ReissueNo, // 재발급차수- S1ACCESS 체크 

                        Memo = Memo, // 처리사유 

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    CarCardIssueApplyHistoryData.Insert(carCardIssueApplyHistory);
                    CarCardIssueApplyHistoryData.Save();
                }
            }else if (mode.Equals("E")){ // 출입증신청 정보 수정 
                if(carCardIssueApplyID > 0)
                {
                    // 1. 출입증신청 출입증신청사유 수정 
                    var orgObj = GetCarCardIssueApply(carCardIssueApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.ApplyReason = ApplyReason; // 출입증신청사유
                    newObj.UpdateDate = DateTime.Now;
                    CarCardIssueApplyData.Update(newObj);
                    CarCardIssueApplyData.Save();

                    // 1.1 출입증신청히스토리 입력: 출입증신청사유만 수정시에는 히스토리 입력 안함. 

                } 
            }
            return RedirectToAction("CarApplyDetail", new { carCardIssueApplyID, culture=GetLang()});
        }
        

        [HttpPost]
        public FileResult? Download(string CarCardIssueApplyID, string FileIdx){
            if (IsAccess() == false) {
                return null;
            }
            WriteLog("start download> CarCardIssueApplyID:"+CarCardIssueApplyID+" , FileIdx:" + FileIdx);
            if (!string.IsNullOrEmpty(CarCardIssueApplyID) && !string.IsNullOrEmpty(FileIdx) && !CarCardIssueApplyID.Equals("0")) 
            {
                int id = int.Parse(CarCardIssueApplyID);
                int fileIdx = int.Parse(FileIdx);
                CarCardIssueApply? carCardIssueApply = CarCardIssueApplyData?.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (carCardIssueApply!= null)
                { 
                    WriteLog("carCardIssueApply:"+Dump(carCardIssueApply));
                    if (fileIdx == 0) { // 자동차 등록증
                        if (carCardIssueApply.CarLIcenseFileDataHash != null && carCardIssueApply.CarLIcenseFileData != null) {
                            fileData = carCardIssueApply.CarLIcenseFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(carCardIssueApply.CarLIcenseFileData);
                        } else {
                            return null;
                        }
                    } else if (fileIdx == 1) { // 개인정보 활용 동의서
                        if (carCardIssueApply.TermsPrivarcyFileDataHash != null && carCardIssueApply.TermsPrivarcyFileData != null) {
                            fileData = carCardIssueApply.TermsPrivarcyFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(carCardIssueApply.TermsPrivarcyFileData);
                        } else {
                            return null;
                        }
                    } 
                    WriteLog("fileIdx:" + fileIdx + ", meta:"+Dump(meta));
                    if (fileData != null && meta != null && !String.IsNullOrEmpty(meta.ContentType)) {
                        WriteLog("meta.ContentType:" + meta.ContentType);
                        // 이미지의 경우 meta.FileName 을 주지 않으면 부라우저에서 열림
                        return File(fileData, meta.ContentType, meta.FileName);
                    }
                }
            }
            return null;
        }
        // ##### 공통 methods #####
        /// <summary>
        /// PASSPORT.VIEW_CARD_PERSON 에서 PID로 ViewCardPerson 가져오기
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        private ViewCardPerson GetViewCardPersonByPID(int PID){
            ViewCardPerson vcp = new();
            if (DbPASSPORT != null)
            {
                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.VIEW_CARD_PERSON ");
                sb.Append(" WHERE ");
                sb.Append(" PID = '");
                sb.Append(PID);
                sb.Append("' ");
                var fs1 = FormattableStringFactory.Create(sb.ToString());
                var list = DbPASSPORT.ViewCardPeople
                    .FromSql(fs1).ToList();
                if (list.Count > 0){
                    vcp = list[0];
                }
            }
            return vcp;
        }

        /// <summary>
        /// PASSPORT.VIEW_CARD_PERSON 에서 검색 조건에 맞게 리스트 가져오기
        /// </summary>
        /// <param name="filterhValue"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        private dynamic GetViewCardPersonList(CardGridData filterhValue, bool isAll = false){
            List<ViewCardPerson> list = new();
            var cnt = 0;
            var my = GetMe();
            if (DbPASSPORT != null){
                List<string> strWhere = new();
                // if (filterhValue.SearchLocation > -1) {
                //     sbWhere.Append(" ( )"); // VIEW_CARD_PERSON 에서 Location ID를 줄 경우 실행
                // }
                if (IsMaster() == false)
                {
                    WriteLog("LevelCodeID:" + my.LevelCodeID);
                    if (IsGeneralManager() || IsSecurity() || IsDietitian() || IsHR() || IsESH())
                    {
                        strWhere.Add("CampusCode = '"+my.Location+"'");
                    } else if (IsPartnerNonresidentManager()) {
                        strWhere.Add("CompanyID = '" + my.CompanyID + "'"); // todo: VIEW UPDATE 이후 처리 가능.
                    }else if (IsEmployee() && !string.IsNullOrEmpty(my.Sabun)) {
                        strWhere.Add("Sabun = '" + my.Sabun + "'");
                    }
                }
                
                if(filterhValue.SearchLocation > -1){
                    strWhere.Add(" CampusCode = '"+filterhValue.SearchLocation+"'");
                }
                if(filterhValue.SearchCardTypeID > -1){
                    strWhere.Add(" CardType = '"+filterhValue.SearchCardTypeID+"'");
                }
                if(filterhValue.SearchCardIssueStatus > -1){
                    strWhere.Add(" CardStatusId = '"+filterhValue.SearchCardIssueStatus+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchCardNo)){
                    strWhere.Add(" CardNo = '"+filterhValue.SearchCardNo+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchName)){
                    strWhere.Add(" Name = '"+filterhValue.SearchName+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchSabun)){
                    strWhere.Add(" Sabun = '"+filterhValue.SearchSabun+"'");
                }
                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.VIEW_CARD_PERSON ");
                if(strWhere.Count > 0){
                    sb.Append(" WHERE ");
                    bool isFist = true;
                    foreach(var m in strWhere){
                        if(!isFist) {
                            sb.Append(" AND ");
                        } 
                        sb.Append(" ( ");
                        sb.Append(m);
                        sb.Append(" ) ");
                        isFist = false;
                    }
                }
                var fs2 = FormattableStringFactory.Create(sb.ToString());
                sb.Append(" ORDER BY PID ");

                if (isAll == false) {
                    sb.Append(" OFFSET ");
                    var offset = (filterhValue.PageNumber -1) * filterhValue.PageSize;
                    sb.Append(offset);
                    sb.Append(" ROWS FETCH NEXT ");
                    sb.Append(filterhValue.PageSize);
                    sb.Append(" ROWS ONLY");
                }
                WriteLog("query: " + sb.ToString());

                var fs1 = FormattableStringFactory.Create(sb.ToString());
                list = DbPASSPORT.ViewCardPeople
                    .FromSql(fs1)
                    .ToList();
                WriteLog("list: "+Dump(list));
                if(isAll == false){
                    cnt = DbPASSPORT.ViewCardPeople
                        .FromSql(fs2)
                        .Count();
                }
                        // .FromSql($"SELECT * FROM dbo.VIEW_CARD_PERSON")
                WriteLog("cnt: "+cnt);
            }
            return new { a = list, b = cnt };
        }
        private IEnumerable<TempCardIssueApply> GetTempCardIssueApplyList(TempCardIssueApplyGridData filterhValue){
            if (TempCardIssueApplyData != null){
                var options = new QueryOptions<TempCardIssueApply>
                {
                    PageNumber = filterhValue.PageNumber,
                    PageSize = filterhValue.PageSize,
                    OrderByDirection = filterhValue.SortDirection,
                    OrderBy = a => a.TempCardIssueApplyID,
                };
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                // {searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}/{searchname?}/{searchsabun?}")
                if (!string.IsNullOrEmpty(filterhValue.SearchCardNo))
                {
                    options.MultipleWhere.Add(a => a.CardNo != null && EF.Functions.Like(a.CardNo, "%" + filterhValue.SearchCardNo + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchName))
                {
                    options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%" + filterhValue.SearchName + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchSabun))
                {
                    options.MultipleWhere.Add(a => a.Sabun != null && EF.Functions.Like(a.Sabun, "%" + filterhValue.SearchSabun + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchStartInsertDate))
                {
                    string d = filterhValue.SearchStartInsertDate + " 00:00:01";
                    options.MultipleWhere.Add(a => a.InsertDate != null && (DateTime)(object)a.InsertDate >= Convert.ToDateTime(d));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndInsertDate))
                {
                    string d = filterhValue.SearchEndInsertDate + " 23:59:59";
                    options.MultipleWhere.Add(a => a.InsertDate != null && (DateTime)(object)a.InsertDate <= Convert.ToDateTime(d));
                }
                WriteLog("options:" + Dump(options));
                return TempCardIssueApplyData.List(options);
            }else{
                return default;
            }
        }

        //임직원 차량관리 리스트 가져오기 
        private IEnumerable<CarCardIssueApply> GetCarList(CarCardGridData filterhValue){
            if (CarCardIssueApplyData != null){
                var options = new QueryOptions<CarCardIssueApply>
                {
                    PageNumber = filterhValue.PageNumber,
                    PageSize = filterhValue.PageSize,
                    OrderByDirection = filterhValue.SortDirection,
                    OrderBy = a => a.CarCardIssueApplyID,
                };
                options.MultipleWhere.Add(a => a.CardApplyStatus == 1); // 승인인 차량리스트만 가져오기 
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                // {searchlocation?}/{searchcartypeid?}/{searchcardissuestatus?}/{searchcarno?}/{searchname?}/{searchsabun?}")]
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsSecurity() || IsDietitian() || IsHR() || IsESH())
                    {
                        options.MultipleWhere.Add(a => a.Location == my.Location);
                    } else if (IsPartnerNonresidentManager()) {
                        options.MultipleWhere.Add(a => a.CompanyID == my.CompanyID);
                    }else if (IsEmployee() && !string.IsNullOrEmpty(my.Sabun)) {
                        options.MultipleWhere.Add(a => a.Sabun == my.Sabun);
                    }
                }

                if (filterhValue.SearchLocation > 0) {
                    options.MultipleWhere.Add(a => a.Location == filterhValue.SearchLocation.ToString());
                }
                if (filterhValue.SearchCarTypeID != -1)
                {
                    options.MultipleWhere.Add(a => a.CarTypeCodeID == filterhValue.SearchCarTypeID);
                }
                //2023.09.05 차량관리 발급상태 숨김처리
                // if (filterhValue.SearchCardIssueStatus != -1)
                // {
                //     options.MultipleWhere.Add(a => a.CardIssueStatus == filterhValue.SearchCardIssueStatus);
                // }
                if (!string.IsNullOrEmpty(filterhValue.SearchCarNo))
                {
                    options.MultipleWhere.Add(a => a.CarNo != null && EF.Functions.Like(a.CarNo, "%" + filterhValue.SearchCarNo + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchName))
                {
                    options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%" + filterhValue.SearchName + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchSabun))
                {
                    options.MultipleWhere.Add(a => a.Sabun != null && EF.Functions.Like(a.Sabun, "%" + filterhValue.SearchSabun + "%"));
                }
                WriteLog("options:" + Dump(options));
                return CarCardIssueApplyData.List(options);
            }else{
                return new List<CarCardIssueApply>();
            }
        }

        //인원출입증현황 리스트 가져오기 
        private IEnumerable<CardIssueApply> GetApplyList(CardIssueApplyGridData filterhValue){
            if (CardIssueApplyData != null){
                var options = new QueryOptions<CardIssueApply>
                {
                    PageNumber = filterhValue.PageNumber,
                    PageSize = filterhValue.PageSize,
                    OrderByDirection = filterhValue.SortDirection,
                    OrderBy = a => a.CardIssueApplyID,
                };
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager())
                    {
                        options.MultipleWhere.Add(x => x.Location == my.Location);
                    } else if(IsPartnerNonresidentManager()){
                        options.MultipleWhere.Add(x => x.CompanyID == my.CompanyID);
                    } else {
                        options.MultipleWhere.Add(x => x.Sabun == my.Sabun);
                    }
                }
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                // {searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchpersontypeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}")]
                if (filterhValue.SearchLocation > 0) {
                    options.MultipleWhere.Add(a => a.Location == filterhValue.SearchLocation.ToString());
                }
                if (filterhValue.SearchCompanyID != -1){ // 회사
                    options.MultipleWhere.Add(a => a.CompanyID == filterhValue.SearchCompanyID);
                }
                // if (!string.IsNullOrEmpty(filterhValue.SearchCompanyName))
                // {
                //     options.MultipleWhere.Add(a => a.CompanyName != null && EF.Functions.Like(a.CompanyName, "%" + filterhValue.SearchCompanyName + "%"));
                // }
                if (filterhValue.SearchCardIssueType != -1)
                {
                    options.MultipleWhere.Add(a => a.CardIssueType == filterhValue.SearchCardIssueType);
                }
                if (filterhValue.SearchCardApplyStatus != -1)
                {
                    options.MultipleWhere.Add(a => a.CardApplyStatus == filterhValue.SearchCardApplyStatus);
                }
                if (filterhValue.SearchPersonTypeID != -1)
                {
                    options.MultipleWhere.Add(a => a.PersonTypeID == filterhValue.SearchPersonTypeID);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchStartInsertDate))
                {
                    string d = filterhValue.SearchStartInsertDate + " 00:00:01";
                    options.MultipleWhere.Add(a => a.InsertDate != null && (DateTime)(object)a.InsertDate >= Convert.ToDateTime(d));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndInsertDate))
                {
                    string d = filterhValue.SearchEndInsertDate + " 23:59:59";
                    options.MultipleWhere.Add(a => a.InsertDate != null && (DateTime)(object)a.InsertDate <= Convert.ToDateTime(d));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchName))
                {
                    options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%" + filterhValue.SearchName + "%"));
                }
                WriteLog("options:" + Dump(options));
                return CardIssueApplyData.List(options);
            }else{
                return default;
            }
        }

        //차량출입증현황 리스트 가져오기 
        private IEnumerable<CarCardIssueApply> GetCarApplyList(CarCardIssueApplyGridData filterhValue){
            if (CarCardIssueApplyData != null){
                var options = new QueryOptions<CarCardIssueApply>
                {
                    PageNumber = filterhValue.PageNumber,
                    PageSize = filterhValue.PageSize,
                    OrderByDirection = filterhValue.SortDirection,
                    OrderBy = a => a.CarCardIssueApplyID,
                };
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager())
                    {
                        options.MultipleWhere.Add(x => x.Location == my.Location);
                    } else if(IsPartnerNonresidentManager()){
                        options.MultipleWhere.Add(x => x.CompanyID == my.CompanyID);
                    } else {
                        options.MultipleWhere.Add(x => x.Sabun == my.Sabun);
                    }
                }
                // {searchlocation?}/{searchcompanyid?}/{searchcardissuetype?}/{searchcardapplystatus?}/{searchcartypecodeid?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchname?}/{searchcarno?}")]
                if (filterhValue.SearchLocation > 0) {
                    options.MultipleWhere.Add(a => a.Location == filterhValue.SearchLocation.ToString());
                }
                if (filterhValue.SearchCompanyID != -1){ // 회사
                    options.MultipleWhere.Add(a => a.CompanyID == filterhValue.SearchCompanyID);
                }
                // if (!string.IsNullOrEmpty(filterhValue.SearchCompanyName))
                // {
                //     options.MultipleWhere.Add(a => a.CompanyName != null && EF.Functions.Like(a.CompanyName, "%" + filterhValue.SearchCompanyName + "%"));
                // }
                if (filterhValue.SearchCardIssueType != -1)
                {
                    options.MultipleWhere.Add(a => a.CardIssueType == filterhValue.SearchCardIssueType);
                }
                if (filterhValue.SearchCardApplyStatus != -1)
                {
                    options.MultipleWhere.Add(a => a.CardApplyStatus == filterhValue.SearchCardApplyStatus);
                }
                if (filterhValue.SearchCarTypeCodeID != -1)
                {
                    options.MultipleWhere.Add(a => a.CarTypeCodeID == filterhValue.SearchCarTypeCodeID);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchStartInsertDate))
                {
                    string d = filterhValue.SearchStartInsertDate + " 00:00:01";
                    options.MultipleWhere.Add(a => a.InsertDate != null && (DateTime)(object)a.InsertDate >= Convert.ToDateTime(d));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndInsertDate))
                {
                    string d = filterhValue.SearchEndInsertDate + " 23:59:59";
                    options.MultipleWhere.Add(a => a.InsertDate != null && (DateTime)(object)a.InsertDate <= Convert.ToDateTime(d));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchName))
                {
                    options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%" + filterhValue.SearchName + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchCarNo))
                {
                    options.MultipleWhere.Add(a => a.CarNo != null && EF.Functions.Like(a.CarNo, "%" + filterhValue.SearchCarNo + "%"));
                }
                WriteLog("options:" + Dump(options));
                return CarCardIssueApplyData.List(options);
            }else{
                return default;
            }
        }

        
        
        //인원출입증신청 상세정보 가져오기 
        private CardIssueApply GetCardIssueApply(int cardIssueApplyID=-1, bool isNoTracking = false)
        {
            CardIssueApply cardIssueApply = null;
            if(cardIssueApplyID > 0) {
                var options = new QueryOptions<CardIssueApply>
                {
                    Where = a => a.CardIssueApplyID == cardIssueApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    cardIssueApply = CardIssueApplyData.GetNT(cardIssueApplyID);
                } else {
                    cardIssueApply = CardIssueApplyData.Get(cardIssueApplyID);
                }
            }
            return cardIssueApply;
        }
        
        //차량출입증신청 상세정보 가져오기 
        private CarCardIssueApply GetCarCardIssueApply(int carCardIssueApplyID=-1, bool isNoTracking = false)
        {
            CarCardIssueApply carCardIssueApply = null;
            if(carCardIssueApplyID > 0) {
                var options = new QueryOptions<CarCardIssueApply>
                {
                    Where = a => a.CarCardIssueApplyID == carCardIssueApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    carCardIssueApply = CarCardIssueApplyData.GetNT(carCardIssueApplyID);
                } else {
                    carCardIssueApply = CarCardIssueApplyData.Get(carCardIssueApplyID);
                }
            }
            return carCardIssueApply;
        }
        /// <summary>
        /// 보유차량리스트 가져오기
        /// </summary>
        /// <param name="sabun"></param>
        /// <returns></returns>
        public JsonResult? GetMyCarList(string sabun) {
            // 등록일 차량번호 신청사유
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("name:" + sabun);
            var options = new QueryOptions<CarCardIssueApply>
            {
                //a.CardIssueStatus == 0 // 출입증발급상태: 발급(0)/미발급(1)/분실(2)  <= 2023.08.11 수정
                //a.CardApplyStatus == 1 // 출입증신청상태: 승인대기(0)/승인완료(1)/반려(2)
                Where = a => a.CardApplyStatus == 1 && a.Sabun == sabun && a.IsDelete == "N",
            };

            IEnumerable<CarCardIssueApply> carList = CarCardIssueApplyData.List(options);
            return Json(carList);
        }
        private Person? GetPerson(string sabun)
        {
            WriteLog("sabun: " + sabun);
            var options = new QueryOptions<Person>
            {
                Where = a => a.Sabun == sabun && a.IsDelete == "N",
            };
            if (PersonData != null){
                var person = PersonData.GetNT(options);
                return person;
            }
            return null;
        }
        private TempCardIssueApply GetTempCardIssueApply(int tempCardIssueApplyID=-1, bool isNoTracking = false)
        {
            TempCardIssueApply tempCardIssueApply = null;
            if(tempCardIssueApplyID > 0) {
                var options = new QueryOptions<TempCardIssueApply>
                {
                    Where = a => a.TempCardIssueApplyID == tempCardIssueApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    tempCardIssueApply = TempCardIssueApplyData.GetNT(tempCardIssueApplyID);
                } else {
                    tempCardIssueApply = TempCardIssueApplyData.Get(tempCardIssueApplyID);
                }
            }
            return tempCardIssueApply;
        }
     }    
}
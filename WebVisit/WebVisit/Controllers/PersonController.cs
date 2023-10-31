using System.Data;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using System.Globalization;
using ClosedXML.Excel;
using WebVisit.Models;
using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class PersonController : BaseController
    {
        // private Repository<Company>? CompanyData { get; set; }
        private Repository<BlackList>? BlackListData { get; set; }
        private Repository<BlackListHistory>? BlackListHistoryData { get; set; }
        private Repository<MealAccess>? MealAccessData { get; set; }
        

        public PersonController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer)
        {
            // personData = new Repository<Person>(db);
            // levelData = new Repository<Level>(db);
            // companyData = new Repository<Company>(db);
        }

        protected override void Init(){
            if (DbSVISIT != null) {
                PersonData ??= new Repository<Person>(DbSVISIT);
                LevelData ??= new Repository<Level>(DbSVISIT);
                CompanyData ??= new Repository<Company>(DbSVISIT);
                CommonCodeData ??= new Repository<CommonCode>(DbSVISIT);
                BlackListData ??= new Repository<BlackList>(DbSVISIT);
                BlackListHistoryData ??= new Repository<BlackListHistory>(DbSVISIT);
                MealAccessData ??= new Repository<MealAccess>(DbSVISIT);
            }
        }
        
        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("List");
        }
        
        /// <summary>
        /// 마스터관리자 : 사원정보 조회 / 엑셀다운로드
        /// 일반관리자 : 해당 사업장의 사원정보조회 / 엑셀다운로드 Location
        /// 보안실 : 해당 사업장의 사원정보 조회 / 엑셀다운로드
        /// 비상주업체 : 자사 임직원 사원정보 조회 / 엑셀다운로드
        /// 임직원 : 자신의 사원정보 조회
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // /person/list/1/10/name/desc/2/1/1/a/b/?culture=ko
        // PageNumber=1&PageSize=10&SortField=name&SortDirection=desc&PersonTypeID=-1&CompanyID=1&PersonStatusID=-1&Name=조력자2
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}/{searchcompanyname?}")]
        public IActionResult List (PersonGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO? personDTO = GetMe();
            if (personDTO != null){
                WriteLog("personDTO:" + Dump(personDTO));
            }

            PersonDTO my = GetMe();
            ViewBag.IsMaster = IsMaster();
            // ViewBag.ExcelDownloadable = my.LevelCodeID == (int)VisitEnum.LevelType.Master || my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager || my.LevelCodeID == (int)VisitEnum.LevelType.Security || my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager;
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsHR() || IsESH() || IsDietitian() || IsPartnerNonresidentManager();
            ViewBag.Registable = IsMaster() || IsPartnerNonresidentManager();
            ViewBag.ErrorMsg = GetErrorMessage();

            PersonGridData filterValue = (PersonGridData) FilterGridData(values);
            var m = GetUnionPerson(values:filterValue);
            // WriteLog("GetUnionPerson:"+Dump(m));
            var options = new QueryOptions<Person>
            {
                PageNumber = filterValue.PageNumber,
                PageSize = filterValue.PageSize,
                OrderByDirection = filterValue.SortDirection,
            };
            if (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager) { //일반관리자 : 해당 사업장의 사원정보조회 / 엑셀다운로드 Location
                options.MultipleWhere.Add(a => a.Location == my.Location);
            } else if (my.LevelCodeID == (int)VisitEnum.LevelType.Security){ // 보안실 : 해당 사업장의 사원정보 조회 / 엑셀다운로드
                options.MultipleWhere.Add(a => a.Location == my.Location);
            } else if (my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager){ // 비상주업체 : 자사 임직원 사원정보 조회 / 엑셀다운로드
                options.MultipleWhere.Add(a => a.CompanyID == my.CompanyID);
            } else if (IsEmployee(my.LevelCodeID)){ // 임직원 : 자신의 사원정보 조회
                options.MultipleWhere.Add(a => a.PersonID == my.PersonID);
            }
            if (int.Parse(filterValue.SearchPersonTypeID) >-1) { // 회원 구분
                options.MultipleWhere.Add(a => a.PersonTypeID == int.Parse(filterValue.SearchPersonTypeID));
            }
            // WriteLog("filterValue:" + Dump(filterValue));
            if(!string.IsNullOrEmpty(filterValue.SearchCompanyName)){
                int id = GetCompanyID(filterValue.SearchCompanyName);
                WriteLog("SearchCompanyName:" + filterValue.SearchCompanyName + ", id:" + id);
                // if(id > 0) {
                    options.MultipleWhere.Add(a => a.CompanyID == id);
                // }
            }
            if (int.Parse(filterValue.SearchPersonStatusID) >-1) { // 재직 상태
                WriteLog("[Search] PersonStatusID:" + filterValue.SearchPersonStatusID);
                options.MultipleWhere.Add(a => a.PersonStatusID == int.Parse(filterValue.SearchPersonStatusID));
            }
            if (!string.IsNullOrEmpty(filterValue.SearchOrgName) && !filterValue.SearchOrgName.Equals("+") && !filterValue.SearchOrgName.Equals("%20")) { // 부서명
                WriteLog("[Search] OrgName:" + filterValue.SearchOrgName);
                options.MultipleWhere.Add(a => EF.Functions.Like(a.OrgNameEng, "%"+filterValue.SearchOrgName+"%") || EF.Functions.Like(a.OrgNameKor, "%"+filterValue.SearchOrgName+"%"));
            }
            if (!string.IsNullOrEmpty(filterValue.SearchName) && !filterValue.SearchName.Equals("+") && !filterValue.SearchName.Equals("%20")) { // 이름
                WriteLog("[Search] Name:" + filterValue.SearchName);
                options.MultipleWhere.Add(a => EF.Functions.Like(a.Name, "%"+filterValue.SearchName+"%"));
            }

            // CompanyStatus: 승인대기(0)/승인(1)/반려(2)
            var options2 = new QueryOptions<Company>
            {
                Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
            };
            // ViewBag.SearchOrgName = filterValue.SearchOrgName;
            // ViewBag.SearchName = filterValue.SearchName;

            PersonListViewModel vm = new();
            try{
                // Persons = PersonData.List(options),
                if (PersonData != null && CompanyData != null) {
                    if (m == null || m.c == false) {
                        vm =new(){
                            Persons = PersonData.List(options),
                            Companies = CompanyData.List(options2),
                            CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus),
                            CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                            CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                            CurrentRoute = values,
                            SearchRoute = filterValue,
                            TotalPages = values.GetTotalPages(PersonData.Count),
                            TotalCnt = PersonData.Count,
                        };
                    } else {
                        vm =new(){
                            Persons = m.a,
                            Companies = CompanyData.List(options2),
                            CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus),
                            CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                            CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                            CurrentRoute = values,
                            SearchRoute = filterValue,
                            TotalPages = values.GetTotalPages(m.b),
                            TotalCnt = m.b,
                        };                        
                    }

                }
            }catch(Exception e){
                WriteError(e.ToString());
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}/{searchcompanyname?}")]
        public IActionResult? ExcelDownload(PersonGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return new EmptyResult();
            }
            bool isDownloadable = IsMaster() || IsGeneralManager() || IsHR() || IsESH() || IsDietitian() || IsPartnerNonresidentManager();
            if(isDownloadable == false || Localizer == null) {
                return new EmptyResult();
            }
            DataTable dt = new(ExTitle);
            // 사업장	회원구분	사번	성명	직급	휴대전화	출입증	개인차량	주소
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Person Classify"]), new DataColumn(@Localizer["Sabun"]),
                                        new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Position"]), new DataColumn(@Localizer["CellPhone"]),
                                        new DataColumn(@Localizer["Access Card"]),new DataColumn(@Localizer["Personal Vehicle"]),new DataColumn(@Localizer["Address"]), });
            WriteLog(Dump(values));
            PersonGridData filterValue = (PersonGridData) FilterGridData(values);
            filterValue.PageSize = 0;
            var l = GetUnionPerson(values:filterValue);

            PersonDTO my = GetMe();
            List< CommonCode > CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            // List<CommonCode> CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus);

            if (PersonData != null){
                List<Person> list = new();
                if (l == null || l.c == false)
                {
                    var options = new QueryOptions<Person>
                    {
                        PageNumber = values.PageNumber,
                        PageSize = values.PageSize,
                        OrderByDirection = values.SortDirection,
                    };
                    if (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager) { //일반관리자 : 해당 사업장의 사원정보조회 / 엑셀다운로드 Location
                        options.MultipleWhere.Add(a => a.Location == my.Location);
                    } else if (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager){ // 보안실 : 해당 사업장의 사원정보 조회 / 엑셀다운로드
                        options.MultipleWhere.Add(a => a.Location == my.Location);
                    } else if (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager){ // 비상주업체 : 자사 임직원 사원정보 조회 / 엑셀다운로드
                        options.MultipleWhere.Add(a => a.CompanyID == my.CompanyID);
                    } else if (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager){ // 임직원 : 자신의 사원정보 조회
                        options.MultipleWhere.Add(a => a.PersonID == my.PersonID);
                    }
                    // /{persontypeid?}/{companyid?}/{personstatusid?}/{orgname?}/{name?}
                    if (int.Parse(filterValue.SearchPersonTypeID) >-1) { // 회원 구분
                        options.MultipleWhere.Add(a => a.PersonTypeID == int.Parse(filterValue.SearchPersonTypeID));
                    }
                    // if (int.Parse(filterValue.SearchCompanyID) >-1) { // 회사
                    //     options.MultipleWhere.Add(a => a.CompanyID == int.Parse(filterValue.SearchCompanyID));
                    // }
                    //todo: 회사명 검색 확인 
                    // if (!string.IsNullOrEmpty(filterValue.SearchCompanyName) && !filterValue.SearchCompanyName.Equals("+") && !filterValue.SearchCompanyName.Equals("%20")) { // 회사명
                    //     WriteLog("[Search] CompanyName:" + filterValue.SearchCompanyName);
                    //     options.MultipleWhere.Add(a => EF.Functions.Like(a.CompanyName, "%"+filterValue.SearchCompanyName+"%"));
                    // }
                    if (int.Parse(filterValue.SearchPersonStatusID) >-1) { // 재직 상태
                        WriteLog("[Search] PersonStatusID:" + filterValue.SearchPersonStatusID);
                        options.MultipleWhere.Add(a => a.PersonStatusID == int.Parse(filterValue.SearchPersonStatusID));
                    }
                    if (!string.IsNullOrEmpty(filterValue.SearchOrgName) && !filterValue.SearchOrgName.Equals("+") && !filterValue.SearchOrgName.Equals("%20")) { // 부서명
                        WriteLog("[Search] OrgName:" + filterValue.SearchOrgName);
                        options.MultipleWhere.Add(a => EF.Functions.Like(a.OrgNameEng, "%"+filterValue.SearchOrgName+"%") || EF.Functions.Like(a.OrgNameKor, "%"+filterValue.SearchOrgName+"%"));
                    }
                    if (!string.IsNullOrEmpty(filterValue.SearchName) && !filterValue.SearchName.Equals("+") && !values.SearchName.Equals("%20")) { // 이름
                        WriteLog("[Search] Name:" + filterValue.SearchName);
                        options.MultipleWhere.Add(a => EF.Functions.Like(a.Name, "%"+filterValue.SearchName+"%"));
                    }                    list = (List<Person>) PersonData.List(options);
                } else {
                    list = l.a;
                }
                // 사업장	회원구분	사번	성명	직급	휴대전화	출입증	개인차량	주소
                string strPersonType = string.Empty;
                string strPersonStatus = string.Empty;
                string strLocation = string.Empty;
                string strCardYN = string.Empty;
                string strCarYN = string.Empty;
                foreach(var m in list){
                    strPersonType = "";
                    strPersonStatus = "";
                    strLocation = "";
                    strCardYN = "N";
                    strCarYN = "N";
                    // WriteLog("CodePersonType: "+Dump(CodePersonType));
                    foreach(var m2 in CodePersonType) {
                        if (m2.SubCodeID == m.PersonTypeID){
                            strPersonType = m2.CodeNameKor;
                            break;
                        }
                        WriteLog(m2.SubCodeID+"==:" + m.PersonTypeID);
                    }
                    strLocation = "";
                    foreach(var m2 in CodeLocation) {
                        if (m2.SubCodeDesc != null && m2.SubCodeDesc.Equals(m.Location)){
                            strLocation = m2.CodeNameKor;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(m.CardNo)) {
                        strCardYN = "Y";
                    }
                    if(m.CarRegCnt > 0){
                        strCarYN = "Y";
                    }
                    // strPersonStatus = "";
                    // foreach(var m2 in CodePersonType) {
                    //     if (m2.SubCodeID == m.PersonStatusID){
                    //         strPersonType = m2.CodeNameKor;
                    //         break;
                    //     }
                    // }
                    dt.Rows.Add(strLocation, strPersonType, m.Sabun, m.Name, m.GradeName, m.Mobile, strCardYN, strCarYN, m.HomeAddr);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            } else {
                WriteLog("PersonData is null");
                return null;
            }
        }

        /// <summary>
        /// 마스터 : 사원정보 변경 가능
        /// 임직원 : 본인정보변경가능(차량허용대수 제외)
        /// 보안실: 조회
        /// 비상주업체 : 자사 소속 비상주직원의 정보만 조회
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}/{searchcompanyname?}")]
        public IActionResult Detail(PersonGridData values, string sabun) // int personID,
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            
            // WriteLog("sabun:" + sabun + ", values:" + Dump(values));
            PersonDTO my = GetMe();
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsNotMaster = !ViewBag.IsMaster;
            ViewBag.IsEditable = IsMaster() || IsGeneralManager() || IsHR() || IsPartnerNonresidentManager() || sabun == my.Sabun;

            PersonDetailViewModel vm = new();
            Person person = new();
            if (PersonData != null && CompanyData != null && LevelData != null) {
                var m = GetUnionPerson(values:null, sabun:sabun);
                if(m == null || m.c == false) {
                    person = PersonData.Get(new QueryOptions<Person>{
                        Where = a => a.Sabun == sabun,
                    }) ?? new Person();
                } else {
                    person = m.a[0];
                }
                if(person.BirthDate != null && string.IsNullOrEmpty(person.BirthDate.Trim()) && !string.IsNullOrEmpty(person.Location)){
                    BirthInfo birthInfo = GetBirthdayGender(person.Sabun);
                    WriteLog("get birthday"+Dump(birthInfo));
                    person.BirthDate = birthInfo.Birthday;
                    person.Gender = birthInfo.Gender;
                }
                WriteLog("person.ImageData:" + person.ImageData);
                // WriteLog("person.ImageDataHash:" + person.ImageDataHash);
                if (person.ImageData != null && person.ImageDataHash != null) {
                    var meta = _Dump<ImageMeta>(person.ImageData);
                    // WriteLog("person.ImageData exsits:"+meta?.ContentType); // ContentType ContentDisposition ContentDisposition FileName
                    var base64 = Convert.ToBase64String(person.ImageDataHash);
                    if(meta != null && meta.ContentType != null){
                        ViewBag.img1= String.Format("data:"+meta.ContentType+";base64,{0}", base64);
                    } else {
                        ViewBag.img1= String.Format("data:image/gif;base64,{0}", base64);
                    }
                }
                ViewBag.IsEmployee = IsEmployee(person.LevelCodeID);
                //IsPartnerNonresidentManager
                if(ViewBag.IsEditable == false) {
                    ViewBag.IsEditable = IsPartnerNonresidentManager(person.CompanyID??0);
                }

                if (ViewBag.IsEmployee && sabun != my.Sabun && ViewBag.IsEditable == false&& IsGeneralManager(person.Location)){ 
                    // 일반관리자, 마스터, 사원(본인)
                    return RedirectToAction("List"); // 비상주 업체 관리자는 임직원 정보를 볼 수 없음.
                }
                // 성별, 생일 가져오기

                var options = new QueryOptions<Level>
                {
                    Where = a => a.IsDelete == "N",
                };
                var options2 = new QueryOptions<Company>
                {
                    Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
                };
                ViewPersonCard vp = GetViewPersonCardBySabun(person.Sabun);
                WriteLog("vp:" + Dump(vp));
                person.CardNo = vp.CardNo;
                vm = new(){
                    Levels = LevelData.List(options), 
                    Companies = CompanyData.List(options2),
                    Person = person,
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                    CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus),
                    CodeForeignerType = GetCommonCodes((int)VisitEnum.CommonCode.ForeignerType),
                    CodeWorkTypeCode = GetCommonCodes((int)VisitEnum.CommonCode.WorkTypeCode),
                    CodeImmStatusCode = GetCommonCodes((int)VisitEnum.CommonCode.ImmStatusCode),
                };
            }
            return View(vm);
        }
        private BirthInfo GetBirthdayGender(string sabun){
            var obj = new BirthInfo("", 0);
            if(DbPASSPORT != null){
                IQueryable<VGateBirthday> birthdays = DbPASSPORT.VGateBirthdays.Where(x => x.UserId == sabun);
                var rs = birthdays.ToList();
                if(rs.Count > 0){
                    var b = rs[0].Birthday;
                    b = b.Substring(0, 4) + "-" + b.Substring(4, 2) + "-"+b.Substring(6, 2);
                    var g = int.Parse(rs[0].SexCode) % 2;
                    if(g == 0){ // 여자
                        g = 1;
                    }else if(g==1){ // 남자
                        g = 0;
                    }
                    obj = new BirthInfo(b, g);
                }
            }
            return obj;
        }

        private ViewPersonCard GetViewPersonCardBySabun(string sabun){
            ViewPersonCard vcp = new();
            if (DbPASSPORT != null)
            {
                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.VIEW_PERSON_CARD ");
                sb.Append(" WHERE ");
                sb.Append(" Sabun = '");
                sb.Append(sabun);
                sb.Append("' ");
                var fs1 = FormattableStringFactory.Create(sb.ToString());
                var list = DbPASSPORT.ViewPersonCards
                    .FromSql(fs1).ToList();
                if (list.Count > 0){
                    vcp = list[0];
                }
            }
            return vcp;
        }

        private ViewPerson GetViewPersonBySabun(string sabun){
            ViewPerson vcp = new();
            if (DbPASSPORT != null)
            {
                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.VIEW_PERSON ");
                sb.Append(" WHERE ");
                sb.Append(" Sabun = '");
                sb.Append(sabun);
                sb.Append("' ");
                var fs1 = FormattableStringFactory.Create(sb.ToString());
                var list = DbPASSPORT.ViewPeople
                    .FromSql(fs1).ToList();
                if (list.Count > 0){
                    vcp = list[0];
                }
            }
            return vcp;
        }

        /// <summary>
        /// 마스터 : 사원정보 변경 가능
        /// 임직원 : 본인정보변경가능(차량허용대수 제외)
        /// 보안실: 조회
        /// 비상주업체 : 자사 소속 비상주직원의 정보만 조회
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <param name="person"></param>
        /// <returns></returns>t("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchcompanyname?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}")]
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}/{searchcompanyname?}")]
        public async Task<IActionResult> Detail(PersonGridData values, string mode, Person person)
        {
            // WriteLog("mode: "+mode+", personID: "+personID+", Location: "+Location+", LevelCodeID: "+LevelCodeID+", CompanyID: "+CompanyID+", Name: "+Name+
            // ", BirthDate: "+BirthDate+", Gender: "+Gender+", OrgNameKor: "+OrgNameKor+", GradeName: "+GradeName+", Mobile: "+Mobile+", Tel: "+Tel+", IsForeigner: "+IsForeigner);
            WriteLog("mode: " + mode + ", person:" + Dump(person));
            
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("E")) // 임직원 수정
            {
                PersonDTO my = GetMe();
                // 비상주 인력의 사번은 사업장을 기준으로 생성되며 수정 불가, 그러나 사업장은 수정 가능 
                if(!string.IsNullOrEmpty(person.Sabun))
                {
                    Person? orgObj = null;
                    Person? newObj = null;
                    if (person.PersonID == 0){
                        orgObj = GetPersonBySabun(person.Sabun, true);
                        if(orgObj == null) {
                            return new EmptyResult();
                        }
                        newObj = orgObj;
                    } else {
                        orgObj = GetPerson(person.PersonID, true);
                        if(orgObj == null) {
                            return new EmptyResult();
                        }
                        newObj = orgObj.Clone();
                    }
                    WriteLog("orgObj:" + Dump(orgObj));
                    // var orgObj = GetPerson(person.PersonID, true);
                    if (PersonData != null && orgObj != null){
                        // 수정되는 회원이 비상주 직원이거나 비상주 업체 관리자일 경우에만 기본 정보 수정.
                        if (orgObj.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresident || orgObj.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager){
                            // 사번, 출입증 번호 변경 불가.
                            newObj.Location = person.Location??newObj.Location;
                            newObj.Name = person.Name??newObj.Name;
                            newObj.BirthDate = person.BirthDate??newObj.BirthDate;
                            newObj.Gender = person.Gender??newObj.Gender;
                            newObj.OrgNameKor = person.OrgNameKor??newObj.OrgNameKor;
                            newObj.GradeName = person.GradeName??newObj.GradeName;
                            newObj.Mobile = person.Mobile??newObj.Mobile;
                            newObj.Tel = person.Tel??newObj.Tel;
                            newObj.IsForeigner = person.IsForeigner??newObj.IsForeigner;
                            if(person.IsForeigner == 1){ //외국인 
                                newObj.Nationality = person.Nationality;
                                newObj.ImmStatusCodeID = person.ImmStatusCodeID;
                                newObj.ImmStartDate = person.ImmStartDate;
                                newObj.ImmEndDate = person.ImmEndDate;
                            }else{
                                newObj.Nationality = null;
                                newObj.ImmStatusCodeID = null;
                                newObj.ImmStartDate = null;
                                newObj.ImmEndDate = null;
                            }
                            newObj.WorkTypeCodeID = person.WorkTypeCodeID??newObj.WorkTypeCodeID;
                            newObj.Email = person.Email??newObj.Email;
                            newObj.HomeAddr = person.HomeAddr??newObj.HomeAddr;
                            if(person.FormFile != null && person.FormFile.Count > 0){
                                FileData fileData = await GetFileData(person.FormFile[0]);
                                newObj.ImageDataHash = fileData.Hash;
                                if (!String.IsNullOrEmpty(fileData.Meta)) {
                                    newObj.ImageData = fileData.Meta;
                                }
                            }
                        } else{ // 임직원: 자택 전화와 자택 주소 수정 가능 2023.08.26
                            if(IsMaster() || IsGeneralManager() || IsHR() || IsPartnerNonresidentManager()  || (person.Sabun == my.Sabun)){
                                newObj.HomeAddr = person.HomeAddr??newObj.HomeAddr;
                                newObj.Tel = person.Tel??newObj.Tel;
                            }
                        }
                        if (IsMaster()){
                            // 회사 정보, 차량 허용대수, 레벨은 마스터만 수정할 수 있음.
                            newObj.CarAllowCnt = person.CarAllowCnt??newObj.CarAllowCnt;
                            if (person.LevelCodeID > 0)
                                newObj.LevelCodeID = person.LevelCodeID;
                            if (person.CompanyID != null && orgObj.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresident || orgObj.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager)
                                newObj.CompanyID = person.CompanyID;
                        }
                        newObj.UpdateDate = DateTime.Now;
                        // WriteLog("person id:" + newObj.PersonID);
                        PersonData.Update(newObj);
                        PersonData.Save();
                    }
                }
            }
            // return new EmptyResult();
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                dict.Add("sabun", person.Sabun.ToString());
                return RedirectToAction("Detail", dict);
            }
            return RedirectToAction("Detail", new { person.PersonID, culture = GetLang() });
        }

        /// <summary>
        /// 출입증 회수처리
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ReturnAccessCard(int personID){
            SetDbContext();
            Init();
            if (PersonData != null && IsMaster())
            {
                var person = PersonData.Get(new QueryOptions<Person>
                {
                    Where = a => a.PersonID == personID,
                }) ?? new Person();
                person.CardNo = string.Empty;
                PersonData.Update(person);
                PersonData.Save();
                // todo: S1ACCESS 카드 처리

            }
            return new EmptyResult();
        }

        /// <summary>
        /// 마스터 : 사원정보 등록 가능
        /// 비상주업체 : 자사 사원정보 등록 가능
        /// </summary>
        /// <returns></returns>
        // /person/reg/1/10/name/desc/-1/-1/0/%2B/조/
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}/{searchcompanyname?}")]
        public IActionResult Reg (PersonGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsPartnerNonresidentManager();
            if(accessible == false){
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            ViewBag.IsPartnerNonresidentManager = (my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager);
            ViewBag.IsMaster = IsMaster();
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }

            var options = new QueryOptions<Level>
            {
                Where = a => a.IsDelete == "N",
            };
            var options2 = new QueryOptions<Company>
            {
                Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
            };
            PersonRegViewModel vm = new();
            if (LevelData != null && CompanyData != null){
                vm =new(){
                    Levels = LevelData.List(options), 
                    Companies = CompanyData.List(options2),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                    CodeForeignerType = GetCommonCodes((int)VisitEnum.CommonCode.ForeignerType),
                    CodeWorkTypeCode = GetCommonCodes((int)VisitEnum.CommonCode.WorkTypeCode),
                    CodeImmStatusCode = GetCommonCodes((int)VisitEnum.CommonCode.ImmStatusCode),
                };
            }
            return View(vm);
        }
        
        /// <summary>
        /// 마스터 : 사원정보 등록 가능
        /// 비상주업체 : 자사 사원정보 등록 가능
        /// </summary>
        /// <returns></returns>
        // string mode, String Location, int LevelCodeID, int CompanyID, String Name,
        // String BirthDate, int Gender, String OrgNameKor, String GradeName, String Mobile, String Tel,
        // int IsForeigner, int WorkTypeCodeID, String Email, String HomeAddr, int CarAllowCnt, String Nationality, 
        // int ImmStatusCodeID, String ImmStartDate, String ImmEndDate
        // /person/reg/?PageNumber=1&PageSize=10&SortField=name&SortDirection=desc&SearchPersonTypeID=-1&SearchCompanyID=-1&SearchPersonStatusID=0&SearchName=%EC%A1%B0
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchpersontypeid?}/{searchpersonstatusid?}/{searchorgname?}/{searchname?}/{searchcompanyname?}")]
        public async Task<IActionResult> Reg(PersonGridData values, string mode, Person person)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsPartnerNonresidentManager();
            if(accessible == false){
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            if (mode.Equals("A") && person.Location != null) // 임직원 추가
            {
                // 비상주 인력의 사번은 8자리: 부천 B0000000, 상우 S0000000 , 판교 R0000000 자동생성
                String strSabun = "";
                if(person.Location == "1000"){ // 서울 
                    strSabun="I";
                }else if(person.Location == "2000"){ // 부천
                    strSabun="B";
                }else if(person.Location == "3000"){ // 상우 
                    strSabun="S";
                }else if(person.Location == "6000"){ // 글로벌칩스 
                    strSabun="R";
                }
                //사번 생성 
                int maxPersonCntByLocation = GetNewSabun(person.Location);
                strSabun += Convert.ToString(maxPersonCntByLocation).PadLeft(7, '0');
                if(person.IsForeigner == 0){ //내국인
                    person.Nationality = null;
                    person.ImmStatusCodeID = 0; // int 컬럼을 null 처리 (외국인을 내국인으로 수정 시 값을 비우기)
                    person.ImmStartDate = null;
                    person.ImmEndDate = null;
                }
                person.PersonStatusID = 0;  
                if (PersonData != null) {
                    //PersonStatusID: 재직(0)/휴직(1)/퇴직(2)
                    person.Sabun = strSabun;
                    person.InsertSabun = my.Sabun;
                    person.InsertName = my.Name;
                    person.InsertOrgID = my.OrgID;
                    person.InsertOrgNameKor = my.OrgNameKor;
                    person.InsertOrgNameEng = my.OrgNameEng;
                    person.InsertDate = DateTime.Now;
                    person.IsDelete = "N";
                    if(person.FormFile != null && person.FormFile.Count > 0){
                        FileData fileData = await GetFileData(person.FormFile[0]);
                        person.ImageDataHash = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta)) {
                            person.ImageData = fileData.Meta;
                        }
                    }
                    if(person.FormFile2 != null && person.FormFile2.Count > 0){
                        FileData fileData = await GetFileData(person.FormFile2[0]);
                        person.TermsPrivarcyFileDataHash = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta)) {
                            person.TermsPrivarcyFileData = fileData.Meta;
                        }
                    }
                    person.LevelCodeID = (int)VisitEnum.LevelType.PartnerNonresident; // 비상주 직원만 입력 가능하므로 비상주 고정
                    person.UpdateIP = GetUserIP();
                    PersonData.Insert(person);
                    PersonData.Save();
                    WriteLog("insert person: "+Dump(person));

                    // 1. 식수권한 (MealAccess) 정보 입력 
                    MealAccess mealAccess = new()
                    {
                        Sabun = strSabun, //회원
                        // Name = person.Name, //이름 
                        // OrgID = person.OrgID, //부서ID
                        // OrgNameKor = person.OrgNameKor, //부서명Kor
                        // OrgNameEng = person.OrgNameEng, //부서명Eng
                        MealGB1 = 0, //조식권한: 있음(1), 없음(0)
                        MealGB2 = 0, //중식권한: 있음(1), 없음(0)
                        MealGB3 = 0, //석식권한: 있음(1), 없음(0)
                        MealGB4 = 0, //야식권한: 있음(1), 없음(0)

                        // UpdateIP = GetUserIP(), // 입력한장치의IP
                        // UpdateSabun = my.Sabun,//등록자
                        // InsertName = my.Name,
                        // InsertOrgID = my.OrgID,
                        // InsertOrgNameKor = my.OrgNameKor,
                        // InsertOrgNameEng = my.OrgNameEng,
                        InsertDate = DateTime.Now
                    };
                    MealAccessData.Insert(mealAccess);
                    MealAccessData.Save();

                    // PROCI_PERSON_REG: PersonTypeID 2: 비상주 관리자, 3: 비상주 사원
                    if (DbPASSPORT != null){
                        var companyName = GetCompanyName(person.CompanyID??0);
                        StringBuilder sb = new();
                        sb.Append("PROCI_PERSON_REG @PageType='I', @Sabun='");
                        sb.Append(person.Sabun);
                        sb.Append("' , @Name='");
                        sb.Append(person.Name);
                        sb.Append("', @GradeName='"); 
                        sb.Append(person.GradeName);
                        sb.Append("', @CompanyName='"); 
                        sb.Append(companyName);
                        sb.Append("', @CompanyID='"); 
                        sb.Append(person.CompanyID);
                        sb.Append("', @OrgCode='"); 
                        sb.Append(person.OrgID);
                        sb.Append("', @Email='"); 
                        sb.Append(person.Email);
                        sb.Append("', @Tel='"); 
                        sb.Append(person.Tel);
                        sb.Append("', @Mobile='"); 
                        sb.Append(person.Mobile);
                        sb.Append("', @PersonTypeID='3', @PersonStatusID='0', @PhotFile=NULL, @PersonUser3=NULL, @PersonUser4=NULL, @PersonUser5=NULL, @ValidDate=NULL, @UpdateIP='");
                        sb.Append(person.UpdateIP);
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
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                return RedirectToAction("List", dict);
            }
            return RedirectToAction("List", new { culture = GetLang() });
        }

        [HttpPost]
        public FileResult? Download(string personID, string FileIdx){
            if (IsAccess() == false) {
                return null;
            }
            WriteLog("start download> personID:"+personID+" , FileIdx:" + FileIdx);
            if (!string.IsNullOrEmpty(personID) && !string.IsNullOrEmpty(FileIdx) && !personID.Equals("0")) 
            {
                int id = int.Parse(personID);
                int fileIdx = int.Parse(FileIdx);
                Person? person = PersonData?.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (person!= null)
                { 
                    WriteLog("person:"+Dump(person));
                    if (fileIdx == 0) {
                        if (person.TermsPrivarcyFileDataHash != null && person.TermsPrivarcyFileData != null) {
                            fileData = person.TermsPrivarcyFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(person.TermsPrivarcyFileData);
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

        private int GetNewSabun(String Location)
        {
            if (PersonData != null){
                int personCntByLocation=0;
                var options = new QueryOptions<Person>
                {
                    Where = a => a.Location == Location,
                };
                // options.Max = a => (int?)a.PersonID??0;
                IEnumerable<Person> persons = PersonData.List(options);
                personCntByLocation = PersonData.Count;
                // maxPersonIDByLocation=personData.Max;

                int maxPersonCntByLocation = personCntByLocation + 1;
                return maxPersonCntByLocation;
            } else {
                return -1;
            }
        }

        public IActionResult MyInfo()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO? personDTO = GetMe();
            string sabun = personDTO?.Sabun??"";
            int companyID = personDTO?.CompanyID??0;
            WriteLog("personDTO:" + Dump(personDTO));
            PersonDetailViewModel vm = new();
            if(!string.IsNullOrEmpty(sabun) ){
                Person person = new();
                Company company=new();
                var l = GetUnionPerson(null, sabun, null);
                if (l != null && l.a != null && l.a.Count > 0){
                    person = l.a[0];
                    if (person.ImageData != null && person.ImageDataHash != null) {
                        var base64 = Convert.ToBase64String(person.ImageDataHash);
                        ViewBag.img1= String.Format("data:image/gif;base64,{0}", base64);
                    }
                    if(companyID > 0){
                        company = GetCompanyByID(companyID);
                    }
                }
                if(string.IsNullOrEmpty(company.CompanyName)){
                    company.CompanyName = "DB HiTek";
                }
                if(string.IsNullOrEmpty(person.BirthDate.Trim()) && !string.IsNullOrEmpty(person.Location)){
                    BirthInfo birthInfo = GetBirthdayGender(person.Sabun);
                    WriteLog("get birthday"+Dump(birthInfo));
                    person.BirthDate = birthInfo.Birthday;
                    person.Gender = birthInfo.Gender;
                }
                WriteLog("person info:" + Dump(person));
                vm = new(){
                    Person = person,
                    Company = company,
                    CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                    CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus),
                };
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult MyInfo(string mode, string sabun, string IsAllowSMS)
        {
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mode: "+mode+", sabun: "+sabun);
            if (mode.Equals("E") && PersonData != null) // 내정보 수정
            {
                Person? person = GetPersonBySabun(sabun);
                if (person == null)
                {
                    WriteLog("mode: insert");
                    PersonDTO? my = GetMe();
                    person = new();
                    var l = GetUnionPerson(null, sabun, null);
                    if (l != null && l.a != null && l.a.Count > 0) {
                        person = l.a[0];
                    } else {
                        person.Sabun = sabun;
                        person.IsAllowSMS = IsAllowSMS ?? "N";
                    }
                    person.InsertSabun = my.Sabun;
                    person.InsertOrgID = my.OrgID;
                    person.InsertOrgNameKor = my.OrgNameKor;
                    person.InsertOrgNameEng = my.OrgNameEng;
                    person.InsertName = my.Name;

                    PersonData.Insert(person);
                } else {
                    WriteLog("mode: update");
                    person.IsAllowSMS = IsAllowSMS ?? "N";
                    PersonData.Update(person);
                }
                PersonData.Save();
            }
            return RedirectToAction("MyInfo", new { culture=GetLang()});
        }

        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
        public IActionResult BlackList (BlackListGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            BlackListListViewModel vm =new();
            if(BlackListData != null){
                BlackListGridData filterhValue = (BlackListGridData) FilterGridData(values);
                vm =new(){
                    BlackListLists=GetBlackListList(filterhValue),
                    // BlackListLists = BlackListData.List(options),
                    CodeBlackListType = GetCommonCodes((int)VisitEnum.CommonCode.BlackListType),
                    CodeBlackListStatus = GetCommonCodes((int)VisitEnum.CommonCode.BlackListStatus),
                    CurrentRoute = values,
                    SearchRoute=filterhValue,
                    TotalPages = values.GetTotalPages(BlackListData.Count),
                    TotalCnt = BlackListData.Count,
                };                
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
        public IActionResult? BlackListExcelDownload(BlackListGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);          
            // 성명	생년월일	회사명	등록일	상태	등록사유	경위서 new DataColumn(@Localizer["Report Circumstances"]), 
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Birth Date"]), new DataColumn(@Localizer["Company Name"]),
                                new DataColumn(@Localizer["Registration Date"]), new DataColumn(@Localizer["Status"]), new DataColumn(@Localizer["Registration Reason"]),
                                });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            BlackListGridData filterhValue = (BlackListGridData) FilterGridData(values);
            var v = GetBlackListList(filterhValue);
            List<CommonCode> CodeBlackListType = GetCommonCodes((int)VisitEnum.CommonCode.BlackListType);
            List< CommonCode > CodeBlackListStatus = GetCommonCodes((int)VisitEnum.CommonCode.BlackListStatus);
            if(v != null){
                string blackListType = string.Empty;
                string blackListStatus = string.Empty;
                string insertDate="";

                foreach(var blackList in v){
                    blackListType = "";
                    blackListStatus = "";
                    insertDate = "";
                    if (blackList.BlackListType >= 0 && CodeBlackListType != null) {
                        foreach(var m in CodeBlackListType) {
                            if (m.SubCodeID == blackList.BlackListType) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    blackListType = m.CodeNameKor;
                                }else {
                                    blackListType = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (blackList.BlackListStatus >= 0 && CodeBlackListStatus != null) {
                        foreach(var m in CodeBlackListStatus) {
                            if (m.SubCodeID == blackList.BlackListStatus) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    blackListStatus = m.CodeNameKor;
                                }else {
                                    blackListStatus = m.CodeNameEng??"";
                                }
                            }
                        }
                    }

                    if(blackList.InsertDate != null){
                        insertDate=string.Format("{0:yyyy-MM-dd}", blackList.InsertDate);
                    }
                    dt.Rows.Add(blackList.Name, blackList.BirthDate, blackList.CompanyName, insertDate, blackListStatus, blackListType);
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

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
        public IActionResult BlackReg ()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            BlackListRegViewModel vm =new(){
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeBlackListType = GetCommonCodes((int)VisitEnum.CommonCode.BlackListType),
                CodeBlackListStatus = GetCommonCodes((int)VisitEnum.CommonCode.BlackListStatus),                
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
        public async Task<IActionResult> BlackReg(string mode, BlackList blackList)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("A") && BlackListData != null && BlackListHistoryData!=null){
                // if (values != null && values.ToDictionary() != null){
                //     var dict = values.ToDictionary();
                //     dict.Add("culture", GetLang());
                //     ViewBag.prevDic = dict;
                // }
                // 블랙리스트 등록
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                // 1. 블랙리스트 (BlackList) 정보 입력 
                blackList.BlackListStatus = 0; //블랙리스트상태: 등록요청(0)/등록(1)/해제(2) 
                blackList.InsertSabun = my.Sabun; //등록자가 방문자가 아닐경우 회원정보 입력 
                blackList.InsertName = my.Name;
                blackList.InsertOrgID = my.OrgID;
                blackList.InsertOrgNameKor = my.OrgNameKor;
                blackList.InsertOrgNameEng = my.OrgNameEng;
                blackList.InsertDate = today;
                if(blackList.FormFile != null && blackList.FormFile.Count > 0){
                    FileData fileData = await GetFileData(blackList.FormFile[0]);
                    blackList.StatementFileDataHash = fileData.Hash;
                    if (!String.IsNullOrEmpty(fileData.Meta)) {
                        blackList.StatementFileData = fileData.Meta;
                    }
                }
                
                DateTime StartDate;
                if(blackList.BlackListType == 0){
                    //퇴직사원 BlackListType = 0 
                    StartDate = DateTime.Now; //string.Format("{0:yyyy-MM-dd}", "m.InsertDate");
                }
                // StartDate = StartDate, //시작일시
                // EndDate = EndDate, //종료일시

                BlackListData.Insert(blackList);
                BlackListData.Save();
                WriteLog("blackList: "+Dump(blackList));                
                //1.1 블랙리스트히스토리 등록 
                BlackListHistory blackListHistory = new(){
                    BlackListID = blackList.BlackListID, //블랙리스트ID 
                    VisitorType = blackList.VisitorType, //방문자구분(출입자)
                    VisitPersonID = blackList.VisitPersonID, //방문객회원ID(출입자)

                    Sabun = blackList.Sabun, //출입자
                    Name = blackList.Name,
                    OrgID = blackList.OrgID,
                    OrgNameKor = blackList.OrgNameKor,
                    OrgNameEng = blackList.OrgNameEng,

                    BirthDate = blackList.BirthDate, //생년월일
                    Gender = blackList.Gender, //성별
                    Mobile = blackList.Mobile, //휴대폰
                    CompanyName = blackList.CompanyName, //회사명
                    Tel = blackList.Tel, //전화번호

                    BlackListType = blackList.BlackListType, //등록사유구분: 퇴직임직원(0)/안전수칙위반(1)/보안수칙위반(2)/기타(3)

                    // StatementFileData = blackList.StatementFileData, //경위서(첨부파일)
                    // StatementFileDataHash = blackList.StatementFileDataHash, //경위서(첨부파일Hash)

                    Reason = blackList.Reason, //등록사유
                    BlackListStatus = blackList.BlackListStatus, //블랙리스트상태: 등록요청(0)/등록(1)/해제(2) 

                    // StartDate = blackList.StartDate, //시작일시
                    // EndDate = blackList.EndDate, //종료일시
                    
                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = today
                };
                BlackListHistoryData.Insert(blackListHistory);
                BlackListHistoryData.Save();
            }
            return RedirectToAction("BlackList", new { culture=GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
        public IActionResult BlackDetail (int blackListID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("blackListID:" + blackListID + ", slug" + slug);
            // if (values != null && values.ToDictionary() != null){
            //     var dict = values.ToDictionary();
            //     dict.Add("culture", GetLang());
            //     ViewBag.prevDic = dict;
            // }

            var blackList = BlackListData.Get(new QueryOptions<BlackList>{
                Where = a => a.BlackListID == blackListID,
            }) ?? new BlackList();

            var options = new QueryOptions<BlackListHistory>
            {
                Where = a => a.BlackListID == blackListID,
            };
            BlackListDetailViewModel vm = new(){
                BlackList = blackList,
                BlackListHistory = BlackListHistoryData.List(options), 
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeBlackListType = GetCommonCodes((int)VisitEnum.CommonCode.BlackListType),
                CodeBlackListStatus = GetCommonCodes((int)VisitEnum.CommonCode.BlackListStatus),  
                  
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
        public IActionResult BlackDetail(string mode, int blackListID, int BlackListStatus, String Memo)
        {
            WriteLog("mode: "+mode+", blackListID: "+blackListID+", BlackListStatus: "+BlackListStatus+", Memo: "+Memo);            
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("E")) // 블랙리스트 수정
            {
                PersonDTO my = GetMe();

                if(blackListID > 0 && BlackListData != null && BlackListHistoryData!=null)
                {
                    // 1. 블랙리스트 수정 
                    var orgObj = GetBlackList(blackListID, true);
                    var newObj = orgObj.Clone();

                    newObj.BlackListStatus = BlackListStatus; // 블랙리스트 상태 BlackListStatus : 0 등록요청, 1 등록, 2 해제
                    DateTime now = DateTime.Now;
                    if(BlackListStatus == 1){ // 1 등록
                        newObj.StartDate = now;
                        newObj.EndDate = now.AddDays(3); //안전위반 시 해당 날짜만큼 
                        // DateTime now = DateTime.Now;
                        // DateTime 하루추가 = now.AddDays(1);
                    }else{ //2 해제
                    }
                    newObj.UpdateDate = DateTime.Now;
                    BlackListData.Update(newObj);
                    BlackListData.Save();

                    // 2. 블랙리스트 진행상태 비고와 함께 입력
                    // 처리사유는 BlackListHistory 만 에 입력 
                    BlackListHistory blackListHistory = new()
                    {
                        BlackListID = blackListID, //블랙리스트ID 
                        VisitorType = orgObj.VisitorType, //방문자구분(출입자)
                        VisitPersonID = orgObj.VisitPersonID, //방문객회원ID(출입자)

                        Sabun = orgObj.Sabun, //출입자
                        Name = orgObj.Name,
                        OrgID = orgObj.OrgID,
                        OrgNameKor = orgObj.OrgNameKor,
                        OrgNameEng = orgObj.OrgNameEng,

                        BirthDate = orgObj.BirthDate, //생년월일
                        Gender = orgObj.Gender, //성별
                        Mobile = orgObj.Mobile, //휴대폰
                        CompanyName = orgObj.CompanyName, //회사명
                        Tel = orgObj.Tel, //전화번호

                        BlackListType = orgObj.BlackListType, //등록사유구분: 퇴직임직원(0)/안전수칙위반(1)/보안수칙위반(2)/기타(3)

                        // StatementFileData = orgObj.StatementFileData, //경위서(첨부파일)
                        // StatementFileDataHash = orgObj.StatementFileDataHash, //경위서(첨부파일Hash)

                        Reason = orgObj.Reason, //등록사유


                        Memo = Memo, // 처리사유
                        BlackListStatus = BlackListStatus, //블랙리스트상태: 등록요청(0)/등록(1)/해제(2) 

                        // StartDate = newObj.StartDate, //시작일시
                        // EndDate = newObj.EndDate, //종료일시
                        
                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    BlackListHistoryData.Insert(blackListHistory);
                    BlackListHistoryData.Save();
                }
            }
            return RedirectToAction("BlackDetail", new { blackListID, culture=GetLang()});
        }

        [HttpPost]
        public FileResult? DownloadBlackList(string BlackListID, string FileIdx){
            if (IsAccess() == false) {
                return null;
            }
            WriteLog("start download> BlackListID:"+BlackListID+" , FileIdx:" + FileIdx);
            if (!string.IsNullOrEmpty(BlackListID) && !string.IsNullOrEmpty(FileIdx) && !BlackListID.Equals("0")) 
            {
                int id = int.Parse(BlackListID);
                int fileIdx = int.Parse(FileIdx);
                BlackList? blackList = BlackListData?.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (blackList!= null)
                { 
                    WriteLog("blackList:"+Dump(blackList));
                    if (fileIdx == 0) {
                        if (blackList.StatementFileDataHash != null && blackList.StatementFileData != null) {
                            fileData = blackList.StatementFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(blackList.StatementFileData);
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
        private IEnumerable<BlackList> GetBlackListList(BlackListGridData filterhValue){
            if (BlackListData != null){
                var options = new QueryOptions<BlackList>
                {
                    PageNumber = filterhValue.PageNumber,
                    PageSize = filterhValue.PageSize,
                    OrderByDirection = filterhValue.SortDirection,
                    OrderBy = a => a.BlackListID,
                };
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                // {searchblackliststatus?}/{searchblacklisttype?}/{searchname?}/{searchcompanyname?}/{searchstartinsertdate?}/{searchendinsertdate?}")]
                if (filterhValue.SearchBlackListStatus != -1)
                {
                    options.MultipleWhere.Add(a => a.BlackListStatus == filterhValue.SearchBlackListStatus);
                }
                if (filterhValue.SearchBlackListType != -1)
                {
                    options.MultipleWhere.Add(a => a.BlackListType == filterhValue.SearchBlackListType);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchName))
                {
                    options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%" + filterhValue.SearchName + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchCompanyName))
                {
                    options.MultipleWhere.Add(a => a.CompanyName != null && EF.Functions.Like(a.CompanyName, "%" + filterhValue.SearchCompanyName + "%"));
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
                return BlackListData.List(options);
            }else{
                return default;
            }
        }


        private BlackList GetBlackList(int blackListID=-1, bool isNoTracking = false)
        {
            BlackList blackList = null;
            if(blackListID > 0) {
                var options = new QueryOptions<BlackList>
                {
                    Where = a => a.BlackListID == blackListID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    blackList = BlackListData.GetNT(blackListID);
                } else {
                    blackList = BlackListData.Get(blackListID);
                }
            }
            return blackList;
        }

       /// <summary>
        /// 사원 정보 검색
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="searchType">A: 임직원(기본), B: 전체 , P: 비상주 협력사 직원 Partner </param>
        /// <param name="persontypeid">0:임직원, 1:임직원 - 상주업체 직원, 2:비상주업체 관리자, 3:비상주업체 직원</param>
        /// <param name="issafeeduonly"></param>
        /// <returns></returns>
        public JsonResult? GetSearchPerson(string name, string department, string birthday="", string searchType = "A", int PersonTypeID = 3, bool isSafeEduOnly = true, bool isEmployeeOnly=false) {
            // 홍길동 B12541254 인사 031-259-96996 010-2159-96996 hone@test.com
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("searchType:"+searchType+", name:" + name + ", department:" + department+", isEmployeeOnly:"+isEmployeeOnly);
            // new PersonRecord("", "", "", "", "", "");
            List<PersonRecord> list = new();
            PersonData ??= new Repository<Person>(DbSVISIT);
            if (PersonData != null && CompanyData!=null) {
                /*
                    IQueryable<Person> outer = db.Person.Where(x => x.IsDelete == "N");
                    if (!string.IsNullOrEmpty(name))
                    {
                        outer = outer.Where(x => x.Name != null && EF.Functions.Like(x.Name, "%" + name + "%"));
                    }
                    if (!string.IsNullOrEmpty(department))
                    {
                        outer = outer.Where(x => x.Department != null && EF.Functions.Like(x.Department, "%" + department + "%"));
                    }
                    var list = outer.ToList();
                    foreach(var m in list) {
                        list.Add(new PersonRecord(m.Name, m.Sabun, m.Department, m.Tel, m.Mobile, m.Email));
                    }
                */ 
                //성명  회원사번    부서명Kor   부서명Eng     부서ID  자택전화  휴대전화    이메일      사업장      사업장구분명                업체ID      회사명      직급ID     직급명      회원구분      회원구분명                     출입증번호  차량허용대수    차량등록대수     성별Type    성별Name     생년월일    재직상태ID       재직상태명           자택주소 
                //Name  Sabun       OrgNameKor  OrgNameEng  OrgID   Tel       Mobile	 Email      Location    (LocationName-공통코드이용) CompanyID  CompanyName GradeID   GradeName   PersonTypeID (PersonTypeIDName-공통코드이용) CardNo     CarAllowCnt    CarRegCnt       Gender      GenderName  BirthDate   PersonStatusID  PersonStatusName    HomeAddr
                var today = DateTime.Now;
                List<CommonCode> CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType);
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                List< CommonCode > CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType);
                List< CommonCode > CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus);

                if (searchType.Equals("A")) {
                    // 임직원 검색
                    IQueryable<VImsUserInfo> userInfo = DbPASSPORT.VImsUserInfos.Where(x => x.IsDeleted == 0);
                    if(!string.IsNullOrEmpty(name)){
                        userInfo = userInfo.Where(x => x.UserName.Equals(name));
                    }
                    if(!string.IsNullOrEmpty(department)){
                        userInfo = userInfo.Where(x => x.DeptName.Equals(department));
                    }
                    var l = userInfo.ToList(); // SqlException

                    WriteLog("l:" + Dump(l));
                    foreach (var m in l) {
                        var LocationName = "";
                        foreach (var m2 in CodeLocation)
                        {
                            if (m2.SubCodeDesc != null && m2.SubCodeDesc.Equals(m.Location))
                            {
                                LocationName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var CompanyName ="DBHiteK"; // DB HiTek
                        var PersonTypeIDName = "";
                        var PTypeID = 0; //0 임직원, 1 상주, 비상주(2 관리자/3 사원)
                        if(m.EmployeeType.Equals("C")){
                            PersonTypeID = 1;
                        }
                        foreach (var m2 in CodePersonType)
                        {
                            if (m2.SubCodeID == PTypeID)
                            {
                                PersonTypeIDName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var GenderName = ""; // 없음.
                        // foreach (var m2 in CodeGenderType)
                        // {
                        //     if (m2.SubCodeID == m.Gender)
                        //     {
                        //         GenderName = m2.CodeNameKor;
                        //         break;
                        //     }
                        // }
                        var PersonStatusName = "";
                        foreach (var m2 in CodePersonStatus)
                        {
                            if (m2.SubCodeID == m.IsDeleted)
                            {
                                PersonStatusName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var IsVisitorEdu = "N";
                        var IsCleanEdu = "N";
                        var IsSafetyEdu = "N";
                        if(string.IsNullOrEmpty(birthday)){
                            list.Add(new PersonRecord(m.UserName, m.EmployeeId, m.DeptName, m.DeptName, m.SapDeptCode, m.OfficeTel, m.Mobile, m.Email, m.Location, LocationName,
                                "", CompanyName, m.SapTitleCode, m.SapTitleName, PTypeID.ToString() ?? "", PersonTypeIDName,
                                "", "", "", "", GenderName, "", m.IsDeleted.ToString() ?? "", PersonStatusName, "", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, ""));
                        } else if(!string.IsNullOrEmpty(m.EmployeeId)){
                            BirthInfo birthInfo = GetBirthdayGender(m.EmployeeId);
                            if(birthInfo.Birthday.Equals(birthday)){
                                list.Add(new PersonRecord(m.UserName, m.EmployeeId, m.DeptName, m.DeptName, m.SapDeptCode, m.OfficeTel, m.Mobile, m.Email, m.Location, LocationName,
                                    "", CompanyName, m.SapTitleCode, m.SapTitleName, PTypeID.ToString() ?? "", PersonTypeIDName,
                                    "", "", "", "", GenderName, "", m.IsDeleted.ToString() ?? "", PersonStatusName, "", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, ""));
                                break;
                            }
                        }
                    }
                } else if(searchType.Equals("B")) {
                    // 전체 검색
                    PersonGridData values = new()
                    {
                        PageSize = 0
                    };
                    if (!string.IsNullOrEmpty(name)){
                        values.SearchName = name;
                    }
                    if(!string.IsNullOrEmpty(department)){
                        values.SearchOrgName = department;
                    }
                    if(isEmployeeOnly) {
                        values.SearchPersonTypeID = "100";
                    }
                    var l = GetUnionPerson(values:values);
                    WriteLog("person list:" + Dump(l));
                    if (l != null && l.c != false){
                        foreach (var m in l.a)
                        {
                            var LocationName = "";
                            foreach(var m2 in CodeLocation) {
                                if (m2.SubCodeDesc != null && m2.SubCodeDesc.Equals(m.Location)){
                                    LocationName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var CompanyName = GetCompanyName(m.CompanyID??0);
                            if(CompanyName == null){
                                CompanyName = "";
                            }
                            var PersonTypeIDName = "";
                            foreach(var m2 in CodePersonType) {
                                if(m2.SubCodeID == m.PersonTypeID) {
                                    PersonTypeIDName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var GenderName = "";
                            foreach(var m2 in CodeGenderType) {
                                if(m2.SubCodeID == m.Gender) {
                                    GenderName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var PersonStatusName = "";
                            foreach(var m2 in CodePersonStatus) {
                                if(m2.SubCodeID == m.PersonStatusID) {
                                    PersonStatusName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var IsVisitorEdu = "N";
                            if (m.VisitorEduLastDate != null) {
                                var diffOfDates = today - m.VisitorEduLastDate;
                                if (diffOfDates.Days < 365) {
                                    IsVisitorEdu = "Y";
                                }
                            }
                            var IsCleanEdu = "N";
                            if (m.CleanEduLastDate != null) {
                                var diffOfDates = today - m.CleanEduLastDate;
                                if (diffOfDates.Days < 365) {
                                    IsCleanEdu = "Y";
                                }
                            }
                            var IsSafetyEdu = "N";
                            if (m.SafetyEduLastDate != null) {
                                var diffOfDates = today - m.SafetyEduLastDate;
                                if (diffOfDates.Days < 365) {
                                    IsSafetyEdu = "Y";
                                }
                            }

                            var strWorkTypeCodeID = "";
                            if(m.WorkTypeCodeID != null) {
                                strWorkTypeCodeID = m.WorkTypeCodeID.ToString();
                            }
                            var strGradeID = "";
                            if(m.GradeID != null){
                                strGradeID = m.GradeID.ToString();
                            }
                            var strOrgID = "";
                            if(m.OrgID != null){
                                strOrgID = m.OrgID.ToString();
                            }
                            var strCompanyID = "0";
                            if(m.CompanyID != null){
                                strCompanyID = m.CompanyID.ToString();
                            }
                            var strPersonTypeID = "";
                            if(m.PersonTypeID != null){
                                strPersonTypeID = m.PersonTypeID.ToString();
                            }
                            var strCarAllowCnt = "0";
                            if(m.CarAllowCnt != null){
                                strCarAllowCnt = m.CarAllowCnt.ToString();
                            }
                            var strCarRegCnt = "0";
                            if(m.CarRegCnt != null){
                                strCarRegCnt = m.CarRegCnt.ToString();
                            }
                            var strGender = "";
                            if(m.Gender != null){
                                strGender = m.Gender.ToString();
                            }
                            var strPersonStatusID = "";
                            if(m.PersonStatusID != null){
                                strPersonStatusID = m.PersonStatusID.ToString();
                            }
                            
                            // var p = new PersonRecord(m.Name, m.Sabun, m.OrgNameKor ?? "", m.OrgNameEng ?? "", strOrgID, m.Tel ?? "", m.Mobile ?? "", m.Email ?? "", m.Location ?? "", LocationName,
                            //     strCompanyID, CompanyName, strGradeID, m.GradeName ?? "", strPersonTypeID, PersonTypeIDName,
                            //     m.CardNo ?? "", strCarAllowCnt, strCarRegCnt, strGender, GenderName, m.BirthDate ?? "",
                            //     strPersonStatusID, PersonStatusName, m.HomeAddr ?? "", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, strWorkTypeCodeID);
                            // WriteLog("p:" + Dump(p));

                            list.Add(new PersonRecord(m.Name, m.Sabun, m.OrgNameKor??"", m.OrgNameEng??"", strOrgID, m.Tel??"", m.Mobile??"", m.Email??"", m.Location??"", LocationName,
                                strCompanyID, CompanyName, strGradeID, m.GradeName??"", strPersonTypeID, PersonTypeIDName,
                                m.CardNo??"", strCarAllowCnt, strCarRegCnt, strGender, GenderName, m.BirthDate??"", 
                                strPersonStatusID, PersonStatusName, m.HomeAddr??"", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, strWorkTypeCodeID));
                        }
                    }
                    WriteLog("list:" + Dump(list));
                    // list.Add(new PersonRecord("홍길동", "B0000001", "인사팀", "HR", "1", "031-259-96996", "010-2159-96996", "hone1@test.com", "2000", "부천", "", "DBHiTek", "1", "수석", "0", "임직원", "A1234031", "1", "0", "0", "남", "2012-05-09", "0", "재직", "서울시 강남구 역삼로1","Y","Y","Y","1"));
                    // list.Add(new PersonRecord("심청이", "B0000001", "개발", "Dev", "", "02-6203-2900", "010-6551-3899", "pandora@conoz.net", "2000", "부천", "", "협력사", "", "부장", "3", "비상주직원", "", "1", "", "1", "여", "2012-12-29", "1", "휴직", "서울시 강남구 역삼로2","Y","Y","Y","1"));
                    // list.Add(new PersonRecord("손흥민", "B0000001", "인사팀", "HR", "1", "031-259-96998", "010-2159-96998", "hone3@test.com", "2000", "부천", "", "DBHiTek", "1", "수석", "0", "임직원", "A1234039", "1", "1", "0", "남", "2012-05-19", "0", "재직", "서울시 강남구 역삼로3","Y","Y","Y","1"));
                }
                else if (searchType.Equals("C")) //2023.09.21 dsyoo 휴대물품 검색시 임직원및비상주협력사를 조회
                {
                    // 전체 검색
                    PersonGridData values = new()
                    {
                        PageSize = 0
                    };
                    if (!string.IsNullOrEmpty(name))
                    {
                        values.SearchName = name;
                    }
                    if (!string.IsNullOrEmpty(department))
                    {
                        values.SearchOrgName = department;
                    }

                    var l = GetAvailabePerson(values: values);
                    WriteLog("person list:" + Dump(l));
                    if (l != null && l.c != false)
                    {
                        foreach (var m in l.a)
                        {
                            var LocationName = "";
                            foreach (var m2 in CodeLocation)
                            {
                                if (m2.SubCodeDesc != null && m2.SubCodeDesc.Equals(m.Location))
                                {
                                    LocationName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var CompanyName = GetCompanyName(m.CompanyID ?? 0);
                            if (CompanyName == null)
                            {
                                CompanyName = "";
                            }
                            var PersonTypeIDName = "";
                            foreach (var m2 in CodePersonType)
                            {
                                if (m2.SubCodeID == m.PersonTypeID)
                                {
                                    PersonTypeIDName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var GenderName = "";
                            foreach (var m2 in CodeGenderType)
                            {
                                if (m2.SubCodeID == m.Gender)
                                {
                                    GenderName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var PersonStatusName = "";
                            foreach (var m2 in CodePersonStatus)
                            {
                                if (m2.SubCodeID == m.PersonStatusID)
                                {
                                    PersonStatusName = m2.CodeNameKor;
                                    break;
                                }
                            }
                            var IsVisitorEdu = "N";
                            if (m.VisitorEduLastDate != null)
                            {
                                var diffOfDates = today - m.VisitorEduLastDate;
                                if (diffOfDates.Days < 365)
                                {
                                    IsVisitorEdu = "Y";
                                }
                            }
                            var IsCleanEdu = "N";
                            if (m.CleanEduLastDate != null)
                            {
                                var diffOfDates = today - m.CleanEduLastDate;
                                if (diffOfDates.Days < 365)
                                {
                                    IsCleanEdu = "Y";
                                }
                            }
                            var IsSafetyEdu = "N";
                            if (m.SafetyEduLastDate != null)
                            {
                                var diffOfDates = today - m.SafetyEduLastDate;
                                if (diffOfDates.Days < 365)
                                {
                                    IsSafetyEdu = "Y";
                                }
                            }

                            var strWorkTypeCodeID = "";
                            if (m.WorkTypeCodeID != null)
                            {
                                strWorkTypeCodeID = m.WorkTypeCodeID.ToString();
                            }
                            var strGradeID = "";
                            if (m.GradeID != null)
                            {
                                strGradeID = m.GradeID.ToString();
                            }
                            var strOrgID = "";
                            if (m.OrgID != null)
                            {
                                strOrgID = m.OrgID.ToString();
                            }
                            var strCompanyID = "0";
                            if (m.CompanyID != null)
                            {
                                strCompanyID = m.CompanyID.ToString();
                            }
                            var strPersonTypeID = "";
                            if (m.PersonTypeID != null)
                            {
                                strPersonTypeID = m.PersonTypeID.ToString();
                            }
                            var strCarAllowCnt = "0";
                            if (m.CarAllowCnt != null)
                            {
                                strCarAllowCnt = m.CarAllowCnt.ToString();
                            }
                            var strCarRegCnt = "0";
                            if (m.CarRegCnt != null)
                            {
                                strCarRegCnt = m.CarRegCnt.ToString();
                            }
                            var strGender = "";
                            if (m.Gender != null)
                            {
                                strGender = m.Gender.ToString();
                            }
                            var strPersonStatusID = "";
                            if (m.PersonStatusID != null)
                            {
                                strPersonStatusID = m.PersonStatusID.ToString();
                            }

                            // var p = new PersonRecord(m.Name, m.Sabun, m.OrgNameKor ?? "", m.OrgNameEng ?? "", strOrgID, m.Tel ?? "", m.Mobile ?? "", m.Email ?? "", m.Location ?? "", LocationName,
                            //     strCompanyID, CompanyName, strGradeID, m.GradeName ?? "", strPersonTypeID, PersonTypeIDName,
                            //     m.CardNo ?? "", strCarAllowCnt, strCarRegCnt, strGender, GenderName, m.BirthDate ?? "",
                            //     strPersonStatusID, PersonStatusName, m.HomeAddr ?? "", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, strWorkTypeCodeID);
                            // WriteLog("p:" + Dump(p));

                            list.Add(new PersonRecord(m.Name, m.Sabun, m.OrgNameKor ?? "", m.OrgNameEng ?? "", strOrgID, m.Tel ?? "", m.Mobile ?? "", m.Email ?? "", m.Location ?? "", LocationName,
                                strCompanyID, CompanyName, strGradeID, m.GradeName ?? "", strPersonTypeID, PersonTypeIDName,
                                m.CardNo ?? "", strCarAllowCnt, strCarRegCnt, strGender, GenderName, m.BirthDate ?? "",
                                strPersonStatusID, PersonStatusName, m.HomeAddr ?? "", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, strWorkTypeCodeID));
                        }
                    }
                    WriteLog("list:" + Dump(list));
                    // list.Add(new PersonRecord("홍길동", "B0000001", "인사팀", "HR", "1", "031-259-96996", "010-2159-96996", "hone1@test.com", "2000", "부천", "", "DBHiTek", "1", "수석", "0", "임직원", "A1234031", "1", "0", "0", "남", "2012-05-09", "0", "재직", "서울시 강남구 역삼로1","Y","Y","Y","1"));
                    // list.Add(new PersonRecord("심청이", "B0000001", "개발", "Dev", "", "02-6203-2900", "010-6551-3899", "pandora@conoz.net", "2000", "부천", "", "협력사", "", "부장", "3", "비상주직원", "", "1", "", "1", "여", "2012-12-29", "1", "휴직", "서울시 강남구 역삼로2","Y","Y","Y","1"));
                    // list.Add(new PersonRecord("손흥민", "B0000001", "인사팀", "HR", "1", "031-259-96998", "010-2159-96998", "hone3@test.com", "2000", "부천", "", "DBHiTek", "1", "수석", "0", "임직원", "A1234039", "1", "1", "0", "남", "2012-05-19", "0", "재직", "서울시 강남구 역삼로3","Y","Y","Y","1"));
                }
                else {
                    // 협력사
                    var options = new QueryOptions<Person>
                    {
                        PageNumber = 0,
                        PageSize = 0,
                        OrderBy = a => a.Name,
                        OrderByDirection = "asc",
                    };
                    if(PersonTypeID >-1){
                        options.MultipleWhere.Add(a => a.PersonTypeID == PersonTypeID); // 비상주 사원
                    }
                    if(isSafeEduOnly){
                        options.MultipleWhere.Add(a => a.SafetyEduLastDate != null); // 비상주 사원
                    }
                    
                    if (!string.IsNullOrEmpty(name)) {
                        options.MultipleWhere.Add(a => EF.Functions.Like(a.Name, "%"+ name +"%"));
                    }
                    if (!string.IsNullOrEmpty(department)) {
                        options.MultipleWhere.Add(a => a.OrgNameKor != null && EF.Functions.Like(a.OrgNameKor, "%"+ department +"%"));
                    }
                    var Persons = PersonData.List(options);
                    foreach(var m in Persons) {
                        var LocationName = "";
                        foreach(var m2 in CodeLocation) {
                            if (m2.SubCodeDesc != null && m2.SubCodeDesc.Equals(m.Location)){
                                LocationName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var CompanyName = GetCompanyName(m.CompanyID??0);
                        var PersonTypeIDName = "";
                        foreach(var m2 in CodePersonType) {
                            if(m2.SubCodeID == m.PersonTypeID) {
                                PersonTypeIDName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var GenderName = "";
                        foreach(var m2 in CodeGenderType) {
                            if(m2.SubCodeID == m.Gender) {
                                GenderName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var PersonStatusName = "";
                        foreach(var m2 in CodePersonStatus) {
                            if(m2.SubCodeID == m.PersonStatusID) {
                                PersonStatusName = m2.CodeNameKor;
                                break;
                            }
                        }
                        var IsVisitorEdu = "N";
                        if (m.VisitorEduLastDate != null) {
                            var diffOfDates = today - m.VisitorEduLastDate.Value;
                            if (diffOfDates.Days < 365) {
                                IsVisitorEdu = "Y";
                            }
                        }
                        var IsCleanEdu = "N";
                        if (m.CleanEduLastDate != null) {
                            var diffOfDates = today - m.CleanEduLastDate.Value;
                            if (diffOfDates.Days < 365) {
                                IsCleanEdu = "Y";
                            }
                        }
                        var IsSafetyEdu = "N";
                        if (m.SafetyEduLastDate != null) {
                            var diffOfDates = today - m.SafetyEduLastDate.Value;
                            if (diffOfDates.Days < 365) {
                                IsSafetyEdu = "Y";
                            }
                        }
                        if (isSafeEduOnly == false || (isSafeEduOnly && IsSafetyEdu.Equals("Y"))) {
                            list.Add(new PersonRecord(m.Name, m.Sabun, m.OrgNameKor??"", m.OrgNameEng??"", m.OrgID.ToString()??"", m.Tel??"", m.Mobile??"", m.Email??"", m.Location??"", LocationName,
                                m.CompanyID.ToString()??"", CompanyName, m.GradeID.ToString()??"", m.GradeName??"", m.PersonTypeID.ToString()??"", PersonTypeIDName,
                                m.CardNo??"", m.CarAllowCnt.ToString()??"", m.CarRegCnt.ToString()??"", m.Gender.ToString()??"", GenderName, m.BirthDate??"", 
                                m.PersonStatusID.ToString()??"", PersonStatusName, m.HomeAddr??"", IsVisitorEdu, IsCleanEdu, IsSafetyEdu, m.WorkTypeCodeID.ToString()));
                        }
                    }
                    // list.Add(new PersonRecord("손흥민", "B0000002", "안전1팀", "HR", "1", "031-259-96996", "010-0001-0001", "hone1@test.com", "2000", "부천", "", "협력사1", "1", "수석", "0", "임직원", "A1234031", "1", "0", "0", "남", "2012-05-09", "0", "재직", "서울시 강남구 역삼로1","Y","Y","Y"));
                    // list.Add(new PersonRecord("심청이", "B0000003", "공사1팀", "HR", "1", "031-259-96996", "010-0002-0001", "hone1@test.com", "2000", "부천", "", "협력사2", "1", "수석", "0", "임직원", "A1234031", "1", "0", "0", "남", "2012-05-09", "0", "재직", "서울시 강남구 역삼로1","Y","Y","Y"));
                    // list.Add(new PersonRecord("이윤희", "B0000001", "개발팀", "HR", "1", "031-259-96996", "010-0003-0001", "hone1@test.com", "2000", "부천", "", "협력사3", "1", "수석", "0", "임직원", "A1234031", "1", "0", "0", "남", "2012-05-09", "0", "재직", "서울시 강남구 역삼로1","Y","Y","Y"));
                }
            }
            return Json(list);
        }
     }
    internal record PersonRecord(string Name, string Sabun, string OrgNameKor, string OrgNameEng, string OrgID, string Tel, string Mobile, string Email, 
        string Location, string LocationName, string CompanyID, string CompanyName, string GradeID, string GradeName, string PersonTypeID, string PersonTypeIDName, 
        string CardNo, string CarAllowCnt, string CarRegCnt, string Gender, string GenderName, string BirthDate, string PersonStatusID, string PersonStatusName, string HomeAddr, 
        string IsVisitorEdu, string IsCleanEdu, string IsSafetyEdu, string WorkTypeCodeID);
    internal record BirthInfo (string Birthday, int Gender);

    public class ImageMeta {
        public string ContentType { get; set; } = string.Empty; 
        public int Length { get; set; } = 0; 
        public string FileName { get; set; } = string.Empty; 
        public string ContentDisposition { get; set; } = string.Empty; 
    }
}
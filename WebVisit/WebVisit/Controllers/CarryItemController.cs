using System.Data;
using System.Linq.Expressions;
using System.Globalization;
using System.Reflection;
using System;
using System.Collections.Generic;

using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using WebVisit.Models;
using WebVisit.Models.DomainModels.Passport;
using WebVisit.Models.DomainModels.MySQL;
using WebVisit.Models.ViewModels;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class CarryItemController : BaseController
    {
        private Repository<CarryItemApply>? CarryItemApplyData { get; set; }
        private Repository<CarryItemApplyHistory>? CarryItemApplyHistoryData { get; set; }
        private Repository<CarryItemInfo>? CarryItemInfoData { get; set; }        
       private Repository<CarryItemInfoHistory>? CarryItemInfoHistoryData { get; set; }        

        public CarryItemController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer)
        {
        }
        
        protected override void Init(){
            if (DbSVISIT != null) {
                CarryItemApplyData ??= new Repository<CarryItemApply>(DbSVISIT);
                CarryItemApplyHistoryData ??= new Repository<CarryItemApplyHistory>(DbSVISIT);
                CarryItemInfoData ??= new Repository<CarryItemInfo>(DbSVISIT);
                CarryItemInfoHistoryData ??= new Repository<CarryItemInfoHistory>(DbSVISIT);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("List");
        }

        /// <summary>
        /// 관리자 : 휴대물품조회/다운로드
        /// 일반관리자는 사업장의 휴대물품만  조회/다운로드
        /// 임직원 : 본인이 신청한 휴대물품만 조회
        /// 비상주업체 : 자사 비상주 직원의 휴대물품 신청건만 조회
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        public IActionResult List(CarryItemGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity() || IsPartnerNonresidentManager();
            if(accessible == false) {
                return View("_Inaccessible");
            }
            var my = GetMe();
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsSecurity() || IsPartnerNonresidentManager();
            // IsMasterWithSabun
            ViewBag.Registable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            ViewBag.IsModStatus = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsSecurity();
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsSecurity = IsSecurity();
            ViewBag.my = my;
            // WriteLog("db13579**: "+"1234**".SHA256Encrypt());
            CarryItemGridData filterhValue = (CarryItemGridData) FilterGridData(values);
            // WriteLog("VisitGridData:"+Dump(values));

            var v = GetCarryItemApplyList(filterhValue);
            CarryItemListViewModel vm = new();
                if(CarryItemApplyData != null){
                // CarryItemApplyStatus 휴대물품신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
                // CarryItemStatus 휴대물품반입반출상태: 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)
                // ImportPurpose 반입목적: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
                // CarryItemImportType 휴대물품반입구분: 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
                vm =new(){
                    CarryItemApplys = v,
                    CodeCarryItemApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemApplyStatus),
                    CodeCarryItemStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemStatus),
                    CodeCarryItemImportType = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemImportType),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                    CurrentRoute = values,
                    SearchRoute = filterhValue,
                    TotalPages = values.GetTotalPages(CarryItemApplyData.Count),
                    TotalCnt = CarryItemApplyData.Count,
                };

            }
            return View(vm);
        }

        /// <summary>
        /// 관리자 : 휴대물품조회/다운로드
        /// 일반관리자는 사업장의 휴대물품만  조회/다운로드
        /// 임직원 : 본인이 신청한 휴대물품만 조회
        /// 비상주업체 : 자사 비상주 직원의 휴대물품 신청건만 조회
        /// </summary>
        /// <param name="values"></param>
        /// <param name="carryItemApplyID"></param>
        /// <param name="CarryItemStatus"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        public IActionResult List (CarryItemGridData values, int carryItemApplyID, int CarryItemStatus, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mode: "+mode+", carryItemApplyID: "+carryItemApplyID+", CarryItemStatus: "+CarryItemStatus);
           
            if(mode == "ECarryItemStatus"){
                if(carryItemApplyID > 0)
                {
                    PersonDTO my = GetMe();
                    var orgObj = GetCarryItemApply(carryItemApplyID, true);
                    var newObj2 = orgObj.Clone();
                    newObj2.CarryItemStatus = CarryItemStatus;
                    // CarryItemStatus 휴대물품반입반출상태: 신청(0), 반려(1), 반출대기(2), 반출확인(3), 정문반송(4), 반출완료(5), 반입대기(6), 반입확인(7), 반입완료(8)
                    if(CarryItemStatus == 1){// || CarryItemStatus == 2
                        newObj2.EntryDate = DateTime.Now;
                        TempData["PrintAsses"] = carryItemApplyID;
                    }else if(CarryItemStatus == 8){
                        newObj2.ExitDate = DateTime.Now;
                    }
                    newObj2.UpdateDate = DateTime.Now;
                    CarryItemApplyData.Update(newObj2);
                    CarryItemApplyData.Save();

                    CarryItemApplyHistory carryItemApplyHistory = new()
                    {
                        CarryItemApplyID = carryItemApplyID,
                        // VisitApplyPersonID = orgObj.VisitApplyPersonID, // 1. 출입증이 있는 비상주사원 -> 빈값 2. 방문/공사/안전교육 신청(승인필수)된 방문자 -> 방문신청회원No 입력 
                        ImportDate = orgObj.ImportDate, //반입일
                        ExportDate = orgObj.ExportDate, //반출예정일
                        Location = orgObj.Location, //사업장구분
                        PlaceCodeID = orgObj.PlaceCodeID, //장소코드ID(반입장소, 공통코드)
                        ImportPurposeCodeID = orgObj.ImportPurposeCodeID, //반입목적코드ID(공통코드)
                        ImportReason = orgObj.ImportReason, //반입사유
                        ImportWayType = orgObj.ImportWayType, //반입자구분: 대행등록(0,임직원신청)/본인(1,방문신청) (공통코드)

                        CarryVisitorType = orgObj.CarryVisitorType, //방문자구분 (반입자)
                        VisitPersonID = orgObj.VisitPersonID, //방문객회원ID (반입자)
                        Sabun = orgObj.Sabun, //회원사번(반입자)
                        Name = orgObj.Name, //회원이름(반입자)
                        OrgID = orgObj.OrgID, //부서ID(반입자)
                        OrgNameKor = orgObj.OrgNameKor, //부서명Kor(반입자)
                        OrgNameEng = orgObj.OrgNameEng, //부서명Eng(반입자)
                        CompanyName = orgObj.CompanyName, //회사명(반입자)
                        Mobile = orgObj.Mobile, //휴대전화(반입자)

                        ApprovalSabun = orgObj.ApprovalSabun, //회원사번(결재자)
                        ApprovalName = orgObj.ApprovalName, //회원이름(결재자)
                        ApprovalOrgID = orgObj.ApprovalOrgID, //부서ID(결재자)
                        ApprovalOrgNameKor = orgObj.ApprovalOrgNameKor, //부서명Kor(결재자)
                        ApprovalOrgNameEng = orgObj.ApprovalOrgNameEng, //부서명Eng(결재자)

                        ContactSabun = orgObj.ContactSabun, //회원사번(담당자)
                        ContactName = orgObj.ContactName, //회원이름(담당자)
                        ContactOrgID = orgObj.ContactOrgID, //부서ID(담당자)
                        ContactOrgNameKor = orgObj.ContactOrgNameKor, //부서명Kor(담당자)
                        ContactOrgNameEng = orgObj.ContactOrgNameEng, //부서명Eng(담당자)
                        ContactMobile = orgObj.ContactMobile, //휴대전화(담당자)

                        CarryItemApplyStatus = orgObj.CarryItemApplyStatus, //휴대물품신청상태 CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                        CarryItemStatus = CarryItemStatus, //휴대물품반입반출상태 CarryItemStatus : 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)
                        ApprovalOpinion = orgObj.ApprovalOpinion, //결재자 의견
                        // ApprovalDate = ApprovalDate, //결제일시
                        ImportTime = orgObj.ImportTime, //반입시간
                        ExportTime = orgObj.ExportTime, //반출시간


                        UpdateVisitorType = orgObj.InsertVisitorType, //방문자구분(등록/변경자)
                        UpdateVisitPersonID = orgObj.InsertVisitPersonID, //방문객회원ID(등록/변경자)

                        // Memo = Memo, // 비고
                        UpdateSabun = my.Sabun,//등록자가 방문자가 아닐경우 회원정보 입력 
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    CarryItemApplyHistoryData.Insert(carryItemApplyHistory);
                    CarryItemApplyHistoryData.Save();

                }
            }
            return RedirectToAction("List", new { culture = GetLang() });
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        public IActionResult? ExcelDownload(CarryItemGridData values, string ExTitle = "DBHiTek")
        {
            if(IsAccessAPI() == false){
                return default;
            }
            var accessible = IsMaster() || IsGeneralManager() || IsSecurity() || IsPartnerNonresidentManager();
            if(accessible == false) {
                return default;
            }

            DataTable dt = new(ExTitle);
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Approval Classify"]), new DataColumn(@Localizer["Apply Period"]),
                                        new DataColumn(@Localizer["Import DateTime"]), new DataColumn(@Localizer["Export DateTime"]), new DataColumn(@Localizer["Company Name"]),
                                        new DataColumn(@Localizer["Name"]),new DataColumn(@Localizer["Visit Purpose"]),new DataColumn(@Localizer["Contact Name"]),
                                        new DataColumn(@Localizer["Status Classify"]), });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            CarryItemGridData filterhValue = (CarryItemGridData) FilterGridData(values);
            // WriteLog("CarryItemGridData:"+Dump(values));
            var v = GetCarryItemApplyList(filterhValue);

            List<CommonCode> CodeCarryItemApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemApplyStatus);
            List< CommonCode > CodeCarryItemStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemStatus);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            List< CommonCode > CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose);
            if(v != null){
                foreach (var c in v){
                    // 사업장	승인구분	신청기간	반입일시	반출일시	회사명	성명	반입목적	담당자명	상태구분
                    string location = "";
                    string importPurpose = "";
                    string carryItemApplyStatus = "";
                    string carryItemStatus = "";
                    if (c.Location != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(c.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (c.ImportPurposeCodeID > -1 && CodeImportPurpose != null) {
                        foreach(var m in CodeImportPurpose) {
                            if (m.SubCodeID == c.ImportPurposeCodeID) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    importPurpose = m.CodeNameKor;
                                }else {
                                    importPurpose = m.CodeNameEng;
                                }
                            }
                        }
                    }
                    if (c.CarryItemApplyStatus > -1 && CodeCarryItemApplyStatus != null) {
                        foreach(var m in CodeCarryItemApplyStatus) {
                            if (m.SubCodeID == c.CarryItemApplyStatus) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    carryItemApplyStatus = m.CodeNameKor;
                                }else {
                                    carryItemApplyStatus = m.CodeNameEng;
                                }
                            }
                        }
                    }
                    /*-------------------------------------------------------------------------*/
                    /* 2023.09.11 dsyoo 상태정보 표시 변경*/
                    //if (c.CarryItemStatus > -1 && CodeCarryItemStatus != null) {
                    //    foreach(var m in CodeCarryItemStatus) {
                    //        if (m.SubCodeID == c.CarryItemStatus) {
                    //            if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                    //                carryItemStatus = m.CodeNameKor;
                    //            }else {
                    //                carryItemStatus = m.CodeNameEng??"";
                    //            }
                    //        }
                    //    }
                    //}

                    carryItemStatus = SelectItems.CarryItemText(CultureInfo.CurrentCulture.ToString(),
                            CodeCarryItemStatus,
                            c.ImportPurposeCodeID,
                            c.CarryItemApplyStatus,
                            c.CarryItemStatus,
                            "",
                            c.ContactSabun);
                    /*-------------------------------------------------------------------------*/
                    // location carryItemApplyStatus c.InsertDate.ToString("yyyy-MM-dd")  c.EntryDate c.ExitDate c.CompanyName importPurpose c.ContactName
                    // 사업장	승인구분	신청기간	반입일시	반출일시	회사명	성명	반입목적	담당자명	상태구분
                    dt.Rows.Add(location, carryItemApplyStatus, c.InsertDate.ToString("yyyy-MM-dd"), c.EntryDate, c.ExitDate, c.CompanyName, c.Name, importPurpose, c.ContactName, carryItemStatus);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            } else {
                // WriteLog("CarryItem is null");
                return null;
            }            
        }

        /// <summary>
        /// 마스터 : 휴대물품신청
        /// 임직원 : 휴대물품 신청
        /// 비상주업체 : 자사 휴대물품 반입신청 가능
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        public IActionResult Reg(CarryItemGridData values) {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            List<ApprovalPerson> approvalPeople = GetApprovalPerson();
            WriteLog("approvalPeople:" + Dump(approvalPeople));
            ViewBag.IsEmployee = IsEmployee();
            //2023.09.21 dsyoo 작성자가 포르테 사번이있는 임직원이면 전체조회건으로 처리
            PersonDTO my = GetMe();
            if (string.IsNullOrEmpty(my.Sabun) == false)
            { // PORTE ID 있음
                ViewBag.searchType = "C"; //전체조회
            }
            else
            {
                ViewBag.searchType = "P"; //비상주업체조회
            }
            CarryItemRegViewModel vm =new(){
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                // 반입장소 Place: 0 공통-통제구역-적색-클린룸, 1 공통-제한구역-황색-건물내부, 2 공통-일반구역-청색-건물외부/접견실/납품 ....... 
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                // 휴대물품 구분 CarryItem : 0 노트북, 1 PC, 2 카메라, 3 기타 
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
                // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
                CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),
                ApprovalPeople = approvalPeople,
            };
            return View(vm);
        }

        /// <summary>
        /// 마스터 : 휴대물품신청
        /// 임직원 : 휴대물품 신청
        /// 비상주업체 : 자사 휴대물품 반입신청 가능
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <param name="CarryItems"></param>
        /// <param name="carryItemApply"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        public IActionResult Reg(CarryItemGridData values, string mode, string CarryItems, CarryItemApply carryItemApply)
        {
            // 휴대물품신청: CarryItemApplyID VisitApplyPersonID	ImportDate	ExportDate	Location	ImportPurposeCodeID	PlaceCodeID	ImportReason	ImportWayType	CarryVisitorType	
            //              VisitPersonID	Sabun	Name	OrgID	OrgNameKor	OrgNameEng	ApprovalSabun	ApprovalName	ApprovalOrgID	ApprovalOrgNameKor	ApprovalOrgNameEng	
            //              ContactSabun	ContactName	ContactOrgID	ContactOrgNameKor	ContactOrgNameEng	CarryItemApplyStatus	CarryItemStatus	ApprovalOpinion	ApprovalDate	
            //              ImportTime	ExportTime	InsertVisitorType	InsertVisitPersonID
            // 휴대물품정보(arr): CarryItemInfoID CarryItemApplyID	CarryItemCodeID	ItemName	ItemSN	Quantity	Unit	ImportCnt	ExportCnt	UpdateDate	InsertDate	IsDelete
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("A") && CarryItemApplyData != null) // 휴대물품 신청 추가
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                // 1. 휴대물품신청 (CarryItemApply) 정보 입력 
                // VisitApplyPersonID = VisitApplyPersonID, // 1. 출입증이 있는 비상주사원 -> 빈값 2. 방문/공사/안전교육 신청(승인필수)된 방문자 -> 방문신청회원No 입력 
                
                carryItemApply.CarryItemApplyStatus = 0; //휴대물품신청상태 CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                carryItemApply.CarryItemStatus = 0; //휴대물품반입반출상태 CarryItemStatus : 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)

                carryItemApply.InsertSabun = my.Sabun;//등록자가 방문자가 아닐경우 회원정보 입력 
                carryItemApply.InsertName = my.Name;
                carryItemApply.InsertOrgID = my.OrgID;
                carryItemApply.InsertOrgNameKor = my.OrgNameKor;
                carryItemApply.InsertOrgNameEng = my.OrgNameEng;
                carryItemApply.InsertDate = today;

                CarryItemApplyData.Insert(carryItemApply);
                CarryItemApplyData.Save();
                WriteLog("carryItemApply: "+Dump(carryItemApply));
                if (CarryItemApplyHistoryData != null){
                    CarryItemApplyHistory carryItemApplyHistory = new()
                    {
                        CarryItemApplyID = carryItemApply.CarryItemApplyID,
                        // VisitApplyPersonID = carryItemApply.VisitApplyPersonID, // 1. 출입증이 있는 비상주사원 -> 빈값 2. 방문/공사/안전교육 신청(승인필수)된 방문자 -> 방문신청회원No 입력 
                        ImportDate = carryItemApply.ImportDate, //반입일
                        ExportDate = carryItemApply.ExportDate, //반출예정일
                        Location = carryItemApply.Location, //사업장구분
                        PlaceCodeID = carryItemApply.PlaceCodeID, //장소코드ID(반입장소, 공통코드)
                        ImportPurposeCodeID = carryItemApply.ImportPurposeCodeID, //반입목적코드ID(공통코드)
                        ImportReason = carryItemApply.ImportReason, //반입사유
                        ImportWayType = carryItemApply.ImportWayType, //반입자구분: 대행등록(0,임직원신청)/본인(1,방문신청) (공통코드)

                        CarryVisitorType = carryItemApply.CarryVisitorType, //방문자구분 (반입자)
                        VisitPersonID = carryItemApply.VisitPersonID, //방문객회원ID (반입자)
                        Sabun = carryItemApply.Sabun, //회원사번(반입자)
                        Name = carryItemApply.Name, //회원이름(반입자)
                        OrgID = carryItemApply.OrgID, //부서ID(반입자)
                        OrgNameKor = carryItemApply.OrgNameKor, //부서명Kor(반입자)
                        OrgNameEng = carryItemApply.OrgNameEng, //부서명Eng(반입자)
                        CompanyName = carryItemApply.CompanyName, //회사명(반입자)
                        Mobile = carryItemApply.Mobile, //휴대전화(반입자)

                        ApprovalSabun = carryItemApply.ApprovalSabun, //회원사번(결재자)
                        ApprovalName = carryItemApply.ApprovalName, //회원이름(결재자)
                        ApprovalOrgID = carryItemApply.ApprovalOrgID, //부서ID(결재자)
                        ApprovalOrgNameKor = carryItemApply.ApprovalOrgNameKor, //부서명Kor(결재자)
                        ApprovalOrgNameEng = carryItemApply.ApprovalOrgNameEng, //부서명Eng(결재자)

                        ContactSabun = carryItemApply.ContactSabun, //회원사번(담당자)
                        ContactName = carryItemApply.ContactName, //회원이름(담당자)
                        ContactOrgID = carryItemApply.ContactOrgID, //부서ID(담당자)
                        ContactOrgNameKor = carryItemApply.ContactOrgNameKor, //부서명Kor(담당자)
                        ContactOrgNameEng = carryItemApply.ContactOrgNameEng, //부서명Eng(담당자)
                        ContactMobile = carryItemApply.ContactMobile, //휴대전화(담당자)

                        CarryItemApplyStatus = carryItemApply.CarryItemApplyStatus, //휴대물품신청상태 CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                        CarryItemStatus = carryItemApply.CarryItemStatus, //휴대물품반입반출상태 CarryItemStatus : 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)
                        ApprovalOpinion = carryItemApply.ApprovalOpinion, //결재자 의견
                        // ApprovalDate = ApprovalDate, //결제일시
                        ImportTime = carryItemApply.ImportTime, //반입시간
                        ExportTime = carryItemApply.ExportTime, //반출시간

                        UpdateVisitorType = carryItemApply.InsertVisitorType, //방문자구분(등록/변경자)
                        UpdateVisitPersonID = carryItemApply.InsertVisitPersonID, //방문객회원ID(등록/변경자)

                        // Memo = Memo, // 비고
                        UpdateSabun = my.Sabun,//등록자가 방문자가 아닐경우 회원정보 입력 
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = today
                    };
                    CarryItemApplyHistoryData.Insert(carryItemApplyHistory);
                    CarryItemApplyHistoryData.Save();
                }

                //2. 휴대물품정보 입력 (arr)
                var m = CarryItems;
                if (m != null)
                {
                    WriteLog("visitDTO CarryItems:" + m);
                    var carryItemDTO = Des<CarryItemDTO>(m);
                    if (carryItemDTO != null)
                    {
                        // 반입자 정보
                        // carryItemDTO.SetCarryItemApply(carryItemApply.InsertVisitorType, carryItemApply.InsertVisitPersonID??0, carryItemApply.Sabun, carryItemApply.Name, carryItemApply.Mobile, carryItemApply.CompanyName);
                        WriteLog("visitDTO carryItemDTO:" + Dump(carryItemDTO.CarryItemInfoList));
                        if (CarryItemInfoData != null && carryItemDTO.CarryItemInfoList != null){
                            foreach(var m3 in carryItemDTO.CarryItemInfoList){
                                m3.CarryItemApplyID = carryItemApply.CarryItemApplyID;
                                m3.RemainCnt = m3.Quantity; // 남은 수량에 수량을 입력
                                m3.InsertDate = today;
                                CarryItemInfoData.Insert(m3);
                                WriteLog("CarryItemInfoData: "+Dump(m3));
                                CarryItemInfoData.Save();
                                var m4 = new CarryItemInfoHistory
                                {
                                    CarryItemInfoHistoryID = 0,
                                    CarryItemInfoID = m3.CarryItemInfoID,
                                    CarryItemApplyID = m3.CarryItemApplyID,
                                    CarryItemCodeID = m3.CarryItemCodeID,
                                    ItemName = m3.ItemName,
                                    ItemSN = m3.ItemSN,
                                    Quantity = m3.Quantity,
                                    Unit = m3.Unit,
                                    RemainCnt = m3.Quantity, // 남은 수량에 수량을 입력
                                    InsertUpdateDate = today,
                                };
                                CarryItemInfoHistoryData?.Insert(m4);
                                CarryItemInfoHistoryData?.Save();
                                /*2023.10.10 dsyoo 노트북 NAC연동은 처리하지 않는다.
                                if (DbMySQL != null && m3.CarryItemCodeID == 0) { // PC 노트북
                                    // 기본값이 0이며 노트북 휴대물품 반입 신청 승인이 완료되면 NAC로 데이터를 전송하고 통문관리에서는 해당 데이터를 삭제
                                    TUserAdd userAdd = new()
                                    {
                                        //ExportDate
                                        ReqTime = today,
                                        UserId = "",
                                        UserName = carryItemApply.Name??"",
                                        UserPassword = "",
                                        ExpireDate = (uint) Convert.ToDateTime(carryItemApply.ExportDate+" 23:59:59").GetUnixEpoch(),
                                        Position2 = "", // 소속정보: 회사명
                                        Rank = carryItemApply.GradeName, // 직책
                                        TelNum = carryItemApply.Tel, // 전화번호
                                        ErrorFlag = 0,
                                        CellPhone = carryItemApply.Mobile, // 핸드폰 번호
                                    };
                                    DbMySQL.TUserAdds.Add(userAdd);
                                    DbMySQL.SaveChanges();                                    
                                }
                                */
                            } // end foreach
                        } // end if CarryItemDTO.CarryItemInfoList != null
                    }
                }

                //PORTE ID 가 있는 사용자만 이용 가능
                if(!string.IsNullOrEmpty(my.PorteID)){
                    TempData["DATA.CarryItemApplyID"] = carryItemApply.CarryItemApplyID;
                }
            }
            // var m = DbMySQL.TUserAdds.ToList();
            // WriteLog("mysql: " + Dump(m));
            // return new EmptyResult();
            return RedirectToAction("List", new { culture=GetLang() });
        }

        /// <summary>
        /// 관리자 : 휴대물품 상세내영 확인
        /// 임직원 : 본인이 담당자인 휴대물품 반입처리(반출요/반출불필요(유상)/반출불필요(무상)), 반출할 수량입력(남은수량자동입력)
        /// 보안실 : 승인된 휴대물품 반입처리, 반출요청된 휴대물품 반출처리
        /// 반출요청시 담당자가 요청한 수량과 실수량 비교하여 반출처리함
        /// 비상주업체 : 미승인된 휴대물품 관련 반입 수량 변경가능
        /// </summary>
        /// <param name="values"></param>
        /// <param name="carryItemApplyID"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        public IActionResult Detail (CarryItemGridData values, int carryItemApplyID) 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // WriteLog("carryItemApplyID:" + carryItemApplyID);
            var my = GetMe();
            ViewBag.CarryItemApplyID = carryItemApplyID;
            ViewBag.IsReApproval = false;
            ViewBag.IsApprovalInfo = false;

            ViewBag.IsModApproval = IsMaster() || IsGeneralManager();// 마스터 가능. 나머지는 본인이 담당자일 경우만 가능.
            ViewBag.IsModStatus = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity(); 
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsSecurity = IsSecurity();
            ViewBag.IsGeneralManager = IsGeneralManager();
            ViewBag.my = my;
            CarryItemDetailViewModel vm = new();
            if (CarryItemApplyData != null && CarryItemInfoData != null && CarryItemApplyHistoryData != null){
                var carryItemApply = CarryItemApplyData.Get(new QueryOptions<CarryItemApply>{
                    Where = a => a.CarryItemApplyID == carryItemApplyID,
                }) ?? new CarryItemApply();


                if (!string.IsNullOrEmpty(carryItemApply.InsertSabun)){
                    //2023.09.15 dsyoo 상신전(신청) 상태이고 로그인한사람이 신청한사람이면 재상신할수 있다
                    if (carryItemApply.CarryItemApplyStatus==0
                        && my.Sabun.Equals(carryItemApply.InsertSabun)){
                        ViewBag.IsReApproval = true;
                    }
                }
                if (!string.IsNullOrEmpty(carryItemApply.ApprovalSabun)){
                    ViewBag.IsApprovalInfo = true;
                }
                var options = new QueryOptions<CarryItemInfo>
                {
                    Where = a => a.CarryItemApplyID == carryItemApplyID && a.IsDelete == "N",
                };

                var optionsHistory = new QueryOptions<CarryItemApplyHistory>
                {
                    Where = a => a.CarryItemApplyID == carryItemApplyID,
                };
                if(ViewBag.IsModApproval == false && (IsEmployeeOnly() || IsHR() || IsESH())){
                    if(carryItemApply.ContactSabun != null && carryItemApply.ContactSabun.Equals(my.Sabun)){
                            ViewBag.IsModApproval = true;
                    }
                }

                //2023.09.20 dsyoo 작성자가 포르테 사번이있는 임직원이면 전자결재건으로 진행
                if (string.IsNullOrEmpty(carryItemApply.InsertSabun)==false)
                { // PORTE ID 있음
                    ViewBag.IsElApproveItem = 1; //
                }
                else
                {
                    ViewBag.IsElApproveItem = 0;
                }

                // IEnumerable<CarryItemApplyHistory> carryItemApplyHistoryList = CarryItemApplyHistoryData.List(optionsHistory);
                // WriteLog("ExpireDate:"+carryItemApply.ExportDate+" / "+Convert.ToDateTime(carryItemApply.ExportDate)+" => "+Convert.ToDateTime(carryItemApply.ExportDate).GetUnixEpoch());
                vm = new(){
                    CarryItemApply = carryItemApply,
                    CarryItemInfos = CarryItemInfoData.List(options), 
                    CarryItemApplyHistoryList = CarryItemApplyHistoryData.List(optionsHistory),
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
                    CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                    // 반입장소 Place: 0 공통-통제구역-적색-클린룸, 1 공통-제한구역-황색-건물내부, 2 공통-일반구역-청색-건물외부/접견실/납품 ....... 
                    CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                    // 휴대물품 구분 CarryItem : 0 노트북, 1 PC, 2 카메라, 3 기타 
                    CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem), 
                    // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
                    CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),
                    // 휴대물품신청상태  CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                    CodeCarryItemApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemApplyStatus),  
                    // 휴대물품반입구분 CarryItemImportType 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
                    CodeCarryItemImportType = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemImportType),  
                    CodeCarryItemStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemStatus),
                };
            }
            return View(vm);
        }

        /// <summary>
        /// 관리자 : 휴대물품 상세내영 확인
        /// 임직원 : 본인이 담당자인 휴대물품 반입처리(반출요/반출불필요(유상)/반출불필요(무상)), 반출할 수량입력(남은수량자동입력)
        /// 보안실 : 승인된 휴대물품 반입처리, 반출요청된 휴대물품 반출처리
        /// 반출요청시 담당자가 요청한 수량과 실수량 비교하여 반출처리함
        /// 비상주업체 : 미승인된 휴대물품 관련 반입 수량 변경가능
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <param name="carryItemApplyID"></param>
        /// <param name="CarryItemApplyStatus"></param>
        /// <param name="Memo"></param>
        /// <param name="ImportCnt"></param>
        /// <param name="ExportCnt"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcarryitemapplystatus?}/{searchcarryitemstatus?}/{searchcarryitemimporttype?}/{searchname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcontactname?}")]
        // public IActionResult Detail(CarryItemGridData values, string mode, int carryItemApplyID, int CarryItemApplyStatus, String Memo,
        //     int ImportCnt, int ExportCnt)
        public IActionResult Detail(CarryItemGridData values, string mode, CarryItemApplyHistory CarryItemApplyHistory, string[] CarryItemInfoID
                                  , string[] ExportCnt, string[] RemainCnt
                                  , string[] hIsVaccineUpdated, string[] hIsVirusScanned
                                  , string[] hQuarantineConfirmationGate
                                  , string[] hQuarantineConfirmationContact
            )
        { 
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // WriteLog("mode: "+mode+", carryItemApplyID: "+carryItemApplyID+", CarryItemApplyStatus: "+CarryItemApplyStatus+", Memo: "+Memo);
            // WriteLog("mode: "+mode+", ImportCnt: "+ImportCnt+", ExportCnt: "+ExportCnt);
            WriteLog("CarryItemApplyHistory:" + Dump(CarryItemApplyHistory));
            WriteLog("CarryItemInfoID:" + Dump(CarryItemInfoID));
            WriteLog("ExportCnt:" + Dump(ExportCnt));
            WriteLog("RemainCnt:" + Dump(RemainCnt));
            WriteLog("IsVaccineUpdated:" + Dump(hIsVaccineUpdated));
            WriteLog("IsVirusScanned:" + Dump(hIsVirusScanned));
            WriteLog("QuarantineConfirmationGate:" + Dump(hQuarantineConfirmationGate));
            WriteLog("QuarantineConfirmationContact:" + Dump(hQuarantineConfirmationContact));
            // return new EmptyResult();

            if (mode.Equals("E")) // 휴대물품신청 상태 수정
            {
                PersonDTO my = GetMe();

                if(CarryItemApplyData != null && CarryItemApplyHistoryData != null && CarryItemInfoData != null && CarryItemApplyHistory.CarryItemApplyID > 0)
                {
                    // 1. 휴대물품신청 상태 수정 
                    var orgObj = GetCarryItemApply(CarryItemApplyHistory.CarryItemApplyID, true);
                    var newObj = orgObj.Clone();


                    newObj.CarryItemApplyStatus = CarryItemApplyHistory.CarryItemApplyStatus; // 휴대물품신청상태 CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                    // newObj.CarryItemImportType = CarryItemApplyHistory.CarryItemImportType;
                    newObj.CarryItemStatus = CarryItemApplyHistory.CarryItemStatus; // 휴대물품반입반출상태 CarryItemStatus : 신청(0), 반려(1), 반출대기(2), 반출확인(3), 정문반송(4), 반출완료(5), 반입대기(6), 반입확인(7), 반입완료(8)
                    if(CarryItemApplyHistory.CarryItemStatus == 1){// || CarryItemStatus == 2
                        newObj.EntryDate = DateTime.Now;
                        // TempData["PrintAsses"] = carryItemApplyID;
                    }else if(CarryItemApplyHistory.CarryItemStatus == 8){
                        newObj.ExitDate = DateTime.Now;
                    }

                    newObj.UpdateDate = DateTime.Now;
                    //2023.09.11 dsyoo 휴대물품 부분반출시 처리를 위해 상태정보를 맨 마지막에 업데이트한다
                    /*
                    CarryItemApplyData.Update(newObj);
                    CarryItemApplyData.Save();
                    */
                    // 2. 휴대물품 진행상태 비고와 함께 입력
                    // 비고는 CarryItemApplyHistory 만 에 입력 
                    CarryItemApplyHistory carryItemApplyHistory = new()
                    {
                        CarryItemApplyID = CarryItemApplyHistory.CarryItemApplyID,
                        // VisitApplyPersonID = orgObj.VisitApplyPersonID, // 1. 출입증이 있는 비상주사원 -> 빈값 2. 방문/공사/안전교육 신청(승인필수)된 방문자 -> 방문신청회원No 입력 
                        ImportDate = newObj.ImportDate, //반입일
                        ExportDate = newObj.ExportDate, //반출예정일
                        Location = newObj.Location, //사업장구분
                        PlaceCodeID = newObj.PlaceCodeID, //장소코드ID(반입장소, 공통코드)
                        ImportPurposeCodeID = newObj.ImportPurposeCodeID, //반입목적코드ID(공통코드)
                        ImportReason = newObj.ImportReason, //반입사유
                        ImportWayType = newObj.ImportWayType, //반입자구분: 대행등록(0,임직원신청)/본인(1,방문신청) (공통코드)
                        CarryVisitorType = newObj.CarryVisitorType, //방문자구분 (반입자)
                        VisitPersonID = newObj.VisitPersonID, //방문객회원ID (반입자)
                        Sabun = newObj.Sabun, //회원사번(반입자)
                        Name = newObj.Name, //회원이름(반입자)
                        OrgID = newObj.OrgID, //부서ID(반입자)
                        OrgNameKor = newObj.OrgNameKor, //부서명Kor(반입자)
                        OrgNameEng = newObj.OrgNameEng, //부서명Eng(반입자)
                        CompanyName = newObj.CompanyName, //회사명(반입자)
                        Mobile = newObj.Mobile, //휴대전화(반입자)
                        ApprovalSabun = newObj.ApprovalSabun, //회원사번(결재자)
                        ApprovalName = newObj.ApprovalName, //회원이름(결재자)
                        ApprovalOrgID = newObj.ApprovalOrgID, //부서ID(결재자)
                        ApprovalOrgNameKor = newObj.ApprovalOrgNameKor, //부서명Kor(결재자)
                        ApprovalOrgNameEng = newObj.ApprovalOrgNameEng, //부서명Eng(결재자)
                        ContactSabun = newObj.ContactSabun, //회원사번(담당자)
                        ContactName = newObj.ContactName, //회원이름(담당자)
                        ContactOrgID = newObj.ContactOrgID, //부서ID(담당자)
                        ContactOrgNameKor = newObj.ContactOrgNameKor, //부서명Kor(담당자)
                        ContactOrgNameEng = newObj.ContactOrgNameEng, //부서명Eng(담당자)
                        ContactMobile = newObj.ContactMobile, //휴대전화(담당자)
                        CarryItemApplyStatus = newObj.CarryItemApplyStatus, //휴대물품신청상태 CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                        CarryItemImportType = newObj.CarryItemImportType,
                        CarryItemStatus = CarryItemApplyHistory.CarryItemStatus, //휴대물품반입반출상태 CarryItemStatus : 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)
                        ApprovalOpinion = newObj.ApprovalOpinion, //결재자 의견
                        // ApprovalDate = ApprovalDate, //결제일시
                        ImportTime = newObj.ImportTime, //반입시간
                        ExportTime = newObj.ExportTime, //반출시간
                        UpdateVisitorType = newObj.InsertVisitorType, //방문자구분(등록/변경자)
                        UpdateVisitPersonID = newObj.InsertVisitPersonID, //방문객회원ID(등록/변경자)

                        Memo = CarryItemApplyHistory.Memo, // 비고
                        UpdateSabun = my.Sabun,//등록자가 방문자가 아닐경우 회원정보 입력 
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    CarryItemApplyHistoryData.Insert(carryItemApplyHistory);
                    CarryItemApplyHistoryData.Save();

                    // 3. 휴대물품들의 (arr) 반입수량, 반출수량 수정 (arr) 
                    var options = new QueryOptions<CarryItemInfo>
                    {
                        Where = a => a.CarryItemApplyID == CarryItemApplyHistory.CarryItemApplyID && a.IsDelete == "N",
                    };
                    bool isPartialExport = false; //2023.09.11 dsyoo 부분반출 체크 플래그
                    bool isExport = newObj.CarryItemStatus switch //반출불필요, 선입고도 반출완료와 같이 부분반출관련작업을한다.
                    {
                        //반출완료(8), 반출불필요(유상)(4), 반출불필요(무상)(5), 선입고(6)
                        4 or 5 or 6 or 8 => true,
                        _ => false
                    };
                    // 휴대물품 상세 정보 수정
                    if (ExportCnt != null && ExportCnt.Length > 0) {
                        //휴대물품 리스트 가져오기
                        IEnumerable<CarryItemInfo> carryItemInfos = CarryItemInfoData.List(options);
                        WriteLog("carryItemInfos" + Dump(carryItemInfos));
                        int i = 0;
                        foreach(var m in carryItemInfos){
                            if (i < ExportCnt.Length){
                                m.ExportCnt = string.IsNullOrEmpty(ExportCnt[i]) ? 0: int.Parse(ExportCnt[i]);
                                m.RemainCnt = string.IsNullOrEmpty(RemainCnt[i]) ? 0: int.Parse(RemainCnt[i]);
                                //2023.09.11 dsyoo 남은수량계산하기
                                //--------------------------------------------------------------
                                if (isExport)
                                {
                                    m.RemainCnt = m.RemainCnt - m.ExportCnt;
                                    m.ExportCnt = 0;
                                }
                                //--------------------------------------------------------------
                                m.IsVaccineUpdated = hIsVaccineUpdated[i] == "Y" ? "Y" : "N";
                                m.IsVirusScanned = hIsVirusScanned[i] == "Y" ? "Y" : "N";
                                m.QuarantineConfirmationGate = hQuarantineConfirmationGate[i] == "Y" ? "Y" : "N";
                                m.QuarantineConfirmationContact = hQuarantineConfirmationContact[i] == "Y" ? "Y" : "N";
                                CarryItemInfoData.Update(m);
                                CarryItemInfoData.Save();
                                //2023.09.11 dsyoo 남은수량이 있으면 부분반출임
                                if (m.RemainCnt > 0)
                                {
                                    isPartialExport = true;
                                }
                            }
                            i++;
                        }
                    }

                    //2023.09.11 dsyoo 휴대물품 부분반출시 처리를 위해 상태정보를 맨 마지막에 업데이트한다
                    if(isExport==true && isPartialExport==true) //보안실에서 반출처리시 부분반출(isPartialExport==true)인지 체크한다.
                    {
                        newObj.CarryItemStatus = 2; //확인대기(2)
                    }
                    CarryItemApplyData.Update(newObj);
                    CarryItemApplyData.Save();
                    
                    // var orgObj = GetCarryItemList(carryItemApplyID, true);
                    // var newObj = orgObj.Clone();
                    // newObj.ImportCnt = ImportCnt;
                    // newObj.ExportCnt = ExportCnt;
                    // carryItemInfoData.Update(newObj);
                    // carryItemInfoData.Save();
                }
            }
            return RedirectToAction("Detail", new { CarryItemApplyHistory.CarryItemApplyID, culture=GetLang()});
        }

        public IActionResult PersonalAsses(int carryItemApplyID){
            if(IsAccessAPI() == false){
                return default;
            }
            CarryItemDetailViewModel vm = new();
            if (CarryItemApplyData != null && CarryItemInfoData != null && CarryItemApplyHistoryData != null){
                var carryItemApply = CarryItemApplyData.Get(new QueryOptions<CarryItemApply>{
                    Where = a => a.CarryItemApplyID == carryItemApplyID,
                }) ?? new CarryItemApply();

                var options = new QueryOptions<CarryItemInfo>
                {
                    Where = a => a.CarryItemApplyID == carryItemApplyID && a.IsDelete == "N",
                };

                var optionsHistory = new QueryOptions<CarryItemApplyHistory>
                {
                    Where = a => a.CarryItemApplyID == carryItemApplyID,
                };
                // IEnumerable<CarryItemApplyHistory> carryItemApplyHistoryList = CarryItemApplyHistoryData.List(optionsHistory);
                // WriteLog("ExpireDate:"+carryItemApply.ExportDate+" / "+Convert.ToDateTime(carryItemApply.ExportDate)+" => "+Convert.ToDateTime(carryItemApply.ExportDate).GetUnixEpoch());
                vm = new(){
                    CarryItemApply = carryItemApply,
                    CarryItemInfos = CarryItemInfoData.List(options), 
                    CarryItemApplyHistoryList = CarryItemApplyHistoryData.List(optionsHistory),
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
                    CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                    // 반입장소 Place: 0 공통-통제구역-적색-클린룸, 1 공통-제한구역-황색-건물내부, 2 공통-일반구역-청색-건물외부/접견실/납품 ....... 
                    CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                    // 휴대물품 구분 CarryItem : 0 노트북, 1 PC, 2 카메라, 3 기타 
                    CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem), 
                    // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
                    CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),
                    // 휴대물품신청상태  CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                    CodeCarryItemApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemApplyStatus),  
                    // 휴대물품반입구분 CarryItemImportType 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
                    CodeCarryItemImportType = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemImportType),  
                };
                SaveCarryPrintHistory(carryItemApplyID); //2023.09.25 dsyoo 지입증 출력이력 저장
            }
            return View(vm);
        }
        #region SaveCarryPrintHistory  2023.09.21 휴대물품 승인상태정보를 저장한다.
        private void SaveCarryPrintHistory(int CarryItemApplyID)
        {
            //2023.09.22 dsyoo 휴대물품 전자결재 정보 저장하기
            var newObj = CarryItemApplyData.Get(new QueryOptions<CarryItemApply>
            {
                Where = a => a.CarryItemApplyID == CarryItemApplyID,
            }) ?? new CarryItemApply();

            // 비고는 CarryItemApplyHistory 만 에 입력 
            CarryItemApplyHistory carryItemApplyHistory = new()
            {
                CarryItemApplyID = newObj.CarryItemApplyID,
                // VisitApplyPersonID = orgObj.VisitApplyPersonID, // 1. 출입증이 있는 비상주사원 -> 빈값 2. 방문/공사/안전교육 신청(승인필수)된 방문자 -> 방문신청회원No 입력 
                ImportDate = newObj.ImportDate, //반입일
                ExportDate = newObj.ExportDate, //반출예정일
                Location = newObj.Location, //사업장구분
                PlaceCodeID = newObj.PlaceCodeID, //장소코드ID(반입장소, 공통코드)
                ImportPurposeCodeID = newObj.ImportPurposeCodeID, //반입목적코드ID(공통코드)
                ImportReason = newObj.ImportReason, //반입사유
                ImportWayType = newObj.ImportWayType, //반입자구분: 대행등록(0,임직원신청)/본인(1,방문신청) (공통코드)
                CarryVisitorType = newObj.CarryVisitorType, //방문자구분 (반입자)
                VisitPersonID = newObj.VisitPersonID, //방문객회원ID (반입자)
                Sabun = newObj.Sabun, //회원사번(반입자)
                Name = newObj.Name, //회원이름(반입자)
                OrgID = newObj.OrgID, //부서ID(반입자)
                OrgNameKor = newObj.OrgNameKor, //부서명Kor(반입자)
                OrgNameEng = newObj.OrgNameEng, //부서명Eng(반입자)
                CompanyName = newObj.CompanyName, //회사명(반입자)
                Mobile = newObj.Mobile, //휴대전화(반입자)
                ApprovalSabun = newObj.ApprovalSabun, //회원사번(결재자)
                ApprovalName = newObj.ApprovalName, //회원이름(결재자)
                ApprovalOrgID = newObj.ApprovalOrgID, //부서ID(결재자)
                ApprovalOrgNameKor = newObj.ApprovalOrgNameKor, //부서명Kor(결재자)
                ApprovalOrgNameEng = newObj.ApprovalOrgNameEng, //부서명Eng(결재자)
                ContactSabun = newObj.ContactSabun, //회원사번(담당자)
                ContactName = newObj.ContactName, //회원이름(담당자)
                ContactOrgID = newObj.ContactOrgID, //부서ID(담당자)
                ContactOrgNameKor = newObj.ContactOrgNameKor, //부서명Kor(담당자)
                ContactOrgNameEng = newObj.ContactOrgNameEng, //부서명Eng(담당자)
                ContactMobile = newObj.ContactMobile, //휴대전화(담당자)
                CarryItemApplyStatus = newObj.CarryItemApplyStatus, //휴대물품신청상태 CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                CarryItemImportType = newObj.CarryItemImportType,
                CarryItemStatus = newObj.CarryItemStatus, //휴대물품반입반출상태 CarryItemStatus : 반입대기(0), 반출대기(1), 확인대기(2), 반출완료(3), 미반입(4), 미반출(5), 반입완료(6), 확인완료(7)
                ApprovalOpinion = newObj.ApprovalOpinion, //결재자 의견
                                                          // ApprovalDate = ApprovalDate, //결제일시
                ImportTime = newObj.ImportTime, //반입시간
                ExportTime = newObj.ExportTime, //반출시간
                UpdateVisitorType = newObj.InsertVisitorType, //방문자구분(등록/변경자)
                UpdateVisitPersonID = newObj.InsertVisitPersonID, //방문객회원ID(등록/변경자)


                Memo = "휴대물품 지입증 재발급", // 비고
                UpdateSabun = GetMe().Sabun,//등록자가 방문자가 아닐경우 회원정보 입력 
                UpdateName = GetMe().Name,
                UpdateOrgID = GetMe().OrgNameKor,
                InsertUpdateDate = DateTime.Now
            };
            CarryItemApplyHistoryData.Insert(carryItemApplyHistory);
            CarryItemApplyHistoryData.Save();

        }
        #endregion

        private CarryItemApply GetCarryItemApply(int carryItemApplyID=-1, bool isNoTracking = false)
        {
            CarryItemApply carryItemApply = null;
            if(carryItemApplyID > 0) {
                var options = new QueryOptions<CarryItemApply>
                {
                    Where = a => a.CarryItemApplyID == carryItemApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    carryItemApply = CarryItemApplyData.GetNT(carryItemApplyID);
                } else {
                    carryItemApply = CarryItemApplyData.Get(carryItemApplyID);
                }
            }
            return carryItemApply;
        }

        private List<CarryItemApply>? GetCarryItemApplyList(CarryItemGridData filterhValue){
            if (CarryItemApplyData == null){
                return default;
            }
            var options = new QueryOptions<CarryItemApply>
            {
                PageNumber = filterhValue.PageNumber,
                PageSize = filterhValue.PageSize,
                OrderByDirection = filterhValue.SortDirection,
                OrderBy = x => x.CarryItemApplyID
            };
            options.MultipleWhere.Add(a => a.IsDelete == "N");
            if (IsMaster() == false)
            {
                var my = GetMe();
                if (IsGeneralManager() || IsSecurity())
                {
                    options.MultipleWhere.Add(x => x.Location == my.Location);
                } 
                else if(IsEmployee())  
                {
                    options.MultipleWhere.Add(x => x.ContactOrgID == my.SapDeptCode); //2023.10.23 dsyoo 접견인비교에서 접견인 조직비교로 변경. 접견인 조직에서 대리로 승인처리등을 할수 있다.
                    //    options.MultipleWhere.Add(x => x.ContactSabun == my.Sabun);
                }
                else if (IsPartnerNonresidentManager()){
                    options.MultipleWhere.Add(x => x.InsertSabun == my.Sabun); // 비상주업체 관리자 신청건
                    // 외부에서 신청건에 대해서 회사명 LIKE 검색으로 처리할 지는 논의 중.
                }
            }
            if (filterhValue.SearchCarryItemApplyStatus > -1){
                options.MultipleWhere.Add(a => a.CarryItemApplyStatus == filterhValue.SearchCarryItemApplyStatus);
            }
            if (filterhValue.SearchCarryItemStatus > -1){
                options.MultipleWhere.Add(a => a.CarryItemStatus == filterhValue.SearchCarryItemStatus);
            }
            if (filterhValue.SearchCarryItemImportType > -1){
                options.MultipleWhere.Add(a => a.CarryItemImportType == filterhValue.SearchCarryItemImportType);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchName))
            {
                options.MultipleWhere.Add(a => EF.Functions.Like(a.Name, "%"+filterhValue.SearchName+"%"));
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchContactName))
            {
                options.MultipleWhere.Add(a => EF.Functions.Like(a.ContactName, "%"+filterhValue.SearchContactName+"%"));
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchStartInsertDate))
            {
                    string d = filterhValue.SearchStartInsertDate + " 00:00:00";
                try{
                    options.MultipleWhere.Add(a => a.InsertDate >= Convert.ToDateTime(d));
                }catch(FormatException e){
                    WriteError(e.ToString());
                }
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchEndInsertDate))
            {
                try{
                    string d = filterhValue.SearchEndInsertDate + " 23:59:59";
                    options.MultipleWhere.Add(a => a.InsertDate <= Convert.ToDateTime(d));
                }catch(FormatException e){
                    WriteError(e.ToString());
                }
            }
            return (List<CarryItemApply>)CarryItemApplyData.List(options);
        }
    } // end class
} // end namespace
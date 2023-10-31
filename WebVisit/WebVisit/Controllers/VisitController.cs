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
using WebVisit.Models.DomainModels.MySQL;
using System.Text;
using System.Runtime.CompilerServices;
using WebVisit.Models.ViewModels;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class VisitController : BaseController
    {
        private Repository<VisitPerson>? VisitPersonData { get; set; }
        private Repository<VisitApply>? VisitApplyData { get; set; }
        private Repository<VisitApplyHistory>? VisitApplyHistoryData { get; set; }
        private Repository<VisitApplyPerson>? VisitApplyPersonData { get; set; }
        private Repository<VisitApplyPersonHistory>? VisitApplyPersonHistoryData { get; set; }
        private Repository<BlackList>? BlackListData { get; set; }

        private Repository<CarryItemApply>? CarryItemApplyData { get; set; }
        private Repository<CarryItemApplyHistory>? CarryItemApplyHistoryData { get; set; }
        private Repository<CarryItemInfo>? CarryItemInfoData { get; set; }
        private Repository<CarryItemInfoHistory>? CarryItemInfoHistoryData { get; set; }        

        // 공사 관련 테이블
        private Repository<WorkVisitApply>? WorkVisitApplyData { get; set; }
        private Repository<WorkVisitApplyHistory>? WorkVisitApplyHistoryData { get; set; }
        private Repository<WorkVisitPersonApply>? WorkVisitPersonApplyData { get; set; }
        private Repository<WorkVisitPersonApplyHistory>? WorkVisitPersonApplyHistoryData { get; set; }
        private Repository<SafetyEdu>? SafetyEduData { get; set; }
        private Repository<SafetyEduHistory>? SafetyEduHistoryData { get; set; }

        public enum VisitApplyListType
        {
            General     = 0,    // 방문신청
            Work        = 1,    // 공사출입인원신청
            SafetyEdu   = 2,    // 안전교육
            Manual      = 3,    // 방문객수기등록
        }
            // Delivery    = 4,    // 택배 deprecated

        public VisitController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<VisitController> localizer):base(logger, configuration, localizer){}

        protected override void Init(){
            if (DbSVISIT != null) {
                VisitPersonData ??= new Repository<VisitPerson>(DbSVISIT);
                VisitApplyData ??= new Repository<VisitApply>(DbSVISIT);
                VisitApplyHistoryData ??= new Repository<VisitApplyHistory>(DbSVISIT);
                VisitApplyPersonData ??= new Repository<VisitApplyPerson>(DbSVISIT);
                VisitApplyPersonHistoryData ??= new Repository<VisitApplyPersonHistory>(DbSVISIT);
                BlackListData ??= new Repository<BlackList>(DbSVISIT);

                CarryItemApplyData ??= new Repository<CarryItemApply>(DbSVISIT);
                CarryItemApplyHistoryData ??= new Repository<CarryItemApplyHistory>(DbSVISIT);
                CarryItemInfoData ??= new Repository<CarryItemInfo>(DbSVISIT);
                CarryItemInfoHistoryData ??= new Repository<CarryItemInfoHistory>(DbSVISIT);
  
                WorkVisitApplyData ??= new Repository<WorkVisitApply>(DbSVISIT);
                WorkVisitApplyHistoryData ??= new Repository<WorkVisitApplyHistory>(DbSVISIT);
                WorkVisitPersonApplyData ??= new Repository<WorkVisitPersonApply>(DbSVISIT);
                WorkVisitPersonApplyHistoryData ??= new Repository<WorkVisitPersonApplyHistory>(DbSVISIT);
                SafetyEduData ??= new Repository<SafetyEdu>(DbSVISIT);
                SafetyEduHistoryData ??= new Repository<SafetyEduHistory>(DbSVISIT);

                CommonCodeData ??= new Repository<CommonCode>(DbSVISIT);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            return RedirectToAction("List");
        }

        /// <summary>
        /// 관리자 : 방문객정보 조회/출입증교부 및 회수/방문상태변경
        /// 일반관리자는 자신의 사업장만 처리가능
        /// 임직원 : 자신이 접견인인 방문 정보만 조회
        /// 보안실 : 해당 사업장 방문객 정보 조회/출입증교부 및 회수/방문상태변경
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        // PageNumber=2&PageSize=10&SortField=name&SortDirection=desc&SearchVisitApplyType=-1&SearchVisitApplyStatus=-1&SearchVisitStatus=-1&SearchStartInsertDate=2023-07-11
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult List(VisitGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            VisitGridData filterhValue = (VisitGridData)FilterGridData(values);
            WriteLog("VisitGridData:" + Dump(filterhValue));       
            var m = GetVisitApplyList(VisitApplyListType.General, filterhValue);
            WriteLog("GetVisitApplyList:"+ m.a.Count +" / "+Dump(m));
            ViewBag.visitApplyPersonList = m.a;
            ViewBag.IsEditable = SelectItems.IsSecurityManager(my.LevelCodeID); //IsMaster() || IsSecurity();
            ViewBag.IsSecurity = IsSecurity();
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsApproval = IsMaster() || IsEmployeeOnly() || IsGeneralManager() || IsHR() || IsESH();
            //            ViewBag.IsModCardNo = IsMaster() || IsSecurity(); // 출입증 번호 수정
            //2023.09.13 dsyoo 카드번호 설정 권한을 IsSecurityManager로 확장
            ViewBag.IsModCardNo = SelectItems.IsSecurityManager(my.LevelCodeID); //IsMaster() || IsSecurity(); // 출입증 번호 수정
            ViewBag.IsDownloadable = IsMaster() || IsGeneralManager() || IsSecurity() || IsPartnerNonresidentManager();
            ViewBag.Registable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            ViewBag.Readable = !IsDietitian();
            ViewBag.my = my;  //2023.09.13 dsyoo

            // WriteLog("permission:" + IsMaster() + IsEmployeeOnly() + IsGeneralManager() + IsHR() + IsESH()+":"+my.LevelCodeID);
            VisitListViewModel vm =new(){
                CodeVisitApplyType = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyType),
                CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus),
                CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                CurrentRoute = values,
                SearchRoute=filterhValue,
                TotalPages = values.GetTotalPages(m.b),
                TotalCnt = m.b,
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public async Task<IActionResult> List (VisitGridData values, int visitApplyID, int VisitApplyStatus, int visitApplyPersonID, int VisitStatus, String CardNo, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mode: "+mode+", visitApplyID: "+visitApplyID+", VisitApplyStatus: "+VisitApplyStatus+", visitApplyPersonID: "+visitApplyPersonID+", VisitStatus: "+VisitStatus+", CardNo: "+CardNo);
            PersonDTO my = GetMe();
            var today = DateTime.Now;
            if(mode == "EVisitApplyStatus"){
                if(visitApplyID > 0)
                {
                    var orgObj = GetVisitApply(visitApplyID, true);
                    if (orgObj != null && VisitApplyPersonData != null){
                        var newObj = orgObj.Clone();
                        // VisitApplyStatus 방문신청상태: 승인대기(0)/승인완료(1)/반려(2) 
                        newObj.VisitApplyStatus = VisitApplyStatus;
                        newObj.UpdateDate = DateTime.Now;
                        VisitApplyData?.Update(newObj);
                        VisitApplyData?.Save();
                        //방문자들의 방문신청상태(VisitApplyStatus) 수정 추가 
                        var options = new QueryOptions<VisitApplyPerson>
                        {
                            Where = a =>a.VisitApplyID == orgObj.VisitApplyID && a.IsDelete == "N",
                        };
                        List<VisitApplyPerson> v= (List<VisitApplyPerson>) VisitApplyPersonData.List(options);
                        foreach(var m in v) {
                            m.VisitApplyStatus = VisitApplyStatus;
                            VisitApplyPersonData?.Update(m);
                            VisitApplyPersonData?.Save();

                            if (VisitApplyStatus == 1)
                            {
                                // 승인 완료 시 SMS 발송
                                if (m.Mobile != null)
                                {
                                    await SendMessageAsync("SMS", 2, "032-680-4700", "출입관리시스템", m.Mobile, m.Name, "", orgObj.Location);
                                }
                            } else if(VisitApplyStatus == 2){
                                // 반려
                                if (m.Mobile != null)
                                {
                                    await SendMessageAsync("SMS", 3, "032-680-4700", "출입관리시스템", m.Mobile, m.Name, "", orgObj.Location);
                                }
                            }

                        }
                        
                        // VisitApplyType - 0: 방문신청, 1: 공사출입 인원, 2: 안전교육
                        if (orgObj.VisitApplyType == 1 && WorkVisitApplyData != null && WorkVisitPersonApplyData != null){ // 공사출입신청
                            // 공사출입신청
                            var options2 = new QueryOptions<WorkVisitApply>
                            {
                                Where = a =>a.WorkVisitApplyID == orgObj.WorkVisitApplyID && a.IsDelete == "N",
                            };
                            WorkVisitApply v2= WorkVisitApplyData.Get(options2);
                            v2.WorkVisitApplyStatus = VisitApplyStatus;
                            WorkVisitApplyData?.Update(v2);
                            WorkVisitApplyData?.Save();
                            WorkVisitApplyHistory workVisitApplyHistory = new()
                            {
                                WorkVisitApplyID = v2.WorkVisitApplyID, //공사출입신청ID
                                WorkApplyID = v2.WorkApplyID, //공사신청ID

                                WorkDate = v2.WorkDate, //공사일
                                WorkVisitApplyStatus = VisitApplyStatus, // 수정시 공사출입신청상태 값 등록 

                                UpdateSabun = my.Sabun,//등록자
                                UpdateName = my.Name,
                                UpdateOrgID = my.OrgID,
                                UpdateOrgNameKor = my.OrgNameKor,
                                UpdateOrgNameEng = my.OrgNameEng,
                                InsertUpdateDate = today
                            };
                            WorkVisitApplyHistoryData.Insert(workVisitApplyHistory);
                            WorkVisitApplyHistoryData.Save();

                            // 공사출입 인원 신청 WorkVisitApplyData WorkVisitPersonApplyData
                            var options3 = new QueryOptions<WorkVisitPersonApply>
                            {
                                Where = a =>a.WorkVisitApplyID == orgObj.WorkVisitApplyID && a.IsDelete == "N",
                            };
                            List<WorkVisitPersonApply> v3= (List<WorkVisitPersonApply>) WorkVisitPersonApplyData.List(options3);
                            foreach(var m in v3) {
                                m.WorkVisitApplyStatus = VisitApplyStatus;
                                WorkVisitPersonApplyData?.Update(m);
                                WorkVisitPersonApplyData?.Save();

                                // 2.1 공사출입인원신청히스토리 (WorkVisitPersonApplyHistory) 입력 
                                WorkVisitPersonApplyHistory workVisitPersonApplyHistory = new() {
                                    WorkVisitPersonApplyID = m.WorkVisitPersonApplyID,
                                    WorkVisitApplyID = m.WorkVisitApplyID,
                                    WorkVisitApplyStatus = VisitApplyStatus,
                                    Sabun = m.Sabun,
                                    Name = m.Name,
                                    OrgID = m.OrgID,
                                    OrgNameKor = m.OrgNameKor,
                                    OrgNameEng = m.OrgNameEng,
                                    InsertUpdateDate = today
                                };
                                WorkVisitPersonApplyHistoryData.Insert(workVisitPersonApplyHistory);
                                WorkVisitPersonApplyHistoryData?.Save();
                            }
                        }else if (orgObj.VisitApplyType == 2 && SafetyEduData != null){ 
                            // 안전교육 인원 신청 SafetyEduData
                            var options2 = new QueryOptions<SafetyEdu>
                            {
                                Where = a =>a.SafetyEduID == orgObj.SafetyEduID && a.IsDelete == "N",
                            };
                            SafetyEdu v2= SafetyEduData.Get(options2);
                            v2.EduApplyStatus = VisitApplyStatus;
                            SafetyEduData?.Update(v2);
                            SafetyEduData?.Save();

                            SafetyEduHistory safetyEduHistory = new() {
                                SafetyEduID = v2.SafetyEduID,
                                EduDate = v2.EduDate,
                                WorkApplyID = v2.WorkApplyID,
                                EduApplyStatus = v2.EduApplyStatus,
                                UpdateSabun = my.Sabun,//등록자
                                UpdateName = my.Name,
                                UpdateOrgID = my.OrgID,
                                UpdateOrgNameKor = my.OrgNameKor,
                                UpdateOrgNameEng = my.OrgNameEng,
                                InsertUpdateDate = today
                            };
                            SafetyEduHistoryData.Insert(safetyEduHistory);
                            SafetyEduHistoryData?.Save();
                        }// end if - VisitApplyType
                    }// end if - obj null
                }// end if - visitApplyID > 0
            }
            if(mode == "EVisitStatus"){
                if(visitApplyPersonID > 0)
                {
                    var orgObj2 = GetVisitApplyPerson(visitApplyPersonID, true);
                    var newObj2 = orgObj2.Clone();
                    newObj2.VisitStatus = VisitStatus;
                    // VisitStatus 방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                    string RStatus = string.Empty;
                    if(VisitStatus == 1){
                        newObj2.EntryDate = today;
                        RStatus = "R";
                    }else if(VisitStatus == 2){// || VisitStatus == 4
                        newObj2.ExitDate = today;
                        RStatus = "N";
                    }
                    newObj2.CardNo = CardNo;
                    newObj2.UpdateDate = today;
                    VisitApplyPersonData?.Update(newObj2);
                    VisitApplyPersonData?.Save();

                    if (DbPASSPORT != null && !string.IsNullOrEmpty(RStatus)){
                        string placeName = string.Empty;
                        string locationName = string.Empty;
                        if(VisitApplyData != null){
                            var visitApply = VisitApplyData.Get(newObj2.VisitApplyID);
                            if(visitApply != null){
                                List<CommonCode> CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place);
                                foreach(var m in CodePlace){
                                    if(m.SubCodeID == visitApply.PlaceCodeID){
                                        placeName = m.CodeNameKor;
                                        break;
                                    }
                                }

                                List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                                foreach(var m in CodeLocation){
                                    if(m.SubCodeDesc != null && m.SubCodeDesc.Equals(visitApply.Location)){
                                        locationName = m.CodeNameKor;
                                        break;
                                    }
                                }
                            }
                        }
                        
                        StringBuilder sb = new();
                        sb.Append("PROCI_CARD_VISITOR @PageType='U', @RStatus='");
                        sb.Append(RStatus);
                        sb.Append("' , @VCardNo='");
                        sb.Append(CardNo);
                        sb.Append("' , @VisitorName='");
                        sb.Append(newObj2.Name);
                        sb.Append("', @Mobile='"); 
                        sb.Append(newObj2.Mobile);
                        sb.Append("', @UpdateSabun='"); 
                        sb.Append(my.Sabun);
                        sb.Append("', @ValidDate='"); 
                        sb.Append(today.ToString("yyyy-MM-dd "));
                        sb.Append("23:59:59");
                        sb.Append("', @IP='"); 
                        sb.Append(GetUserIP());
                        sb.Append("', @VisitArea='"); 
                        sb.Append(placeName);
                        sb.Append("', @Location='"); 
                        sb.Append(locationName);
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

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult? ExcelManualDownload(VisitGridData values, string mode = "G", string ExTitle = "DBHiTek")
        {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            // 회사	방문일	입문 시간	출문 시간	회사명	성명	방문목적	근무자	방문상태	출입증번호
            // dt.Columns.AddRange(new DataColumn[13] { new DataColumn("사업장"), new DataColumn("승인구분"), new DataColumn("방문일"),
            //                             new DataColumn("입문"), new DataColumn("출문"), new DataColumn("회사명"),
            //                             new DataColumn("성명"),new DataColumn("방문목적"),new DataColumn("담당자명"),
            //                             new DataColumn("안전교육"),new DataColumn("차량번호"),new DataColumn("방문상태"),new DataColumn("출입증번호"), });
            // new DataColumn(@Localizer["Approval Classify"]), new DataColumn(@Localizer["Safety Education"]),new DataColumn(@Localizer["Car No"]),
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Visit Date"]), 
                                        new DataColumn(@Localizer["Come In Time"]), new DataColumn(@Localizer["Go Out Time"]), 
                                        new DataColumn(@Localizer["Company Name"]), new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Visit Purpose"]),
                                        new DataColumn(@Localizer["Contact Name"]), new DataColumn(@Localizer["Visit Status"]), new DataColumn(@Localizer["Access CardNo"]),});
                                                    
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            VisitGridData filterhValue = (VisitGridData) FilterGridData(values);
            WriteLog("VisitGridData:"+values.SearchCardNo+" /"+Dump(filterhValue));
            var v = GetVisitApplyList(VisitApplyListType.Manual, filterhValue, true);

            List< CommonCode > CodeVisitApplyType = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyType);
            List< CommonCode > CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus);
            List< CommonCode > CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus);
            List< CommonCode > CodeVisitManualPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitManualPurpose);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            // List<CommonCode> CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus);
            if(v.a != null){
                // 사업장	방문일	입문 시간	출문 시간	회사명	성명	방문목적	근무자	방문상태	출입증번호            
                string location = string.Empty;
                string visitApplyStatusName = string.Empty;
                string visitPurposeCodeName = string.Empty;
                string InsertName = string.Empty;
                string strVisitStatus = string.Empty;
                foreach(var m in v.a){
                    location = m.b.Location;
                    visitApplyStatusName = "";
                    visitPurposeCodeName = "";
                    InsertName = m.b.InsertName??"";
                    strVisitStatus = "";

                    var visitApplyStatus = m.b.VisitApplyStatus??-1;
                    var visitManualPurposeCodeID = m.b.VisitManualPurposeCodeID??-1;
    
                    if (location != null && CodeLocation != null) {
                        foreach(var a in CodeLocation) {
                            if (a.SubCodeDesc != null && a.SubCodeDesc.Equals(location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = a.CodeNameKor;
                                }else {
                                    location = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    }
                    if (visitApplyStatus >= 0 && CodeVisitApplyStatus != null) {
                        foreach(var a in CodeVisitApplyStatus) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(visitApplyStatus)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    visitApplyStatusName = a.CodeNameKor;
                                }else {
                                    visitApplyStatusName = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    } 
                    if (visitManualPurposeCodeID >= 0 && CodeVisitManualPurpose != null) {
                        foreach(var a in CodeVisitManualPurpose) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(visitManualPurposeCodeID)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    visitPurposeCodeName = a.CodeNameKor;
                                }else {
                                    visitPurposeCodeName = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    }
                    if (m.a.VisitStatus>=0 && CodeVisitStatus!=null){
                        foreach(var a in CodeVisitStatus){
                            if (a.SubCodeID != null && a.SubCodeID.Equals(m.a.VisitStatus)){
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    strVisitStatus = a.CodeNameKor;
                                }else {
                                    strVisitStatus = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    }
                    // 사업장	방문일	입문 시간	출문 시간	회사명	성명	방문목적	근무자	방문상태	출입증번호
                    // visitApplyStatusName, m.a.IsSafetyEdu??"N", m.a.CarNo, 
                    
                    dt.Rows.Add(location, m.a.VisitDate, m.a.EntryDate, m.a.ExitDate, m.a.CompanyName, m.a.Name, visitPurposeCodeName, InsertName, strVisitStatus, m.a.CardNo);
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

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult? ExcelDownload(VisitGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            //2023-08-11 안전교육, 차량번호 삭제 
            //new DataColumn(@Localizer["Safety Education"]), new DataColumn(@Localizer["Car No"]), 
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Visit Date"]),
                                        new DataColumn(@Localizer["Come In Time"]), new DataColumn(@Localizer["Go Out Time"]), new DataColumn(@Localizer["Company Name"]),
                                        new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Visit Purpose"]), new DataColumn(@Localizer["Contact Name"]),
                                        new DataColumn(@Localizer["Approval Status"]), new DataColumn(@Localizer["Visit Status"]), 
                                        new DataColumn(@Localizer["Access CardNo"]), });
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            VisitGridData filterhValue = (VisitGridData) FilterGridData(values);
            WriteLog("VisitGridData:"+values.SearchCardNo+" /"+Dump(filterhValue));
            var v = GetVisitApplyList(VisitApplyListType.General, filterhValue, true);

            List< CommonCode > CodeVisitApplyType = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyType);
            List< CommonCode > CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus);
            List< CommonCode > CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus);
            List< CommonCode > CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            // List<CommonCode> CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus);
            if(v.a != null){
                // 사업장	승인구분	방문일	입문	출문	회사명	성명	방문목적	담당자명	안전교육	차량번호	방문상태	출입증번호
                string location = string.Empty;
                string visitApplyStatusName = string.Empty;
                string visitPurposeCodeName = string.Empty;
                string ContactName = string.Empty;
                string strVisitStatus = string.Empty;
                foreach(var m in v.a){
                    location = m.b.Location;
                    visitApplyStatusName = "";
                    visitPurposeCodeName = "";
                    ContactName = m.b.ContactName??"";
                    strVisitStatus = "";

                    var visitApplyStatus = m.b.VisitApplyStatus??-1;
                    var visitPurposeCodeID = m.b.VisitPurposeCodeID??-1;
    
                    if (location != null && CodeLocation != null) {
                        foreach(var a in CodeLocation) {
                            if (a.SubCodeDesc != null && a.SubCodeDesc.Equals(location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = a.CodeNameKor;
                                }else {
                                    location = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    }
                    if (visitApplyStatus >= 0 && CodeVisitApplyStatus != null) {
                        foreach(var a in CodeVisitApplyStatus) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(visitApplyStatus)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    visitApplyStatusName = a.CodeNameKor;
                                }else {
                                    visitApplyStatusName = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    } 
                    if (visitPurposeCodeID >= 0 && CodeVisitPurpose != null) {
                        foreach(var a in CodeVisitPurpose) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(visitPurposeCodeID)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    visitPurposeCodeName = a.CodeNameKor;
                                }else {
                                    visitPurposeCodeName = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    }
                    if (m.a.VisitStatus>=0 && CodeVisitStatus!=null){
                        foreach(var a in CodeVisitStatus){
                            if (a.SubCodeID != null && a.SubCodeID.Equals(m.a.VisitStatus)){
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    strVisitStatus = a.CodeNameKor;
                                }else {
                                    strVisitStatus = a.CodeNameEng;
                                }
                                break;
                            }
                        }
                    }
                    
                    //m.a.IsSafetyEdu??"N", m.a.CarNo, 
                    dt.Rows.Add(location, m.a.VisitDate, m.a.EntryDate, m.a.ExitDate, m.a.CompanyName, m.a.Name, visitPurposeCodeName, ContactName, visitApplyStatusName, strVisitStatus, m.a.CardNo);
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

        /// <summary>
        /// 관리자 : 방문신청 승인/반려
        /// 임직원(접견인) : 방문신청 승인
        /// 임직원(총무):VIP 정보 확인
        /// 보안실:승인된 방문신청에 대하여 출입증번호를  부여하고 방문처리
        /// 방문/출입증미반납한 방문신청에 대하여 출입증을 회수하고 방문완료처리
        /// </summary>
        /// <param name="visitApplyID"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult Detail (VisitGridData values, int visitApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if(IsDietitian()){
                return View("_Inaccessible");
            }
            // WriteLog("visitApplyID:" + visitApplyID + ", slug" + slug);

            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }

            var visitApply = VisitApplyData?.Get(new QueryOptions<VisitApply>{
                Where = a => a.VisitApplyID == visitApplyID,
            }) ?? new VisitApply();

            var my = GetMe();
            // 관리자, 임직원(접견인), 보안실
            ViewBag.IsContactPerson = IsMaster() || IsGeneralManager(visitApply.Location) || (IsEmployee() && visitApply.ContactSabun.Equals(my.Sabun));
//            ViewBag.IsEditable = SelectItems.IsSecurityManager(my.LevelCodeID) || ViewBag.IsContactPerson;// IsSecurity() || ViewBag.IsContactPerson;
            //2023.10.24 dsyoo IsEditable 담당자 => 담당자부서로 확장
            ViewBag.IsEditable = SelectItems.IsSecurityManager(my.LevelCodeID) || my.SapDeptCode==visitApply.ContactOrgID;
            //            ViewBag.IsModCardNo = IsMaster() || IsSecurity(); // 출입증 번호 수정
            ViewBag.IsModCardNo = SelectItems.IsSecurityManager(my.LevelCodeID); //IsMaster() || IsSecurity(); // 출입증 번호 수정
            ViewBag.IsModVisitStatus = IsMaster() || IsSecurity();
            ViewBag.IsApproval = IsMaster() || IsEmployeeOnly() || IsGeneralManager() || IsHR() || IsESH();
            ViewBag.IsSecurity = IsSecurity();
            ViewBag.IsMaster = IsMaster();
            ViewBag.my = my;  //2023.09.13 dsyoo
            WriteLog("ViewBag.IsMaster:" + ViewBag.IsMaster);
            // WriteLog("TT: " + Dump(visitApply));

            var options = new QueryOptions<VisitApplyPerson>
            {
                Where = a => a.VisitApplyID == visitApplyID && a.IsDelete == "N",
            };
            VisitDetailViewModel vm = new(){
                VisitApply = visitApply,
                VisitApplyPersons = VisitApplyPersonData?.List(options)??new List<VisitApplyPerson>(), 
                CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeVipType = GetCommonCodes((int)VisitEnum.CommonCode.VipType),
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
                CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public async Task<IActionResult> Detail(VisitGridData values,string mode, int visitApplyID, int VisitApplyStatus, int visitApplyPersonID, int VisitStatus, String CardNo)
        {
            WriteLog("mode: "+mode+", visitApplyID: "+visitApplyID+", VisitApplyStatus: "+VisitApplyStatus);
            WriteLog("mode: "+mode+", visitApplyPersonID: "+visitApplyPersonID+", VisitStatus: "+VisitStatus+", CardNo: "+CardNo);
           
            if (IsAccess(false) == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            var today = DateTime.Now;
            if (mode.Equals("E")) // 방문신청 상태 수정
            {
                if(visitApplyID > 0)
                {
                    var orgObj = GetVisitApply(visitApplyID, true);
                    if (orgObj != null && VisitApplyPersonData != null){
                        var newObj = orgObj.Clone();
                        // VisitApplyStatus 방문신청상태: 승인대기(0)/승인완료(1)/반려(2) 
                        newObj.VisitApplyStatus = VisitApplyStatus;
                        newObj.UpdateDate = DateTime.Now;
                        VisitApplyData?.Update(newObj);
                        VisitApplyData?.Save();
                        //방문자들의 방문신청상태(VisitApplyStatus) 수정 추가 
                        var options = new QueryOptions<VisitApplyPerson>
                        {
                            Where = a =>a.VisitApplyID == orgObj.VisitApplyID && a.IsDelete == "N",
                        };
                        List<VisitApplyPerson> v= (List<VisitApplyPerson>) VisitApplyPersonData.List(options);
                        foreach(var m in v) {
                            m.VisitApplyStatus = VisitApplyStatus;
                            VisitApplyPersonData?.Update(m);
                            VisitApplyPersonData?.Save();
                        }
                        foreach (var m in v)
                        {
                            if (VisitApplyStatus == 1)
                            {
                                // 승인 완료 시 SMS 발송
                                if (m.Mobile != null)
                                {
                                    await SendMessageAsync("SMS", 2, "032-680-4700", "출입관리시스템", m.Mobile, m.Name, "", orgObj.Location);
                                }
                            } else if(VisitApplyStatus == 2){
                                // 반려
                                if (m.Mobile != null)
                                {
                                    await SendMessageAsync("SMS", 3, "032-680-4700", "출입관리시스템", m.Mobile, m.Name, "", orgObj.Location);
                                }
                            }
                        }
                        // VisitApplyType - 0: 방문신청, 1: 공사출입 인원, 2: 안전교육
                        if (orgObj.VisitApplyType == 1 && WorkVisitApplyData != null && WorkVisitPersonApplyData != null){ // 공사출입신청
                            // 공사출입신청
                            var options2 = new QueryOptions<WorkVisitApply>
                            {
                                Where = a =>a.WorkVisitApplyID == orgObj.WorkVisitApplyID && a.IsDelete == "N",
                            };
                            WorkVisitApply v2= WorkVisitApplyData.Get(options2);
                            v2.WorkVisitApplyStatus = VisitApplyStatus;
                            WorkVisitApplyData?.Update(v2);
                            WorkVisitApplyData?.Save();
                            WorkVisitApplyHistory workVisitApplyHistory = new()
                            {
                                WorkVisitApplyID = v2.WorkVisitApplyID, //공사출입신청ID
                                WorkApplyID = v2.WorkApplyID, //공사신청ID

                                WorkDate = v2.WorkDate, //공사일
                                WorkVisitApplyStatus = VisitApplyStatus, // 수정시 공사출입신청상태 값 등록 

                                UpdateSabun = my.Sabun,//등록자
                                UpdateName = my.Name,
                                UpdateOrgID = my.OrgID,
                                UpdateOrgNameKor = my.OrgNameKor,
                                UpdateOrgNameEng = my.OrgNameEng,
                                InsertUpdateDate = today
                            };
                            WorkVisitApplyHistoryData.Insert(workVisitApplyHistory);
                            WorkVisitApplyHistoryData.Save();

                            // 공사출입 인원 신청 WorkVisitApplyData WorkVisitPersonApplyData
                            var options3 = new QueryOptions<WorkVisitPersonApply>
                            {
                                Where = a =>a.WorkVisitApplyID == orgObj.WorkVisitApplyID && a.IsDelete == "N",
                            };
                            List<WorkVisitPersonApply> v3= (List<WorkVisitPersonApply>) WorkVisitPersonApplyData.List(options3);
                            foreach(var m in v3) {
                                m.WorkVisitApplyStatus = VisitApplyStatus;
                                WorkVisitPersonApplyData?.Update(m);
                                WorkVisitPersonApplyData?.Save();

                                // 2.1 공사출입인원신청히스토리 (WorkVisitPersonApplyHistory) 입력 
                                WorkVisitPersonApplyHistory workVisitPersonApplyHistory = new() {
                                    WorkVisitPersonApplyID = m.WorkVisitPersonApplyID,
                                    WorkVisitApplyID = m.WorkVisitApplyID,
                                    WorkVisitApplyStatus = VisitApplyStatus,
                                    Sabun = m.Sabun,
                                    Name = m.Name,
                                    OrgID = m.OrgID,
                                    OrgNameKor = m.OrgNameKor,
                                    OrgNameEng = m.OrgNameEng,
                                    InsertUpdateDate = today
                                };
                                WorkVisitPersonApplyHistoryData.Insert(workVisitPersonApplyHistory);
                                WorkVisitPersonApplyHistoryData?.Save();
                            }
                        }else if (orgObj.VisitApplyType == 2 && SafetyEduData != null){ 
                            // 안전교육 인원 신청 SafetyEduData
                            var options2 = new QueryOptions<SafetyEdu>
                            {
                                Where = a =>a.SafetyEduID == orgObj.SafetyEduID && a.IsDelete == "N",
                            };
                            SafetyEdu v2= SafetyEduData.Get(options2);
                            v2.EduApplyStatus = VisitApplyStatus;
                            SafetyEduData?.Update(v2);
                            SafetyEduData?.Save();

                            SafetyEduHistory safetyEduHistory = new() {
                                SafetyEduID = v2.SafetyEduID,
                                EduDate = v2.EduDate,
                                WorkApplyID = v2.WorkApplyID,
                                EduApplyStatus = v2.EduApplyStatus,
                                UpdateSabun = my.Sabun,//등록자
                                UpdateName = my.Name,
                                UpdateOrgID = my.OrgID,
                                UpdateOrgNameKor = my.OrgNameKor,
                                UpdateOrgNameEng = my.OrgNameEng,
                                InsertUpdateDate = today
                            };
                            SafetyEduHistoryData.Insert(safetyEduHistory);
                            SafetyEduHistoryData?.Save();
                        }// end if - VisitApplyType
                    }// end if - obj null
                }// end if - visitApplyID > 0
            }else if (mode.Equals("EVisitStatus")){// 방문자 상태 및 출입증 번호 추가 
                if(visitApplyPersonID > 0)
                {
                    var orgObj2 = GetVisitApplyPerson(visitApplyPersonID, true);
                    if (orgObj2!= null){
                        var newObj2 = orgObj2.Clone();
                        newObj2.VisitStatus = VisitStatus;
                        // VisitStatus 방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4):방문신청후 미 방문
                        string RStatus = string.Empty;
                        if(VisitStatus == 1){
                            newObj2.EntryDate = DateTime.Now;
                            RStatus = "R";
                        }else if(VisitStatus == 2){
                            newObj2.ExitDate = DateTime.Now;
                            RStatus = "N";
                        }
                        newObj2.CardNo = CardNo;
                        newObj2.UpdateDate = DateTime.Now;
                        VisitApplyPersonData?.Update(newObj2);
                        VisitApplyPersonData?.Save();

                        if (DbPASSPORT != null && !string.IsNullOrEmpty(RStatus)){
                            string placeName = string.Empty;
                            string locationName = string.Empty;
                            if(VisitApplyData != null){
                                var visitApply = VisitApplyData.Get(newObj2.VisitApplyID);
                                if(visitApply != null){
                                    List<CommonCode> CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place);
                                    foreach(var m in CodePlace){
                                        if(m.SubCodeID == visitApply.PlaceCodeID){
                                            placeName = m.CodeNameKor;
                                            break;
                                        }
                                    }

                                    List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                                    foreach(var m in CodeLocation){
                                        if(m.SubCodeDesc != null && m.SubCodeDesc.Equals(visitApply.Location)){
                                            locationName = m.CodeNameKor;
                                            break;
                                        }
                                    }
                                }
                            }
                            
                            StringBuilder sb = new();
                            sb.Append("PROCI_CARD_VISITOR @PageType='U', @RStatus='");
                            sb.Append(RStatus);
                            sb.Append("' , @VCardNo='");
                            sb.Append(CardNo);
                            sb.Append("' , @VisitorName='");
                            sb.Append(newObj2.Name);
                            sb.Append("', @Mobile='"); 
                            sb.Append(newObj2.Mobile);
                            sb.Append("', @UpdateSabun='"); 
                            sb.Append(my.Sabun);
                            sb.Append("', @ValidDate='"); 
                            sb.Append(today.ToString("yyyy-MM-dd "));
                            sb.Append("23:59:59");
                            sb.Append("', @IP='"); 
                            sb.Append(GetUserIP());
                            sb.Append("', @VisitArea='"); 
                            sb.Append(placeName);
                            sb.Append("', @Location='"); 
                            sb.Append(locationName);
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
            }
            
            return RedirectToAction("List", new { culture=GetLang() }); // 2023-08-04 수정 상세보기에서 승인후 저장을 누르면 목록화면으로 이동
            // return RedirectToAction("Detail", new { visitApplyID, culture=GetLang()});
        }

        /// <summary>
        /// 관리자 : 방문신청
        /// 임직원 : 방문신청
        ///관리자 : 휴대물품신청
        ///임직원 : 휴대물품신청
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult Reg (VisitGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if(accessible == false){
                return View("_Inaccessible");
            }
            ViewBag.IsPartner = IsPartnerNonresidentManager();
            PersonDTO my = GetMe();
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }

            VisitRegViewModel vm =new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeVipType = GetCommonCodes((int)VisitEnum.CommonCode.VipType),
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
                CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),
            };
            return View(vm);
        }

        /*
            [VisitApply] String Location, String VisitStartDate, String VisitEndDate, int PlaceCodeID, int VisitPurposeCodeID, 
                        String ContactSabun, String ContactName, int ContactOrgID, String ContactOrgNameKor, String ContactOrgNameEng, int RegVisitorType,
            [VisitApply|VisitApplyPerson] int VisitorType, int VisitApplyType, int VisitPersonID, 
            [VisitApplyPerson] String ckVisitorEdu, String ckCleanEdu, String IsSafetyEdu, String SafetyEduDate, String ckIsTermsPrivarcy, String ckIsVIP, int VipTypeCodeID,
            [VisitApplyPerson|VisitPerson] String Name, String BirthDate, int Gender, String Mobile, String CompanyName, String CarNo, 
            [VisitPerson] String GradeName, String Tel)
        */
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcompanyname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public async Task<IActionResult> RegAsync(VisitGridData values, string mode, VisitDTO pVisitDTO, string contactEmail, string contactMobile)
        {
            var RegVisitorType = 0;// 방문신청자구분(등록자):임직원(0)/방문자(1)
            if (mode.Equals("AH")) { // 방문자의 방문객 등록
                SetDbContext();
                Init();
                RegVisitorType = 1;
                TempData["UI.AlertMsg"] = "신청이 완료되었습니다";
            }else  if (IsAccess(false) == false) {
                return View("_Inaccessible");
            }else{
                var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
                if(accessible == false){
                    return View("_Inaccessible");
                }
            }

            if (mode.Equals("A") || mode.Equals("AH")) // 방문신청 추가 A: 방문객 추가, AH: 홈 화면에서 추가
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                pVisitDTO.SetVisitApply();
                pVisitDTO.SetVisitApplyPerson();
                // WriteLog("visitDTO>VisitApply: " +Dump(pVisitDTO.VisitApply));
                // WriteLog("visitDTO>VisitPersonList: " +Dump(pVisitDTO.VisitApplyPersonList[0].VisitPerson));
                // WriteLog("visitDTO>CarryItemDTO: " +Dump(pVisitDTO.VisitApplyPersonList[0].CarryItemDTO));

                if (pVisitDTO != null && pVisitDTO.VisitApply != null && pVisitDTO.VisitApplyHistory != null && pVisitDTO?.Gender != null){
                    // 1. 방문신청(VisitApply) 정보 입력 
                    pVisitDTO.VisitApply.VisitApplyStatus = 0; // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    pVisitDTO.VisitApply.InsertSabun = my.Sabun;//등록자가 방문자가 아닐경우 회원정보 입력 
                    pVisitDTO.VisitApply.InsertName = my.Name;
                    pVisitDTO.VisitApply.InsertOrgID = my.OrgID;
                    pVisitDTO.VisitApply.InsertOrgNameKor = my.OrgNameKor;
                    pVisitDTO.VisitApply.InsertOrgNameEng = my.OrgNameEng;
                    pVisitDTO.VisitApply.InsertDate = today;
                    VisitApplyData?.Insert(pVisitDTO.VisitApply);
                    // WriteLog("VisitApplyData: "+Dump(pVisitDTO.VisitApply));
                    VisitApplyData?.Save();
                    // WriteLog("VisitApplyData: "+Dump(pVisitDTO.VisitApply));

                    // 1.1 방문신청히스토리 (VisitApplyHistory) 입력 
                    pVisitDTO.VisitApplyHistory.VisitApplyID = pVisitDTO.VisitApply.VisitApplyID;
                    pVisitDTO.VisitApplyHistory.UpdateSabun = my.Sabun;
                    pVisitDTO.VisitApplyHistory.UpdateName = my.Name;
                    pVisitDTO.VisitApplyHistory.UpdateOrgID = my.OrgID;
                    pVisitDTO.VisitApplyHistory.UpdateOrgNameKor = my.OrgNameKor;
                    pVisitDTO.VisitApplyHistory.UpdateOrgNameEng = my.OrgNameEng;
                    pVisitDTO.VisitApplyHistory.InsertUpdateDate = today;
                    VisitApplyHistoryData?.Insert(pVisitDTO.VisitApplyHistory);
                    VisitApplyHistoryData?.Save();

                    var l = GetUnionPerson(null, pVisitDTO.ContactSabun, null);
                    Person? contactPerson = null;
                    if (l != null && l.a != null && l.a.Count > 0)
                    {
                        contactPerson = l.a[0];
                    }
                    foreach(VisitApplyPerson m in pVisitDTO.VisitApplyPersonList){
                        if (m != null && m.Name != null && m.BirthDate != null){
                            // 2. 방문이 처음인 방문객회원(VisitPerson )
                            // 방문신청회원이 임직원(비상주사원, 비상주관리자) 도 아니고 이전에 방문신청했던 회원도 아닐 경우 => 방문객회원(VisitPerson)에 입력
                            // 방문객회원이면 VisitorType=1, VisitPersonID, Name, OrgNameKor, 임직원 이면 VisitorType=0, Sabun, Name, OrgID, OrgNameKor, OrgNameEng 
                            var VisitorType = 1;    // 방문객인지(VisitPerson) or 임직원(Person) 인지 확인 후 값을 결정, 방문자구분(방문자): 임직원(0, 비상주업체관리자 또는 비상주사원) / 방문자(1)
                            var VisitPersonID = 0;  // 휴대물품 등록에 사용. VisitPerson 에서 값을 가져옴
                            var IsSafetyEdu = "N";  // 안전교육이력 확인하여 값 변경 
                            var vPerson = GetVisitPerson(m.Name, m.BirthDate);
                            if (vPerson != null){ // 기존 방문 회원
                                VisitPersonID = vPerson.VisitPersonID;
                                if (vPerson.SafetyEduLastDate != null) {
                                    var diffOfDates = today - vPerson.SafetyEduLastDate.Value;
                                    if (diffOfDates.Days < 365) {
                                        IsSafetyEdu = "Y";
                                    }
                                }
                                // Name BirthDate Gender Mobile CompanyName CarNo
                                vPerson.Gender = m.VisitPerson.Gender;
                                if (!string.IsNullOrEmpty(m.VisitPerson?.Mobile))
                                    vPerson.Mobile = m.VisitPerson.Mobile;
                                if (!string.IsNullOrEmpty(m.VisitPerson?.CompanyName))
                                    vPerson.CompanyName = m.VisitPerson.CompanyName;
                                if (!string.IsNullOrEmpty(m.VisitPerson?.CarNo))
                                    vPerson.CarNo = m.VisitPerson.CarNo;
                                
                                if (vPerson.VisitorEduLastDate != null) {
                                    var diffOfDates = today - vPerson.VisitorEduLastDate.Value;
                                    if (diffOfDates.Days >= 365) {
                                        vPerson.VisitorEduLastDate = null;
                                    }
                                }
                                if (vPerson.VisitorEduLastDate == null && m.VisitPerson?.VisitorEduLastDate != null)
                                    vPerson.VisitorEduLastDate = m.VisitPerson.VisitorEduLastDate;

                                if (vPerson.CleanEduLastDate != null) {
                                    var diffOfDates = today - vPerson.CleanEduLastDate.Value;
                                    if (diffOfDates.Days >= 365) {
                                        vPerson.CleanEduLastDate = null;
                                    }
                                }
                                if (vPerson.CleanEduLastDate == null && m.VisitPerson?.CleanEduLastDate != null)
                                    vPerson.CleanEduLastDate = m.VisitPerson.CleanEduLastDate;
                                m.VisitPerson = vPerson;
                            }
                            var partnerPerson = GetPartnerPerson(m.Name, m.BirthDate);
                            if (partnerPerson != null){
                                VisitorType = 0; // 비상주업체관리자 또는 비상주사원
                            }
                            var m2 = m.VisitPerson;
                            if (m2 != null){
                                m2.InsertSabun = my.Sabun;
                                m2.InsertName = my.Name;
                                m2.InsertOrgID = my.OrgID;
                                m2.InsertOrgNameKor = my.OrgNameKor;
                                m2.InsertOrgNameEng = my.OrgNameEng;
                                m2.InsertDate = today;

                                if (vPerson == null) {
                                    VisitPersonData?.Insert(m2);
                                    VisitPersonID = m2.VisitPersonID;
                                } else {
                                    VisitPersonData?.Update(m2);
                                }
                                WriteLog("VisitPersonData: "+Dump(m2));
                                VisitPersonData?.Save();
                            }

                            //3. 방문신청회원(VisitApplyPerson)(arr) 등록  
                            // 방문이 처음인 방문객만 방문객회원(VisitPerson) 등록 후 방문신청회원(VisitApplyPerson) 등록 
                            // 임직원(비상주사원, 비상주관리자) 이거나 이전에 방문한 적이 있는 방문객회원은 방문신청회원(VisitApplyPerson) 등록  
                            // VisitDate 는 VisitStartDate ~ VisitEndDate  기간만큼 입력하기 
                            var diffOfDates2 = Convert.ToDateTime(pVisitDTO.VisitApply.VisitEndDate) - Convert.ToDateTime(pVisitDTO.VisitApply.VisitStartDate);
                            int d = diffOfDates2.Days + 1;
                            var s = Convert.ToDateTime(pVisitDTO.VisitApply.VisitStartDate);
                            for(var i = 0;i<d;i++){
                                m.VisitApplyPersonID = 0;
                                m.VisitApplyID = pVisitDTO.VisitApply.VisitApplyID;
                                m.VisitDate= pVisitDTO.VisitApply.VisitStartDate;
                                m.VisitorType = VisitorType;
                                m.VisitDate = s.ToString("yyyy-MM-dd");
                                m.VisitPersonID= m2?.VisitPersonID; // 방문이 처음일때 (이전 방문기록이 있으면 해당 pk 저장 )
                                m.OrderNo = i; // OrderNo: 신청자와 신청자가 추가한 방문자 정렬 순서
                                m.CleanEduScore = 0;
                                m.VisitApplyStatus = 0; // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                                m.VisitStatus = 0; //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                                m.InsertSabun = my.Sabun;
                                m.InsertName = my.Name;
                                m.InsertOrgID = my.OrgID;
                                m.InsertOrgNameKor = my.OrgNameKor;
                                m.InsertOrgNameEng = my.OrgNameEng;
                                m.InsertDate = today;
                                m.IsSafetyEdu = IsSafetyEdu;
                                VisitApplyPersonData?.Insert(m);
                                WriteLog("VisitApplyPersonData: "+Dump(m));
                                VisitApplyPersonData?.Save();

                                //3.1 방문신청회원히스토리(VisitApplyPersonHistory)(arr) 등록 
                                var m4 = m.VisitApplyPersonHistory;
                                if(m4 != null){
                                    m4.VisitApplyPersonHistoryID = 0;
                                    m4.VisitApplyPersonID = m.VisitApplyPersonID;
                                    m4.VisitApplyID = pVisitDTO.VisitApply.VisitApplyID;
                                    m4.VisitDate= pVisitDTO.VisitApply.VisitStartDate;
                                    m4.VisitorType = VisitorType;
                                    m4.VisitDate = s.ToString("yyyy-MM-dd");
                                    m4.VisitPersonID= m2?.VisitPersonID; // 방문이 처음일때 (이전 방문기록이 있으면 해당 pk 저장 )
                                    m4.OrderNo = i; // OrderNo: 신청자와 신청자가 추가한 방문자 정렬 순서
                                    m4.CleanEduScore = 0;
                                    m4.VisitApplyStatus = 0; // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                                    m4.VisitStatus = 0; //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 

                                    m4.VisitDate = s.ToString("yyyy-MM-dd");
                                    m4.UpdateSabun = my.Sabun;//등록자
                                    m4.UpdateName = my.Name;
                                    m4.UpdateOrgID = my.OrgID;
                                    m4.UpdateOrgNameKor = my.OrgNameKor;
                                    m4.UpdateOrgNameEng = my.OrgNameEng;
                                    m4.InsertUpdateDate = today;
                                    m4.IsSafetyEdu = IsSafetyEdu;
                                    VisitApplyPersonHistoryData?.Insert(m4);
                                    VisitApplyPersonHistoryData?.Save();
                                    WriteLog("visitApplyPersonHistory: "+Dump(m4));
                                }
                                s = s.AddDays(1);
                            } // end for 날짜별 등록

                            // 휴대물품 등록: CarryItemApplyData CarryItemApplyHistoryData 
                            if(CarryItemApplyData != null && CarryItemApplyHistoryData != null && CarryItemInfoData != null){
                                var VisitApplyPersonID = m.VisitApplyPersonID;
                                CarryItemDTO? CarryItemDTO = m.CarryItemDTO;
                                if (CarryItemDTO != null){
                                    // InsertSabun InsertName InsertOrgID InsertOrgNameKor InsertOrgNameEng InsertDate InsertVisitorType InsertVisitPersonID
                                    CarryItemDTO.CarryItemApply.CarryVisitorType = VisitorType;
                                    CarryItemDTO.CarryItemApply.VisitPersonID = VisitPersonID;
                                    CarryItemDTO.CarryItemApply.InsertVisitorType = RegVisitorType; // 방문자구분(등록자) 회원(0)/방문자(1)
                                    // 방문자(1)
                                    // CarryItemDTO.CarryItemApply.InsertVisitPersonID = m.VisitPerson?.VisitPersonID; // 방문자구분(등록자) 이 방문자(1) 일 경우 방문객회원ID 입력, 회원일경우 InsertSabun ~ InsertOrgNameEng 입력
                                    // 회원(0) 사번, 이름, 부서 정보
                                    CarryItemDTO.CarryItemApply.InsertSabun = my.Sabun;
                                    CarryItemDTO.CarryItemApply.InsertName = my.Name;
                                    CarryItemDTO.CarryItemApply.InsertOrgID = my.OrgID;
                                    CarryItemDTO.CarryItemApply.InsertOrgNameKor = my.OrgNameKor;
                                    CarryItemDTO.CarryItemApply.InsertOrgNameEng = my.OrgNameEng;

                                    CarryItemDTO.CarryItemApply.InsertDate = today;
                                    CarryItemApplyData.Insert(CarryItemDTO.CarryItemApply);
                                    WriteLog("CarryItemApplyData: "+Dump(CarryItemDTO.CarryItemApply));
                                    CarryItemApplyData.Save();

                                    CarryItemDTO.CarryItemApplyHistory.CarryItemApplyID = CarryItemDTO.CarryItemApply.CarryItemApplyID;
                                    CarryItemDTO.CarryItemApplyHistory.CarryVisitorType = VisitorType;
                                    CarryItemDTO.CarryItemApplyHistory.VisitPersonID = VisitPersonID;
                                    
                                    CarryItemDTO.CarryItemApplyHistory.UpdateVisitorType = RegVisitorType; // 방문자구분(등록자) 회원(0)/방문자(1)
                                    // 방문자(1)
                                    // CarryItemDTO.CarryItemApplyHistory.UpdateVisitPersonID = m.VisitPerson?.VisitPersonID; // 방문자구분(등록자) 이 방문자(1) 일 경우 방문객회원ID 입력, 회원일경우 InsertSabun ~ InsertOrgNameEng 입력
                                    // 회원(0) 사번, 이름, 부서 정보
                                    CarryItemDTO.CarryItemApplyHistory.UpdateSabun = my.Sabun;
                                    CarryItemDTO.CarryItemApplyHistory.UpdateName = my.Name;
                                    CarryItemDTO.CarryItemApplyHistory.UpdateOrgID = my.OrgID;
                                    CarryItemDTO.CarryItemApplyHistory.UpdateOrgNameKor = my.OrgNameKor;
                                    CarryItemDTO.CarryItemApplyHistory.UpdateOrgNameEng = my.OrgNameEng;

                                    CarryItemDTO.CarryItemApplyHistory.InsertUpdateDate = today;
                                    CarryItemApplyHistoryData.Insert(CarryItemDTO.CarryItemApplyHistory);
                                    WriteLog("CarryItemApplyHistoryData: "+Dump(CarryItemDTO.CarryItemApplyHistory));
                                    CarryItemApplyHistoryData.Save();

                                    // 휴대물품 리스트 등록 CarryItemInfoData
                                    if (CarryItemDTO.CarryItemInfoList != null){
                                        foreach(var m3 in CarryItemDTO.CarryItemInfoList){
                                            m3.CarryItemApplyID = CarryItemDTO.CarryItemApply.CarryItemApplyID;
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
                                                InsertUpdateDate = today
                                            };
                                            CarryItemInfoHistoryData?.Insert(m4);
                                            CarryItemInfoHistoryData?.Save();
                                            /*2023.10.10 dsyoo 노트북 NAC연동은 처리하지 않는다.
                                            try{
                                                if (DbMySQL != null && m3.CarryItemCodeID == 0) { // PC 노트북
                                                    TUserAdd userAdd = new()
                                                    {
                                                        //ExportDate
                                                        ReqTime = today,
                                                        UserId = "",
                                                        UserName = CarryItemDTO.CarryItemApply.Name??"",
                                                        UserPassword = "",
                                                        ExpireDate = (uint) Convert.ToDateTime(CarryItemDTO.CarryItemApply.ExportDate+" 23:59:59").GetUnixEpoch(),
                                                        Position2 = "", // 소속정보: 회사명
                                                        Rank = CarryItemDTO.CarryItemApply.GradeName??"", // 직책
                                                        TelNum = CarryItemDTO.CarryItemApply.Tel??"", // 전화번호
                                                        CellPhone = CarryItemDTO.CarryItemApply.Mobile, // 핸드폰 번호
                                                    };
                                                    DbMySQL.TUserAdds.Add(userAdd);
                                                    DbMySQL.SaveChanges();
                                                }
                                            }catch(Exception e){
                                                WriteLog(e.ToString());
                                            }
                                            */
                                        } // end foreach
                                    } // end if CarryItemDTO.CarryItemInfoList != null
                                } // end if CarryItemDTO != null
                            } // end if CarryItemApplyData != null
                        } // end if m != nul
                        // 방문신청완료: 담당자에게 이메일과 SMS
                            
                        if (contactPerson != null)
                        {
                            if (!string.IsNullOrEmpty(contactPerson.Mobile))
                            {
                                await SendMessageAsync("SMS", 1, "032-680-4700", "출입관리시스템", contactPerson.Mobile, m.Name, "", pVisitDTO.VisitApply.Location);
                            }
                            // 이메일
                            if(!string.IsNullOrEmpty(contactPerson.Email)){
                                await SendMessageAsync("MAIL", 24015, "gcms@dbhitek.com", "출입관리시스템", contactPerson.Email, m.Name, "", pVisitDTO.VisitApply.Location);
                            }
                        }
                    } // end foreach VisitPerson, VisitApplyPerson 등록
                    // SMS 를 한번만 보낼경우엔 여기에서 처리.

                } // end if pVisitDTO != null
            } // end if mode == A
            // return new EmptyResult();
            if (mode.Equals("A")){
                return RedirectToAction("List", new { culture=GetLang() });
            }else { // AH: from Home Controller
                TempData["msg"] = "success";
                return RedirectToAction("Index","Home", new { culture=GetLang() });
            }
        }
        

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchinsertname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]  //2023.10.05 신인아 searchinsertname추가, searchcontactname삭제
        public IActionResult ManualList (VisitManualGridData values, VisitGridData values2, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsSecurity();
            if(accessible == false){
                return View("_Inaccessible");
            }
            ViewBag.IsEditable = true;
            VisitListViewModel vm = new();
            // var m = GetVisitApplyList(VisitApplyListType.Manual);
            VisitManualGridData filterhValue = (VisitManualGridData) FilterGridData(values);
            VisitGridData filterhValue2 = (VisitGridData) FilterGridData(values2);
            WriteLog("VisitManualGridData:"+Dump(filterhValue));
            var m = GetVisitApplyList(VisitApplyListType.Manual, filterhValue2);
            WriteLog("GetVisitApplyList:"+ m.a.Count +" / "+Dump(m));

            if (m != null) {
                ViewBag.visitApplyPersonList = m.a;
                vm =new(){
                    CodeVisitApplyType = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyType),
                    CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus),
                    CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus),
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                    CodeVisitManualPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitManualPurpose),
                    //CurrentManualRoute = values,   //2023.09.25 신인아
                    //SearchManualRoute=filterhValue,   //2023.09.25 신인아
                    CurrentRoute = values2,    //2023.09.25 신인아
                    SearchRoute = filterhValue2,   //2023.09.25 신인아
                    TotalPages = values.GetTotalPages(m.b),
                    TotalCnt = m.b,
                };
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchinsertname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]  //2023.10.05 신인아 searchinsertname추가, searchcontactname삭제
        public IActionResult ManualList (VisitManualGridData values, int visitApplyPersonID, int VisitStatus, String CardNo, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mode: "+mode+", visitApplyPersonID: "+visitApplyPersonID+", VisitStatus: "+VisitStatus+", CardNo: "+CardNo);
           
            if(mode == "EVisitStatus"){
                if(visitApplyPersonID > 0)
                {
                    var orgObj2 = GetVisitApplyPerson(visitApplyPersonID, true);
                    var newObj2 = orgObj2.Clone();
                    newObj2.VisitStatus = VisitStatus;
                    // VisitStatus 방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                    if(VisitStatus == 1 ){
                        newObj2.EntryDate = DateTime.Now;
                    }else if(VisitStatus == 2 || VisitStatus == 4){
                        newObj2.ExitDate = DateTime.Now;
                    }
                    newObj2.CardNo = CardNo;
                    newObj2.UpdateDate = DateTime.Now;
                    VisitApplyPersonData?.Update(newObj2);
                    VisitApplyPersonData?.Save();
                }
            }
            return RedirectToAction("ManualList", new { culture = GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult ManualReg (VisitManualGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }

            VisitRegViewModel vm =new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                CodeVisitManualPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitManualPurpose),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeVipType = GetCommonCodes((int)VisitEnum.CommonCode.VipType),
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult ManualReg(VisitManualGridData values, string mode, int VisitorType, int VisitApplyType, int RegVisitorType, int VisitPersonID, String Location, String VisitStartDate,
            String VisitEndDate, int PlaceCodeID, int VisitManualPurposeCodeID, String Purpose, 
            String Name, String BirthDate, int Gender, String Mobile, String CompanyName, String CarNo)
        {
            // VisitorType VisitApplyType RegVisitorType VisitPersonID Location VisitStartDate VisitEndDate  PlaceCodeID VisitPurposeCodeID Purpose  
            //  Name BirthDate Gender Mobile CompanyName CarNo 

            WriteLog("mode: "+mode+", VisitorType: "+VisitorType+", VisitApplyType: "+VisitApplyType+", RegVisitorType: "+RegVisitorType+", VisitPersonID: "+VisitPersonID+", Location: "+Location);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("A")){ // 방문신청 추가            
                PersonDTO my = GetMe();

                // 1. 방문수기신청 (VisitApply) 정보 입력 
                VisitApply visitApply = new()
                {
                    Location = Location, //사업장구분
                    VisitStartDate = VisitStartDate, //방문시작일
                    VisitEndDate = VisitEndDate, //방문종료일
                    PlaceCodeID = PlaceCodeID, //장소코드ID(공통코드)
                    VisitManualPurposeCodeID = VisitManualPurposeCodeID, //방문수기등록방문목적코드ID(공통코드)

                    // ContactSabun = ContactSabun, //접견자 사번
                    // ContactName = ContactName, //접견자 이름 
                    // ContactOrgID = ContactOrgID, //접견자 부서번호
                    // ContactOrgNameKor = ContactOrgNameKor, //접견자 부서명(한글)
                    // ContactOrgNameEng = ContactOrgNameEng, //접견자 부서명(영문)

                    // VisitorType = VisitorType, //방문자구분: 임직원(0)-비상주업체관리자 또는 비상주사원 /방문자(1) 
                    VisitApplyType = VisitApplyType, //방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/ => 방문신청에서는 (2) 안전교육까지만 사용 /방문객수기등록(3)/택배(4) => 방문객수기등록 메뉴에서 필요상으로 추가함.  
                    RegVisitorType = RegVisitorType, //방문신청자구분(등록자): 임직원(0)-비상주업체관리자 또는 비상주사원 /방문자(1) 
                    // VisitPersonID = VisitPersonID, //방문객회원ID(등록자) : 방문자 본인이 신청할 경우 본인 회원 ID 입력 
                    Purpose = Purpose, //상세목적 방문자수기등록에만 있음. 

                    VisitApplyStatus = 1, //방문수기신청 입력 시 자동으로 승인완료 처리 .방문신청상태: 승인대기(0)/승인완료(1)/반려(2)

                    InsertSabun = my.Sabun,//등록자: 경비실 임직원 
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now,
                    // IsDelete = "N"
                };
                VisitApplyData?.Insert(visitApply);
                VisitApplyData?.Save();
                WriteLog("visitApply: "+Dump(visitApply));

                // 1.1 방문신청히스토리 (VisitApplyHistory) 입력 
                VisitApplyHistory visitApplyHistory = new()
                {
                    VisitApplyID = visitApply.VisitApplyID, // 방문신청ID 
                    Location = visitApply.Location, //사업장구분
                    VisitStartDate = visitApply.VisitStartDate, //방문시작일
                    VisitEndDate = visitApply.VisitEndDate, //방문종료일
                    PlaceCodeID = visitApply.PlaceCodeID, //장소코드ID(공통코드)
                    VisitManualPurposeCodeID = visitApply.VisitManualPurposeCodeID, //방문수기등록방문목적코드ID(공통코드)

                    // ContactSabun = visitApply.ContactSabun, //접견자 사번
                    // ContactName = visitApply.ContactName, //접견자 이름 
                    // ContactOrgID = visitApply.ContactOrgID, //접견자 부서번호
                    // ContactOrgNameKor = visitApply.ContactOrgNameKor, //접견자 부서명(한글)
                    // ContactOrgNameEng = visitApply.ContactOrgNameEng, //접견자 부서명(영문)

                    // VisitorType = visitApply.VisitorType, //방문자구분: 임직원(0)-비상주업체관리자 또는 비상주사원 /방문자(1) 
                    VisitApplyType = visitApply.VisitApplyType, //방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/ => 방문신청에서는 (2) 안전교육까지만 사용 /방문객수기등록(3)/택배(4) => 방문객수기등록 메뉴에서 필요상으로 추가함.  
                    RegVisitorType = visitApply.RegVisitorType, //방문신청자구분(등록자): 임직원(0)-비상주업체관리자 또는 비상주사원 /방문자(1) 
                    // VisitPersonID = visitApply.VisitPersonID, //방문객회원ID(등록자) : 방문자 본인이 신청할 경우 본인 회원 ID 입력 
                    Purpose = visitApply.Purpose, //상세목적 방문자수기등록에만 있음. 
                    VisitApplyStatus = visitApply.VisitApplyStatus, //방문수기신청 입력 시 자동으로 승인완료 처리 .

                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = DateTime.Now
                };
                VisitApplyHistoryData?.Insert(visitApplyHistory);
                VisitApplyHistoryData?.Save();
                WriteLog("visitApplyHistory: "+Dump(visitApplyHistory));
                
                //2. 방문이 처음인 방문객회원(VisitPerson )  
                // 방문신청회원이 임직원(비상주사원, 비상주관리자) 도 아니고 이전에 방문신청했던 회원도 아닐 경우 => 방문객회원(VisitPerson)에 입력 
                if(VisitPersonID>0)
                {
                    //이전 등록된 회원
                }else{
                    VisitPerson visitPerson = new()
                    {
                        Name = Name, 
                        BirthDate = BirthDate, 
                        Gender = Gender, 
                        Mobile = Mobile,
                        CompanyName = CompanyName, 
                        // GradeName = GradeName, 
                        CarNo = CarNo, 
                        // Tel = Tel, 
                        RegVisitorType = RegVisitorType, //방문신청자구분(등록자): 임직원(0)/방문자(1) 

                        InsertSabun = my.Sabun,
                        InsertName = my.Name,
                        InsertOrgID = my.OrgID,
                        InsertOrgNameKor = my.OrgNameKor,
                        InsertOrgNameEng = my.OrgNameEng,
                        InsertDate = DateTime.Now,
                        IsDelete = "N"
                    };
                    VisitPersonID = visitPerson.VisitPersonID;
                }

                //3. 방문신청회원(VisitApplyPerson ) 추가 
                // 방문이 처음인 방문객만 방문객회원(VisitPerson) 등록 후 방문신청회원(VisitApplyPerson) 등록 
                // 임직원(비상주사원, 비상주관리자) 이거나 이전에 방문한 적이 있는 방문객회원은 방문신청회원(VisitApplyPerson) 등록  
                VisitApplyPerson visitApplyPerson = new()
                {
                    VisitApplyID = visitApply.VisitApplyID,
                    VisitDate = VisitStartDate,
                    VisitorType = VisitorType, //방문객회원이면 VisitorType=1, VisitPersonID, Name, OrgNameKor, 임직원 이면 VisitorType=0, Sabun, Name, OrgID, OrgNameKor, OrgNameEng 
                    VisitPersonID = VisitPersonID, //방문이 처음일때 (이전 방문기록이 있으면 해당 pk 저장 )
                    BirthDate = BirthDate, 
                    Gender = Gender, 
                    Mobile = Mobile, 
                    CompanyName = CompanyName, 
                    // Sabun = Sabun, //임직원(비상주사원 또는 비상주관리자 일경우 사번 입력 )
                    Name = Name, 
                    // OrgID = OrgID, 
                    // OrgNameKor = OrgNameKor, 
                    // OrgNameEng = OrgNameEng, 
                    OrderNo = 0,
                    IsVisitorEdu = "N",
                    VisitorEduDate = null,
                    IsCleanEdu = "N",
                    CleanEduDate = null,
                    CleanEduScore = 0,
                    IsSafetyEdu = "N",
                    SafetyEduDate = null, 
                    CarNo = CarNo, 
                    IsTermsPrivarcy = "N",

                    VisitApplyStatus = 1, //방문수기신청 입력 시 자동으로 승인완료 처리 .방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    VisitStatus = 0, //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 

                    InsertVisitorType = RegVisitorType,
                    // InsertVisitPersonID = VisitPersonID,

                    InsertSabun = my.Sabun,
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now,
                };
                VisitApplyPersonData?.Insert(visitApplyPerson);
                VisitApplyPersonData?.Save();
                WriteLog("visitApplyPerson: "+Dump(visitApplyPerson));

                //3.1 방문신청회원히스토리(VisitApplyPersonHistory)(arr) 등록 
                 VisitApplyPersonHistory visitApplyPersonHistory = new()
                {
                    VisitApplyPersonID = visitApplyPerson.VisitApplyPersonID, //방문신청회원ID 

                    VisitApplyID = visitApplyPerson.VisitApplyID,
                    VisitDate = visitApplyPerson.VisitDate,
                    VisitorType = visitApplyPerson.VisitorType, //방문객회원이면 VisitorType=1, VisitPersonID, Name, OrgNameKor, 임직원 이면 VisitorType=0, Sabun, Name, OrgID, OrgNameKor, OrgNameEng 
                    VisitPersonID = visitApplyPerson.VisitPersonID, //방문이 처음일때 (이전 방문기록이 있으면 해당 pk 저장 )
                    BirthDate = visitApplyPerson.BirthDate, 
                    Gender = visitApplyPerson.Gender, 
                    Mobile = visitApplyPerson.Mobile, 
                    CompanyName = visitApplyPerson.CompanyName, 
                    // Sabun = visitApplyPerson.Sabun, //임직원(비상주사원 또는 비상주관리자 일경우 사번 입력 )
                    Name = visitApplyPerson.Name, 
                    // OrgID = visitApplyPerson.OrgID, 
                    // OrgNameKor = visitApplyPerson.OrgNameKor, 
                    // OrgNameEng = visitApplyPerson.OrgNameEng, 
                    OrderNo = visitApplyPerson.OrderNo,
                    IsVisitorEdu = visitApplyPerson.IsVisitorEdu,
                    VisitorEduDate = visitApplyPerson.VisitorEduDate,
                    IsCleanEdu = visitApplyPerson.IsCleanEdu,
                    CleanEduDate = visitApplyPerson.CleanEduDate,
                    CleanEduScore = visitApplyPerson.CleanEduScore,
                    IsSafetyEdu = visitApplyPerson.IsSafetyEdu,
                    SafetyEduDate = visitApplyPerson.SafetyEduDate, 
                    CarNo = visitApplyPerson.CarNo, 
                    IsTermsPrivarcy = visitApplyPerson.IsTermsPrivarcy,
                    
                    VisitApplyStatus = visitApplyPerson.VisitApplyStatus, //방문신청상태
                    VisitStatus = visitApplyPerson.VisitStatus, //방문상태
                    
                    UpdateVisitorType = visitApplyPerson.InsertVisitorType,
                    // UpdateVisitPersonID = VisitPersonID,

                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = DateTime.Now
                };
                VisitApplyPersonHistoryData?.Insert(visitApplyPersonHistory);
                VisitApplyPersonHistoryData?.Save();
                WriteLog("visitApplyPersonHistory: "+Dump(visitApplyPersonHistory));

            }

            return RedirectToAction("ManualList", new { culture=GetLang() });  
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult ManualDetail (VisitManualGridData values, int visitApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            ViewBag.IsEditable = true;
           
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            WriteLog("visitApplyID:" + visitApplyID + ", slug" + slug);
            
            var visitApply = VisitApplyData?.Get(new QueryOptions<VisitApply>{
                Where = a => a.VisitApplyID == visitApplyID,
            }) ?? new VisitApply();            

            var options = new QueryOptions<VisitApplyPerson>
            {
                Where = a => a.VisitApplyID == visitApplyID && a.IsDelete == "N",
            };

            VisitDetailViewModel vm = new(){
                VisitApply = visitApply,
                VisitApplyPersons = VisitApplyPersonData?.List(options)??new List<VisitApplyPerson>(), 
                CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                CodeVisitManualPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitManualPurpose),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeVipType = GetCommonCodes((int)VisitEnum.CommonCode.VipType),
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
                CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchvisitapplystatus?}/{searchvisitstatus?}/{searchname?}/{searchcontactname?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchcardno?}")]
        public IActionResult ManualDetail(VisitManualGridData values, string mode, int visitApplyID, int visitApplyPersonID, 
            int VisitorType, int VisitPersonID, 
            String Location, String VisitStartDate, String VisitEndDate, int PlaceCodeID, int VisitPurposeCodeID, String Purpose, 
            String Name, String BirthDate, int Gender, String Mobile, String CompanyName, String CarNo, int VisitStatus, String CardNo)
        {
            WriteLog("mode: "+mode+", visitApplyID: "+visitApplyID);
            WriteLog("mode: "+mode+", visitApplyPersonID: "+visitApplyPersonID+", VisitStatus: "+VisitStatus+", CardNo: "+CardNo);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("E")) // 수기등록 수정
            {
                PersonDTO my = GetMe();
                if(visitApplyID > 0)
                {
                    var orgObj = GetVisitApply(visitApplyID, true);
                    var newObj = orgObj.Clone();
                    // newObj.VisitorType = VisitorType;
                    newObj.VisitPersonID = VisitPersonID;
                    newObj.Location = Location;
                    newObj.VisitStartDate = VisitStartDate;
                    newObj.VisitEndDate = VisitEndDate;
                    newObj.PlaceCodeID = PlaceCodeID;
                    newObj.VisitPurposeCodeID = VisitPurposeCodeID;
                    newObj.Purpose = Purpose;
                    newObj.UpdateDate = DateTime.Now;
                    
                    VisitApplyData?.Update(newObj);
                    VisitApplyData?.Save();

                    //히스토리 등록 
                    // 1.1 방문신청히스토리 (VisitApplyHistory) 입력 
                    VisitApplyHistory visitApplyHistory = new()
                    {
                        VisitApplyID = orgObj.VisitApplyID, // 방문신청ID 
                        Location = orgObj.Location, //사업장구분
                        VisitStartDate = orgObj.VisitStartDate, //방문시작일
                        VisitEndDate = orgObj.VisitEndDate, //방문종료일
                        PlaceCodeID = orgObj.PlaceCodeID, //장소코드ID(공통코드)
                        VisitPurposeCodeID = orgObj.VisitPurposeCodeID, //방문목적코드ID(공통코드)

                        ContactSabun = orgObj.ContactSabun, //접견자 사번
                        ContactName = orgObj.ContactName, //접견자 이름 
                        ContactOrgID = orgObj.ContactOrgID, //접견자 부서번호
                        ContactOrgNameKor = orgObj.ContactOrgNameKor, //접견자 부서명(한글)
                        ContactOrgNameEng = orgObj.ContactOrgNameEng, //접견자 부서명(영문)
                        
                        // VisitorType = orgObj.VisitorType, //방문자구분: 임직원(0)-비상주업체관리자 또는 비상주사원 /방문자(1) 
                        VisitApplyType = orgObj.VisitApplyType, //방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/ => 방문신청에서는 (2) 안전교육까지만 사용 /방문객수기등록(3)/택배(4) => 방문객수기등록 메뉴에서 필요상으로 추가함.  
                        RegVisitorType = orgObj.RegVisitorType, //방문신청자구분(등록자): 임직원(0)-비상주업체관리자 또는 비상주사원 /방문자(1) 
                        VisitPersonID = orgObj.VisitPersonID, //방문객회원ID(등록자) : 방문자 본인이 신청할 경우 본인 회원 ID 입력 

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    VisitApplyHistoryData?.Insert(visitApplyHistory);
                    VisitApplyHistoryData?.Save();
                    WriteLog("visitApplyHistory: "+Dump(visitApplyHistory));
                }
                if(visitApplyPersonID > 0)
                {
                    var orgObj2 = GetVisitApplyPerson(visitApplyPersonID, true);
                    if (orgObj2!=null){
                        var newObj2 = orgObj2.Clone();
                        newObj2.Name = Name;
                        newObj2.BirthDate = BirthDate;
                        newObj2.Gender = Gender;
                        newObj2.Mobile = Mobile;
                        newObj2.CompanyName = CompanyName;
                        newObj2.CarNo = CarNo;
                        newObj2.VisitStatus = VisitStatus;
                        newObj2.CardNo = CardNo;
                        newObj2.UpdateDate = DateTime.Now;
                        if(VisitStatus == 1 ){
                            newObj2.EntryDate = DateTime.Now;
                        }else if(VisitStatus == 2 || VisitStatus == 4){
                            newObj2.ExitDate = DateTime.Now;
                        }
                        
                        VisitApplyPersonData?.Update(newObj2);
                        VisitApplyPersonData?.Save();
                        //히스토리 등록
                        //3.1 방문신청회원히스토리(VisitApplyPersonHistory)(arr) 등록 
                        VisitApplyPersonHistory visitApplyPersonHistory = new()
                        {
                            VisitApplyPersonID = orgObj2.VisitApplyPersonID, //방문신청회원ID 
                            VisitApplyID = orgObj2.VisitApplyID,
                            VisitDate = orgObj2.VisitDate,
                            VisitorType = orgObj2.VisitorType, //방문객회원이면 VisitorType=1, VisitPersonID, Name, OrgNameKor, 임직원 이면 VisitorType=0, Sabun, Name, OrgID, OrgNameKor, OrgNameEng 
                            VisitPersonID = orgObj2.VisitPersonID, //방문이 처음일때 (이전 방문기록이 있으면 해당 pk 저장 )
                            BirthDate = orgObj2.BirthDate, 
                            Gender = orgObj2.Gender, 
                            Mobile = orgObj2.Mobile, 
                            CompanyName = orgObj2.CompanyName, 
                            // Sabun = orgObj2.Sabun, //임직원(비상주사원 또는 비상주관리자 일경우 사번 입력 )
                            Name = orgObj2.Name, 
                            // OrgID = orgObj2.OrgID, 
                            // OrgNameKor = orgObj2.OrgNameKor, 
                            // OrgNameEng = orgObj2.OrgNameEng, 
                            OrderNo = orgObj2.OrderNo,
                            IsVisitorEdu = orgObj2.IsVisitorEdu,
                            VisitorEduDate = orgObj2.VisitorEduDate,
                            IsCleanEdu = orgObj2.IsCleanEdu,
                            CleanEduDate = orgObj2.CleanEduDate,
                            CleanEduScore = orgObj2.CleanEduScore,
                            IsSafetyEdu = orgObj2.IsSafetyEdu, // to-do: 안전교육이력 확인하여 추가 
                            SafetyEduDate = orgObj2.SafetyEduDate, 
                            CarNo = orgObj2.CarNo, 
                            IsTermsPrivarcy = orgObj2.IsTermsPrivarcy,
                            VisitStatus = orgObj2.VisitStatus,
                            IsVIP = orgObj2.IsVIP,
                            VipTypeCodeID = orgObj2.VipTypeCodeID,
                            UpdateVisitorType = orgObj2.InsertVisitorType,
                            UpdateVisitPersonID = orgObj2.InsertVisitPersonID,

                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = DateTime.Now
                        };
                        VisitApplyPersonHistoryData?.Insert(visitApplyPersonHistory);
                        VisitApplyPersonHistoryData?.Save();
                        WriteLog("visitApplyPersonHistory: "+Dump(visitApplyPersonHistory)); 
                    }
                }
            }
            return RedirectToAction("ManualDetail", new { visitApplyID, culture=GetLang()});
        }

        /// <summary>
        /// 방문객관리>방문신청 후 SMS로 받은 교육을 듣는 페이지
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edu(){
            // TempData["UI.AlertMsg"] = "방문교육이수 확인이 되었습니다";
            return View();
        }

        /// <summary>
        /// 방문객관리>방문신청 후 교육 정보 저장
        /// </summary>
        /// <param name="VisitApplyPersonID"></param>
        /// <param name="name"></param>
        /// <param name="birth"></param>
        /// <param name="completeVisitorEdu"></param>
        /// <param name="completeCleanEdu"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edu(int VisitApplyPersonID, string ckVisitorEdu, string ckCleanEdu ){
            if(IsAccessAPI() == false){
                return View("_Inaccessible");
            }
            WriteLog("VisitApplyPersonID:"+VisitApplyPersonID);
            WriteLog("ckVisitorEdu:"+ckVisitorEdu+", ckCleanEdu:"+ckCleanEdu);
            //VisitPerson: VisitorEduLastDate	CleanEduLastDate
            //VisitApplyPerson: IsTermsPrivarcy IsVisitorEdu	VisitorEduDate	IsCleanEdu	CleanEduDate	CleanEduScore

            if(VisitApplyPersonID > 0)
            {
                // PersonDTO my = GetMe();
                var today = DateTime.Now;
                var orgObj = GetVisitApplyPerson(VisitApplyPersonID, true);
                WriteLog("orgObj:"+Dump(orgObj));
                if (orgObj!=null){
                    var newObj = orgObj.Clone();
                    WriteLog("newObj:"+Dump(newObj));
                    if(!string.IsNullOrEmpty(ckVisitorEdu) && ckVisitorEdu.Equals("Y")){
                        newObj.IsVisitorEdu = "Y";
                        newObj.VisitorEduDate = today;
                    }
                    if (!string.IsNullOrEmpty(ckCleanEdu) && ckCleanEdu.Equals("Y"))
                    {
                        newObj.IsCleanEdu = "Y";
                        newObj.CleanEduDate = today;
                    }
                    newObj.IsTermsPrivarcy = "Y";
                    newObj.UpdateDate = today;
                    VisitApplyPersonData?.Update(newObj);
                    VisitApplyPersonData?.Save();
                    //히스토리 등록
                    //3.1 방문신청회원히스토리(VisitApplyPersonHistory)(arr) 등록 
                    VisitApplyPersonHistory visitApplyPersonHistory = new()
                    {
                        VisitApplyPersonID = newObj.VisitApplyPersonID, //방문신청회원ID 
                        VisitApplyID = newObj.VisitApplyID,
                        VisitDate = newObj.VisitDate,
                        VisitorType = newObj.VisitorType, //방문객회원이면 VisitorType=1, VisitPersonID, Name, OrgNameKor, 임직원 이면 VisitorType=0, Sabun, Name, OrgID, OrgNameKor, OrgNameEng 
                        VisitPersonID = newObj.VisitPersonID, //방문이 처음일때 (이전 방문기록이 있으면 해당 pk 저장 )
                        BirthDate = newObj.BirthDate, 
                        Gender = newObj.Gender, 
                        Mobile = newObj.Mobile, 
                        CompanyName = newObj.CompanyName, 
                        Sabun = newObj.Sabun, //임직원(비상주사원 또는 비상주관리자 일경우 사번 입력 )
                        Name = newObj.Name, 
                        OrgID = newObj.OrgID, 
                        OrgNameKor = newObj.OrgNameKor, 
                        OrgNameEng = newObj.OrgNameEng, 
                        OrderNo = newObj.OrderNo,
                        IsVisitorEdu = newObj.IsVisitorEdu,
                        VisitorEduDate = newObj.VisitorEduDate,
                        IsCleanEdu = newObj.IsCleanEdu,
                        CleanEduDate = newObj.CleanEduDate,
                        CleanEduScore = newObj.CleanEduScore,
                        IsSafetyEdu = newObj.IsSafetyEdu, // to-do: 안전교육이력 확인하여 추가 
                        SafetyEduDate = newObj.SafetyEduDate, 
                        CarNo = newObj.CarNo, 
                        IsTermsPrivarcy = newObj.IsTermsPrivarcy,
                        VisitStatus = newObj.VisitStatus,
                        IsVIP = newObj.IsVIP,
                        VipTypeCodeID = newObj.VipTypeCodeID,
                        UpdateVisitorType = newObj.InsertVisitorType,
                        UpdateVisitPersonID = newObj.InsertVisitPersonID,

                        // UpdateSabun = my.Sabun,
                        // UpdateName = my.Name,
                        // UpdateOrgID = my.OrgID,
                        // UpdateOrgNameKor = my.OrgNameKor,
                        // UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    VisitApplyPersonHistoryData?.Insert(visitApplyPersonHistory);
                    VisitApplyPersonHistoryData?.Save();
                    WriteLog("visitApplyPersonHistory: "+Dump(visitApplyPersonHistory)); 
                    if(newObj.VisitPersonID != null){
                        var orgObj2 = GetVisitPersonID((int)newObj.VisitPersonID, true);
                        if (orgObj2!=null){                        
                            WriteLog("orgObj2:"+Dump(orgObj2));
                            var newObj2 = orgObj2.Clone();
                            if (!string.IsNullOrEmpty(ckVisitorEdu) && ckVisitorEdu.Equals("Y"))
                            {
                                newObj2.VisitorEduLastDate = today;
                            }
                            if (!string.IsNullOrEmpty(ckCleanEdu) && ckCleanEdu.Equals("Y")){
                                newObj2.CleanEduLastDate = today;
                            }
                            newObj2.UpdateDate = today;
                            VisitPersonData?.Update(newObj2);
                            VisitPersonData?.Save();   
                        }                     
                    }
                    TempData["UI.AlertMsg"] = "방문교육이수 확인이 되었습니다";
                }
            }

            return RedirectToAction("Edu", new { culture=GetLang()});
        }

        /// <summary>
        /// 방문객 리스트 불러오기 left outer join VisitApplyPerson VisitApply
        /// </summary>
        /// <param name="visitMode">0: 방문객관리, 1: 방문객수기입력</param>
        /// <returns></returns>
        private dynamic? GetVisitApplyList(VisitApplyListType visitMode=0, VisitGridData? values = null, bool isAll = false){
            WriteLog("visitMode:"+visitMode);
            // <Search Field>
            // SearchVisitApplyType - VisitApply 공통코드 2023.8.11 검색조건 삭제
            // SearchContactName - VisitApply
            // SearchInsertName - VisitApply  2023.10.05 신인아 삽입 
            // SearchVisitApplyStatus - VisitApplyPerson 공통코드
            // SearchVisitStatus - VisitApplyPerson 공통코드
            // SearchName - VisitApplyPerson
            // SearchBirthDate - VisitApplyPerson 2023.8.11 검색조건 삭제
            // SearchCompanyName - VisitApplyPerson
            // SearchStartInsertDate - VisitApplyPerson VisitDate or VisitApply (VisitStartDate)
            // SearchEndInsertDate - VisitApplyPerson VisitDate or VisitApply (VisitEndDate)
            // SearchCardNo - VisitApplyPerson
            // SearchCarNo - VisitApplyPerson 2023.8.11 검색조건 삭제
            // a: VisitApplyPerson, b: VisitApply
            // visitMode - 0: 방문객관리(VisitApplyType-0,1,2), 1: 방문객수기입력(VisitApplyType-3), 2: 택배 
            // VisitApplyType(방문신청구분) - 0 방문신청, 1 공사출입인원신청, 2 안전교육, 3 방문객수기등록, 4 택배
            if (DbSVISIT!=null && values != null && VisitApplyPersonData != null) {
                IQueryable<VisitApply> inner = DbSVISIT.VisitApply.Where(x => x.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsSecurity())
                    {
                        inner = inner.Where(x => x.Location == my.Location);
                    } 
                    else if(IsEmployee())
                    {
                        inner = inner.Where(x => x.ContactOrgID == my.SapDeptCode); //2023.10.23 dsyoo 접견인비교에서 접견인 조직비교로 변경. 접견인 조직에서 대리로 승인처리등을 할수 있다.
                      //  inner = inner.Where(x => x.ContactSabun == my.Sabun);
                    } 
                    else if (IsPartnerNonresidentManager()){
                        inner = inner.Where(x => x.InsertSabun == my.Sabun); // 비상주업체 관리자 신청건
                        // 외부에서 신청건에 대해서 회사명 LIKE 검색으로 처리할 지는 논의 중.
                    }
                }

                if(visitMode == VisitApplyListType.Manual){ //3: 방문객수기입력
                    inner = inner.Where(x => x.VisitApplyType == 3);
                    // var query = db.VisitApplyPerson
                    //     .Where(x => x.IsDelete == "N")
                    //     .OrderBy(x => x.InsertDate)
                    //     .GroupJoin(
                    //         db.VisitApply.Where(x => x.IsDelete == "N" && x.VisitApplyType==3),
                    //         a => a.VisitApplyID, 
                    //         b => b.VisitApplyID,
                    //         (a, b) => new {a, b});
                    // var visitApplyPersons = query.ToList()
                    // WriteLog("visitApplyPersons:"+Dump(visitApplyPersons));
                    // return visitApplyPersons;
                }else{ //0: 방문객관리isAll
                    if (values.SearchVisitApplyType > -1) {
                        inner = inner.Where(x => x.VisitApplyType == values.SearchVisitApplyType);
                    } else {
                        inner = inner.Where(x => x.VisitApplyType == 0 || x.VisitApplyType == 1 || x.VisitApplyType == 2);
                    }
                }
                if (!string.IsNullOrEmpty(values.SearchContactName)){
                    inner = inner.Where(x => x.ContactName != null && EF.Functions.Like(x.ContactName, "%" + values.SearchContactName + "%"));
                }
                if (!string.IsNullOrEmpty(values.SearchInsertName))  //2023.10.05 신인아 삽입 start
                {
                    inner = inner.Where(x => x.InsertName != null && EF.Functions.Like(x.InsertName, "%" + values.SearchInsertName + "%"));
                } //2023.10.05 신인아 삽입 end, 근무자 검색(담당자X)

                IQueryable<VisitApplyPerson> outer = DbSVISIT.VisitApplyPerson.Where(x => x.IsDelete == "N");
                // Expression<Func<VisitApplyPerson, bool>> w = x => x.IsDelete == "N";
                if(IsSecurity() && values.SearchVisitApplyStatus==null) { values.SearchVisitApplyStatus = 1; }//2023.09.25 신인아 보안팀이면 승인완료로 디폴트값 시작
                if (values.SearchVisitApplyStatus > -1) {
                    outer = outer.Where(x => x.VisitApplyStatus == values.SearchVisitApplyStatus);
                }
                if (values.SearchVisitStatus > -1) {
                    outer = outer.Where(x => x.VisitStatus == values.SearchVisitStatus);
                }
                if (!string.IsNullOrEmpty(values.SearchName))
                {
                    outer = outer.Where(x => x.Name != null && EF.Functions.Like(x.Name, "%" + values.SearchName + "%"));
                }
                // if (!string.IsNullOrEmpty(values.SearchBirthDate))
                // {
                //     outer = outer.Where( x => x.BirthDate == values.SearchBirthDate);
                // }
                if (!string.IsNullOrEmpty(values.SearchCompanyName))
                {
                    outer = outer.Where(x => x.CompanyName != null && EF.Functions.Like(x.CompanyName, "%" + values.SearchCompanyName + "%"));
                }
                if (!string.IsNullOrEmpty(values.SearchCardNo))
                {
                    outer = outer.Where(x => x.CardNo != null && EF.Functions.Like(x.CardNo, "%" + values.SearchCardNo + "%"));
                }
                // if (!string.IsNullOrEmpty(values.SearchCarNo))
                // {
                //     outer = outer.Where(x => x.CarNo != null && EF.Functions.Like(x.CarNo, "%" + values.SearchCarNo + "%"));
                // }
                try{
                    //2023.09.25 shin start 신인아
                    if (string.IsNullOrEmpty(values.SearchStartInsertDate))
                    {
                        values.SearchStartInsertDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    }
                    if (string.IsNullOrEmpty(values.SearchEndInsertDate))
                    {
                        values.SearchEndInsertDate = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    //2023.09.25 shin end 신인아
                    if (!string.IsNullOrEmpty(values.SearchStartInsertDate))
                    {
                        //string d = values.SearchStartInsertDate + " 00:00:01";  //2023.09.25 신인아
                        string d = values.SearchStartInsertDate; //2023.09.25 신인아
                        outer = outer.Where(x => x.VisitDate != null && (DateTime)(object)x.VisitDate >= Convert.ToDateTime(d));
                    }
                    if (!string.IsNullOrEmpty(values.SearchEndInsertDate))
                    {
                        //string d = values.SearchEndInsertDate + " 23:59:59";  //2023.09.25 신인아
                        string d = values.SearchEndInsertDate;  //2023.09.25 신인아
                        outer = outer.Where(x => x.VisitDate != null && (DateTime)(object)x.VisitDate <= Convert.ToDateTime(d));
                    }
                } catch(Exception e){
                    WriteError(e.ToString());
                }
                var query = outer.Join(
                        inner,
                        a => a.VisitApplyID,
                        b => b.VisitApplyID,
                        (a, b) => new { a, b });
                int count = query.Count();
                // WriteLog("count..." + count);
                outer=outer.OrderByDescending(x => x.InsertDate);
                if (!isAll) {
                    var q = outer.Join(
                            inner,
                            a => a.VisitApplyID,
                            b => b.VisitApplyID,
                            (a, b) => new { a, b });
                    q=q.Skip((values.PageNumber - 1) * values.PageSize);
                    q=q.Take(values.PageSize);
                    var visitApplyPersons = q.ToList();
                    return new { a=visitApplyPersons, b=count };
                } else {
                    var visitApplyPersons = query.ToList();
                    return new { a=visitApplyPersons, b=count };
                }
            }

            return default;
        }

        private VisitApply? GetVisitApply(int visitApplyID = -1, bool isNoTracking = false)
        {
            VisitApply? visitApply = null;
            if(visitApplyID > 0) {
                var options = new QueryOptions<VisitApply>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.VisitApplyID == visitApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    visitApply = VisitApplyData?.GetNT(visitApplyID);
                } else {
                    visitApply = VisitApplyData?.Get(visitApplyID);
                }
            }
            return visitApply;
        }
        private VisitApplyPerson? GetVisitApplyPerson(int visitApplyPersonID=-1, bool isNoTracking = false)
        {
            WriteLog("visitApplyPersonID:"+visitApplyPersonID);
            VisitApplyPerson? visitApplyPerson = null;
            if(visitApplyPersonID > 0) {
                if(isNoTracking) {
                    visitApplyPerson = VisitApplyPersonData?.GetNT(visitApplyPersonID);
                } else {
                    visitApplyPerson = VisitApplyPersonData?.Get(visitApplyPersonID);
                }
                WriteLog("visitApplyPerson:"+Dump(visitApplyPerson));
            }
            return visitApplyPerson;
        }

        private VisitPerson? GetVisitPersonID(int visitPersonID=-1, bool isNoTracking = false)
        {
            VisitPerson? visitPerson = null;
            if(visitPersonID > 0) {
                var options = new QueryOptions<VisitPerson>
                {
                    Where = a =>a.VisitPersonID == visitPersonID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    visitPerson = VisitPersonData?.GetNT(visitPersonID);
                } else {
                    visitPerson = VisitPersonData?.Get(visitPersonID);
                }
            }
            return visitPerson;
        }

        /// <summary>
        /// 비상주 업체 직원 검색
        /// </summary>
        /// <param name="name"></param>
        /// <param name="birth"></param>
        /// <returns></returns>
        private Person? GetPartnerPerson(string name, string birth){
            var options = new QueryOptions<Person>
            {
                Where = a => a.Name == name && a.BirthDate == birth && 
                    (a.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager || a.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresident ) && a.IsDelete == "N",
            };
            if (PersonData != null){
                var visitPerson = PersonData.Get(options);
                return visitPerson;
            }
            return null;
        }

        /// <summary>
        /// 방문기록에서 방문자 검색
        /// </summary>
        /// <param name="name"></param>
        /// <param name="birth"></param>
        /// <returns></returns>
        private VisitPerson? GetVisitPerson(string name, string birth)
        {
            WriteLog("name: " + name + ", birth: " + birth);
            var options = new QueryOptions<VisitPerson>
            {
                Where = a => a.Name == name && a.BirthDate == birth && a.IsDelete == "N",
            };
            if (VisitPersonData != null){
                var visitPerson = VisitPersonData.GetNT(options);
                return visitPerson;
            }
            return null;
        }

        //[FromBody] System.Text.Json.JsonElement values
        // [Consumes("application/json")]
        public JsonResult? GetVisitorInfo(string name, string birth) {
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("name:" + name + ", birth:" + birth);
            // PersonDTO personDTO = GetMe();            
            // gender: 0
            // mobile: "010-6551-3899"
            // carNo: "가 2345"
            // companyName: "코노즈"
            // cleanEduLastDate: null
            // safetyEduLastDate: null
           var v = new VisitorRecord(0, "", "", "", "", "", "N");
            if (VisitPersonData != null){
                var options = new QueryOptions<VisitPerson>
                {
                    Where = a => a.Name == name && a.BirthDate == birth && a.IsDelete == "N",
                };
                var visitPerson = VisitPersonData.Get(options);
                // WriteLog("visitPerson: " + Dump(visitPerson)); // 김진희 2022-08-01
                if (visitPerson != null){
                    var isBlackList = "N";
                    if(BlackListData != null){
                        var options2 = new QueryOptions<BlackList>
                        {
                            Where = a => a.Name == name && a.BirthDate == birth && a.IsDelete == "N",
                        };
                        var blacklist = BlackListData.Get(options2);
                        if(blacklist != null && blacklist.BlackListID > 0) {
                            isBlackList = "Y";
                        }
                    }
                    v = new VisitorRecord(visitPerson.Gender, visitPerson.Mobile, visitPerson.CarNo,visitPerson.CompanyName,
                        visitPerson.CleanEduLastDate?.ToString("yyyy-MM-dd")??"", visitPerson.VisitorEduLastDate?.ToString("yyyy-MM-dd")??"", isBlackList);
                }
            }
            return Json(v);
        }

        //[FromBody] System.Text.Json.JsonElement values
        // [Consumes("application/json")]
        public JsonResult? GetVisitApplyPersonInfo(string name, string birth) {
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("name:" + name + ", birth:" + birth);
            // PersonDTO personDTO = GetMe();            
            // gender: 0
            // mobile: "010-6551-3899"
            // carNo: "가 2345"
            // companyName: "코노즈"
            // cleanEduLastDate: null
            // safetyEduLastDate: null
            var v = new VisitApplyPersonRecord(0, 0, "", "", "", "", "", 6, "2000");
            if (VisitApplyPersonData != null){
                var options = new QueryOptions<VisitApplyPerson>
                {
                    Where = a => a.Name == name && a.BirthDate == birth && a.IsDelete == "N",
                    OrderBy = a => a.VisitApplyID,
                    OrderByDirection = "desc",
                };
                var visitApplyPerson = VisitApplyPersonData.Get(options);
                WriteLog("visitApplyPerson: " + Dump(visitApplyPerson));
                if (visitApplyPerson != null){
                    var options2 = new QueryOptions<VisitApply>
                    {
                        Where = a => a.VisitApplyID == visitApplyPerson.VisitApplyID && a.IsDelete == "N",
                    };
                    var visitApply = VisitApplyData.Get(options2);
                    v = new VisitApplyPersonRecord(visitApplyPerson.VisitApplyPersonID, (int)visitApplyPerson.Gender, visitApplyPerson.Mobile, visitApplyPerson.CarNo, visitApplyPerson.CompanyName,
                        visitApplyPerson.CleanEduDate?.ToString("yyyy-MM-dd")??"", visitApplyPerson.VisitorEduDate?.ToString("yyyy-MM-dd")??"", visitApply.PlaceCodeID, visitApply.Location);
                }
            }
            return Json(v);
        }
    }

    internal record VisitorRecord(int Gender, string Mobile, string? CarNo, string? CompanyName, string CleanEduLastDate, string SafetyEduLastDate, string IsBlackList);
    internal record VisitApplyPersonRecord(int VisitApplyPersonID, int Gender, string Mobile, string? CarNo, string? CompanyName, string CleanEduDate, string VisitorEduDate, int PlaceCodeID, string Location);
}
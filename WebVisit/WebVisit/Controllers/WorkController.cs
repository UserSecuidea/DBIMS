using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using ClosedXML.Excel;
using WebVisit.Models;
using WebVisit.Models.DomainModels.MySQL;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class WorkController : BaseController
    {
        // private Repository<Person>? PersonData { get; set; }

        //공사신청 
        private Repository<WorkApply>? WorkApplyData { get; set; }
        private Repository<WorkApplyAttachFile>? WorkApplyAttachFileData { get; set; }
        private Repository<WorkApplyHistory>? WorkApplyHistoryData { get; set; }
        
        //공사출입신청 
        private Repository<WorkVisitApply>? WorkVisitApplyData { get; set; }
        private Repository<WorkVisitApplyHistory>? WorkVisitApplyHistoryData { get; set; }
        private Repository<WorkVisitPersonApply>? WorkVisitPersonApplyData { get; set; }
        private Repository<WorkVisitPersonApplyHistory>? WorkVisitPersonApplyHistoryData { get; set; }

        //휴대물품신청 
        private Repository<CarryItemApply>? CarryItemApplyData { get; set; }
        private Repository<CarryItemApplyHistory>? CarryItemApplyHistoryData { get; set; }
        private Repository<CarryItemInfo>? CarryItemInfoData { get; set; }  
        private Repository<CarryItemInfoHistory>? CarryItemInfoHistoryData { get; set; }        

        // 안전교육신청 
        private Repository<SafetyEdu>? SafetyEduData { get; set; }
        private Repository<SafetyEduHistory>? SafetyEduHistoryData { get; set; }
        private Repository<SafetyEduApply>? SafetyEduApplyData { get; set; }
        private Repository<SafetyEduApplyHistory>? SafetyEduApplyHistoryData { get; set; }

        //안전수칙위반 
        private Repository<SafetyViolation>? SafetyViolationData { get; set; }
        private Repository<SafetyViolationHistory>? SafetyViolationHistoryData { get; set; }
        private Repository<SafetyViolationPerson>? SafetyViolationPersonData { get; set; }
        private Repository<SafetyViolationPersonHistory>? SafetyViolationPersonHistoryData { get; set; }

        // 안전수칙위반사유
        private Repository<SafetyViolationReason>? SafetyViolationReasonData { get; set; }
        private Repository<SafetyViolationReasonHistory>? SafetyViolationReasonHistoryData { get; set; }

        // 방문객 
        private Repository<VisitPerson>? VisitPersonData { get; set; }
        private Repository<VisitApply>? VisitApplyData { get; set; }
        private Repository<VisitApplyHistory>? VisitApplyHistoryData { get; set; }
        private Repository<VisitApplyPerson>? VisitApplyPersonData { get; set; }
        private Repository<VisitApplyPersonHistory>? VisitApplyPersonHistoryData { get; set; }

        private List<WorkAttachFileRecord> WorkAttachFileDefinitions { get; set; } = new();

        public WorkController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer){}
        
        protected override void Init(){
            if (DbSVISIT != null) {
                // PersonData = new Repository<Person>(db);

                WorkApplyData ??= new Repository<WorkApply>(DbSVISIT);
                WorkApplyAttachFileData ??= new Repository<WorkApplyAttachFile>(DbSVISIT);
                WorkApplyHistoryData ??= new Repository<WorkApplyHistory>(DbSVISIT);

                WorkVisitApplyData ??= new Repository<WorkVisitApply>(DbSVISIT);
                WorkVisitApplyHistoryData ??= new Repository<WorkVisitApplyHistory>(DbSVISIT);
                WorkVisitPersonApplyData ??= new Repository<WorkVisitPersonApply>(DbSVISIT);
                WorkVisitPersonApplyHistoryData ??= new Repository<WorkVisitPersonApplyHistory>(DbSVISIT);

                CarryItemApplyData ??= new Repository<CarryItemApply>(DbSVISIT);
                CarryItemApplyHistoryData ??= new Repository<CarryItemApplyHistory>(DbSVISIT);
                CarryItemInfoData ??= new Repository<CarryItemInfo>(DbSVISIT);
                CarryItemInfoHistoryData ??= new Repository<CarryItemInfoHistory>(DbSVISIT);

                SafetyEduData ??= new Repository<SafetyEdu>(DbSVISIT);
                SafetyEduHistoryData ??= new Repository<SafetyEduHistory>(DbSVISIT);
                SafetyEduApplyData ??= new Repository<SafetyEduApply>(DbSVISIT);
                SafetyEduApplyHistoryData ??= new Repository<SafetyEduApplyHistory>(DbSVISIT);

                SafetyViolationData ??= new Repository<SafetyViolation>(DbSVISIT);
                SafetyViolationHistoryData ??= new Repository<SafetyViolationHistory>(DbSVISIT);
                SafetyViolationPersonData ??= new Repository<SafetyViolationPerson>(DbSVISIT);
                SafetyViolationPersonHistoryData ??= new Repository<SafetyViolationPersonHistory>(DbSVISIT);

                SafetyViolationReasonData ??= new Repository<SafetyViolationReason>(DbSVISIT);
                SafetyViolationReasonHistoryData ??= new Repository<SafetyViolationReasonHistory>(DbSVISIT);

                VisitPersonData ??= new Repository<VisitPerson>(DbSVISIT);
                VisitApplyData ??= new Repository<VisitApply>(DbSVISIT);
                VisitApplyHistoryData ??= new Repository<VisitApplyHistory>(DbSVISIT);
                VisitApplyPersonData ??= new Repository<VisitApplyPerson>(DbSVISIT);
                VisitApplyPersonHistoryData ??= new Repository<VisitApplyPersonHistory>(DbSVISIT);
            }
            //List<WorkAttachFileRecord>
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(1, "협력업체 공사 등록신청서", "File1"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(2, "1.공사공정표", "File2"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(3, "2.반입기계기구List", "File3"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(4, "3.공사시방서", "File4"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(5, "4.작업공정별 예상위험 및 대책", "File5"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(6, "5.안전서약서", "File6"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(7, "6.산재납입증명원", "File7"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(8, "7.산업안전보건관리비 사용계획서", "File8"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(9, "8.위험성평가(FMEA)", "File9"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(10, "8.위험성평가(JSA)", "File10"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(11, "9.외부업체 출입자명단", "File11"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(12, "10. 안전관리자 선임계", "File12"));
            WorkAttachFileDefinitions.Add(new WorkAttachFileRecord(13, "11. 현장대리인계", "File13"));
        }

        private string GetWorkAttachFileTitle(string fileElementName){
            foreach(var m in WorkAttachFileDefinitions){
                if (m.FileElementName.Equals(fileElementName)){
                    return m.Title;
                }
            }
            return "";
        }

        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            return RedirectToAction("List");
        }
        
        /// <summary>
        /// 공사등록현황
        /// 비상주업체에서 공사등록 - 통문관리시스템
        /// 담당자가 공사신청 – 통문관리시스템
        /// ESH팀에서 공사승인 – 전자결재
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}")]
        public IActionResult List (WorkApplyGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();

            int maxWorkApplyCntByToday = GetWorkApplyNo();
            String strWorkApplyNo = "P" + DateTime.Now.ToString("yyyyMMdd") + Convert.ToString(maxWorkApplyCntByToday).PadLeft(4, '0');
            WriteLog("strWorkApplyNo:"+strWorkApplyNo + "/ "+maxWorkApplyCntByToday);
            WorkApplyListViewModel vm = new();
            // WorkApplyStatus 공사신청상태: 승인대기(0)/승인완료(1)/반려(2)
            if (WorkApplyData != null){
                WorkApplyGridData filterhValue = (WorkApplyGridData) FilterGridData(values);
                vm =new(){
                    WorkApplys = GetWorkList(filterhValue),
                    CodeWorkApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkApplyStatus), 
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CurrentRoute = values,
                    SearchRoute=filterhValue,
                    TotalPages = values.GetTotalPages(WorkApplyData.Count),
                    TotalCnt = WorkApplyData.Count,
                };
                // WriteLog("WorkApplys: "+Dump(WorkApplyData.List(options)));
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}")]
        public IActionResult? ExcelDownload(WorkApplyGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("사업장"), new DataColumn("공사번호"), new DataColumn("승인구분"),
                                        new DataColumn("공사기간"), new DataColumn("공사명"), new DataColumn("담당자명"), new DataColumn("담당부서"), });
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            WorkApplyGridData filterhValue = (WorkApplyGridData) FilterGridData(values);
            WriteLog("WorkApplyGridData:"+Dump(filterhValue));
            var v = GetWorkList(filterhValue);

            List<CommonCode> CodeWorkApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkApplyStatus);
            // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            // List<CommonCode> CodePersonStatus = GetCommonCodes((int)VisitEnum.CommonCode.PersonStatus);
            if(v != null){
                // 사업장	승인구분	방문일	입문	출문	회사명	성명	방문목적	담당자명	안전교육	차량번호	방문상태	출입증번호
                string location = string.Empty;
                string workApplyStatus = string.Empty;
                foreach(var workApply in v){
                    location = "";
                    workApplyStatus = "";

                    if (workApply.Location != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(workApply.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    if (workApply.WorkApplyStatus >= 0 && CodeWorkApplyStatus != null) {
                        foreach(var m in CodeWorkApplyStatus) {
                            if (m.SubCodeID == workApply.WorkApplyStatus) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    workApplyStatus = m.CodeNameKor;
                                }else {
                                    workApplyStatus = m.CodeNameEng??"";
                                }
                            }
                        }
                    }
                    dt.Rows.Add(location, workApply.WorkApplyNo, workApplyStatus, workApply.WorkStartDate+" ~ "+workApply.WorkEndDate, workApply.WorkName, workApply.ContactName, CultureInfo.CurrentCulture.ToString().Equals("ko") ? workApply.ContactOrgNameKor : workApply.ContactOrgNameEng);
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

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}")]
        public IActionResult Reg (WorkApplyGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            ViewBag.WorkAttachFileDefinitions = WorkAttachFileDefinitions;
            WorkApplyRegViewModel vm =new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location), // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}")]
        public async Task<IActionResult> Reg(WorkApplyGridData values, string mode, WorkApply workApply, IFormFile FormFile1 )
        {
            if(IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            // WriteLog("mode: "+mode+", workApply:"+Dump(workApply));
            if (mode.Equals("A") && WorkApplyData != null && WorkApplyHistoryData != null && WorkApplyAttachFileData != null) // 공사 신청 추가
            {
                PersonDTO my = GetMe();
                //공사번호 생성: P202306250002
                int maxWorkApplyCntByToday = GetWorkApplyNo();
                String strWorkApplyNo = "P" + DateTime.Now.ToString("yyyyMMdd") + Convert.ToString(maxWorkApplyCntByToday).PadLeft(4, '0');
                //공사신청 등록 
                workApply.WorkApplyNo = strWorkApplyNo;
                if(workApply.WorkContractFormFile != null && workApply.WorkContractFormFile.Count > 0){
                    FileData fileData = await GetFileData(workApply.WorkContractFormFile[0]);
                    workApply.WorkContractFileDataHash = fileData.Hash;
                    if (!String.IsNullOrEmpty(fileData.Meta)) {
                        workApply.WorkContractFileData = fileData.Meta;
                    }
                }
                // WorkContractFileData = WorkContractFileData, //공사계약서(첨부파일)
                // WorkContractFileDataHash = WorkContractFileDataHash, //공사계약서(첨부파일Hash)
                workApply.WorkApplyStatus = 0; //공사신청상태

                workApply.InsertSabun = my.Sabun; //등록자
                workApply.InsertName = my.Name;
                workApply.InsertOrgID = my.OrgID;
                workApply.InsertOrgNameKor = my.OrgNameKor;
                workApply.InsertOrgNameEng = my.OrgNameEng;
                workApply.InsertDate = DateTime.Now;
                WorkApplyData.Insert(workApply);
                WorkApplyData.Save();
                // WriteLog("workApply: "+Dump(workApply));
                if(workApply.WorkApplyID > 0){                    //히스토리 저장 
                    WorkApplyHistory workApplyHistory = new()
                    {
                        WorkApplyID = workApply.WorkApplyID,
                        WorkApplyNo = workApply.WorkApplyNo, //공사번호 P202306250002
                        Location = workApply.Location, //사업장구분
                        ContactSabun = workApply.ContactSabun, //회원사번(담당자)
                        ContactName = workApply.ContactName, //회원이름 (담당자)
                        ContactOrgID = workApply.ContactOrgID, //부서ID(담당자)
                        ContactOrgNameKor = workApply.ContactOrgNameKor, //부서명Kor(담당자)
                        ContactOrgNameEng = workApply.ContactOrgNameEng, //부서명Eng(담당자)
                        WorkName = workApply.WorkName, //공사명
                        WorkMemo = workApply.WorkMemo, //공사설명
                        WorkStartDate = workApply.WorkStartDate, //공사시작일
                        WorkEndDate = workApply.WorkEndDate, //공사종료일
                        // WorkContractFileData = WorkContractFileData, //공사계약서(첨부파일)
                        // WorkContractFileDataHash = WorkContractFileDataHash, //공사계약서(첨부파일Hash)
                        WorkApplyStatus = 0, //공사신청상태
                        
                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    WorkApplyHistoryData.Insert(workApplyHistory);
                    WorkApplyHistoryData.Save();
                }

                //첨부파일 등록
                if(workApply.FormFile1 != null && workApply.FormFile1.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile1[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[0].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 1: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile2 != null && workApply.FormFile2.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile2[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[1].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 2: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile3 != null && workApply.FormFile3.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile3[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[2].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 3: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile4 != null && workApply.FormFile4.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile4[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[3].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 4: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile5 != null && workApply.FormFile5.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile5[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[4].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 5: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile6 != null && workApply.FormFile6.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile6[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[5].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 6: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile7 != null && workApply.FormFile7.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile7[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[6].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 7: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile8 != null && workApply.FormFile8.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile8[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[7].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 8: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile9 != null && workApply.FormFile9.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile9[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[8].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 9: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile10 != null && workApply.FormFile10.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile10[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[9].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 10: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile11 != null && workApply.FormFile11.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile11[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[10].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 11: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile12 != null && workApply.FormFile12.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile12[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[11].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 12: "+workApplyAttachFile.Title);
                    }
                }
                if(workApply.FormFile13 != null && workApply.FormFile13.Count > 0){
                    FileData fileData = await GetFileData(workApply.FormFile13[0]);
                    if (fileData.ErrorCode > 0) {
                        WorkApplyAttachFile workApplyAttachFile = new()
                        {
                            WorkApplyID = workApply.WorkApplyID, //공사신청번호
                            Title = WorkAttachFileDefinitions[12].Title,
                            AttachFileData = fileData.Meta,  //공사관련(첨부파일)
                            AttachFileDataHash = fileData.Hash,//공사관련(첨부파일Hash)
                            InsertDate = DateTime.Now
                        };
                        WorkApplyAttachFileData.Insert(workApplyAttachFile);
                        WorkApplyAttachFileData.Save();
                        WriteLog("workApplyAttachFile 13: "+workApplyAttachFile.Title);
                    }
                }
            } // end if mode
            // WriteLog("end...");
            // return new EmptyResult();
            return RedirectToAction("List", new { culture=GetLang() });          
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}")]
        public IActionResult Detail (WorkApplyGridData values, int workApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            // WriteLog("workApplyID:" + workApplyID + ", slug" + slug);
            WorkApplyDetailViewModel vm = new();
            ViewBag.IsApproval = IsMaster();
            if (WorkApplyData != null && WorkApplyAttachFileData != null){
                var workApply = WorkApplyData.Get(new QueryOptions<WorkApply>{
                    Where = a => a.WorkApplyID == workApplyID,
                }) ?? new WorkApply();
                var options = new QueryOptions<WorkApplyAttachFile>
                {
                    Where = a => a.WorkApplyID == workApplyID && a.IsDelete == "N",
                };
               if(ViewBag.IsApproval == false){
                    var my = GetMe();
                    if(IsGeneralManager() || IsESH()){
                        if(workApply.Location.Equals(my.Location)){
                            ViewBag.IsApproval = true;
                        }
                    } else if(IsEmployeeOnly() || IsHR()){
                        if(workApply.ContactSabun.Equals(my.Sabun)){
                            ViewBag.IsApproval = true;
                        }
                    }
               }
               
                vm = new(){
                    WorkApply = workApply,
                    WorkApplyAttachFiles = WorkApplyAttachFileData.List(options), 
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeWorkApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkApplyStatus),                
                };
                // WriteLog("WorkApplyAttachFiles:" + Dump(vm.WorkApplyAttachFiles));
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}")]
        public IActionResult Detail(WorkApplyGridData values, string mode, int workApplyID, int WorkApplyStatus)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // WriteLog("mode: "+mode+", workApplyID: "+workApplyID+",WorkApplyStatus: "+WorkApplyStatus);
            if (mode.Equals("E") && WorkApplyData != null && WorkApplyHistoryData != null) // 공사신청 상태 수정
            {
                PersonDTO my = GetMe();
                if(workApplyID > 0){

                    var orgObj = GetWorkApply(workApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.WorkApplyStatus = WorkApplyStatus; // 공사신청상태 WorkApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                    newObj.UpdateDate = DateTime.Now;
                    WorkApplyData.Update(newObj);
                    WorkApplyData.Save();

                    //히스토리 저장 
                    WorkApplyHistory workApplyHistory = new()
                    {
                        WorkApplyID = workApplyID,
                        WorkApplyNo = orgObj.WorkApplyNo, //공사번호 P202306250002
                        Location = orgObj.Location, //사업장구분
                        ContactSabun = orgObj.ContactSabun, //회원사번(담당자)
                        ContactName = orgObj.ContactName, //회원이름 (담당자)
                        ContactOrgID = orgObj.ContactOrgID, //부서ID(담당자)
                        ContactOrgNameKor = orgObj.ContactOrgNameKor, //부서명Kor(담당자)
                        ContactOrgNameEng = orgObj.ContactOrgNameEng, //부서명Eng(담당자)
                        WorkName = orgObj.WorkName, //공사명
                        WorkMemo = orgObj.WorkMemo, //공사설명
                        WorkStartDate = orgObj.WorkStartDate, //공사시작일
                        WorkEndDate = orgObj.WorkEndDate, //공사종료일
                        // WorkContractFileData = WorkContractFileData, //공사계약서(첨부파일)
                        // WorkContractFileDataHash = WorkContractFileDataHash, //공사계약서(첨부파일Hash)
                        WorkApplyStatus = WorkApplyStatus, // 수정할때 공사신청상태 값 등록 
                        
                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    WorkApplyHistoryData.Insert(workApplyHistory);
                    WorkApplyHistoryData.Save();
                }
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                dict.Add("workApplyID", workApplyID.ToString());
                return RedirectToAction("Detail", dict);
            }
            return RedirectToAction("Detail", new { workApplyID, culture=GetLang()});
        }

        /// <summary>
        /// 공사신청현황
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public IActionResult VisitList (WorkVisitApplyGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsSecurity = IsSecurity();
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsSecurity() || IsPartnerNonresidentManager();
            ViewBag.Registable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsPartnerNonresidentManager();
            ViewBag.Readable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH()|| IsSecurity() || IsPartnerNonresidentManager();
            ViewBag.IsEditable =IsMaster() || IsSecurity();
            // WorkVisitApplyStatus 공사출입신청상태: 승인대기(0)/승인완료(1)/반려(2)
            // VisitStatus 방문상태: 승인대기(0)/승인완료(1)/반려(2)
            // WorkApply: 사업장 승인 공사기간 공사명 담당자명	
            // VisitApplyPerson: 회사명 성명 방문객입문교육 방문상태
            WorkVisitApplyGridData filterhValue = (WorkVisitApplyGridData) FilterGridData(values);
            WorkVisitApplyListViewModel vm =new(){
                CodeWorkVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkVisitApplyStatus), 
                CodeWorkApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkApplyStatus), 
                CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus), 
                CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus), 
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location), 
                CurrentRoute = values,
                SearchRoute = filterhValue,
            };
            var m = GetWorkVisitApplyPersonList(filterhValue);
            // WriteLog("m:"+Dump(m));
            if(m != null){
                ViewBag.workVisitApplyPersonList = m.a;
                vm.TotalPages = values.GetTotalPages(m.b);
                vm.TotalCnt = m.b;
            }            
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public async Task<IActionResult> VisitListAsync (WorkVisitApplyGridData values, int workVisitApplyID, int visitApplyPersonID, int VisitStatus, String CardNo, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // WriteLog("mode: "+mode+", visitApplyPersonID: "+visitApplyPersonID+", VisitStatus: "+VisitStatus+", CardNo: "+CardNo);
            if(mode == "EVisitStatus"){
                var today = DateTime.Now;
                var my = GetMe();
                if(visitApplyPersonID > 0) {
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
                    newObj2.UpdateDate = DateTime.Now;
                    VisitApplyPersonData.Update(newObj2);
                    VisitApplyPersonData.Save();

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
            return RedirectToAction("VisitList", new { culture = GetLang() });
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public IActionResult? ExcelDownloadVisit(WorkVisitApplyGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            // 사업장	승인	공사기간	회사명	성명	공사명	담당자명	방문객입문교육	방문상태	출입증번호	
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("사업장"), new DataColumn("승인"), new DataColumn("공사기간"),
                                        new DataColumn("회사명"),new DataColumn("성명"), new DataColumn("공사명"), new DataColumn("담당자명")
                                        , new DataColumn("방문객입문교육"), new DataColumn("방문상태"), new DataColumn("출입증번호"), });
            PersonDTO my = GetMe();
            WorkVisitApplyGridData filterhValue = (WorkVisitApplyGridData) FilterGridData(values);
            WriteLog("WorkVisitApplyGridData:"+Dump(filterhValue));
            var v = GetWorkVisitApplyPersonList(filterhValue, true);

            List<CommonCode> CodeWorkVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkVisitApplyStatus);
            List<CommonCode> CodeWorkApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkApplyStatus);
            List<CommonCode> CodeVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitApplyStatus);
            List< CommonCode > CodeVisitStatus = GetCommonCodes((int)VisitEnum.CommonCode.VisitStatus);
            // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            if(v != null){
                // 사업장	승인구분	방문일	입문	출문	회사명	성명	방문목적	담당자명	안전교육	차량번호	방문상태	출입증번호
                foreach (var m in v.a){
                    var location = ""; 
                    var workName = "";
                    var contactName = "";
                    var visitApplyStatusName = "";
                    var workPeriod = "";
                    int workApplyStatus = -1;
                    // var workApplyStatusName = "";
                    int visitApplyStatus = -1;
                    string? VisitStatusName = "";
                    location = m.b.Location;
                    workName = m.b.WorkName;
                    contactName = m.b.ContactName;
                    workApplyStatus = m.b.WorkApplyStatus; // 현재는 공사신청 승인 공사출입신청 승인이 아닌지 체크 필요 
                    workPeriod = m.b.WorkStartDate +"~"+m.b.WorkEndDate;
                    // if (m.b.Count > 0){
                    //     location = m.b[0].Location;
                    //     workName = m.b[0].WorkName;
                    //     contactName = m.b[0].ContactName;
                    //     workApplyStatus = m.b[0].WorkApplyStatus; // 현재는 공사신청 승인 공사출입신청 승인이 아닌지 체크 필요 
                    //     workPeriod = m.b[0].WorkStartDate +"~"+m.b[0].WorkEndDate;
                    // }
                    if (location != null && CodeLocation != null) {
                        foreach(var a in CodeLocation) {
                            if (a.SubCodeDesc != null && a.SubCodeDesc.Equals(location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = a.CodeNameKor;
                                }else {
                                    location = a.CodeNameEng;
                                }
                            }
                        }
                    } 
                    //  공사신청상태 
                    // if (workApplyStatus >= 0 && CodeWorkApplyStatus != null) {
                    //     foreach(var a in CodeWorkApplyStatus) {
                    //         if (a.SubCodeID != null && a.SubCodeID.Equals(workApplyStatus)) {
                    //             if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                    //                 workApplyStatusName = a.CodeNameKor;
                    //             }else {
                    //                 workApplyStatusName = a.CodeNameEng;
                    //             }
                    //         }
                    //     }
                    // }
                    foreach(var a in CodeVisitStatus){
                        if (a.SubCodeID != null && a.SubCodeID.Equals(m.a.VisitStatus)) {
                            if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                VisitStatusName = a.CodeNameKor;
                            }else {
                                VisitStatusName = a.CodeNameEng;
                            }
                        }                        
                    }
                    // 방문신청상태
                    visitApplyStatus = m.a.VisitApplyStatus;
                    if (visitApplyStatus >= 0 && CodeVisitApplyStatus != null) {
                        foreach(var a in CodeVisitApplyStatus) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(visitApplyStatus)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    visitApplyStatusName = a.CodeNameKor;
                                }else {
                                    visitApplyStatusName = a.CodeNameEng;
                                }
                            }
                        }
                    }
                    dt.Rows.Add(location, visitApplyStatusName, workPeriod, m.a.CompanyName, m.a.Name, workName, contactName, m.a.IsSafetyEdu, VisitStatusName,m.a.CardNo);
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

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public IActionResult VisitReg(string WorkApplyNo)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            //공사출입신청
            // 1. 리스트에서 신청페이지 접근 
            // 2. 공사상세보기페이지에서 접근 (WorkApplyNo) <- 공사번호를 이용하여 공사정보 가져오기             
            ViewBag.WorkApplyNo = WorkApplyNo;
            WorkVisitApplyRegViewModel vm =new(){
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
                CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
            };
            return View(vm);
        }

        /// <summary>
        /// 공사 신청 DB 입력
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="workApplyID"></param>
        /// <param name="WorkDate"></param>
        /// <param name="pVisitDTO"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public IActionResult VisitReg(string mode, int workApplyID, String WorkDate, VisitDTO pVisitDTO)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mode: "+mode+", workApplyID: "+workApplyID);

            if (mode.Equals("AW") && pVisitDTO != null && CompanyData != null && VisitApplyData != null && VisitApplyHistoryData != null
                && WorkVisitApplyData != null && WorkVisitApplyHistoryData != null && WorkVisitPersonApplyData != null && WorkVisitPersonApplyHistoryData != null
                && VisitApplyPersonData != null && VisitApplyPersonHistoryData != null && CarryItemInfoData != null){ // 공사출입신청 추가
                /*
                1. 공사출입신청 (WorkVisitApply) 정보 입력 
                1.1 공사출입신청 히스토리(WorkVisitApplyHistory) 입력 
                2. 방문신청(VisitApply) 정보 입력 
                2.1 방문신청 히스토리(VisitApplyHistory) 입력 
                3. 공사출입인원들의 회원정보(Person) 가져오기 (arr)
                4. 공사출입인원신청 (WorkVisitPersonApply) (arr)
                4.1 공사출입인원신청 히스토리 (WorkVisitPersonApplyHistory) 입력
                5. 방문신청회원 (VisitApplyPerson) => 공사신청회원 만큼 입력 (arr) 
                5.1 방문신청회원 히스토리 (VisitApplyPersonHistory) 입력
                6. 휴대물품신청 (CarryItemApply) 정보 입력 (arr)
                6.1 휴대물품신청 히스토리 (CarryItemApplyHistory) 정보 입력 (arr)
                8. 휴대물품정보 입력 (arr)
                */
                //로그인 사용자 
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                // WriteLog("pVisitDTO: "+Dump(pVisitDTO));
                pVisitDTO.SetVisitApply();
                pVisitDTO.SetVisitApplyPerson();

                //공사정보 가져오기 
                var workApplyObj = GetWorkApply(workApplyID, true);

                // 1. 공사출입신청 (WorkVisitApply) 정보 입력 
                WorkVisitApply workVisitApply = new()
                {
                    WorkApplyID = workApplyID, //공사신청ID
                    WorkDate = WorkDate, //공사일
                    WorkVisitApplyStatus = 0, //공사출입신청상태
                    InsertSabun = my.Sabun, //등록자
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = today,
                };
                WorkVisitApplyData.Insert(workVisitApply);
                WorkVisitApplyData.Save();
                WriteLog("workVisitApply: "+Dump(workVisitApply));

                // 1.1 공사출입신청 히스토리 입력 
                WorkVisitApplyHistory workVisitApplyHistory = new()
                {
                    WorkVisitApplyID = workVisitApply.WorkVisitApplyID, //공사출입신청ID
                    WorkApplyID = workVisitApply.WorkApplyID, //공사신청ID
                    WorkDate = workVisitApply.WorkDate, //공사일
                    WorkVisitApplyStatus = workVisitApply.WorkVisitApplyStatus, //공사출입신청상태
                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = today
                };
                WorkVisitApplyHistoryData.Insert(workVisitApplyHistory);
                WorkVisitApplyHistoryData.Save();
                // WriteLog("workVisitApplyHistory: "+Dump(workVisitApplyHistory));

                // 2. 방문신청(VisitApply) 정보 입력
                if (pVisitDTO.VisitApply != null && workApplyObj != null) {
                    pVisitDTO.VisitApply.Location = workApplyObj.Location; //사업장구분 ->공사등록시 선택한 사업장 
                    pVisitDTO.VisitApply.VisitStartDate = WorkDate; //방문시작일->공사일
                    pVisitDTO.VisitApply.VisitEndDate = WorkDate; //방문종료일->공사일
                    pVisitDTO.VisitApply.PlaceCodeID = -1; //장소코드ID - 공사에는 방문 장소 코드 선택 UI가 없으므로 
                    pVisitDTO.VisitApply.VisitPurposeCodeID = 1; //방문목적코드ID: 공사,수리 Setup(1)
                    pVisitDTO.VisitApply.ContactSabun = workApplyObj.ContactSabun; //담당자 공사등록시 담당자 정보 
                    pVisitDTO.VisitApply.ContactName = workApplyObj.ContactName;
                    pVisitDTO.VisitApply.ContactOrgID = workApplyObj.ContactOrgID;
                    pVisitDTO.VisitApply.ContactOrgNameKor = workApplyObj.ContactOrgNameKor;
                    pVisitDTO.VisitApply.ContactOrgNameEng = workApplyObj.ContactOrgNameEng;
                    pVisitDTO.VisitApply.VisitApplyType = 1; // 방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/방문객수기등록(3)
                    pVisitDTO.VisitApply.WorkApplyID = workApplyID; // 공사신청ID
                    pVisitDTO.VisitApply.WorkVisitApplyID = workVisitApply.WorkVisitApplyID; // 공사출입신청ID
                    // pVisitDTO.VisitApply.SafetyEduID = SafetyEduID, // 안전교육신청ID <- 안전교육 신청시 사용 
                    pVisitDTO.VisitApply.VisitApplyStatus = 0; // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    // Purpose = Purpose, // 상세목적 <- 방문객수기등록에서 사용 
                    pVisitDTO.VisitApply.RegVisitorType = 0; // 방문신청자구분(등록자): 임직원(0)/방문자(1) 
                    // VisitPersonID = VisitPersonID, // 방문객회원ID(등록자) 메인 방문 신청시 사용 
                    pVisitDTO.VisitApply.InsertSabun = my.Sabun;//등록자가 방문자가 아닐경우 회원정보 입력 
                    pVisitDTO.VisitApply.InsertName = my.Name;
                    pVisitDTO.VisitApply.InsertOrgID = my.OrgID;
                    pVisitDTO.VisitApply.InsertOrgNameKor = my.OrgNameKor;
                    pVisitDTO.VisitApply.InsertOrgNameEng = my.OrgNameEng;
                    pVisitDTO.VisitApply.InsertDate = today;
                    WriteLog("visitApply: "+Dump(pVisitDTO.VisitApply));
                    VisitApplyData.Insert(pVisitDTO.VisitApply);
                    VisitApplyData.Save();

                    // 2.1 방문신청 히스토리 입력 
                    VisitApplyHistory visitApplyHistory = new()
                    {
                        VisitApplyID= pVisitDTO.VisitApply.VisitApplyID, //방문신청ID
                        Location = pVisitDTO.VisitApply.Location, //사업장구분 ->공사등록시 선택한 사업장 
                        VisitStartDate = pVisitDTO.VisitApply.VisitStartDate, //방문시작일->공사일
                        VisitEndDate = pVisitDTO.VisitApply.VisitEndDate, //방문종료일->공사일
                        PlaceCodeID = pVisitDTO.VisitApply.PlaceCodeID, //장소코드ID
                        VisitPurposeCodeID = pVisitDTO.VisitApply.VisitPurposeCodeID, //방문목적코드ID to-do: 공사출입인원신청 시 출입지역No와 방문목적No 입력 확인! 
                        ContactSabun = pVisitDTO.VisitApply.ContactSabun, //담당자 
                        ContactName = pVisitDTO.VisitApply.ContactName,
                        ContactOrgID = pVisitDTO.VisitApply.ContactOrgID,
                        ContactOrgNameKor = pVisitDTO.VisitApply.ContactOrgNameKor,
                        ContactOrgNameEng = pVisitDTO.VisitApply.ContactOrgNameEng,
                        VisitApplyType = pVisitDTO.VisitApply.VisitApplyType, // 방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/방문객수기등록(3)
                        WorkApplyID = pVisitDTO.VisitApply.WorkApplyID, // 공사신청ID
                        WorkVisitApplyID = pVisitDTO.VisitApply.WorkVisitApplyID, // 공사출입신청ID
                        VisitApplyStatus = pVisitDTO.VisitApply.VisitApplyStatus, // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                        RegVisitorType = pVisitDTO.VisitApply.RegVisitorType, // 방문신청자구분(등록자): 임직원(0)/방문자(1) 
                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    VisitApplyHistoryData.Insert(visitApplyHistory);
                    VisitApplyHistoryData.Save();
                    WriteLog("visitApplyHistory: "+Dump(visitApplyHistory));
                }
                
                //* 여기에선 VisitPerson 테이블에는 입력하지 않음.
                foreach (VisitApplyPerson m in pVisitDTO.VisitApplyPersonList)
                {
                    WriteLog("VisitApplyPerson: "+Dump(m));
                    if (m != null && m.Sabun != null)
                    {
                        // 3. 공사출입인원들의 회원정보(Person) 가져오기 (arr)
                        var person = GetPerson(-1, m.Sabun, true);
                        // var vPerson = GetVisitPerson(m.Name, m.BirthDate);

                        // 4. 공사출입인원신청 (WorkVisitPersonApply) (arr)
                        WorkVisitPersonApply workVisitPersonApply = new()
                        {
                            WorkVisitApplyID = workVisitApply.WorkVisitApplyID, //공사출입신청ID
                            WorkVisitApplyStatus = 0, //공사출입신청상태
                            Sabun = person.Sabun, //회원사번(출입자)
                            Name = person.Name, //회원이름(출입자)
                            OrgID = person.OrgID, //부서ID(출입자)
                            OrgNameKor = person.OrgNameKor, //부서명Kor(출입자)
                            OrgNameEng = person.OrgNameEng, //부서명Eng(출입자)
                            InsertDate = today,
                        };
                        WorkVisitPersonApplyData.Insert(workVisitPersonApply);
                        WorkVisitPersonApplyData.Save();
                        WriteLog("workVisitPersonApply: "+Dump(workVisitPersonApply));

                        // 4.1 공사출입인원신청 히스토리 입력 
                        WorkVisitPersonApplyHistory workVisitPersonApplyHistory = new()
                        {
                            WorkVisitPersonApplyID = workVisitPersonApply.WorkVisitPersonApplyID, //공사출입인원신청ID
                            WorkVisitApplyID = workVisitPersonApply.WorkVisitApplyID, //공사출입신청ID
                            WorkVisitApplyStatus = workVisitPersonApply.WorkVisitApplyStatus, //공사출입신청상태
                            Sabun = person.Sabun, //회원사번(출입자)
                            Name = person.Name, //회원이름(출입자)
                            OrgID = person.OrgID, //부서ID(출입자)
                            OrgNameKor = person.OrgNameKor, //부서명Kor(출입자)
                            OrgNameEng = person.OrgNameEng, //부서명Eng(출입자)
                            InsertUpdateDate = today,
                        };
                        WorkVisitPersonApplyHistoryData.Insert(workVisitPersonApplyHistory);
                        WorkVisitPersonApplyHistoryData.Save();
                        WriteLog("workVisitPersonApplyHistory: "+Dump(workVisitPersonApplyHistory));
                        // 5. 방문신청회원 (VisitApplyPerson ) => 공사신청회원 만큼 입력  arr 
                        VisitApplyPerson visitApplyPerson = new()
                        {
                            VisitApplyID = pVisitDTO.VisitApply.VisitApplyID, // 방문신청ID
                            VisitDate = WorkDate, // 방문일->공사일
                            VisitorType = 0, // 방문자구분: 임직원(0)-방문자가 아닌 비상주업체관리자 또는 비상주사원 /방문자(1) , 공사출입신청은 비상주만 가능
                            // VisitPersonID = visitPersonID, // 방문객회원ID
                            BirthDate = person.BirthDate, // 생년월일
                            Gender = person.Gender, // 성별
                            Mobile = person.Mobile, // 휴대전화
                            CompanyName = m.CompanyName,                            
                            Sabun = person.Sabun, // 회원사번(방문자)
                            Name = person.Name, // 회원이름(방문자)
                            OrgID = person.OrgID, // 부서ID(방문자)
                            OrgNameKor = person.OrgNameKor, // 부서명Kor(방문자)
                            OrgNameEng = person.OrgNameEng, // 부서명Eng(방문자)
                            OrderNo = 0, //신청자와 신청자가 추가한 방문자 정렬 순서 
                            WorkApplyID = workApplyID, // 공사신청ID
                            WorkVisitApplyID = workVisitApply.WorkVisitApplyID, // 공사출입신청ID
                            IsVisitorEdu = m.IsVisitorEdu, //방문객입문교육여부 -> 공사신청시 방문객입문교육 받은 비상주사원만 등록 가능 
                            VisitorEduDate = person.VisitorEduLastDate, //방문객입문교육시청일시
                            IsCleanEdu = m.IsCleanEdu, // 클린룸교육여부 -> 공사신청시 클린룸교육 받은 비상주사원만 등록 가능 
                            CleanEduDate = person.CleanEduLastDate, //  클린룸교육시청일시
                            // CleanEduScore = CleanEduScore, // 클린룸교육점수
                            IsSafetyEdu = m.IsSafetyEdu, // 안전교육여부 -> 공사신청시 안전교육 받은 비상주사원만 등록 가능 
                            SafetyEduDate = person.SafetyEduLastDate, //  안전교육이수일
                            // CarNo = CarNo, // 차량번호 
                            IsTermsPrivarcy = "Y", // 개인정보수집동의여부 -> 공사신청시 개인정보수집동의여부 받은 비상주사원만 등록 가능 
                            VisitApplyStatus = 0, // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                            VisitStatus = 0, //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                            // CardID = CardID, // 출입증ID
                            // CardNo = CardNo, // 출입증번호
                            // IsVIP = IsVIP, // VIP여부
                            // VipTypeCodeID = VipTypeCodeID, // VIP구분코드ID
                            InsertSabun = my.Sabun,
                            InsertName = my.Name,
                            InsertOrgID = my.OrgID,
                            InsertOrgNameKor = my.OrgNameKor,
                            InsertOrgNameEng = my.OrgNameEng,
                            InsertDate = today,
                        };
                        VisitApplyPersonData.Insert(visitApplyPerson);
                        VisitApplyPersonData.Save();
                        WriteLog("visitApplyPerson: "+Dump(visitApplyPerson));

                        // 5.1 방문신청회원 히스토리 입력
                        VisitApplyPersonHistory visitApplyPersonHistory = new()
                        {
                            VisitApplyPersonID = visitApplyPerson.VisitApplyPersonID, // 방문신청회원ID
                            VisitApplyID = pVisitDTO.VisitApply.VisitApplyID, // 방문신청ID
                            VisitDate = WorkDate, // 방문일->공사일
                            VisitorType = 0, // 방문자구분: 임직원(0)-방문자가 아닌 비상주업체관리자 또는 비상주사원 /방문자(1) , 공사출입신청은 비상주만 가능
                            // VisitPersonID = visitPersonID, // 방문객회원ID
                            BirthDate = person.BirthDate, // 생년월일
                            Gender = person.Gender, // 성별
                            Mobile = person.Mobile, // 휴대전화
                            CompanyName = m.CompanyName,                            
                            Sabun = person.Sabun, // 회원사번(방문자)
                            Name = person.Name, // 회원이름(방문자)
                            OrgID = person.OrgID, // 부서ID(방문자)
                            OrgNameKor = person.OrgNameKor, // 부서명Kor(방문자)
                            OrgNameEng = person.OrgNameEng, // 부서명Eng(방문자)
                            OrderNo = 0, //신청자와 신청자가 추가한 방문자 정렬 순서 

                            WorkApplyID = workApplyID, // 공사신청ID
                            WorkVisitApplyID = workVisitApply.WorkVisitApplyID, // 공사출입신청ID

                            IsVisitorEdu = m.IsVisitorEdu, //방문객입문교육여부 -> 공사신청시 방문객입문교육 받은 비상주사원만 등록 가능 
                            VisitorEduDate = person.VisitorEduLastDate, //방문객입문교육시청일시
                            IsCleanEdu = m.IsCleanEdu, // 클린룸교육여부 -> 공사신청시 클린룸교육 받은 비상주사원만 등록 가능 
                            CleanEduDate = person.CleanEduLastDate, //  클린룸교육시청일시
                            // CleanEduScore = CleanEduScore, // 클린룸교육점수
                            IsSafetyEdu = m.IsSafetyEdu, // 안전교육여부 -> 공사신청시 안전교육 받은 비상주사원만 등록 가능 
                            SafetyEduDate = person.SafetyEduLastDate, //  안전교육이수일
                            // CarNo = CarNo, // 차량번호 
                            IsTermsPrivarcy = "Y", // 개인정보수집동의여부 -> 공사신청시 개인정보수집동의여부 받은 비상주사원만 등록 가능 
                            VisitApplyStatus = visitApplyPerson.VisitApplyStatus, // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                            VisitStatus = visitApplyPerson.VisitStatus, //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                            // CardID = CardID, // 출입증ID
                            // CardNo = CardNo, // 출입증번호
                            // IsVIP = IsVIP, // VIP여부
                            // VipTypeCodeID = VipTypeCodeID, // VIP구분코드ID
                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = today
                        };
                        VisitApplyPersonHistoryData.Insert(visitApplyPersonHistory);
                        VisitApplyPersonHistoryData.Save();
                        WriteLog("visitApplyPersonHistory: "+Dump(visitApplyPersonHistory));

                        if(CarryItemApplyData != null && CarryItemApplyHistoryData != null && CarryItemInfoData != null){
                            // 6. 휴대물품신청 (CarryItemApply) 정보 입력 
                            var VisitApplyPersonID = m.VisitApplyPersonID;
                            CarryItemDTO? CarryItemDTO = m.CarryItemDTO;
                            if (CarryItemDTO != null){
                                // InsertSabun InsertName InsertOrgID InsertOrgNameKor InsertOrgNameEng InsertDate InsertVisitorType InsertVisitPersonID
                                CarryItemDTO.CarryItemApply.CarryVisitorType = 0;
                                CarryItemDTO.CarryItemApply.ImportPurposeCodeID = 1; // 공사, 수리
                                // CarryItemDTO.CarryItemApply.VisitPersonID = vPerson.VisitPersonID;
                                CarryItemDTO.CarryItemApply.Sabun = person.Sabun;
                                CarryItemDTO.CarryItemApply.Name = person.Name;
                                CarryItemDTO.CarryItemApply.OrgID = person.OrgID;
                                CarryItemDTO.CarryItemApply.OrgNameKor= person.OrgNameKor;
                                CarryItemDTO.CarryItemApply.OrgNameEng= person.OrgNameEng;
                                CarryItemDTO.CarryItemApply.Mobile = person.Mobile;
                                CarryItemDTO.CarryItemApply.CompanyName = GetCompanyName(person.CompanyID??0);

                                CarryItemDTO.CarryItemApply.InsertVisitorType = 0; // 방문자구분(등록자) 회원(0)/방문자(1)
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

                                // 6.1 휴대물품신청 히스토리 (CarryItemApplyHistory) 정보 입력
                                CarryItemDTO.CarryItemApplyHistory.CarryItemApplyID = CarryItemDTO.CarryItemApply.CarryItemApplyID;
                                CarryItemDTO.CarryItemApplyHistory.CarryVisitorType = 0;
                                CarryItemDTO.CarryItemApplyHistory.ImportPurposeCodeID = 1; // 공사, 수리
                                // CarryItemDTO.CarryItemApplyHistory.VisitPersonID = vPerson.VisitPersonID;
                                CarryItemDTO.CarryItemApplyHistory.Sabun = person.Sabun;
                                CarryItemDTO.CarryItemApplyHistory.Name = person.Name;
                                CarryItemDTO.CarryItemApplyHistory.OrgID = person.OrgID;
                                CarryItemDTO.CarryItemApplyHistory.OrgNameKor= person.OrgNameKor;
                                CarryItemDTO.CarryItemApplyHistory.OrgNameEng= person.OrgNameEng;
                                CarryItemDTO.CarryItemApplyHistory.Mobile = person.Mobile;
                                CarryItemDTO.CarryItemApplyHistory.CompanyName = GetCompanyName(person.CompanyID??0);
                                
                                CarryItemDTO.CarryItemApplyHistory.UpdateVisitorType = 0; // 방문자구분(등록자) 회원(0)/방문자(1)
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

                                // 7. 휴대물품정보 입력 (arr): 휴대물품 리스트 등록 CarryItemInfoData
                                if (CarryItemDTO.CarryItemInfoList != null){
                                    foreach(var m3 in CarryItemDTO.CarryItemInfoList){
                                        m3.CarryItemApplyID = CarryItemDTO.CarryItemApply.CarryItemApplyID;
                                        m3.RemainCnt = m3.Quantity; // 남은 수량에 수량을 입력
                                        m3.InsertDate = today;
                                        CarryItemInfoData.Insert(m3);
                                        WriteLog("CarryItemInfoData: "+Dump(m3));
                                        CarryItemInfoData.Save();
                                        // 7.1 휴대물품정보 입력 히스토리 (arr): 휴대물품 리스트 등록 CarryItemInfoData
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
                                                Rank = CarryItemDTO.CarryItemApply.GradeName, // 직책
                                                TelNum = CarryItemDTO.CarryItemApply.Tel, // 전화번호
                                                CellPhone = CarryItemDTO.CarryItemApply.Mobile, // 핸드폰 번호
                                            };
                                            DbMySQL.TUserAdds.Add(userAdd);
                                            DbMySQL.SaveChanges();
                                        }
                                    } // end foreach
                                } // end if CarryItemDTO.CarryItemInfoList != null
                            } // end if CarryItemDTO != null
                        } // end if CarryItemApplyData != null
                    }//end if m!= null
                }
            }
            // return new EmptyResult();
            return RedirectToAction("VisitList", new { culture=GetLang() }); 
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public IActionResult VisitDetail (int workVisitApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("workVisitApplyID:" + workVisitApplyID + ", slug" + slug);
            ViewBag.IsModApproval = IsMaster() || IsGeneralManager() || IsESH();
            var workVisitApply = WorkVisitApplyData.Get(new QueryOptions<WorkVisitApply>{
                Where = a => a.WorkVisitApplyID == workVisitApplyID,
            }) ?? new WorkVisitApply();
            //
            var workApplyID = workVisitApply.WorkApplyID;
            var workApply = WorkApplyData.Get(new QueryOptions<WorkApply>{
                Where = a => a.WorkApplyID == workApplyID,
            }) ?? new WorkApply();
            //공사출입인원정보 가져오기 
            // VisitApply: VisitApplyID, WorkApplyID
            // VisitApplyPerson: VisitApplyPersonID	VisitApplyID Name BirthDate	Gender Mobile CompanyName OrgNameKor Sabun IsVisitorEdu	IsCleanEdu	IsSafetyEdu CarNo
            
            WorkVisitApplyDetailViewModel vm = new();
            var m = GetWorkVisitApplyPersonListByWorkApplyID(workApplyID, workVisitApplyID);
            if(m != null){
                ViewBag.workVisitApplyPersonList = m;
                // var options = new QueryOptions<WorkVisitPersonApply>
                // {
                //     Where = a => a.WorkVisitApplyID == workVisitApplyID && a.IsDelete == "N",
                // };
                vm = new(){
                    WorkApply = workApply,
                    WorkVisitApply = workVisitApply,
                    // WorkVisitPersonApplys = workVisitPersonApplyData.List(options), 
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                    CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                    // WorkVisitApplyStatus 공사출입신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    CodeWorkVisitApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.WorkVisitApplyStatus), 
                };
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchworkvisitapplystatus?}/{searchvisitstatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchcontactname?}/{searchcardno?}")]
        public IActionResult VisitDetail(string mode, int workVisitApplyID, int WorkVisitApplyStatus)
        {
            WriteLog("mode: "+mode+", workVisitApplyID: "+workVisitApplyID+", WorkVisitApplyStatus: "+WorkVisitApplyStatus);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("E")) // 공사출입신청 상태 수정
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;

                if(workVisitApplyID > 0 && WorkVisitApplyData != null && WorkVisitApplyHistoryData != null
                    && VisitApplyData != null && WorkVisitPersonApplyHistoryData != null
                    && VisitApplyPersonData != null && VisitApplyPersonHistoryData != null
                    && WorkVisitPersonApplyData!= null && WorkVisitPersonApplyHistoryData != null)
                {
                    /*
                    1. 공사출입신청(WorkVisitApply) 공사출입신청상태(WorkVisitApplyStatus) 변경 - 1 row 
                    1.1 공사출입신청히스토리 (WorkVisitApplyHistory) 입력 
                    2. 공사출입인원신청 (WorkVisitPersonApply) 공사출입신청상태(WorkVisitApplyStatus) 변경 - n row 
                    2.1 공사출입인원신청히스토리 (WorkVisitPersonApplyHistory) 입력 
                    3. 방문신청(VisitApply) 방문신청상태(VisitApplyStatus) 변경 - 1 row 
                    3.1 방문신청히스토리 (VisitApplyHistory) 입력 
                    4. 방문신청회원(VisitApplyPerson) 방문신청상태(VisitApplyStatus) 변경 - n row 
                    4.1 방문신청회원히스토리 (VisitApplyPersonHistory) 입력
                    */

                    // 1. 공사출입신청(WorkVisitApply) 공사출입신청상태(WorkVisitApplyStatus) 변경 
                    var orgWorkVisitApplyObj = GetWorkVisitApply(workVisitApplyID, true);
                    var newWorkVisitApplyObj = orgWorkVisitApplyObj.Clone();
                    
                    newWorkVisitApplyObj.WorkVisitApplyStatus = WorkVisitApplyStatus; // 공사출입신청상태 WorkVisitApplyStatus : 0 승인대기, 1 승인완료, 2 반려
                    
                    newWorkVisitApplyObj.UpdateDate = today;
                    WorkVisitApplyData.Update(newWorkVisitApplyObj);
                    WorkVisitApplyData.Save();

                    // 1.1 공사출입신청히스토리 (WorkVisitApplyHistory) 입력
                    WorkVisitApplyHistory workVisitApplyHistory = new()
                    {
                        WorkVisitApplyID = orgWorkVisitApplyObj.WorkVisitApplyID, //공사출입신청ID
                        WorkApplyID = orgWorkVisitApplyObj.WorkApplyID, //공사신청ID

                        WorkDate = orgWorkVisitApplyObj.WorkDate, //공사일
                        WorkVisitApplyStatus = WorkVisitApplyStatus, // 수정시 공사출입신청상태 값 등록 

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = today
                    };
                    WorkVisitApplyHistoryData.Insert(workVisitApplyHistory);
                    WorkVisitApplyHistoryData.Save();
                    WriteLog("workVisitApplyHistory: "+Dump(workVisitApplyHistory));

                    // 2. 공사출입인원신청 (WorkVisitPersonApply) 공사출입신청상태(WorkVisitApplyStatus) 변경 - n row 
                    var options2 = new QueryOptions<WorkVisitPersonApply>
                    {
                        Where = a =>a.WorkVisitApplyID == workVisitApplyID && a.IsDelete == "N",
                    };
                    List<WorkVisitPersonApply> v2= (List<WorkVisitPersonApply>) WorkVisitPersonApplyData.List(options2);
                    foreach(var m in v2) {
                        m.WorkVisitApplyStatus = WorkVisitApplyStatus;
                        m.UpdateDate = today;
                        WorkVisitPersonApplyData?.Update(m);
                        WorkVisitPersonApplyData?.Save();

                        // 2.1 공사출입인원신청히스토리 (WorkVisitPersonApplyHistory) 입력 
                        WorkVisitPersonApplyHistory workVisitPersonApplyHistory = new() {
                            WorkVisitPersonApplyID = m.WorkVisitPersonApplyID,
                            WorkVisitApplyID = m.WorkVisitApplyID,
                            WorkVisitApplyStatus = WorkVisitApplyStatus,
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

                    // 3. 방문신청(VisitApply) 방문신청상태(VisitApplyStatus) 변경 - 1 row 
                    var options3 = new QueryOptions<VisitApply>
                    {
                        Where = a =>a.WorkVisitApplyID == workVisitApplyID && a.VisitApplyType == 1 && a.IsDelete == "N", // 공사 신청
                    };
                    VisitApply v3 = VisitApplyData.Get(options3);
                    v3.VisitApplyStatus = WorkVisitApplyStatus; // 공사출입신청상태 변경 시 방문신청상태 함께 변경  WorkVisitApplyStatus : 0 승인대기, 1 승인완료, 2 반려
                    v3.UpdateDate = today;
                    VisitApplyData.Update(v3);
                    VisitApplyData.Save();

                    // 3.1 방문신청히스토리 (VisitApplyHistory) 입력
                    VisitApplyHistory visitApplyHistory = new()
                    {
                        VisitApplyID= v3.VisitApplyID, //방문신청ID
                        Location = v3.Location, //사업장구분 ->공사등록시 선택한 사업장 
                        VisitStartDate = v3.VisitStartDate, //방문시작일->공사일
                        VisitEndDate = v3.VisitEndDate, //방문종료일->공사일
                        // PlaceCodeID = PlaceCodeID, //장소코드ID to-do: 공사출입인원신청 시 출입지역No와 방문목적No 입력 확인! 
                        // VisitPurposeCodeID = VisitPurposeCodeID, //방문목적코드ID to-do: 공사출입인원신청 시 출입지역No와 방문목적No 입력 확인! 

                        ContactSabun = v3.ContactSabun, //담당자 
                        ContactName = v3.ContactName,
                        ContactOrgID = v3.ContactOrgID,
                        ContactOrgNameKor = v3.ContactOrgNameKor,
                        ContactOrgNameEng = v3.ContactOrgNameEng,

                        VisitApplyType = v3.VisitApplyType, // 방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/방문객수기등록(3)
                        WorkApplyID = v3.WorkApplyID, // 공사신청ID
                        WorkVisitApplyID = v3.WorkVisitApplyID, // 공사출입신청ID
                        // SafetyEduID = SafetyEduID, // 안전교육신청ID <- 안전교육 신청시 사용 
                        
                        VisitApplyStatus = WorkVisitApplyStatus, // 공사출입신청상태 WorkVisitApplyStatus : 0 승인대기, 1 승인완료, 2 반려
                        
                        // Purpose = Purpose, // 상세목적 <- 방문객수기등록에서 사용 
                        RegVisitorType = v3.RegVisitorType, // 방문신청자구분(등록자): 임직원(0)/방문자(1) 
                        // VisitPersonID = VisitPersonID, // 방문객회원ID(등록자) 메인 방문 신청시 사용 

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = today
                    };
                    VisitApplyHistoryData?.Insert(visitApplyHistory);
                    VisitApplyHistoryData?.Save();
                    // WriteLog("visitApplyHistory: "+Dump(visitApplyHistory));

                    // 4. 방문신청회원(VisitApplyPerson) 방문신청상태(VisitApplyStatus) 변경 - n row 
                    var options4 = new QueryOptions<VisitApplyPerson>
                    {
                        Where = a =>a.VisitApplyID == v3.VisitApplyID && a.IsDelete == "N",
                    };
                    List<VisitApplyPerson> v4= (List<VisitApplyPerson>) VisitApplyPersonData.List(options4);
                    foreach(var m in v4) {
                        m.VisitApplyStatus = WorkVisitApplyStatus;
                        m.UpdateDate = today;
                        VisitApplyPersonData?.Update(m);
                        VisitApplyPersonData?.Save();

                        // 4.1 방문신청회원히스토리 (VisitApplyPersonHistory) 입력
                        VisitApplyPersonHistory workVisitPersonApplyHistory = new() {
                            VisitApplyPersonID = m.VisitApplyPersonID,
                            VisitApplyID = m.VisitApplyID,
                            VisitDate = m.VisitDate,
                            VisitPersonID = m.VisitPersonID,
                            BirthDate = m.BirthDate,
                            Gender = m.Gender,
                            Mobile = m.Mobile,
                            CompanyName = m.CompanyName,
                            Sabun = m.Sabun,
                            Name = m.Name,
                            OrgID = m.OrgID,
                            OrgNameKor = m.OrgNameKor,
                            OrgNameEng = m.OrgNameEng,
                            OrderNo = m.OrderNo,
                            WorkApplyID = m.WorkApplyID,
                            WorkVisitApplyID = m.WorkVisitApplyID,
                            IsVisitorEdu = m.IsVisitorEdu,
                            VisitorEduDate = m.VisitorEduDate,
                            IsCleanEdu = m.IsCleanEdu,
                            CleanEduDate = m.CleanEduDate,
                            CleanEduScore = m.CleanEduScore,
                            IsSafetyEdu = m.IsSafetyEdu,
                            SafetyEduDate = m.SafetyEduDate,
                            CarNo = m.CarNo,
                            IsTermsPrivarcy = m.IsTermsPrivarcy,
                            CardID = m.CardID,
                            CardNo = m.CardNo,
                            IsVIP = m.IsVIP,
                            VipTypeCodeID = m.VipTypeCodeID,
                            EntryDate = m.EntryDate,
                            ExitDate = m.ExitDate,
                            CleanEntryDate = m.CleanEntryDate,
                            CleanExitDate = m.CleanExitDate,
                            UpdateVisitorType = 0, // 입력자 0
                            UpdateVisitPersonID = 0,
                            UpdateSabun = my.Sabun,
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            VisitApplyStatus = WorkVisitApplyStatus,
                            VisitStatus = m.VisitStatus,
                            InsertUpdateDate = today
                        };
                        VisitApplyPersonHistoryData.Insert(workVisitPersonApplyHistory);
                        VisitApplyPersonHistoryData?.Save();
                    }
                }
            }
            return RedirectToAction("VisitDetail", new { workVisitApplyID, culture=GetLang()});
        }

        /// <summary>
        /// 안전교육 현황
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searcheducationcompletestatus?}/{searchcompanyname?}/{searchstartedudate?}/{searchendedudate?}/{searchname?}")]
        public IActionResult SafetyEduList (SafetyEduGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity() || IsPartnerNonresidentManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            ViewBag.Registable = IsMaster() || IsGeneralManager() || IsESH() || IsPartnerNonresidentManager();
            ViewBag.Readable = IsMaster() || IsGeneralManager() || IsESH() || IsPartnerNonresidentManager();;
            var options = new QueryOptions<SafetyEdu>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = values.SortDirection,
                Where = a => a.IsDelete == "N",
            };
            SafetyEduGridData filterhValue = (SafetyEduGridData) FilterGridData(values);
            // EducationCompleteStatus 교육이수상태: 미이수(0)/이수(1)
            SafetyEduListViewModel vm =new();
            var m = GetSafetyEduPersonList(filterhValue);
            WriteLog("m:"+Dump(m));
            if(m != null && SafetyEduData != null){
                ViewBag.safetyEduPersonList = m.a;
                vm =new(){
                    CodeEducationCompleteStatus = GetCommonCodes((int)VisitEnum.CommonCode.EducationCompleteStatus),
                    CurrentRoute = values,
                    SearchRoute = filterhValue,
                    TotalPages = values.GetTotalPages(m.b),
                    TotalCnt = m.b,
                };
            }
            return View(vm);
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searcheducationcompletestatus?}/{searchcompanyname?}/{searchstartedudate?}/{searchendedudate?}/{searchname?}")]
        public IActionResult SafetyEduReg (SafetyEduGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }

            SafetyEduRegViewModel vm =new(){
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeWorkTypeCode = GetCommonCodes((int)VisitEnum.CommonCode.WorkTypeCode),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searcheducationcompletestatus?}/{searchcompanyname?}/{searchstartedudate?}/{searchendedudate?}/{searchname?}")]
        public IActionResult ExcelDownloadSafetyEdu(SafetyEduGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            // 회사명	성명	휴대전화	이수상태	교육일자	유효기간
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("회사명"),new DataColumn("성명"), new DataColumn("휴대전화"), new DataColumn("이수상태")
                                        , new DataColumn("교육일자"), new DataColumn("유효기간"), });
            PersonDTO my = GetMe();
            SafetyEduGridData filterhValue = (SafetyEduGridData) FilterGridData(values);
            WriteLog("SafetyEduGridData:"+Dump(filterhValue));
            var v = GetSafetyEduPersonList(filterhValue, true);

            // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            List<CommonCode> CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType);
            List< CommonCode > CodeWorkTypeCode = GetCommonCodes((int)VisitEnum.CommonCode.WorkTypeCode);
            List<CommonCode> CodeEducationCompleteStatus = GetCommonCodes((int)VisitEnum.CommonCode.EducationCompleteStatus);
            if(v != null){
                int SafetyEduApplyID = -1;
                int eduCompleteStatus = -1;
                var eduCompleteStatusName = "";
                string eduDate="";
                string validDate="";
                // 회사명	성명	휴대전화	이수상태	교육일자	유효기간
                foreach (var m in v.a){
                    SafetyEduApplyID = -1;
                    eduCompleteStatus = -1;
                    eduCompleteStatusName = "";
                    eduDate="";
                    validDate="";
                    SafetyEduApplyID = m.c.b.SafetyEduApplyID;
                    eduCompleteStatus = m.c.b.EduCompleteStatus;
                    if(m.c.b.EduDate != null){
                        eduDate=string.Format("{0:yyyy-MM-dd}", m.c.b.EduDate);
                    }
                    if(m.c.b.ValidDate != null){
                        validDate=string.Format("{0:yyyy-MM-dd}", m.c.b.ValidDate);
                    }
                    if (eduCompleteStatus >= 0 && CodeEducationCompleteStatus != null) {
                        foreach(var a in CodeEducationCompleteStatus) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(eduCompleteStatus)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    eduCompleteStatusName = a.CodeNameKor;
                                }else {
                                    eduCompleteStatusName = a.CodeNameEng;
                                }
                            }
                        }
                    } 
                    dt.Rows.Add(m.c.a.CompanyName, m.c.a.Name, m.c.a.Mobile, eduCompleteStatusName, eduDate, validDate);
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

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searcheducationcompletestatus?}/{searchcompanyname?}/{searchstartedudate?}/{searchendedudate?}/{searchname?}")]
        public IActionResult SafetyEduReg(SafetyEduGridData values, string mode, int workApplyID, String EduDate, VisitDTO pVisitDTO)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            // WriteLog("mode: "+mode+", EduDate: "+EduDate);
            if (mode.Equals("AW")) // 안전교육 신청 추가
            {
                /*
                1. 안전교육신청(SafetyEdu) 등록 
                1.1 안전교육신청 히스토리 입력 
                2. 방문신청(VisitApply) 정보 입력
                2.1 방문신청 히스토리 입력                 visitApply
                3. 안전교육신청 인원들의 회원정보 가져오기 (arr) 
                4. 안전교육인원신청 (arr)
                4.1 안전교육인원신청 히스토리 입력 
                5. 방문신청회원 (VisitApplyPerson ) => 안전교육신청 회원  arr 
                5.1 방문신청회원 히스토리 입력 
                */
                //로그인 사용자 
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                pVisitDTO.SetVisitApply();
                pVisitDTO.SetVisitApplyPerson();

                //공사정보 가져오기 
                var workApplyObj = GetWorkApply(workApplyID, true);
                
                // 1. 안전교육신청(SafetyEdu) 등록 
                SafetyEdu safetyEdu = new()
                {
                    WorkApplyID = workApplyID, // 공사신청ID
                    EduDate = EduDate, //교육일자
                    EduApplyStatus = 0, //교육신청상태
                    InsertSabun = my.Sabun, //등록자
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = today
                };
                SafetyEduData.Insert(safetyEdu);
                SafetyEduData.Save();
                WriteLog("safetyEdu: "+Dump(safetyEdu));   
                
                // 1.1 안전교육신청 히스토리 입력 
                SafetyEduHistory safetyEduHistory = new()
                {
                    SafetyEduID = safetyEdu.SafetyEduID, //안전교육신청ID 
                    WorkApplyID = workApplyID, // 공사신청ID
                    EduDate = EduDate, //교육일자
                    EduApplyStatus = 0, //교육신청상태
                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = today
                };
                SafetyEduHistoryData.Insert(safetyEduHistory);
                SafetyEduHistoryData.Save();
                WriteLog("safetyEduHistory: "+Dump(safetyEduHistory));   

                // 2. 방문신청(VisitApply) 정보 입력 
                if (pVisitDTO.VisitApply != null && workApplyObj != null) {
                    pVisitDTO.VisitApply.Location = workApplyObj.Location; //사업장구분 ->공사등록시 선택한 사업장 
                    pVisitDTO.VisitApply.VisitStartDate = EduDate; //방문시작일->공사일
                    pVisitDTO.VisitApply.VisitEndDate = EduDate; //방문종료일->공사일
                    pVisitDTO.VisitApply.PlaceCodeID = -1; //장소코드ID - 공사에는 방문 장소 코드 선택 UI가 없으므로 
                    pVisitDTO.VisitApply.VisitPurposeCodeID = 3; //방문목적코드ID: 교육(3)

                    pVisitDTO.VisitApply.ContactSabun = workApplyObj.ContactSabun; //담당자 공사등록시 담당자 정보 
                    pVisitDTO.VisitApply.ContactName = workApplyObj.ContactName;
                    pVisitDTO.VisitApply.ContactOrgID = workApplyObj.ContactOrgID;
                    pVisitDTO.VisitApply.ContactOrgNameKor = workApplyObj.ContactOrgNameKor;
                    pVisitDTO.VisitApply.ContactOrgNameEng = workApplyObj.ContactOrgNameEng;

                    pVisitDTO.VisitApply.VisitApplyType = 2; // 방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/방문객수기등록(3)
                    // pVisitDTO.VisitApply.WorkApplyID = workApplyID; // 공사신청ID
                    // pVisitDTO.VisitApply.WorkVisitApplyID = workVisitApply.WorkVisitApplyID; // 공사출입신청ID
                    pVisitDTO.VisitApply.SafetyEduID = safetyEdu.SafetyEduID; // 안전교육신청ID <- 안전교육 신청시 사용
                    pVisitDTO.VisitApply.VisitApplyStatus = 0; // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                    // Purpose = Purpose, // 상세목적 <- 방문객수기등록에서 사용 
                    pVisitDTO.VisitApply.RegVisitorType = 0; // 방문신청자구분(등록자): 임직원(0)/방문자(1) 
                    // VisitPersonID = VisitPersonID, // 방문객회원ID(등록자) 메인 방문 신청시 사용 
                    pVisitDTO.VisitApply.InsertSabun = my.Sabun;//등록자가 방문자가 아닐경우 회원정보 입력 
                    pVisitDTO.VisitApply.InsertName = my.Name;
                    pVisitDTO.VisitApply.InsertOrgID = my.OrgID;
                    pVisitDTO.VisitApply.InsertOrgNameKor = my.OrgNameKor;
                    pVisitDTO.VisitApply.InsertOrgNameEng = my.OrgNameEng;
                    pVisitDTO.VisitApply.InsertDate = today;
                    WriteLog("visitApply: "+Dump(pVisitDTO.VisitApply));
                    VisitApplyData.Insert(pVisitDTO.VisitApply);
                    VisitApplyData.Save();

                    // 2.1 방문신청 히스토리 입력 
                    VisitApplyHistory visitApplyHistory = new()
                    {
                        VisitApplyID= pVisitDTO.VisitApply.VisitApplyID, //방문신청ID
                        Location = pVisitDTO.VisitApply.Location, //사업장구분 ->공사등록시 선택한 사업장 
                        VisitStartDate = pVisitDTO.VisitApply.VisitStartDate, //방문시작일->공사일
                        VisitEndDate = pVisitDTO.VisitApply.VisitEndDate, //방문종료일->공사일
                        PlaceCodeID = pVisitDTO.VisitApply.PlaceCodeID, //장소코드ID to-do: 공사출입인원신청 시 출입지역No와 방문목적No 입력 확인! 
                        VisitPurposeCodeID = pVisitDTO.VisitApply.VisitPurposeCodeID, //방문목적코드ID to-do: 공사출입인원신청 시 출입지역No와 방문목적No 입력 확인! 
                        ContactSabun = pVisitDTO.VisitApply.ContactSabun, //담당자 
                        ContactName = pVisitDTO.VisitApply.ContactName,
                        ContactOrgID = pVisitDTO.VisitApply.ContactOrgID,
                        ContactOrgNameKor = pVisitDTO.VisitApply.ContactOrgNameKor,
                        ContactOrgNameEng = pVisitDTO.VisitApply.ContactOrgNameEng,
                        VisitApplyType = pVisitDTO.VisitApply.VisitApplyType, // 방문신청구분: 방문신청(0)/공사출입인원신청(1)/안전교육(2)/방문객수기등록(3)
                        // WorkApplyID = pVisitDTO.VisitApply.WorkApplyID, // 공사신청ID
                        // WorkVisitApplyID = pVisitDTO.VisitApply.WorkVisitApplyID, // 공사출입신청ID
                        SafetyEduID = pVisitDTO.VisitApply.SafetyEduID,// 안전교육신청ID 
                        VisitApplyStatus = pVisitDTO.VisitApply.VisitApplyStatus, // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                        RegVisitorType = pVisitDTO.VisitApply.RegVisitorType, // 방문신청자구분(등록자): 임직원(0)/방문자(1) 
                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = today
                    };
                    VisitApplyHistoryData.Insert(visitApplyHistory);
                    VisitApplyHistoryData.Save();
                    WriteLog("visitApplyHistory: "+Dump(visitApplyHistory));
                }

                // 3. 안전교육신청 인원들의 회원정보 가져오기 (arr)
                foreach (VisitApplyPerson m in pVisitDTO.VisitApplyPersonList)
                {
                    // WriteLog("VisitApplyPerson: " + Dump(m));
                    if (m != null && m.Sabun != null)
                    {
                        // 3. 공사출입인원들의 회원정보(Person) 가져오기 (arr)
                        var person = GetPerson(-1, m.Sabun, true);

                        // 4. 안전교육인원신청(SafetyEduApply) (arr)
                        SafetyEduApply safetyEduApply = new()
                        {
                            SafetyEduID = safetyEdu.SafetyEduID, // 안전교육신청ID
                            EduCompleteStatus = 0, // 교육이수상태: 미이수(0), 이수(1)
                            // EduDate = DateTime.Now, // to-do: 교육 이수 후에 교육이수일시 입력 (EduDate 값 필수 처리 되어있어 DB 수정 후 주석 처리 )
                            // ValidDate = ValidDate, // to-do: 교육 이수 후에 유효기간 입력 
                            Sabun = person.Sabun, //회원사번(교육받는자)
                            Name = person.Name, //회원이름(교육받는자)
                            OrgID = person.OrgID, //부서ID(교육받는자)
                            OrgNameKor = person.OrgNameKor, //부서명Kor(교육받는자)
                            OrgNameEng = person.OrgNameEng, //부서명Eng(교육받는자)
                            InsertDate = today,
                        };
                        SafetyEduApplyData.Insert(safetyEduApply);
                        SafetyEduApplyData.Save();
                        WriteLog("safetyEduApply: " + Dump(safetyEduApply));

                        // 4.1 안전교육인원신청 히스토리 입력  (arr)
                        SafetyEduApplyHistory safetyEduApplyHistory = new()
                        {
                            SafetyEduApplyID = safetyEduApply.SafetyEduApplyID, //안전교육인원신청ID 
                            SafetyEduID = safetyEduApply.SafetyEduID, // 안전교육신청ID
                            EduCompleteStatus = safetyEduApply.EduCompleteStatus, // 교육이수상태: 미이수(0), 이수(1)
                            // EduDate = DateTime.Now, // to-do: 교육 이수 후에 교육이수일시 입력 (EduDate 값 필수 처리 되어있어 DB 수정 후 주석 처리 )
                            // ValidDate = ValidDate, // to-do: 교육 이수 후에 유효기간 입력 
                            Sabun = safetyEduApply.Sabun, //회원사번(교육받는자)
                            Name = safetyEduApply.Name, //회원이름(교육받는자)
                            OrgID = safetyEduApply.OrgID, //부서ID(교육받는자)
                            OrgNameKor = safetyEduApply.OrgNameKor, //부서명Kor(교육받는자)
                            OrgNameEng = safetyEduApply.OrgNameEng, //부서명Eng(교육받는자)
                            InsertUpdateDate = today,
                        };
                        SafetyEduApplyHistoryData.Insert(safetyEduApplyHistory);
                        SafetyEduApplyHistoryData.Save();
                        WriteLog("safetyEduApplyHistory: " + Dump(safetyEduApplyHistory));

                        // 5. 방문신청회원 (VisitApplyPerson ) => 안전교육신청 회원  arr 
                        VisitApplyPerson visitApplyPerson = new()
                        {
                            VisitApplyID = pVisitDTO.VisitApply.VisitApplyID, // 방문신청ID
                            VisitDate = EduDate, // 방문일->안전교육일
                            VisitorType = 0, // 방문자구분: 임직원(0)-방문자가 아닌 비상주업체관리자 또는 비상주사원 /방문자(1) , 공사출입신청은 비상주만 가능
                            // VisitPersonID = visitPersonID, // 방문객회원ID
                            BirthDate = person.BirthDate, // 생년월일
                            Gender = person.Gender, // 성별
                            Mobile = person.Mobile, // 휴대전화
                            CompanyName = m.CompanyName,
                            Sabun = person.Sabun, // 회원사번(방문자)
                            Name = person.Name, // 회원이름(방문자)
                            OrgID = person.OrgID, // 부서ID(방문자)
                            OrgNameKor = person.OrgNameKor, // 부서명Kor(방문자)
                            OrgNameEng = person.OrgNameEng, // 부서명Eng(방문자)
                            OrderNo = 0, //신청자와 신청자가 추가한 방문자 정렬 순서  
                            WorkApplyID = workApplyID, // 공사신청ID
                            SafetyEduID = safetyEdu.SafetyEduID, //안전교육신청ID
                            IsVisitorEdu = m.IsVisitorEdu, //방문객입문교육여부 -> 안전교육신청 회원의 방문객 입문교육 여부 확인 
                            // VisitorEduDate = person.VisitorEduLastDate, //방문객입문교육시청일시
                            IsCleanEdu = m.IsCleanEdu, // 클린룸교육여부 -> 안전교육신청 회원의 클린룸교육 여부 확인 
                            // CleanEduDate = person.CleanEduLastDate, //  클린룸교육시청일시
                            // CleanEduScore = CleanEduScore, // 클린룸교육점수
                            IsSafetyEdu = m.IsSafetyEdu, // 안전교육여부 -> 공사신청시 안전교육 받은 비상주사원만 등록 가능 
                            // SafetyEduDate = person.SafetyEduLastDate, //  안전교육이수일
                            // CarNo = CarNo, // 차량번호 
                            IsTermsPrivarcy = "Y", // 개인정보수집동의여부 -> 공사신청시 개인정보수집동의여부 받은 비상주사원만 등록 가능 
                            VisitApplyStatus = 0, // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                            VisitStatus = 0, //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                            // CardID = CardID, // 출입증ID
                            // CardNo = CardNo, // 출입증번호
                            // IsVIP = IsVIP, // VIP여부
                            // VipTypeCodeID = VipTypeCodeID, // VIP구분코드ID
                            InsertSabun = my.Sabun,
                            InsertName = my.Name,
                            InsertOrgID = my.OrgID,
                            InsertOrgNameKor = my.OrgNameKor,
                            InsertOrgNameEng = my.OrgNameEng,
                            InsertDate = today,
                        };
                        VisitApplyPersonData.Insert(visitApplyPerson);
                        VisitApplyPersonData.Save();
                        WriteLog("visitApplyPerson: " + Dump(visitApplyPerson));

                        // 5.1 방문신청회원 히스토리 입력 (VisitApplyPersonHistory) => 안전교육신청 회원  arr 
                        VisitApplyPersonHistory visitApplyPersonHistory = new()
                        {
                            VisitApplyPersonID = visitApplyPerson.VisitApplyPersonID, //방문신청회원ID                    
                            VisitApplyID = visitApplyPerson.VisitApplyID, // 방문신청ID
                            VisitDate = visitApplyPerson.VisitDate, // 방문일->안전교육일
                            VisitorType = visitApplyPerson.VisitorType, // 방문자구분: 임직원(0)-방문자가 아닌 비상주업체관리자 또는 비상주사원 /방문자(1) , 공사출입신청은 비상주만 가능
                            // VisitPersonID = visitPersonID, // 방문객회원ID
                            BirthDate = visitApplyPerson.BirthDate, // 생년월일
                            Gender = visitApplyPerson.Gender, // 성별
                            Mobile = visitApplyPerson.Mobile, // 휴대전화
                            CompanyName = m.CompanyName,
                            Sabun = visitApplyPerson.Sabun, // 회원사번(방문자)
                            Name = visitApplyPerson.Name, // 회원이름(방문자)
                            OrgID = visitApplyPerson.OrgID, // 부서ID(방문자)
                            OrgNameKor = visitApplyPerson.OrgNameKor, // 부서명Kor(방문자)
                            OrgNameEng = visitApplyPerson.OrgNameEng, // 부서명Eng(방문자)
                            OrderNo = visitApplyPerson.OrderNo, //신청자와 신청자가 추가한 방문자 정렬 순서 

                            WorkApplyID = visitApplyPerson.WorkApplyID, // 공사신청ID
                            SafetyEduID = visitApplyPerson.SafetyEduID, //안전교육신청ID

                            IsVisitorEdu = visitApplyPerson.IsVisitorEdu, //방문객입문교육여부 -> 안전교육신청 회원의 방문객 입문교육 여부 확인 
                            // VisitorEduDate = person.VisitorEduLastDate, //방문객입문교육시청일시
                            IsCleanEdu = visitApplyPerson.IsCleanEdu, // 클린룸교육여부 -> 안전교육신청 회원의 클린룸교육 여부 확인 
                            // CleanEduDate = person.CleanEduLastDate, //  클린룸교육시청일시
                            // CleanEduScore = CleanEduScore, // 클린룸교육점수
                            IsSafetyEdu = visitApplyPerson.IsSafetyEdu, // 안전교육여부 -> 공사신청시 안전교육 받은 비상주사원만 등록 가능 
                            SafetyEduDate = person.SafetyEduLastDate, //  안전교육이수일
                            // CarNo = CarNo, // 차량번호 
                            IsTermsPrivarcy = visitApplyPerson.IsTermsPrivarcy, // 개인정보수집동의여부 -> 공사신청시 개인정보수집동의여부 받은 비상주사원만 등록 가능 
                            VisitApplyStatus = visitApplyPerson.VisitApplyStatus, // 방문신청상태: 승인대기(0)/승인완료(1)/반려(2)
                            VisitStatus = visitApplyPerson.VisitStatus, //방문상태: 방문대기(0)/방문(1)/방문완료(2)/미반납(3)/방문종료(4) 
                            // CardID = CardID, // 출입증ID
                            // CardNo = CardNo, // 출입증번호
                            // IsVIP = IsVIP, // VIP여부
                            // VipTypeCodeID = VipTypeCodeID, // VIP구분코드ID
                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = today
                        };
                        VisitApplyPersonHistoryData.Insert(visitApplyPersonHistory);
                        VisitApplyPersonHistoryData.Save();
                        // WriteLog("visitApplyPersonHistory: " + Dump(visitApplyPersonHistory));
                    } // end if
                } // end foreach
            }// end if
            // return new EmptyResult();
            return RedirectToAction("SafetyEduList", new { culture=GetLang() });          
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searcheducationcompletestatus?}/{searchcompanyname?}/{searchstartedudate?}/{searchendedudate?}/{searchname?}")]
        public IActionResult SafetyEduDetail (SafetyEduGridData values,int safetyEduApplyID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            WriteLog("safetyEduApplyID:" + safetyEduApplyID + ", slug" + slug);
            //SafetyEduID 가져오기
            var safetyEduApply = SafetyEduApplyData.Get(new QueryOptions<SafetyEduApply>{
                Where = a => a.SafetyEduApplyID == safetyEduApplyID,
            }) ?? new SafetyEduApply();
            //교육일자
            var safetyEdu = SafetyEduData.Get(new QueryOptions<SafetyEdu>{
                Where = a => a.SafetyEduID == safetyEduApply.SafetyEduID,
            }) ?? new SafetyEdu();
            //공사명, 공사설명
            var workApply = WorkApplyData.Get(new QueryOptions<WorkApply>{
                Where = a => a.WorkApplyID == safetyEdu.WorkApplyID,
            }) ?? new WorkApply();

            ViewBag.IsEditable = IsMaster() || IsESH();

            //회사명, 성명, 생년월일, 성별, 휴대전화, 
            // WriteLog("safetyEdu.SafetyEduID: "+safetyEdu.SafetyEduID+ "safetyEduApply.Sabun: "+safetyEduApply.Sabun);
            var m = GetSafetyEduApplyInfo(safetyEdu.SafetyEduID, safetyEduApply.Sabun);
            WriteLog("ViewBag.safetyEduApplyInfo: "+Dump(m));
            SafetyEduDetailViewModel vm = new();
            if (m != null){
                ViewBag.safetyEduApplyInfo = m.a;
                vm = new()
                {
                    SafetyEduApply = safetyEduApply,
                    SafetyEdu = safetyEdu,
                    WorkApply = workApply,
                    CodeEducationCompleteStatus = GetCommonCodes((int)VisitEnum.CommonCode.EducationCompleteStatus),
                    CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                };
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searcheducationcompletestatus?}/{searchcompanyname?}/{searchstartedudate?}/{searchendedudate?}/{searchname?}")]
        public IActionResult SafetyEduDetail(SafetyEduGridData values, string mode, int safetyEduApplyID, int EduCompleteStatus)
        {
            WriteLog("mode: "+mode+", safetyEduApplyID: "+safetyEduApplyID+", EduCompleteStatus: "+EduCompleteStatus);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if(IsMaster() == false && IsESH() == false){
                return View("_Inaccessible");
            }

            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            if (mode.Equals("E")) // 안전교육인원 상태 수정
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                if(safetyEduApplyID > 0)
                {
                    // 1. 안전교육인원 상태 수정 
                    var orgObj = GetSafetyEduApply(safetyEduApplyID, true);
                    var newObj = orgObj.Clone();
                    newObj.EduCompleteStatus = EduCompleteStatus; // 안전교육인원 상태 EduCompleteStatus : 미이수(0)/이수(1)
                    if(EduCompleteStatus == 1){
                        newObj.EduDate = today; // 교육 이수 후에 교육이수일시 입력
                        newObj.ValidDate = DateTime.Now.AddYears(1); // 교육 이수 후에 유효기간 입력 (1년후) 
                    }
                    newObj.UpdateDate = today;
                    SafetyEduApplyData.Update(newObj);
                    SafetyEduApplyData.Save();

                    // 1.1 안전교육인원 히스토리 입력 SafetyEduApplyHistory
                    SafetyEduApplyHistory safetyEduApplyHistory = new()
                    {
                        SafetyEduApplyID = newObj.SafetyEduApplyID, //안전교육인원신청ID 

                        SafetyEduID = newObj.SafetyEduID, // 안전교육신청ID
                        EduCompleteStatus = EduCompleteStatus, // 수정시 교육이수상태값 등록 
                        
                        EduDate = today, //교육이수일시 
                        ValidDate = DateTime.Now.AddYears(1), //유효기간

                        Sabun = newObj.Sabun, //회원사번(교육받는자)
                        Name = newObj.Name, //회원이름(교육받는자)
                        OrgID = newObj.OrgID, //부서ID(교육받는자)
                        OrgNameKor = newObj.OrgNameKor, //부서명Kor(교육받는자)
                        OrgNameEng = newObj.OrgNameEng, //부서명Eng(교육받는자)

                        InsertUpdateDate = today,
                    };
                    SafetyEduApplyHistoryData.Insert(safetyEduApplyHistory);
                    SafetyEduApplyHistoryData.Save();
                    WriteLog("safetyEduApplyHistory: "+Dump(safetyEduApplyHistory));

                    //2. 사원정보에 안전교육일 등록 
                    var person = GetPerson(-1, newObj.Sabun, true);
                    var newPersonObj = person.Clone();
                    if (PersonData != null && orgObj != null){
                        newPersonObj.SafetyEduLastDate = today;
                        newPersonObj.UpdateDate = today;
                        PersonData.Update(newPersonObj);
                        PersonData.Save();
                    }
                }
            }
            return RedirectToAction("SafetyEduDetail", new { safetyEduApplyID, culture=GetLang()});
        }

        /// <summary>
        /// 비상주협력사는 자사 직원 관련하여서만 조회
        /// 마스터는 전체
        /// 일반임직원은 본인이 담당인 내용만 조회
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchsafetyviolationstatus?}/{searchsafetyviolationreasonid?}/{searchname?}/{searchcompanyname?}/{searchstartviolationdate?}/{searchendviolationdate?}")]
        public IActionResult SafetyViolationList (SafetyViolationGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsESH();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            ViewBag.Registable = IsMaster() || IsESH();
            ViewBag.Readable = IsMaster() || IsGeneralManager() || IsESH();

            SafetyViolationGridData filterhValue = (SafetyViolationGridData) FilterGridData(values);
            SafetyViolationListViewModel vm = new(){
                CodeSafetyViolationStatus = GetCommonCodes((int)VisitEnum.CommonCode.SafetyViolationStatus),
                CurrentRoute = values,
                SearchRoute=filterhValue,
            };
            if (SafetyViolationReasonData != null && SafetyViolationData != null){
                // var options = new QueryOptions<SafetyViolation>
                // {
                //     PageNumber = values.PageNumber,
                //     PageSize = values.PageSize,
                //     OrderByDirection = values.SortDirection,
                //     Where = a => a.IsDelete == "N",
                // };
                var m = GetSafetyViolationPersonList(filterhValue);

                //위반사유 리스트 가져오기 
                var options2 = new QueryOptions<SafetyViolationReason>
                {
                    Where = a => a.IsDelete == "N",
                };
                // SafetyViolationStatus 안전수칙위반상태: 0 신규발행, 1 방지대책등록, 2 방지대책승인, 3 방지대책재등록요청
                if (m != null){
                    vm.SafetyViolationReasons = SafetyViolationReasonData.List(options2);
                    vm.SafetyViolations = m.a;
                    vm.TotalPages = values.GetTotalPages(m.b);
                    vm.TotalCnt = m.b;
                }
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchsafetyviolationstatus?}/{searchsafetyviolationreasonid?}/{searchname?}/{searchcompanyname?}/{searchstartviolationdate?}/{searchendviolationdate?}")]
        public IActionResult? ExcelDownloadSafetyViolation(SafetyViolationGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            ExTitle = "IR발행"; // '/' 문자 안됨.
            DataTable dt = new(ExTitle);
            // 위반일자	발급자명	위반자명	위반자소속	위반사유	제한일수	제한기간	담당부서	결제상태
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Violation Date"]), new DataColumn(@Localizer["Issuer Name"]), new DataColumn(@Localizer["Violator Name"]),
                                        new DataColumn(@Localizer["Violator Company Name"]), new DataColumn(@Localizer["Violation Reason"]), new DataColumn(@Localizer["Limited Days"]),
                                        new DataColumn(@Localizer["Restricted Period"]),new DataColumn(@Localizer["Department Charge"]),new DataColumn(@Localizer["IR Progress Status"]), });
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            SafetyViolationGridData filterhValue = (SafetyViolationGridData) FilterGridData(values);
            var SafetyViolations = GetSafetyViolationPersonList(filterhValue);
            if (SafetyViolationReasonData != null && SafetyViolations!= null) {
                // 위반 사유
                var options2 = new QueryOptions<SafetyViolationReason>
                {
                    Where = a => a.IsDelete == "N",
                };
                var SafetyViolationReasons = SafetyViolationReasonData.List(options2);
                List<CommonCode> CodeSafetyViolationStatus = GetCommonCodes((int)VisitEnum.CommonCode.SafetyViolationStatus);
                WriteLog("SafetyViolationReasons: " + Dump(SafetyViolationReasons));
                foreach (var m in SafetyViolations.a) {
                    var safetyViolationReasonContents = "";
                    var violationPenaltyPeoriod1 = 0;
                    var violationPenaltyPeoriod2 = 0;
                    var violationPenaltyPeoriod3 = 0;
                    var violationPenaltyPeoriod = 0;
                    var SafetyViolationID = -1;
                    var ViolationDate = "";
                    var InsertName = "";
                    var InsertOrgNameKor = "";
                    var SafetyViolationStatusName = "";
                    if (m.b.SafetyViolationReasonID >= 0 && SafetyViolationReasons != null) {
                        foreach (var a in SafetyViolationReasons) {
                            if (a.SafetyViolationReasonID == m.b.SafetyViolationReasonID) {
                                safetyViolationReasonContents = a.SafetyViolationReasonContents;
                                violationPenaltyPeoriod1 = a.ViolationPenaltyPeoriod1;
                                violationPenaltyPeoriod2 = a.ViolationPenaltyPeoriod2 ?? 0;
                                violationPenaltyPeoriod3 = a.ViolationPenaltyPeoriod3 ?? 0;
                            }
                        }
                        if (m.b.SafetyViolationStatus >= 0 && CodeSafetyViolationStatus != null) {
                            foreach (var a in CodeSafetyViolationStatus) {
                                if (a.SubCodeID == m.b.SafetyViolationStatus) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        SafetyViolationStatusName = a.CodeNameKor;
                                    } else {
                                        SafetyViolationStatusName = a.CodeNameEng ?? "";
                                    }
                                }
                            }
                        }
                        SafetyViolationID = m.b.SafetyViolationID;
                        ViolationDate = m.b.ViolationDate.ToString("yyyy-MM-dd");
                        InsertName = m.b.InsertName;
                        InsertOrgNameKor = m.b.InsertOrgNameKor;
                    }
                    var StartDate = "";
                    var EndDate = "";
                    if (m.a.StartDate != null) {
                        StartDate = m.a.StartDate.ToString("yyyy-MM-dd");
                    }
                    if (m.a.EndDate != null) {
                        EndDate = m.a.EndDate.ToString("yyyy-MM-dd");
                    }
                    // 안전수칙위반자(SafetyViolationPerson )의 ViolationTime 에 따라 ViolationPenaltyPeoriod1, ViolationPenaltyPeoriod2, ViolationPenaltyPeoriod3
                    // if (m.ViolationTime == 2) {
                    //     violationPenaltyPeoriod = violationPenaltyPeoriod2;
                    // } else if (m.ViolationTime == 3) {
                    //     violationPenaltyPeoriod = violationPenaltyPeoriod3;
                    // } else {
                    //     violationPenaltyPeoriod = violationPenaltyPeoriod1;
                    // }
                    violationPenaltyPeoriod = violationPenaltyPeoriod1;
                    dt.Rows.Add(ViolationDate, InsertName, m.a.Name, m.a.CompanyName, safetyViolationReasonContents, 
                        violationPenaltyPeoriod+" "+@Localizer["Day"], StartDate+" ~ "+EndDate, InsertOrgNameKor, SafetyViolationStatusName);
                        // < tr onclick = "selectRow(this, @SafetyViolationID);" ondblclick = "goDetail(@SafetyViolationID)" >
                        //     < td > @ViolationDate </ td >
                        //     < td > @InsertName </ td >
                        //     < td > @m.a.Name </ td >
                        //     < td > @m.a.CompanyName </ td >
                        //     < td > @safetyViolationReasonContents </ td >
                        //     < td > @violationPenaltyPeoriod @Localizer["Day"] </ td >
                        //     < td > @StartDate  ~@EndDate </ td >
                        //     < td > @InsertOrgNameKor </ td >
                        //     < td > @SafetyViolationStatusName </ td >
                        // </ tr >
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            }
            return new EmptyResult();
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchsafetyviolationstatus?}/{searchsafetyviolationreasonid?}/{searchname?}/{searchcompanyname?}/{searchstartviolationdate?}/{searchendviolationdate?}")]
        public IActionResult SafetyViolationReg(SafetyViolationGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            PersonDTO my = GetMe();
            ViewBag.IsMaster = IsMaster();
            ViewBag.my = my;
            WriteLog("my:" + Dump(my));
            SafetyViolationRegViewModel vm =new(){
                Person = my,
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location), // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
            };
            if (SafetyViolationReasonData != null){ //위반사유 리스트 가져오기 
                var options = new QueryOptions<SafetyViolationReason>
                {
                    Where = a => a.IsDelete == "N",
                };
                vm.SafetyViolationReasons = SafetyViolationReasonData.List(options);
            }
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchsafetyviolationstatus?}/{searchsafetyviolationreasonid?}/{searchname?}/{searchcompanyname?}/{searchstartviolationdate?}/{searchendviolationdate?}")]
        public IActionResult SafetyViolationReg(SafetyViolationGridData values, SafetyViolationDTO safetyViolationDTO)
        {
            // WriteLog("mode: "+mode+", ViolationDate: "+ViolationDate+", WorkOrderNo: "+WorkOrderNo+", IsWorkOrder: "+IsWorkOrder);
            // WriteLog("Location: "+Location+", ViolationLocation: "+ViolationLocation+", SafetyViolationReasonID: "+SafetyViolationReasonID+", DetailReason: "+DetailReason);
            // WriteLog("personID: "+personID);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            if (safetyViolationDTO.Mode.Equals("A") && SafetyViolationData != null && SafetyViolationHistoryData != null 
                && SafetyViolationPersonData != null && SafetyViolationPersonHistoryData != null && SafetyViolationReasonData != null) // 안전위반 등록
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                safetyViolationDTO.SetSafetyViolationPerson();

                if(safetyViolationDTO.IsWorkOrder==null || safetyViolationDTO.IsWorkOrder == ""){
                    safetyViolationDTO.IsWorkOrder="N";
                }
                // 1. 안전수칙위반 (SafetyViolation) 정보 입력 
                SafetyViolation safetyViolation = new()
                {
                    ViolationDate = Convert.ToDateTime(safetyViolationDTO.ViolationDate+" 00:00:01"), //위반일시
                    WorkOrderNo = safetyViolationDTO.WorkOrderNo, //WorkOrderNo
                    IsWorkOrder = safetyViolationDTO.IsWorkOrder, //WorkOrder정보없음여부

                    ContactSabun = safetyViolationDTO.ContactSabun.ToString(), //담당자 
                    ContactName = safetyViolationDTO.ContactName, 
                    ContactOrgID = safetyViolationDTO.ContactOrgID, 
                    ContactOrgNameKor = safetyViolationDTO.ContactOrgNameKor, 
                    ContactOrgNameEng = safetyViolationDTO.ContactOrgNameEng, 

                    Location = safetyViolationDTO.Location, //사업장구분
                    ViolationLocation = safetyViolationDTO.ViolationLocation, //위반장소

                    SafetyViolationReasonID = int.Parse(safetyViolationDTO.SafetyViolationReasonID), //안전수칙위반사유ID
                    DetailReason = safetyViolationDTO.DetailReason, //상세사유
                    SafetyViolationStatus = 0, //안전수칙위반상태: 신규발행(0)/방지대책등록 (1)/방지대책승인(2)/방지대책재등록요청(3)
                    // StatementFileData = StatementFileData, //경위및대책자료파일첨부(첨부파일)
                    // StatementFileDataHash = StatementFileDataHash, //경위및대책자료파일첨부(첨부파일Hash)                    

                    InsertSabun = my.Sabun,//등록자
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = today
                };
                SafetyViolationData.Insert(safetyViolation);
                SafetyViolationData.Save();
                WriteLog("safetyViolation: "+Dump(safetyViolation));

                // 1.1 안전수칙위반 히스토리 (SafetyViolationHistory) 입력 
                SafetyViolationHistory safetyViolationHistory = new()
                {
                    SafetyViolationID = safetyViolation.SafetyViolationID, // 안전수칙위반ID
                    ViolationDate = safetyViolation.ViolationDate, //위반일시
                    WorkOrderNo = safetyViolation.WorkOrderNo, //WorkOrderNo
                    IsWorkOrder = safetyViolation.IsWorkOrder, //WorkOrder정보없음여부

                    ContactSabun = safetyViolation.ContactSabun, //담당자 
                    ContactName = safetyViolation.ContactName, 
                    ContactOrgID = safetyViolation.ContactOrgID, 
                    ContactOrgNameKor = safetyViolation.ContactOrgNameKor, 
                    ContactOrgNameEng = safetyViolation.ContactOrgNameEng, 


                    Location = safetyViolation.Location, //사업장구분
                    ViolationLocation = safetyViolation.ViolationLocation, //위반장소

                    SafetyViolationReasonID = safetyViolation.SafetyViolationReasonID, //안전수칙위반사유ID
                    DetailReason = safetyViolation.DetailReason, //상세사유
                    SafetyViolationStatus = safetyViolation.SafetyViolationStatus, //안전수칙위반상태: 신규발행(0)/방지대책등록 (1)/방지대책승인(2)/방지대책재등록요청(3)
                    // StatementFileData = StatementFileData, //경위및대책자료파일첨부(첨부파일)
                    // StatementFileDataHash = StatementFileDataHash, //경위및대책자료파일첨부(첨부파일Hash)                    

                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = today
                };
                SafetyViolationHistoryData.Insert(safetyViolationHistory);
                SafetyViolationHistoryData.Save();
                WriteLog("safetyViolationHistory: "+Dump(safetyViolationHistory));

                // 위반 정보 가져오기
                SafetyViolationReason safetyViolationReason = SafetyViolationReasonData.Get(safetyViolation.SafetyViolationReasonID);

                foreach (SafetyViolationPerson m in safetyViolationDTO.SafetyViolationPersonList)
                {
                    // 2. 안전수칙위반자들의 회원정보(Person) 가져오기 (arr)
                    var person = GetSafetyViolationPersonByReason(-1, m.Sabun, safetyViolation.SafetyViolationReasonID, true);
                    var viorationCount = person == null ? 1 : person.ViolationTime + 1;
                    var addDays = 0;
                    if (safetyViolationReason != null){
                        if(viorationCount == 1){
                            addDays = safetyViolationReason.ViolationPenaltyPeoriod1;
                        } else if(viorationCount == 2){
                            addDays = safetyViolationReason.ViolationPenaltyPeoriod2??safetyViolationReason.ViolationPenaltyPeoriod1;
                        } else{
                            addDays = safetyViolationReason.ViolationPenaltyPeoriod3??safetyViolationReason.ViolationPenaltyPeoriod1;
                        }
                    }
                    var startDate = today; 
                    var endDate = today.AddDays(addDays);
                    // 3. 안전수칙위반자 (SafetyViolationPerson) 등록 (arr)
                    m.SafetyViolationID = safetyViolation.SafetyViolationID; //안전수칙위반ID
                    m.ViolationTime = viorationCount; // 위반차수
                    m.StartDate = startDate;
                    m.EndDate = endDate;
                    m.SafetyViolationReasonID = safetyViolation.SafetyViolationReasonID;
                    m.InsertDate = today;
                    SafetyViolationPersonData.Insert(m);
                    SafetyViolationPersonData.Save();
                    WriteLog("safetyViolationPerson: "+Dump(m));
                    // 3.1 안전수칙위반자히스토리 (SafetyViolationPersonHistory) 등록 (arr)
                    m.SafetyViolationPersonHistory.SafetyViolationPersonID = m.SafetyViolationPersonID;
                    m.SafetyViolationPersonHistory.SafetyViolationID = m.SafetyViolationID;
                    m.SafetyViolationPersonHistory.ViolationTime = m.ViolationTime;
                    m.SafetyViolationPersonHistory.StartDate = startDate;
                    m.SafetyViolationPersonHistory.EndDate = endDate;
                    m.SafetyViolationPersonHistory.SafetyViolationReasonID = safetyViolation.SafetyViolationReasonID;
                    m.SafetyViolationPersonHistory.InsertUpdateDate = today;
                    SafetyViolationPersonHistoryData.Insert(m.SafetyViolationPersonHistory);
                    SafetyViolationPersonHistoryData.Save();
                }
            }
            // return new EmptyResult();
            return RedirectToAction("SafetyViolationList", new { culture=GetLang() });   
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchsafetyviolationstatus?}/{searchsafetyviolationreasonid?}/{searchname?}/{searchcompanyname?}/{searchstartviolationdate?}/{searchendviolationdate?}")]
        public IActionResult SafetyViolationDetail (SafetyViolationGridData values, int safetyViolationID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsESH();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            ViewBag.IsEditable =IsMaster() || IsGeneralManager() || IsESH();
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            WriteLog("safetyViolationID:" + safetyViolationID + ", slug" + slug);
            //안전위반사항 가져오기 
            var safetyViolation = SafetyViolationData.Get(new QueryOptions<SafetyViolation>{
                Where = a => a.SafetyViolationID == safetyViolationID,
            }) ?? new SafetyViolation();
            //안전수칙 위반자 정보 
            var options = new QueryOptions<SafetyViolationPerson>
            {
                Where = a => a.SafetyViolationID == safetyViolationID && a.IsDelete == "N",
            };

            //위반사유 리스트 가져오기 
            var options2 = new QueryOptions<SafetyViolationReason>
            {
                Where = a => a.IsDelete == "N",
            };

            SafetyViolationDetailViewModel vm = new(){
                SafetyViolation = safetyViolation,
                SafetyViolationPersons = SafetyViolationPersonData.List(options), 
                SafetyViolationReasons = SafetyViolationReasonData.List(options2),
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeSafetyViolationStatus = GetCommonCodes((int)VisitEnum.CommonCode.SafetyViolationStatus),
            };
            return View(vm);
        }

        /// <summary>
        /// IR 발행
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="safetyViolationID"></param>
        /// <param name="SafetyViolationStatus"></param>
        /// <param name="StatementFileData"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchsafetyviolationstatus?}/{searchsafetyviolationreasonid?}/{searchname?}/{searchcompanyname?}/{searchstartviolationdate?}/{searchendviolationdate?}")]
        public async Task<IActionResult> SafetyViolationDetail(SafetyViolationGridData values, string mode, int safetyViolationID, int SafetyViolationStatus, List<IFormFile> FormFile )
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            WriteLog("mode: "+mode+", safetyViolationID: "+safetyViolationID+", SafetyViolationStatus: "+SafetyViolationStatus);
            if (mode.Equals("E") && SafetyViolationData != null && SafetyViolationHistoryData != null) // 안전위반사항 상태 수정
            {
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                if(safetyViolationID > 0)
                {
                    // 1. 안전위반사항 상태 수정 
                    var orgObj = GetSafetyViolation(safetyViolationID, "", true);
                    WriteLog("orgObj: "+Dump(orgObj));
                    var newObj = orgObj.Clone();

                    newObj.SafetyViolationStatus = SafetyViolationStatus; // 안전수칙위반상태 SafetyViolationStatus : 신규발행(0)/방지대책등록 (1)/방지대책승인(2)/방지대책재등록요청(3)
                    // StatementFileData = StatementFileData, //경위및대책자료파일첨부(첨부파일)
                    // StatementFileDataHash = StatementFileDataHash, //경위및대책자료파일첨부(첨부파일Hash)  
                    if(FormFile != null && FormFile.Count > 0){
                        FileData fileData = await GetFileData(FormFile[0]);
                        newObj.StatementFileDataHash = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta)) {
                            newObj.StatementFileData = fileData.Meta;
                        }
                    }
                    newObj.UpdateDate = today;
                    SafetyViolationData?.Update(newObj);
                    SafetyViolationData?.Save();

                    // 1.1 안전수칙위반 히스토리 (SafetyViolationHistory) 입력 
                    SafetyViolationHistory safetyViolationHistory = new()
                    {
                        SafetyViolationID = orgObj.SafetyViolationID, // 안전수칙위반ID
                        ViolationDate = orgObj.ViolationDate, //위반일시 to-do: String 을 DateTime 으로 변환 
                        WorkOrderNo = orgObj.WorkOrderNo, //WorkOrderNo
                        IsWorkOrder = orgObj.IsWorkOrder, //WorkOrder정보없음여부

                        ContactSabun = orgObj.ContactSabun, //담당자 
                        ContactName = orgObj.ContactName, 
                        ContactOrgID = orgObj.ContactOrgID, 
                        ContactOrgNameKor = orgObj.ContactOrgNameKor, 
                        ContactOrgNameEng = orgObj.ContactOrgNameEng, 

                        Location = orgObj.Location, //사업장구분
                        ViolationLocation = orgObj.ViolationLocation, //위반장소

                        SafetyViolationReasonID = orgObj.SafetyViolationReasonID, //안전수칙위반사유ID
                        DetailReason = orgObj.DetailReason, //상세사유
                        SafetyViolationStatus = SafetyViolationStatus, //안전수칙위반상태: 신규발행(0)/방지대책등록 (1)/방지대책승인(2)/방지대책재등록요청(3)
                        StatementFileData = newObj.StatementFileData, //경위및대책자료파일첨부(첨부파일)
                        StatementFileDataHash = newObj.StatementFileDataHash, //경위및대책자료파일첨부(첨부파일Hash)                    

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = today
                    };
                    SafetyViolationHistoryData.Insert(safetyViolationHistory);
                    SafetyViolationHistoryData.Save();
                    WriteLog("safetyViolationHistory: "+Dump(safetyViolationHistory));
                    
                }
            } 
            return RedirectToAction("SafetyViolationDetail", new { safetyViolationID, culture=GetLang()});
        }

        [HttpPost]
        public FileResult? DownloadSafetyViolation(string SafetyViolationID, string FileIdx){
            if (IsAccess() == false) {
                return null;
            }
            WriteLog("start download> SafetyViolationID:"+SafetyViolationID+" , FileIdx:" + FileIdx);
            if (!string.IsNullOrEmpty(SafetyViolationID) && !string.IsNullOrEmpty(FileIdx)) 
            {
                int id = int.Parse(SafetyViolationID);
                int fileIdx = int.Parse(FileIdx);
                SafetyViolation? safetyViolation = SafetyViolationData?.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (safetyViolation!= null)
                { 
                    WriteLog("safetyViolation:"+Dump(safetyViolation));
                    if (fileIdx == 0) {
                        if (safetyViolation.StatementFileDataHash != null && safetyViolation.StatementFileData != null) {
                            fileData = safetyViolation.StatementFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(safetyViolation.StatementFileData);
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

        // 통계관리
        public IActionResult StatisticsList ()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsESH();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            return View();
        }

        /// <summary>
        /// 안전위규관리
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public IActionResult SafetyViolationReasonList (SafetyViolationReasonGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsESH();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            var options = new QueryOptions<SafetyViolationReason> {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = values.SortDirection,
                Where = a => a.IsDelete == "N",
            };
            SafetyViolationReasonListViewModel vm =new(){
                SafetyViolationReasons = SafetyViolationReasonData.List(options),                
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(SafetyViolationReasonData.Count),
                TotalCnt = SafetyViolationReasonData.Count,
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult SafetyViolationReasonDetail (int safetyViolationReasonID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("safetyViolationReasonID:" + safetyViolationReasonID + ", slug" + slug);
            SafetyViolationReasonDetailViewModel vm = new();
            if (SafetyViolationReasonData != null) {
                var safetyViolationReason = SafetyViolationReasonData.Get(new QueryOptions<SafetyViolationReason>{
                    Where = a => a.SafetyViolationReasonID == safetyViolationReasonID,
                }) ?? new SafetyViolationReason();
                vm = new() {
                    SafetyViolationReason = safetyViolationReason,
                };
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult SafetyViolationReasonDetail(string mode, int safetyViolationReasonID, String SafetyViolationReasonContents, 
        int ViolationPenaltyPeoriod1, int ViolationPenaltyPeoriod2, int ViolationPenaltyPeoriod3, int ViolationLevel, int OrderNo)
        { 
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // WriteLog("mode: "+mode+", SafetyViolationReasonContents: "+SafetyViolationReasonContents+", ViolationPenaltyPeoriod1: "+ViolationPenaltyPeoriod1+", ViolationPenaltyPeoriod2: "+ViolationPenaltyPeoriod2);
            PersonDTO my = GetMe();
            if (mode.Equals("A")) { // 안전위규 등록 
                // 1. 안전수칙위반사유 (SafetyViolationReason) 정보 입력 
                SafetyViolationReason safetyViolationReason = new()
                {
                    SafetyViolationReasonContents = SafetyViolationReasonContents, //안전수칙위반확인서발급기준(위반사유)
                    ViolationPenaltyPeoriod1 = ViolationPenaltyPeoriod1, //1차
                    ViolationPenaltyPeoriod2 = ViolationPenaltyPeoriod2, //2차
                    ViolationPenaltyPeoriod3 = ViolationPenaltyPeoriod3, //3차
                    
                    ViolationLevel = ViolationLevel, //위험레벨
                    OrderNo = OrderNo, //정렬순서
                    IsActive = "Y", //사용여부

                    InsertSabun = my.Sabun,//등록자
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now
                };
                SafetyViolationReasonData.Insert(safetyViolationReason);
                SafetyViolationReasonData.Save();
                // WriteLog("safetyViolationReason: "+Dump(safetyViolationReason));
                
                // 1.1 안전수칙위반사유히스토리 (SafetyViolationReasonHistory) 입력 
                SafetyViolationReasonHistory safetyViolationReasonHistory = new()
                {
                    SafetyViolationReasonID = safetyViolationReason.SafetyViolationReasonID, //안전수칙위반사유ID
                    SafetyViolationReasonContents = safetyViolationReason.SafetyViolationReasonContents, //안전수칙위반확인서발급기준(위반사유)
                    ViolationPenaltyPeoriod1 = safetyViolationReason.ViolationPenaltyPeoriod1, //1차
                    ViolationPenaltyPeoriod2 = safetyViolationReason.ViolationPenaltyPeoriod2, //2차
                    ViolationPenaltyPeoriod3 = safetyViolationReason.ViolationPenaltyPeoriod3, //3차
                    
                    ViolationLevel = safetyViolationReason.ViolationLevel, //위험레벨
                    OrderNo = safetyViolationReason.OrderNo, //정렬순서
                    IsActive = safetyViolationReason.IsActive, //사용여부

                    UpdateSabun = my.Sabun,//등록자가 방문자가 아닐경우 회원정보 입력 
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = DateTime.Now
                };
                SafetyViolationReasonHistoryData.Insert(safetyViolationReasonHistory);
                SafetyViolationReasonHistoryData.Save();
                // WriteLog("safetyViolationReasonHistory: "+Dump(safetyViolationReasonHistory));               
            } else if (mode.Equals("E")) {// 안전위규 수정
                if(safetyViolationReasonID > 0) {
                    // 1. 안전수칙위반사유 수정 
                    var orgObj = GetSafetyViolationReason(safetyViolationReasonID, true);
                    var newObj = orgObj.Clone();

                    newObj.SafetyViolationReasonContents = SafetyViolationReasonContents; //안전수칙위반확인서발급기준(위반사유)
                    newObj.ViolationPenaltyPeoriod1 = ViolationPenaltyPeoriod1; //1차
                    newObj.ViolationPenaltyPeoriod2 = ViolationPenaltyPeoriod2; //2차
                    newObj.ViolationPenaltyPeoriod3 = ViolationPenaltyPeoriod3; //3차
                    
                    newObj.ViolationLevel = ViolationLevel; //위험레벨
                    newObj.OrderNo = OrderNo; //정렬순서
                    newObj.IsActive = "Y"; //사용여부

                    newObj.UpdateSabun = my.Sabun;//최종수정자
                    newObj.UpdateName = my.Name;
                    newObj.UpdateOrgID = my.OrgID;
                    newObj.UpdateOrgNameKor = my.OrgNameKor;
                    newObj.InsertOrgNameEng = my.OrgNameEng;
                    newObj.UpdateDate = DateTime.Now;

                    SafetyViolationReasonData.Update(newObj);
                    SafetyViolationReasonData.Save();

                    // 1.1 안전수칙위반사유히스토리 (SafetyViolationReasonHistory) 입력 
                    SafetyViolationReasonHistory safetyViolationReasonHistory = new()
                    {
                        SafetyViolationReasonID = orgObj.SafetyViolationReasonID, //안전수칙위반사유ID
                        SafetyViolationReasonContents = SafetyViolationReasonContents, //안전수칙위반확인서발급기준(위반사유)
                        ViolationPenaltyPeoriod1 = ViolationPenaltyPeoriod1, //1차
                        ViolationPenaltyPeoriod2 = ViolationPenaltyPeoriod2, //2차
                        ViolationPenaltyPeoriod3 = ViolationPenaltyPeoriod3, //3차
                        
                        ViolationLevel = ViolationLevel, //위험레벨
                        OrderNo = OrderNo, //정렬순서
                        IsActive = orgObj.IsActive, //사용여부

                        UpdateSabun = my.Sabun,//등록자가 방문자가 아닐경우 회원정보 입력 
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now
                    };
                    SafetyViolationReasonHistoryData.Insert(safetyViolationReasonHistory);
                    SafetyViolationReasonHistoryData.Save();
                    WriteLog("safetyViolationReasonHistory: "+Dump(safetyViolationReasonHistory));
                }
            }
            return RedirectToAction("SafetyViolationReasonList", new { culture=GetLang() });   
        }

        //##### 공통 메소드들 #####
        private IEnumerable<WorkApply> GetWorkList(WorkApplyGridData filterhValue){
            if (WorkApplyData != null){
                var options = new QueryOptions<WorkApply>
                {
                    PageNumber = filterhValue.PageNumber,
                    PageSize = filterhValue.PageSize,
                    OrderByDirection = filterhValue.SortDirection,
                    OrderBy = a => a.WorkApplyID,
                };
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsESH())
                    {
                        options.MultipleWhere.Add(x => x.Location == my.Location);
                    } else if(IsEmployee()){
                        options.MultipleWhere.Add(x => x.ContactSabun == my.Sabun);
                    } else if (IsPartnerNonresidentManager()){
                        options.MultipleWhere.Add(x => x.InsertSabun == my.Sabun); // 비상주업체 관리자 신청건
                        // 외부에서 신청건에 대해서 회사명 LIKE 검색으로 처리할 지는 논의 중.
                    }
                }

                // {searchworkapplystatus?}/{searchworkname?}/{searchworkstartdate?}/{searchworkenddate?}/{searchworkapplyid?}/{searchcontactname?}
                if (!string.IsNullOrEmpty(filterhValue.SearchWorkName))
                {
                    options.MultipleWhere.Add(a => a.WorkName != null && EF.Functions.Like(a.WorkName, "%" + filterhValue.SearchWorkName + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchWorkApplyID))
                {
                    options.MultipleWhere.Add(a => EF.Functions.Like(a.WorkApplyNo, "%" + filterhValue.SearchWorkApplyID + "%"));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchContactName))
                {
                    options.MultipleWhere.Add(a => a.ContactName != null && EF.Functions.Like(a.ContactName, "%" + filterhValue.SearchContactName + "%"));
                }
                if (filterhValue.SearchWorkApplyStatus != -1)
                {
                    options.MultipleWhere.Add(a => a.WorkApplyStatus == filterhValue.SearchWorkApplyStatus);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchWorkStartDate))
                {
                    string d = filterhValue.SearchWorkStartDate + " 00:00:01";
                    options.MultipleWhere.Add(a => a.WorkStartDate != null && (DateTime)(object)a.WorkStartDate >= Convert.ToDateTime(d));
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchWorkEndDate))
                {
                    string d = filterhValue.SearchWorkEndDate + " 23:59:59";
                    options.MultipleWhere.Add(a => a.WorkEndDate != null && (DateTime)(object)a.WorkEndDate <= Convert.ToDateTime(d));
                }
                WriteLog("options:" + Dump(options));
                return WorkApplyData.List(options);
            }else{
                return default;
            }
        }

        private int GetWorkApplyNo()
        {
            int workApplyCntByToday=0;
            var today = DateTime.Now;
            var options = new QueryOptions<WorkApply>
            {
                // Where = a => a.InsertDate == DateTime.Now,
                Where = a => a.InsertDate.Year == today.Year && a.InsertDate.Month == today.Month &&a.InsertDate.Day == today.Day, // 오늘날짜 
            };
            // options.Max = a => (int?)a.PersonID??0;
            if (WorkApplyData != null){
                IEnumerable<WorkApply> workApply = WorkApplyData?.List(options)??new List<WorkApply>();
                workApplyCntByToday=WorkApplyData.Count;
            }
            // IEnumerable<WorkApply> workApply = workApplyData.List(options);
            // workApplyCntByToday=workApplyData.Count;
            // maxPersonIDByLocation=PersonData.Max;

            int maxWorkApplyCntByToday = workApplyCntByToday + 1;
            return maxWorkApplyCntByToday;
        }

        private Person? GetPerson(int personID=-1, string sabun="", bool isNoTracking = false)
        {
            Person? person = null;
            if (PersonData != null){
                if(personID > 0) {
                    var options = new QueryOptions<Person>
                    {
                        PageNumber = 0,
                        PageSize = 0,
                        OrderByDirection = "asc",
                        Where = a => a.PersonID == personID && a.IsDelete == "N",
                    };
                    if(isNoTracking) {
                        person = PersonData.GetNT(personID);
                    } else {
                        person = PersonData.Get(personID);
                    }
                } else if(!string.IsNullOrEmpty(sabun)) {
                    var options = new QueryOptions<Person>
                    {
                        PageNumber = 0,
                        PageSize = 0,
                        OrderByDirection = "asc",
                        Where = a => a.Sabun == sabun && a.IsDelete == "N",
                    };
                    if(isNoTracking) {
                        person = PersonData.GetNT(options);
                    } else {
                        person = PersonData.Get(options);
                    }                
                }
            }
            return person;
        }

        private VisitPerson? GetVisitPersonByID(int visitPersonID=-1, bool isNoTracking = false)
        {
            VisitPerson? visitPerson = null;
            if(VisitPersonData != null && visitPersonID > 0) {
                var options = new QueryOptions<VisitApply>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.VisitPersonID == visitPersonID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    visitPerson = VisitPersonData.GetNT(visitPersonID);
                } else {
                    visitPerson = VisitPersonData.Get(visitPersonID);
                }
            }
            return visitPerson;
        }

        private VisitPerson? GetVisitPerson(string name, string birth)
        {
            // WriteLog("name: " + name + ", birth: " + birth);
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

        //공사출입신청 시 출입인원 방문신청정보 가져오기 
        private VisitApplyPerson GetVisitApplyPerson(int visitApplyPersonID=-1, bool isNoTracking = false)
        {
            VisitApplyPerson visitApplyPerson = new();
            if(VisitApplyPersonData != null && visitApplyPersonID > 0) {
                var options = new QueryOptions<VisitApplyPerson>
                {
                    Where = a => a.VisitApplyPersonID == visitApplyPersonID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    visitApplyPerson = VisitApplyPersonData.GetNT(visitApplyPersonID);
                } else {
                    visitApplyPerson = VisitApplyPersonData.Get(visitApplyPersonID);
                }
            }
            return visitApplyPerson;
        }

        //공사신청정보 가져오기 
        private WorkApply? GetWorkApply(int workApplyID=-1, bool isNoTracking = false)
        {
            WorkApply? workApply = new();
            if(WorkApplyData != null && workApplyID > 0) {
                var options = new QueryOptions<WorkApply>
                {
                    Where = a => a.WorkApplyID == workApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    workApply = WorkApplyData.GetNT(workApplyID);
                } else {
                    workApply = WorkApplyData.Get(workApplyID);
                }
            }
            return workApply;
        }

        //공사출입신청정보 가져오기 
        private WorkVisitApply GetWorkVisitApply(int workVisitApplyID=-1, bool isNoTracking = false)
        {
            WorkVisitApply workVisitApply = new();
            if(WorkVisitApplyData!= null && workVisitApplyID > 0) {
                var options = new QueryOptions<WorkVisitApply>
                {
                    Where = a => a.WorkVisitApplyID == workVisitApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    workVisitApply = WorkVisitApplyData.GetNT(workVisitApplyID);
                } else {
                    workVisitApply = WorkVisitApplyData.Get(workVisitApplyID);
                }
            }
            return workVisitApply;
        }

        //공사출입신청시 등록된 방문신청정보 가져오기 
        private VisitApply GetVisitApplyByWorkVisitApplyID(int workVisitApplyID=-1, bool isNoTracking = false)
        {
            VisitApply visitApply = new();
            if(VisitApplyData!=null && workVisitApplyID > 0) {
                var options = new QueryOptions<VisitApply>
                {
                    Where = a => a.WorkVisitApplyID == workVisitApplyID, // && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    visitApply = VisitApplyData?.GetNT(options);
                } else {
                    visitApply = VisitApplyData.Get(options);
                }
                // WriteLog("visitApply:"+Dump(visitApply));
            }
            return visitApply;
        }
        
        //안전교육신청자의 방문신청 정보 가져오기
        private dynamic? GetSafetyEduApplyInfo(int SafetyEduID, String Sabun){
            if (DbSVISIT != null){
                var query = DbSVISIT.VisitApplyPerson
                    .Where(x => x.IsDelete == "N" && x.Sabun==Sabun)
                    .OrderBy(x => x.InsertDate)
                    .Join(
                        DbSVISIT.VisitApply.Where(x => x.IsDelete == "N" && x.SafetyEduID==SafetyEduID),
                        a => a.VisitApplyID, 
                        b => b.VisitApplyID,
                        (a, b) => new {a, b});
                var safetyEduApplyPersons = query.ToList();
                WriteLog("safetyEduApplyPersons:"+Dump(safetyEduApplyPersons));
                if (safetyEduApplyPersons.Count > 0) {
                    return safetyEduApplyPersons[0];
                }
            }
            return null;
        }

        //안전교육신청자 정보 가져오기 
        private SafetyEduApply GetSafetyEduApply(int safetyEduApplyID=-1, bool isNoTracking = false)
        {
            SafetyEduApply safetyEduApply = new();
            if(SafetyEduApplyData != null && safetyEduApplyID > 0) {
                var options = new QueryOptions<SafetyEduApply>
                {
                    Where = a => a.SafetyEduApplyID == safetyEduApplyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    safetyEduApply = SafetyEduApplyData.GetNT(safetyEduApplyID);
                } else {
                    safetyEduApply = SafetyEduApplyData.Get(safetyEduApplyID);
                }
            }
            return safetyEduApply;
        }

        //안전수칙위반사항 정보 가져오기 
        private SafetyViolation GetSafetyViolation(int safetyViolationID=-1, string sabun="", bool isNoTracking = false)
        {
            SafetyViolation safetyViolation = new();
            if (SafetyViolationData != null){
                if(SafetyViolationData != null && safetyViolationID > 0) {
                    var options = new QueryOptions<SafetyViolation>
                    {
                        Where = a => a.SafetyViolationID == safetyViolationID && a.IsDelete == "N",
                    };
                    if(isNoTracking) {
                        safetyViolation = SafetyViolationData.GetNT(options);
                    } else {
                        safetyViolation = SafetyViolationData.Get(options);
                    }
                } else if(!string.IsNullOrEmpty(sabun)){
                    var options = new QueryOptions<SafetyViolation>
                    {
                        Where = a => a.InsertSabun == sabun && a.IsDelete == "N",
                    };
                    if(isNoTracking) {
                        safetyViolation = SafetyViolationData.GetNT(options);
                    } else {
                        safetyViolation = SafetyViolationData.Get(options);
                    }
                }
            }
            return safetyViolation;
        }

        private SafetyViolationPerson GetSafetyViolationPersonByReason(int safetyViolationID=-1, string sabun="", int safetyViolationReasonID = -1, bool isNoTracking = false)
        {
            SafetyViolationPerson safetyViolationPerson = new();
            if (SafetyViolationPersonData != null){
                if(SafetyViolationData != null && safetyViolationID > 0) {
                    var options = new QueryOptions<SafetyViolationPerson>
                    {
                        Where = a => a.SafetyViolationID == safetyViolationID && a.IsDelete == "N",
                    };
                    if(isNoTracking) {
                        safetyViolationPerson = SafetyViolationPersonData.GetNT(options);
                    } else {
                        safetyViolationPerson = SafetyViolationPersonData.Get(options);
                    }
                } else if(!string.IsNullOrEmpty(sabun)){
                    // safetyViolationReasonID
                    var options = new QueryOptions<SafetyViolationPerson>
                    {
                        Where = a => a.Sabun == sabun && a.SafetyViolationReasonID == safetyViolationReasonID && a.IsDelete == "N",
                    };
                    if(isNoTracking) {
                        safetyViolationPerson = SafetyViolationPersonData.GetNT(options);
                    } else {
                        safetyViolationPerson = SafetyViolationPersonData.Get(options);
                    }
                }
            }
            return safetyViolationPerson;
        }

        private dynamic? GetSafetyViolationPersonList(SafetyViolationGridData values, bool isAll = false)
        {
            // SafetyViolationPerson SafetyViolation
            if(DbSVISIT!= null && SafetyViolationPersonData != null && SafetyViolationData != null && values != null) {
                IQueryable<SafetyViolationPerson> outer = DbSVISIT.SafetyViolationPerson.Where(x => x.IsDelete == "N");
                if (values.SearchSafetyViolationReasonID > -1) {
                    outer = outer.Where(x => x.SafetyViolationReasonID == values.SearchSafetyViolationReasonID);
                }
                if (!string.IsNullOrEmpty(values.SearchName)){
                    outer = outer.Where(x => x.Name != null && EF.Functions.Like(x.Name, "%" + values.SearchName + "%"));
                }
                if (!string.IsNullOrEmpty(values.SearchCompanyName)){
                    outer = outer.Where(x => x.CompanyName != null && EF.Functions.Like(x.CompanyName, "%" + values.SearchCompanyName + "%"));
                }
                // if (!string.IsNullOrEmpty(values.SearchBirthDate)){
                //     outer = outer.Where(x => x.BirthDate == values.SearchBirthDate);
                // }

                IQueryable<SafetyViolation> inner = DbSVISIT.SafetyViolation.Where(x => x.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsESH())
                    {
                        inner = inner.Where(x => x.Location == my.Location);
                    }
                    //  else if(IsEmployee()){
                    //     inner = inner.Where(x => x.ContactSabun == my.Sabun);
                    // } else if (IsPartnerNonresidentManager()){
                    //     inner = inner.Where(x => x.InsertSabun == my.Sabun); // 비상주업체 관리자 신청건
                    // }
                }
               
                if (values.SearchSafetyViolationStatus > -1) {
                    inner = inner.Where(x => x.SafetyViolationStatus == values.SearchSafetyViolationStatus);
                }
                try{
                    if (!string.IsNullOrEmpty(values.SearchStartViolationDate))
                    {
                        string d = values.SearchStartViolationDate + " 00:00:01";
                        inner = inner.Where(x => x.ViolationDate != null && (DateTime)(object)x.ViolationDate >= Convert.ToDateTime(d));
                    }
                    if (!string.IsNullOrEmpty(values.SearchEndViolationDate))
                    {
                        string d = values.SearchEndViolationDate + " 23:59:59";
                        inner = inner.Where(x => x.ViolationDate != null && (DateTime)(object)x.ViolationDate <= Convert.ToDateTime(d));
                    }
                } catch(Exception e){
                    WriteError(e.ToString());
                }
                var query = outer
                    .Join(
                        inner,
                        a => a.SafetyViolationID, 
                        b => b.SafetyViolationID,
                        (a, b) => new {a, b});
                int count = query.Count();
                if (!isAll) {
                    outer=outer.OrderBy(x => x.InsertDate);
                    outer=outer.Skip((values.PageNumber - 1) * values.PageSize);
                    outer=outer.Take(values.PageSize);
                    var q = outer
                        .Join(
                            inner,
                            a => a.SafetyViolationID, 
                            b => b.SafetyViolationID,
                            (a, b) => new {a, b});
                    var safetyViolationPerson = q.ToList();
                    // WriteLog("safetyViolationPerson: "+Dump(safetyViolationPerson));
                    return new { a=safetyViolationPerson, b=count };
                }else{
                    var safetyViolationPerson = query.ToList();
                    // WriteLog("safetyViolationPerson: "+Dump(safetyViolationPerson));
                    return new { a=safetyViolationPerson, b=count };
                }
            }
            return default;
        }

        //안전위규 정보 가져오기 
        private SafetyViolationReason GetSafetyViolationReason(int safetyViolationReasonID=-1, bool isNoTracking = false)
        {
            SafetyViolationReason safetyViolationReason = new();
            if(SafetyViolationReasonData != null && safetyViolationReasonID > 0) {
                var options = new QueryOptions<SafetyViolationReason>
                {
                    Where = a => a.SafetyViolationReasonID == safetyViolationReasonID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    safetyViolationReason = SafetyViolationReasonData.GetNT(safetyViolationReasonID);
                } else {
                    safetyViolationReason = SafetyViolationReasonData.Get(safetyViolationReasonID);
                }
            }
            return safetyViolationReason;
        }

        private dynamic? GetWorkVisitApplyPersonListByWorkApplyID(int WorkApplyID, int workVisitApplyID = -1){
            if(workVisitApplyID > 0){
                var query = DbSVISIT.VisitApplyPerson
                    .Where(x => x.IsDelete == "N" && x.WorkVisitApplyID == workVisitApplyID)
                    .OrderBy(x => x.InsertDate)
                    .Join(
                        DbSVISIT.VisitApply.Where(x => x.IsDelete == "N" && x.WorkApplyID == WorkApplyID),
                        a => a.VisitApplyID, 
                        b => b.VisitApplyID,
                        (a, b) => new {a, b});
                var visitApplyPersons = query.ToList();
                return visitApplyPersons;
            }
            return null;
        }
        
        private dynamic? GetWorkVisitApplyPersonList(WorkVisitApplyGridData values, bool isAll = false){
            if(DbSVISIT != null){
                IQueryable<WorkApply> inner = DbSVISIT.WorkApply.Where(x => x.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsESH() || IsSecurity())
                    {
                        inner = inner.Where(x => x.Location == my.Location);
                    } else if(IsEmployee()){
                        inner = inner.Where(x => x.ContactSabun == my.Sabun);
                    } else if (IsPartnerNonresidentManager()){
                        inner = inner.Where(x => x.InsertSabun == my.Sabun); // 비상주업체 관리자 신청건
                        // 외부에서 신청건에 대해서 회사명 LIKE 검색으로 처리할 지는 논의 중.
                    }
                }                
                // inner = inner.Where(x => x.VisitApplyType == 3);
                if (!string.IsNullOrEmpty(values.SearchWorkName))
                {
                    inner = inner.Where(x => x.WorkName != null && EF.Functions.Like(x.WorkName, "%" + values.SearchWorkName + "%"));
                }
                if (!string.IsNullOrEmpty(values.SearchContactName))
                {
                    inner = inner.Where(x => x.ContactName != null && EF.Functions.Like(x.ContactName, "%" + values.SearchContactName + "%"));
                }
                try{
                    if (!string.IsNullOrEmpty(values.SearchWorkStartDate))
                    {
                        string d = values.SearchWorkStartDate + " 00:00:01";
                        inner = inner.Where(x => x.WorkStartDate != null && (DateTime)(object)x.WorkStartDate >= Convert.ToDateTime(d));
                    }
                    if (!string.IsNullOrEmpty(values.SearchWorkEndDate))
                    {
                        string d = values.SearchWorkEndDate + " 23:59:59";
                        inner = inner.Where(x => x.WorkEndDate != null && (DateTime)(object)x.WorkEndDate <= Convert.ToDateTime(d));
                    }
                } catch(Exception e){
                    WriteError(e.ToString());
                }

                IQueryable<VisitApplyPerson> outer = DbSVISIT.VisitApplyPerson.Where(x => x.IsDelete == "N" && x.WorkVisitApplyID != null);
                if (values.SearchVisitStatus > -1) {
                    outer = outer.Where(x => x.VisitStatus == values.SearchVisitStatus);
                }
                if (values.SearchWorkVisitApplyStatus > -1) {
                    outer = outer.Where(x => x.VisitApplyStatus == values.SearchWorkVisitApplyStatus);
                }
                if (!string.IsNullOrEmpty(values.SearchCardNo))
                {
                    outer = outer.Where(x => x.CardNo != null && EF.Functions.Like(x.CardNo, "%" + values.SearchCardNo + "%"));
                }
                var query = outer
                    .Join(
                        inner,
                        a => a.WorkApplyID, 
                        b => b.WorkApplyID,
                        (a, b) => new {a, b}); // GroupJoin
                int count = query.Count();
                if (!isAll) {
                    outer=outer.OrderByDescending(x => x.InsertDate);
                    outer=outer.Skip((values.PageNumber - 1) * values.PageSize);
                    outer=outer.Take(values.PageSize);
                    var q = outer
                        .Join(
                            inner,
                            a => a.WorkApplyID, 
                            b => b.WorkApplyID,
                            (a, b) => new {a, b});
                    var workVisitApplyPersons = q.ToList();
                    WriteLog("workVisitApplyPersons: "+Dump(workVisitApplyPersons));
                    return new { a=workVisitApplyPersons, b=count };;
                }else{
                    var workVisitApplyPersons = query.ToList();
                    WriteLog("workVisitApplyPersons: "+Dump(workVisitApplyPersons));
                    return new { a=workVisitApplyPersons, b=count };;
                }
            }
            return default;
        }
        
        private dynamic? GetSafetyEduPersonList(SafetyEduGridData values, bool isAll = false){
            if (DbSVISIT != null){
                IQueryable<VisitApplyPerson> outer = DbSVISIT.VisitApplyPerson.Where(x => x.IsDelete == "N" && x.SafetyEduID != null);
                outer = outer.OrderBy(x => x.InsertDate);
                if (!string.IsNullOrEmpty(values.SearchCompanyName)){
                    outer = outer.Where(x => x.CompanyName != null && EF.Functions.Like(x.CompanyName, "%" + values.SearchCompanyName + "%"));
                }
                if (!string.IsNullOrEmpty(values.SearchName)){
                    outer = outer.Where(x => x.Name != null && EF.Functions.Like(x.Name, "%" + values.SearchName + "%"));
                }
                IQueryable<SafetyEduApply> inner = DbSVISIT.SafetyEduApply.Where(x => x.IsDelete == "N");
                if (values.SearchEducationCompleteStatus > -1) {
                    inner = inner.Where(x => x.EduCompleteStatus == values.SearchEducationCompleteStatus);
                }
                try{
                    if (!string.IsNullOrEmpty(values.SearchStartEduDate))
                    {
                        string d = values.SearchStartEduDate + " 00:00:01";
                        inner = inner.Where(x => x.EduDate != null && (DateTime)(object)x.EduDate >= Convert.ToDateTime(d));
                    }
                    if (!string.IsNullOrEmpty(values.SearchEndEduDate))
                    {
                        string d = values.SearchEndEduDate + " 23:59:59";
                        inner = inner.Where(x => x.EduDate != null && (DateTime)(object)x.EduDate <= Convert.ToDateTime(d));
                    }
                } catch(Exception e){
                    WriteError(e.ToString());
                }

                var query = outer
                    .Join(inner,
                        a => a.SafetyEduID, 
                        b => b.SafetyEduID,
                        (a, b) => new {a, b});
                IQueryable<WorkApply> inner2 = DbSVISIT.WorkApply.Where(x => x.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsESH() || IsSecurity())
                    {
                        inner2 = inner2.Where(x => x.Location == my.Location);
                    } else if(IsEmployee()){
                        inner2 = inner2.Where(x => x.ContactSabun == my.Sabun);
                    } else if (IsPartnerNonresidentManager()){
                        inner2 = inner2.Where(x => x.InsertSabun == my.Sabun); // 비상주업체 관리자 신청건
                        // 외부에서 신청건에 대해서 회사명 LIKE 검색으로 처리할 지는 논의 중.
                    }
                }

                var query2 = query.Join(inner2,
                    c => c.a.WorkApplyID,
                    d => d.WorkApplyID,
                    (c, d) => new { c, d }
                );
                int count = query2.Count();

                if (!isAll) {
                    outer=outer.OrderBy(x => x.InsertDate);
                    outer=outer.Skip((values.PageNumber - 1) * values.PageSize);
                    outer=outer.Take(values.PageSize);
                    var q = outer
                        .Join(inner,
                            a => a.SafetyEduID, 
                            b => b.SafetyEduID,
                            (a, b) => new {a, b});
                    var q2 = q.Join(inner2,
                        c => c.a.WorkApplyID,
                        d => d.WorkApplyID,
                        (c, d) => new { c, d }
                    );
                    var safetyEduPersons = q2.ToList();
                    return new { a=safetyEduPersons, b=count };
                } else {
                    var safetyEduPersons = query2.ToList();
                    return new { a=safetyEduPersons, b=count };
                }
            }
            return default;
        }     
 
        [HttpPost]
        public IActionResult? Download (string WorkApplyID, string WorkApplyAttachFileID, string WorkName = ""){
            if (IsAccessAPI() == false) {
                return null;
            }
            WriteLog("start download> WorkApplyID:"+WorkApplyID+" , WorkApplyAttachFileID:" + WorkApplyAttachFileID);
            if (!String.IsNullOrEmpty(WorkApplyID) && !String.IsNullOrEmpty(WorkApplyAttachFileID) && WorkApplyData != null && WorkApplyAttachFileData != null) 
            {
                byte[]? fileData = null;
                FileDTO? meta = null;
                int id = int.Parse(WorkApplyID);
                int fileIdx = int.Parse(WorkApplyAttachFileID);
                if (fileIdx == -1) { // 전체 다운로드
                    var options = new QueryOptions<WorkApplyAttachFile>
                    {
                        Where = a => a.WorkApplyID == id && a.IsDelete == "N",
                    };
                    var WorkApplyAttachFiles = WorkApplyAttachFileData.List(options);
                    if(WorkApplyAttachFiles != null) {
                        if(string.IsNullOrEmpty(WorkName)){
                            WorkName = "공사";
                        }
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                            {
                                foreach (var m in WorkApplyAttachFiles)
                                {
                                    if(m.AttachFileData != null){
                                        FileDTO? ff = _Dump<FileDTO>(m.AttachFileData);
                                        byte[]? fd = m.AttachFileDataHash;
                                        if(ff != null && fd != null) {
                                            var file = archive.CreateEntry(ff.FileName);
                                            using var stream = file.Open();
                                            stream.Write(fd, 0, (int)ff.Length);
                                        }
                                    }
                                }
                            }
                            return File(memoryStream.ToArray(), "application/zip", WorkName+".zip");
                        };      
                    }

                } else if (fileIdx == 0) {
                    WorkApply? n = WorkApplyData.Get(id);
                    if (n!= null)
                    {
                        if (n.WorkContractFileDataHash != null && n.WorkContractFileData != null) {
                            fileData = n.WorkContractFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(n.WorkContractFileData);
                        }
                    }
                } else {
                    var n = WorkApplyAttachFileData.Get(fileIdx);
                    if (n!= null)
                    {
                        if (n.AttachFileDataHash != null && n.AttachFileData != null) {
                            fileData = n.AttachFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(n.AttachFileData);
                        }
                    }
                }
                if (fileData != null && meta != null && !String.IsNullOrEmpty(meta.ContentType)) {
                    WriteLog("meta.ContentType:" + meta.ContentType);
                    // 이미지의 경우 meta.FileName 을 주지 않으면 부라우저에서 열림
                    return File(fileData, meta.ContentType, meta.FileName);
                }
            }
            return null;
        }
 
        public JsonResult? GetWork(string WorkApplyNo) {
            // 홍길동 B12541254 인사 031-259-96996 010-2159-96996 hone@test.com
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("WorkApplyData:" + WorkApplyData);
            PersonData ??= new Repository<Person>(DbSVISIT);
            if (WorkApplyData != null) {
                var workApply = WorkApplyData.Get(new QueryOptions<WorkApply>{
                    Where = a => a.WorkApplyNo == WorkApplyNo,
                }) ?? new WorkApply();
                return Json(workApply);
            }
            return default;
        }

        /// <summary>
        /// IR 발행 통계
        /// </summary>
        /// <param name="mode">D: 기간별, T: 유형별</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        /// 유형별
        /*
            [
                ["x",
                    "작업 중 보호구 미착용 및 착용상태 불량(안전모, 안전화, 호흡용 보호구)",
                    "차량제한속도(20km/h), 안전벨트 미체결, 운전중 휴대전화사용",
                    "흡연장소 미준수(이동중, 작업중, 지정장소 외)",
                    "산, 화학물질 접촉 위험 시 내산복 미착용(신체 일부 노출 포함)",
                    "작업허가 승인 전 임의작업 및 허가(계획) 기준 미준수",
                    "자전거 운행 중 불안전한 행동 실시(한손 운전 등)",
                    "추락방지 미조치(안전대 미착용, 미체결 및 수직사다리 3타점 미실시 등)",
                    "방호장치(방호울, 안전난간, 비상정지장치) 임의해제/훼손, 비상전원키 미소지",
                    "기계기구의 동력전달부 정비, 보수점검 시 전원 미차단, 방호울 내 임의 출입"
                ],
                ["data", 110, 180, 350, 80, 80, 30, 340, 120, 120],
            ]
        */
        /// 기간별
        /*
            [
                ["x", "4월", "3월", "2월", "1월", "12월", "11월", "10월", "9월", "8월"],
                ["data", 110, 180, 210, 80, 80, 30, 340, 120, 120],
            ]
        */
        public async Task<JsonResult?> GetIRStatistics(string startDate, string endDate, string mode="D") {
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("WorkApplyData:" + WorkApplyData);
            if(DbSVISIT != null){
                List<string> x = new()
                {
                    "x"
                };
                List<string> data = new()
                {
                    "data"
                };
                StringBuilder sb = new();
                if (mode.Equals("D")){ // 날짜별
                    // http://localhost:5010/work/getirstatistics?startDate=2023-08&endDate=2023-09
                    var a = new Dictionary<string,string>();
                    // sb.Append("SELECT ViolationDate FROM SafetyViolation GROUP BY ViolationDate");
                    sb.Append("SELECT CONCAT(DATEPART(YEAR, ViolationDate),'.', DATEPART(MONTH, ViolationDate)) as Date, COUNT(*) Count FROM SafetyViolation WHERE ViolationDate >= '");
                    sb.Append(startDate);
                    sb.Append("' AND ViolationDate <= '");
                    sb.Append(endDate);
                    sb.Append("' GROUP BY DATEPART(YEAR, ViolationDate), DATEPART(MONTH, ViolationDate)");
                    // SELECT CONCAT(DATEPART(YEAR, ViolationDate),".",DATEPART(MONTH, ViolationDate)) as date, COUNT(*) Count FROM SafetyViolation WHERE ViolationDate >= '2023-08-01' AND ViolationDate <= '2023-08-31' GROUP BY DATEPART(YEAR, ViolationDate), DATEPART(MONTH, ViolationDate)
                    try{
                        using var command = DbSVISIT.Database.GetDbConnection().CreateCommand();
                        WriteLog("query: " + sb.ToString());
                        command.CommandText = sb.ToString();
                        command.CommandType = CommandType.Text;
                        DbSVISIT.Database.OpenConnection(); // DbSVISIT.Database.GetDbConnection()
                        using var reader = command.ExecuteReader();
                        if(reader.HasRows){

                            while (reader.Read()){
                                var d = "";
                                for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                                {
                                    if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value){
                                        var n = reader.GetName(fieldCount);
                                        var v = reader[fieldCount];
                                        if (v != null){
                                            WriteLog("read data: "+n+"="+v);
                                            if (n.Equals("Date")){
                                                d = v.ToString();
                                            }else if(n.Equals("Count")) {
                                                a.Add(d, v.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DbSVISIT.Database.CloseConnection();      
                    }catch(Exception e) {
                        WriteLog(e.ToString());
                    }finally{
                        DbSVISIT.Database.CloseConnection();                
                    }
                    // var list = DbSVISIT.SafetyViolation.FromSql($"select ViolationDate from SafetyViolation group by ViolationDate").ToList();
                    WriteLog("dbData: " + Dump(a));
                    DateTime sd = DateTime.Parse(startDate);
                    DateTime ed = DateTime.Parse(endDate);
                    int monthCount = ((ed.Year - sd.Year) * 12) + ed.Month - sd.Month + 1;
                    for(var i = 0; i<monthCount; i++){
                        var k = sd.Year + "." + sd.Month;
                        x.Add(k);
                        sd = sd.AddMonths(1);
                        if (a.ContainsKey(k)) {
                            data.Add(a[k]);
                        }else{
                            data.Add("0");
                        }
                    }
                    // x.Add("1월");
                    // data.Add("110");
                } else if (mode.Equals("T") && SafetyViolationReasonData != null){ // 유형별
                    var options = new QueryOptions<SafetyViolationReason>{
                        Where = a => a.IsDelete == "N",
                    };
                // SafetyViolationStatus 안전수칙위반상태: 0 신규발행, 1 방지대책등록, 2 방지대책승인, 3 방지대책재등록요청
                    var SafetyViolationReasons = SafetyViolationReasonData.List(options);
                    // SELECT SafetyViolationReasonID, Count(*) as Cnt FROM SafetyViolation GROUP BY SafetyViolationReasonID
                    sb.Append("SELECT SafetyViolationReasonID, Count(*) as Count FROM SafetyViolation  WHERE ViolationDate >= '");
                    sb.Append(startDate);
                    sb.Append("' AND ViolationDate <= '");
                    sb.Append(endDate);
                    sb.Append("' GROUP BY SafetyViolationReasonID");
                   try{
                        using var command = DbSVISIT.Database.GetDbConnection().CreateCommand();
                        WriteLog("query: " + sb.ToString());
                        command.CommandText = sb.ToString();
                        command.CommandType = CommandType.Text;
                        DbSVISIT.Database.OpenConnection(); // DbSVISIT.Database.GetDbConnection()
                        using var reader = command.ExecuteReader();
                        if(reader.HasRows){

                            while (reader.Read()){
                                int id = 0;
                                for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                                {
                                    if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value){
                                        var n = reader.GetName(fieldCount);
                                        var v = reader[fieldCount];
                                        if (v != null){
                                            WriteLog("read data: "+n+"="+v);
                                            if (n.Equals("SafetyViolationReasonID")){
                                                id = (int) v;
                                            }else if(n.Equals("Count")) {
                                                // a.Add(d, v.ToString());
                                                foreach(var m in SafetyViolationReasons){
                                                    if (m.SafetyViolationReasonID == id) {
                                                        x.Add(m.SafetyViolationReasonContents);
                                                        data.Add(v.ToString());
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        DbSVISIT.Database.CloseConnection();      
                    }catch(Exception e) {
                        WriteLog(e.ToString());
                    }finally{
                        DbSVISIT.Database.CloseConnection();                
                    }
                }
                List<List<string>> rs = new()
                {
                    x,
                    data
                };
                return Json(rs);
            }
            return default;
        }
    } // end class

    internal record WorkAttachFileRecord(int Index, string Title, string FileElementName);

    internal record IRStatistic(string ViolationDate);
} // end ns
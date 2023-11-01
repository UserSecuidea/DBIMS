using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;

using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Pomelo.EntityFrameworkCore.MySql;

using WebVisit.Models;
using WebVisit.Models.DomainModels.Passport;
using WebVisit.Models.DomainModels.MySQL;
using SapNwRfc;


namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class ExportImportController : BaseController
    {
        private Repository<ExportImport>? ExportImportData { get; set; }
        private Repository<ExportImportHistory>? ExportImportHistoryData { get; set; }
        private Repository<ExportImportItem>? ExportImportItemData { get; set; }
        private Repository<ExportImportItemHistory>? ExportImportItemHistoryData { get; set; }
        // private Repository<NotebookSelfApproval>? NotebookSelfApprovalData { get; set; }
        private Repository<NotebookSelfApprovalHistory>? NotebookSelfApprovalHistoryData { get; set; }

        public ExportImportController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer) : base(logger, configuration, localizer)
        {
        }

        protected override void Init()
        {
            if (DbSVISIT != null)
            {
                ExportImportData ??= new Repository<ExportImport>(DbSVISIT);
                ExportImportHistoryData ??= new Repository<ExportImportHistory>(DbSVISIT);
                ExportImportItemData ??= new Repository<ExportImportItem>(DbSVISIT);
                ExportImportItemHistoryData ??= new Repository<ExportImportItemHistory>(DbSVISIT);
                NotebookSelfApprovalData ??= new Repository<NotebookSelfApproval>(DbSVISIT);
                NotebookSelfApprovalHistoryData ??= new Repository<NotebookSelfApprovalHistory>(DbSVISIT);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public IActionResult Index()
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            return RedirectToAction("List");
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchexportimportapplystatus?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")] //2010.10.12 comment 신인아, 1개 추가(승인상태)
        public IActionResult List(ExportImportGridData values, string mode = "")
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity() || IsPartnerNonresidentManager()
                            || IsPurchasing(); //2023.09.27 dsyoo
            if (accessible == false)
            {
                return View("_Inaccessible");
            }
            var my = GetMe();
            ViewBag.ExcelDownloadable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity()
                            || IsPurchasing(); //2023.09.27 dsyoo
            ViewBag.Registable = IsMaster() || IsGeneralManager() || IsEmployeeOnly() || IsHR() || IsESH() || IsSecurity()
                            || IsPurchasing(); //2023.09.27 dsyoo
            if (ViewBag.Registable)
            {
                if (string.IsNullOrEmpty(my.PorteID))
                {
                    ViewBag.Registable = false;
                }
            }
            ExportImportGridData filterhValue = (ExportImportGridData)FilterGridData(values);
            // 반출구분 ExportType: 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
            // 반출입상태 ExportImportStatus: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
            // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
            //2023.10.06 shin start 신인아
            if (string.IsNullOrEmpty(filterhValue.SearchStartInsertDate))
            {
                filterhValue.SearchStartInsertDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(filterhValue.SearchEndInsertDate))
            {
                filterhValue.SearchEndInsertDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (IsSecurity() && filterhValue.SearchExportImportApplyStatus == null) { filterhValue.SearchExportImportApplyStatus = 1; }//2023.09.25 신인아 보안팀이면 승인완료로 디폴트값 시작
            //2023.10.06 shin end 신인아
            ExportImportListViewModel vm = new()
            {
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeExportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportType),
                CodeExportImportStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportStatus),
                CodeExportImportPurposeType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportPurposeType),
                CodeExportImportApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportApplyStatus), //2023.09.14 dsyoo 반출입 승인상태 코드목록 추가
                CurrentRoute = values,
                SearchRoute = filterhValue,
            };
            var m = GetExportImportList(filterhValue);
            if (m != null)
            {
                vm.TotalPages = values.GetTotalPages(m.b);
                vm.TotalCnt = m.b;
                vm.ExportImports = m.a;
            }
            // @TempData["DATA.ExportImportID"] = 9;
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")]
        public IActionResult? ExcelDownload(ExportImportGridData values, string ExTitle = "DBHiTek")
        {
            if (IsAccessAPI() == false)
            {
                return default;
            }
            DataTable dt = new(ExTitle);
            // 반출입번호	반출자	반출자부서명	신청일자	반출구분	반출입목적	인수자	인수자부서명(업체명)	결제상태
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Export Import No"]), new DataColumn(@Localizer["Export Person Name"]), new DataColumn(@Localizer["Export Department Name"]),
                                        new DataColumn(@Localizer["Apply Date"]), new DataColumn(@Localizer["Export Classify"]), new DataColumn(@Localizer["Export Import Purpose"]),
                                        new DataColumn(@Localizer["Take Over Person Name"]),new DataColumn(@Localizer["Take Over Person Department Name"]),new DataColumn(@Localizer["Payment Status"]),});

            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            ExportImportGridData filterhValue = (ExportImportGridData)FilterGridData(values);
            var v = GetExportImportList(filterhValue, true);
            // List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            List<CommonCode> CodeExportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportType);
            List<CommonCode> CodeExportImportStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportStatus);
            List<CommonCode> CodeExportImportPurposeType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportPurposeType);
            if (v != null)
            {
                foreach (ExportImport m in v.a)
                {
                    var exportTypeName = "";
                    var exportImportPurposeTypeName = "";
                    var exportImportStatusName = "";
                    string insertDate = "";
                    var takeOverPerson = "";
                    var takeOverDepartment = "";
                    if (m.ExportType > -1 && CodeExportType != null)
                    {
                        foreach (var a in CodeExportType)
                        {
                            if (a.SubCodeID == m.ExportType)
                            {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko"))
                                {
                                    exportTypeName = a.CodeNameKor;
                                }
                                else
                                {
                                    exportTypeName = a.CodeNameEng;
                                }
                            }
                        }
                    }
                    if (m.ExportImportPurposeType > -1 && CodeExportImportPurposeType != null)
                    {
                        foreach (var a in CodeExportImportPurposeType)
                        {
                            if (a.SubCodeID == m.ExportImportPurposeType)
                            {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko"))
                                {
                                    exportImportPurposeTypeName = a.CodeNameKor;
                                }
                                else
                                {
                                    exportImportPurposeTypeName = a.CodeNameEng;
                                }
                            }
                        }
                    }
                    if (m.ExportImportStatus > -1 && CodeExportImportStatus != null)
                    {
                        foreach (var a in CodeExportImportStatus)
                        {
                            if (a.SubCodeID == m.ExportImportStatus)
                            {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko"))
                                {
                                    exportImportStatusName = a.CodeNameKor;
                                }
                                else
                                {
                                    exportImportStatusName = a.CodeNameEng;
                                }
                            }
                        }
                    }
                    //인수자 
                    //ExportType - 0 외부업체 일때  CompanyContactName CompanyName 
                    //ExportType - 1 공장간이동 일때  Name 	OrgNameKor 
                    //ExportType - 2 외부업체경유공장간이동 일때  Name 	OrgNameKor 
                    if (m.ExportType == 0)
                    {
                        takeOverPerson = m.CompanyContactName;
                        takeOverDepartment = m.CompanyName;
                    }
                    else
                    {
                        takeOverPerson = m.Name;
                        takeOverDepartment = m.OrgNameKor;
                    }
                    insertDate = string.Format("{0:yyyy-MM-dd}", m.InsertDate);
                    dt.Rows.Add(m.ExportImportNo, m.InsertName, m.InsertOrgNameKor, insertDate, exportTypeName, exportImportPurposeTypeName, takeOverPerson, takeOverDepartment, exportImportStatusName);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            }
            else
            {
                WriteLog("ExportImport is null");
                return null;
            }
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")]
        public IActionResult Reg(ExportImportGridData values)
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null)
            {
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            // PORTE ID 가 있는 사용자만 이용 가능
            var my = GetMe();
            WriteLog("my:" + Dump(my));
            ViewBag.IsSelfApproval = IsSelfApproval();
            if (string.IsNullOrEmpty(my.PorteID))
            {
                return RedirectToAction("List", new { culture = GetLang() });
            }
            else
            {
                ViewBag.my = my;
                // 2023.09.11 신인아 start
                var l = GetUnionPerson(null, my.Sabun, null);

                if (l != null && l.a != null && l.a.Count > 0)
                {
                    var person = l.a[0];
                    ViewBag.person = person;
                }
                // 2023.09.11 신인아 end

                List<ApprovalPerson> approvalPeople = GetApprovalPerson();
                WriteLog("approvalPeople:" + Dump(approvalPeople));
                List<SeniorPerson> seniorPeople = GetSeniorPerson();
                WriteLog("seniorPeople:" + Dump(seniorPeople));
                ExportImportRegViewModel vm = new()
                {
                    ApprovalPeople = approvalPeople,

                    SeniorPeople = seniorPeople,

                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),

                    // 반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
                    CodeExportImportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportType),

                    // 반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
                    CodeExportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportType),

                    // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
                    CodeExportImportPurposeType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportPurposeType),

                    // 반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고
                    CodeImportType = GetCommonCodes((int)VisitEnum.CommonCode.ImportType),

                    // 반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량
                    CodeDeliveryMethodType = GetCommonCodes((int)VisitEnum.CommonCode.DeliveryMethodType),

                    CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),

                };
                return View(vm);
            }
        }

        //, int ExportImportType, int ExportType, string ExportLocation, string ImportLocation, int ExportImportPurposeType, int ImportType, string ImportDate, string ExportDate, int DeliveryMethodType, string Reason, string IsVMI, string CompanySabun, string CompanyContactName, string CompanyName, string BizRegNo, string CompanyTel, string IsSelf, string ExportDateHour, string ExportDateMinute, string ImportDateHour, string ImportDateMinute, string ManagementNo, string SerialNo, string IsSelfApproval, string CeoApprovalFileData,
        // string Sabun, string Name, int OrgID, string OrgNameKor, string OrgNameEng, string Tel, 
        // string ApprovalSabun, string ApprovalName, int ApprovalOrgID, string ApprovalOrgNameKor, string ApprovalOrgNameEng, string ApprovalTel, 
        // string ContactSabun, string ContactName, int ContactOrgID, string ContactOrgNameKor, string ContactOrgNameEng, string ContactTel,
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")]
        public async Task<IActionResult> Reg(ExportImportGridData values, ExportImport pExportImport, string mode, List<IFormFile> FormFile,
            string[]? PRNo, string[]? MaterialsCode, string[]? MaterialsName, string[]? Quantity, string[]? Unit, string[]? Standard, string[]? Memo)
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            if (mode.Equals("A") && ExportImportData != null && ExportImportItemData != null && ExportImportHistoryData != null)
            {
                // 휴대물품 신청 추가
                PersonDTO my = GetMe();
                var today = DateTime.Now;
                //반출입번호 생성: C202306250002 
                int maxExportImportCntByToday = GetExportImportNo();
                WriteLog("maxExportImportCntByToday:" + maxExportImportCntByToday);
                String strExportImportNo = "C" + DateTime.Now.ToString("yyyyMMdd") + Convert.ToString(maxExportImportCntByToday).PadLeft(4, '0');
                if (pExportImport.ExportImportType == 2)
                { //반출입구분>노트북                     
                    WriteLog("mode: " + mode + ", exportImport: " + Dump(pExportImport));
                    pExportImport.IsSelfApproval ??= "N";
                    pExportImport.ExportImportPurposeType = -1; // 반출입목적 (자산/수리 에만 존재) 
                    pExportImport.ImportType = -1;              // 반입구분 (자산/수리 에만 존재) 
                    pExportImport.ImportLocation = "";          // 반입소속사업장구분 (자산/수리 에만 존재) 
                    pExportImport.DeliveryMethodType = -1;      // 반출물전달방법 (자산/수리 에만 존재) 

                    pExportImport.Sabun = "";                   // 반입사업장인수자 (자산/수리에만 존재) 
                    pExportImport.Name = "";
                    pExportImport.OrgID = "";
                    pExportImport.OrgNameKor = "";
                    pExportImport.OrgNameEng = "";
                    pExportImport.Tel = "";

                    pExportImport.ContactSabun = "";            // 부담당자 (자산/수리에만 존재) 
                    pExportImport.ContactName = "";
                    pExportImport.ContactOrgID = "";
                    pExportImport.ContactOrgNameKor = "";
                    pExportImport.ContactOrgNameEng = "";
                    pExportImport.ContactTel = "";
                    pExportImport.ContactOrgNameKor = "";

                    pExportImport.IsSelf = "";                  // 본인직접반출 (자산에만 존재) 

                    pExportImport.CompanySabun = "";            // 외부업체인수자 (자산에만 존재) 
                    pExportImport.CompanyContactName = "";
                    pExportImport.CompanyName = "";
                    pExportImport.BizRegNo = "";
                    pExportImport.CompanyTel = "";

                    pExportImport.IsVMI = "";                   // VMI요청 (수리에만 존재)
                    // CEO 승인이력 첨부 파일
                    if (FormFile != null && FormFile.Count > 0)
                    {
                        FileData fileData = await GetFileData(FormFile[0]);
                        pExportImport.CeoApprovalFileDataHash = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta))
                        {
                            pExportImport.CeoApprovalFileData = fileData.Meta;
                        }
                    }
                }
                else if (pExportImport.ExportImportType == 1)
                { // 반출입구분>수리
                    pExportImport.IsVMI ??= "N";
                    pExportImport.ExportDate = "";              // 반출예정일 (노트북에만 존재)
                    pExportImport.ExportDateHour = "";          // 반출예정 - 시 (노트북에만 존재)
                    pExportImport.ExportDateMinute = "";        // 반출예정 - 분 (노트북에만 존재)
                    pExportImport.ImportDateHour = "";          // 반입예정 - 시간 (노트북에만 존재)
                    pExportImport.ImportDateMinute = "";        // 반입예정 - 분 (노트북에만 존재)
                    pExportImport.ManagementNo = "";            // 노트북관리번호 (노트북에만 존재)
                    pExportImport.SerialNo = "";                // 노트북시리얼번호 (노트북에만 존재)
                    pExportImport.IsSelfApproval = "";          // 자기결재여부 (노트북에만 존재)
                    // pExportImport.CeoApprovalFileData="";  // CEO승인이력(첨부파일) (노트북에만 존재)
                    // pExportImport.CeoApprovalFileDataHash="";  // CEO승인이력(첨부파일Hash) (노트북에만 존재)

                    pExportImport.IsSelf = "";                  // 본인직접반출 (자산에만 존재) 
                    pExportImport.CompanySabun = "";            // 외부업체인수자 (자산에만 존재) 
                    pExportImport.CompanyContactName = "";
                    pExportImport.CompanyName = "";
                    pExportImport.BizRegNo = "";
                    pExportImport.CompanyTel = "";
                }
                else
                {  //반출입구분>자산 
                    pExportImport.IsSelf ??= "N";
                    pExportImport.IsVMI = "";                   // VMI요청 (수리에만 존재)
                    pExportImport.ExportDate = "";              // 반출예정일 (노트북에만 존재)                    
                    pExportImport.ExportDateHour = "";          // 반출예정 - 시 (노트북에만 존재)
                    pExportImport.ExportDateMinute = "";        // 반출예정 - 분 (노트북에만 존재)
                    pExportImport.ImportDateHour = "";          // 반입예정 - 시간 (노트북에만 존재)
                    pExportImport.ImportDateMinute = "";        // 반입예정 - 분 (노트북에만 존재)
                    pExportImport.ManagementNo = "";            // 노트북관리번호 (노트북에만 존재)
                    pExportImport.SerialNo = "";                // 노트북시리얼번호 (노트북에만 존재)
                    pExportImport.IsSelfApproval = "";          // 자기결재여부 (노트북에만 존재)
                    // pExportImport.CeoApprovalFileData="";  // CEO승인이력(첨부파일) (노트북에만 존재)
                    // pExportImport.CeoApprovalFileDataHash="";  // CEO승인이력(첨부파일Hash) (노트북에만 존재)
                }
                // 1. 반입반출 (ExportImport) 정보 입력 
                pExportImport.ExportImportNo = strExportImportNo; //반출입번호
                pExportImport.ExportImportApplyStatus = 0; //반출입신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4) // 2023.09.13 반출입상태에서 반출입신청상태 코드 분리
                pExportImport.ExportImportStatus = 4; //반출입상태: 반출대기(4), 반출확인(5), 정문반송(6), 반출완료(7), 반입상신(8), 반입대기(9), 반입확인(10), 반입완료(11)
                pExportImport.InsertSabun = my.Sabun;//등록자
                pExportImport.InsertName = my.Name;
                pExportImport.InsertOrgID = my.OrgID;
                pExportImport.InsertOrgNameKor = my.OrgNameKor;
                pExportImport.InsertOrgNameEng = my.OrgNameEng;
                pExportImport.InsertDate = today;
                ExportImportData.Insert(pExportImport);
                ExportImportData.Save();
                WriteLog("exportImport: " + Dump(pExportImport));
                // 1.1 반입반출히스토리 (ExportImport) 정보 입력 
                ExportImportHistory exportImportHistory = new()
                {
                    ExportImportID = pExportImport.ExportImportID, //반입반출ID
                    ExportImportNo = pExportImport.ExportImportNo, //반출입번호
                    ExportImportType = pExportImport.ExportImportType, //반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
                    ExportType = pExportImport.ExportType, //반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동

                    ExportLocation = pExportImport.ExportLocation, //반출소속사업장구분
                    ImportLocation = pExportImport.ImportLocation, //반입소속사업장구분

                    ExportImportPurposeType = pExportImport.ExportImportPurposeType, //반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
                    ImportType = pExportImport.ImportType, //반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고     
                    ExportDate = pExportImport.ExportDate, //반출예정일
                    ImportDate = pExportImport.ImportDate, //반입예정일
                    DeliveryMethodType = pExportImport.DeliveryMethodType, //반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량

                    Reason = pExportImport.Reason, //반출입사유
                    IsVMI = pExportImport.IsVMI, //VMI요청

                    CompanySabun = pExportImport.CompanySabun, //회원사번(외부업체인수자)
                    CompanyContactName = pExportImport.CompanyContactName, //담당자이름 
                    CompanyName = pExportImport.CompanyName, //업체명
                    BizRegNo = pExportImport.BizRegNo, //사업자번호
                    CompanyTel = pExportImport.CompanyTel, //전화번호

                    IsSelf = pExportImport.IsSelf, //본인직접반출

                    Sabun = pExportImport.Sabun,//반입사업장인수자
                    Name = pExportImport.Name,
                    OrgID = pExportImport.OrgID,
                    OrgNameKor = pExportImport.OrgNameKor,
                    OrgNameEng = pExportImport.OrgNameEng,
                    Tel = pExportImport.Tel,

                    ApprovalSabun = pExportImport.ApprovalSabun,//결재자
                    ApprovalName = pExportImport.ApprovalName,
                    ApprovalOrgID = pExportImport.ApprovalOrgID,
                    ApprovalOrgNameKor = pExportImport.ApprovalOrgNameKor,
                    ApprovalOrgNameEng = pExportImport.ApprovalOrgNameEng,
                    ApprovalTel = pExportImport.ApprovalTel,

                    ContactSabun = pExportImport.ContactSabun,//부담당자
                    ContactName = pExportImport.ContactName,
                    ContactOrgID = pExportImport.ContactOrgID,
                    ContactOrgNameKor = pExportImport.ContactOrgNameKor,
                    ContactOrgNameEng = pExportImport.ContactOrgNameEng,
                    ContactTel = pExportImport.ContactTel,

                    ExportImportApplyStatus = pExportImport.ExportImportApplyStatus, //반출입신청상태 2023.09.13 반출입상태에서 반출입신청상태 코드 분리
                    ExportImportStatus = pExportImport.ExportImportStatus, //반출입상태

                    ExportDateHour = pExportImport.ExportDateHour, // 반출예정 - 시간
                    ExportDateMinute = pExportImport.ExportDateMinute, // 반출예정 - 분
                    ImportDateHour = pExportImport.ImportDateHour, // 반입예정 - 시간
                    ImportDateMinute = pExportImport.ImportDateMinute, // 반입예정 - 분

                    ManagementNo = pExportImport.ManagementNo, // 노트북관리번호
                    SerialNo = pExportImport.SerialNo, // 노트북시리얼번호
                    IsSelfApproval = pExportImport.IsSelfApproval, // 자기결재여부
                    // CeoApprovalFileData = CeoApprovalFileData, // CEO승인이력(첨부파일)
                    // CeoApprovalFileDataHash = CeoApprovalFileDataHash, // CEO승인이력(첨부파일Hash)
                    UpdateSabun = my.Sabun,//등록자
                    UpdateName = my.Name,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    UpdateGradeName = my.GradeName,
                    InsertUpdateDate = today
                };
                ExportImportHistoryData.Insert(exportImportHistory);
                ExportImportHistoryData.Save();
                WriteLog("exportImportHistory: " + Dump(exportImportHistory));

                //2. 반출입품목(ExportImportItem) 입력 (arr)
                if (MaterialsCode != null)
                {
                    for (var i = 0; i < MaterialsCode?.Length; i++)
                    {
                        float qt = 0.0f;
                        if (Quantity != null && Quantity[i] != null)
                        {
                            qt = float.Parse(Quantity[i]);
                            // qt = int.Parse(Quantity[i]);
                        }
                        if (!string.IsNullOrEmpty(MaterialsCode[i]))
                        {
                            ExportImportItem exportImportItem = new()
                            {
                                ExportImportID = pExportImport.ExportImportID, //반출입ID
                                PRNo = PRNo?[i] ?? "", //PRNo
                                MaterialsCode = MaterialsCode[i], //자재코드
                                MaterialsName = MaterialsName?[i] ?? "", //자재이름
                                Quantity = qt, //수량
                                RemainCnt = qt, // 남은 수량에 수량을 입력
                                Unit = Unit?[i] ?? "", //단위
                                Standard = Standard?[i] ?? "", //규격
                                Memo = Memo?[i] ?? "", //비고
                                // UpdateDate = DateTime.Now, //갱신일
                                InsertDate = today //생성일
                            };
                            ExportImportItemData.Insert(exportImportItem);
                            ExportImportItemData.Save();
                            WriteLog("exportImportItem: " + Dump(exportImportItem));

                            ExportImportItemHistory exportImportItemHistory = new()
                            {
                                // ExportImportItemHistoryID = 0,
                                ExportImportItemID = exportImportItem.ExportImportItemID,
                                ExportImportID = exportImportItem.ExportImportID,
                                PRNo = exportImportItem.PRNo,
                                MaterialsCode = exportImportItem.MaterialsCode,
                                MaterialsName = exportImportItem.MaterialsName,
                                Quantity = exportImportItem.Quantity,
                                Unit = exportImportItem.Unit,
                                Standard = exportImportItem.Standard,
                                Memo = exportImportItem.Memo,
                                RemainCnt = exportImportItem.Quantity, // 남은 수량에 수량을 입력
                                InsertUpdateDate = today,
                            };
                            ExportImportItemHistoryData?.Insert(exportImportItemHistory);
                            ExportImportItemHistoryData?.Save();
                            WriteLog("exportImportItemHistory: " + Dump(exportImportItemHistory));
                        }// end if
                    } // end for
                } // end if

                //PORTE ID 가 있는 사용자만 이용 가능
                if (!string.IsNullOrEmpty(my.PorteID))
                {
                    TempData["DATA.ExportImportID"] = pExportImport.ExportImportID;
                }
            } //end if mode == A
            return RedirectToAction("List", new { culture = GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")]
        public IActionResult Detail(ExportImportGridData values, int exportImportID, string slug = "")
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            if (values != null && values.ToDictionary() != null)
            {
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }

            WriteLog("exportImportID:" + exportImportID + ", slug" + slug);
            var my = GetMe();
            ViewBag.ExportImportID = exportImportID;
            ViewBag.IsReApproval = false;
            var exportImport = ExportImportData.Get(new QueryOptions<ExportImport>
            {
                Where = a => a.ExportImportID == exportImportID,
            }) ?? new ExportImport();
            if (!string.IsNullOrEmpty(exportImport.InsertSabun))
            {
                //2023.09.15 dsyoo 상신전(신청) 상태이고 로그인한사람이 신청한사람이면 재상신할수 있다
                if (exportImport.ExportImportApplyStatus == 0
                    && my.Sabun.Equals(exportImport.InsertSabun))
                {
                    ViewBag.IsReApproval = true;
                }
            }
            var options = new QueryOptions<ExportImportItem>
            {
                Where = a => a.ExportImportID == exportImportID && a.IsDelete == "N",
            };

            var options2 = new QueryOptions<ExportImportHistory>
            {
                Where = a => a.ExportImportID == exportImportID,
            };
            ViewBag.my = my; //2023.09.17 dsyoo my를 ViewBag변수어 넣음
            ExportImportDetailViewModel vm = new()
            {
                ExportImport = exportImport,
                ExportImportItems = ExportImportItemData.List(options),
                ExportImportHistory = ExportImportHistoryData.List(options2),

                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),

                // 반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
                CodeExportImportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportType),

                // 반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
                CodeExportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportType),

                // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
                CodeExportImportPurposeType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportPurposeType),

                // 반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고
                CodeImportType = GetCommonCodes((int)VisitEnum.CommonCode.ImportType),

                // 반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량
                CodeDeliveryMethodType = GetCommonCodes((int)VisitEnum.CommonCode.DeliveryMethodType),

                // ExportImportApplyStatus 반출입신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
                CodeExportImportApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportApplyStatus),
                // ExportImportStatus 반출입상태: 
                CodeExportImportStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportStatus),
            };
            return View(vm);
        }



        [HttpGet]
        public IActionResult PrintExportImport(int exportImportID)
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }

            WriteLog("exportImportID:" + exportImportID);
            var my = GetMe();
            ViewBag.ExportImportID = exportImportID;
            ViewBag.IsReApproval = false;
            var exportImport = ExportImportData.Get(new QueryOptions<ExportImport>
            {
                Where = a => a.ExportImportID == exportImportID,
            }) ?? new ExportImport();
            if (!string.IsNullOrEmpty(exportImport.InsertSabun))
            {
                //2023.09.15 dsyoo 상신전(신청) 상태이고 로그인한사람이 신청한사람이면 재상신할수 있다
                if (exportImport.ExportImportApplyStatus == 0
                    && my.Sabun.Equals(exportImport.InsertSabun))
                {
                    ViewBag.IsReApproval = true;
                }
            }
            var options = new QueryOptions<ExportImportItem>
            {
                Where = a => a.ExportImportID == exportImportID && a.IsDelete == "N",
            };

            var options2 = new QueryOptions<ExportImportHistory>
            {
                Where = a => a.ExportImportID == exportImportID,
            };
            ViewBag.my = my; //2023.09.17 dsyoo my를 ViewBag변수어 넣음
            ExportImportDetailViewModel vm = new()
            {
                ExportImport = exportImport,
                ExportImportItems = ExportImportItemData.List(options),
                ExportImportHistory = ExportImportHistoryData.List(options2),

                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),

                // 반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
                CodeExportImportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportType),

                // 반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
                CodeExportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportType),

                // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
                CodeExportImportPurposeType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportPurposeType),

                // 반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고
                CodeImportType = GetCommonCodes((int)VisitEnum.CommonCode.ImportType),

                // 반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량
                CodeDeliveryMethodType = GetCommonCodes((int)VisitEnum.CommonCode.DeliveryMethodType),

                // ExportImportApplyStatus 반출입신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
                CodeExportImportApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportApplyStatus),
                // ExportImportStatus 반출입상태: 
                CodeExportImportStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportStatus),
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult? DownloadExportImport(string ExportImportID)
        {
            if (IsAccessAPI() == false)
            {
                return null;
            }
            WriteLog("start DownloadExportImport> ExportImportID:" + ExportImportID);
            if (!string.IsNullOrEmpty(ExportImportID) && ExportImportData != null)
            {
                var exportImport = ExportImportData.Get(new QueryOptions<ExportImport>
                {
                    Where = a => a.ExportImportID == int.Parse(ExportImportID),
                });
                if (exportImport != null && exportImport.CeoApprovalFileData != null)
                {
                    byte[]? fileData = exportImport.CeoApprovalFileDataHash;
                    FileDTO? meta = _Dump<FileDTO>(exportImport.CeoApprovalFileData);
                    if (fileData != null && meta != null && !String.IsNullOrEmpty(meta.ContentType))
                    {
                        WriteLog("meta.ContentType:" + meta.ContentType);
                        return File(fileData, meta.ContentType, meta.FileName);
                    }
                }
            }
            return null;
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")]
        public IActionResult Detail(ExportImportGridData values, string mode, int exportImportID
            , int ExportType, int ImportType   //2023.09.17 dsyoo 반입구분에따라 상태값처리를 다르게 하기위해 필요
            , string CompanySabun //회원사번(외부업체인수자)
            , string CompanyContactName //담당자이름 
            , string CompanyName //업체명
            , string BizRegNo //사업자번호
            , string CompanyTel //전화번호
            , int ExportImportStatus, string Opinion, int ExportImportApplyStatus,
            int CheckCnt, string Reason, string[] ExportImportItemID, string[] ImportCnt, string[] RemainCnt)
        {
            WriteLog("mode: " + mode + ", exportImportID: " + exportImportID + ", ExportImportStatus: " + ExportImportStatus + ", Opinion: " + Opinion + ", CheckCnt: " + CheckCnt);
            WriteLog("mode: " + mode + ", Reason: " + Reason);

            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();

            if (mode.Equals("EExportImportStatus"))
            { // 결재상태 수정 

                if (exportImportID > 0 && ExportImportData != null && ExportImportHistoryData != null)
                {
                    // 1. 반입반출 결재상태 수정 
                    var orgObj = GetExportImport(exportImportID, true);
                    var newObj = orgObj.Clone();
                    //2023.09.17 dsyoo 반출불요 상태인지 체크하고 반출불요 상태이면 반출완료로
                    switch (ExportImportStatus)
                    {
                        case 5: //반출확인
                            {
                                //4 반출대기, 5 반출확인, 6 정문반송, 7 반출완료, 9 반입대기, 10 반입확인, 11 반입완료
                                if (ImportType == 1 || ImportType == 2 || ImportType == 3) //:0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고
                                {
                                    ExportImportStatus = 7;      //반출완료(7)                       
                                }
                                else
                                {
                                    ExportImportStatus = 9;      //반입대기(9)
                                }
                            }
                            break;

                    }
                    newObj.ExportImportStatus = ExportImportStatus; // 결재상태 ExportImportStatus : 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
                    newObj.UpdateDate = DateTime.Now;
                    newObj.ExportImportApplyStatus = ExportImportApplyStatus;
                    ExportImportData.Update(newObj);
                    ExportImportData.Save();

                    // 1.1 반입반출히스토리 입력 (결재상태, 확인수량, 의견)
                    // 확인수량, 의견은 ExportImportHistory 만 에 입력                 
                    ExportImportHistory exportImportHistory = new()
                    {
                        ExportImportID = orgObj.ExportImportID, //반입반출ID
                        ExportImportNo = orgObj.ExportImportNo, //반출입번호
                        ExportImportType = orgObj.ExportImportType, //반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
                        ExportType = orgObj.ExportType, //반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동

                        ExportLocation = orgObj.ExportLocation, //반출소속사업장구분
                        ImportLocation = orgObj.ImportLocation, //반입소속사업장구분

                        ExportImportPurposeType = orgObj.ExportImportPurposeType, //반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
                        ImportType = orgObj.ImportType, //반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고     
                        ExportDate = orgObj.ExportDate, //반출예정일
                        ImportDate = orgObj.ImportDate, //반입예정일
                        DeliveryMethodType = orgObj.DeliveryMethodType, //반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량

                        Reason = orgObj.Reason, //반출입사유
                        IsVMI = orgObj.IsVMI, //VMI요청

                        CompanySabun = orgObj.CompanySabun, //회원사번(외부업체인수자)
                        CompanyContactName = orgObj.CompanyContactName, //담당자이름 
                        CompanyName = orgObj.CompanyName, //업체명
                        BizRegNo = orgObj.BizRegNo, //사업자번호
                        CompanyTel = orgObj.CompanyTel, //전화번호

                        IsSelf = orgObj.IsSelf, //본인직접반출

                        Sabun = orgObj.Sabun,//반입사업장인수자
                        Name = orgObj.Name,
                        OrgID = orgObj.OrgID,
                        OrgNameKor = orgObj.OrgNameKor,
                        OrgNameEng = orgObj.OrgNameEng,
                        Tel = orgObj.Tel,

                        ApprovalSabun = orgObj.ApprovalSabun,//결재자
                        ApprovalName = orgObj.ApprovalName,
                        ApprovalOrgID = orgObj.ApprovalOrgID,
                        ApprovalOrgNameKor = orgObj.ApprovalOrgNameKor,
                        ApprovalOrgNameEng = orgObj.ApprovalOrgNameEng,
                        ApprovalTel = orgObj.ApprovalTel,

                        ContactSabun = orgObj.ContactSabun,//부담당자
                        ContactName = orgObj.ContactName,
                        ContactOrgID = orgObj.ContactOrgID,
                        ContactOrgNameKor = orgObj.ContactOrgNameKor,
                        ContactOrgNameEng = orgObj.ContactOrgNameEng,
                        ContactTel = orgObj.ContactTel,

                        ExportDateHour = orgObj.ExportDateHour, // 반출예정 - 시간
                        ExportDateMinute = orgObj.ExportDateMinute, // 반출예정 - 분
                        ImportDateHour = orgObj.ImportDateHour, // 반입예정 - 시간
                        ImportDateMinute = orgObj.ImportDateMinute, // 반입예정 - 분

                        ManagementNo = orgObj.ManagementNo, // 노트북관리번호
                        SerialNo = orgObj.SerialNo, // 노트북시리얼번호
                        IsSelfApproval = orgObj.IsSelfApproval, // 자기결재여부
                        // CeoApprovalFileData = orgObj.CeoApprovalFileData, // CEO승인이력(첨부파일)
                        // CeoApprovalFileDataHash = orgObj.CeoApprovalFileDataHash, // CEO승인이력(첨부파일Hash)

                        ExportImportStatus = ExportImportStatus, //반출입상태
                        CheckCnt = CheckCnt, //확인수량 
                        Opinion = Opinion, //의견  

                        UpdateSabun = my.Sabun,//등록자
                        UpdateName = my.Name,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        UpdateGradeName = my.GradeName,
                        InsertUpdateDate = DateTime.Now,
                        ExportImportApplyStatus = ExportImportApplyStatus
                    };
                    ExportImportHistoryData.Insert(exportImportHistory);
                    ExportImportHistoryData.Save();
                    WriteLog("exportImportHistory: " + Dump(exportImportHistory));
                    if (ExportImportStatus == 10)
                    { // 반입확인
                        TempData["DATA.ImportID"] = newObj.ExportImportID;
                    }

                    // 반입반출 아이템 수정 2023.0914 기획 수정으로 인해 추가
                    if (ExportImportItemData != null)
                    {
                        try
                        {
                            for (var i = 0; i < ExportImportItemID.Length; i++)
                            {
                                var m = ExportImportItemID[i];
                                int id = int.Parse(m);
                                ExportImportItem? exportImportItem = ExportImportItemData.Get(id);
                                if (exportImportItem != null)
                                {
                                    exportImportItem.ImportCnt = float.Parse(ImportCnt[i]);
                                    exportImportItem.RemainCnt = float.Parse(RemainCnt[i]);
                                    ExportImportItemData.Update(exportImportItem);
                                    ExportImportItemData.Save();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            WriteError(e.ToString());
                        }
                    }
                }
            }
            else if (mode.Equals("E"))
            { // 반출입사유 수정
                if (exportImportID > 0 && ExportImportData != null)
                {
                    // 1. 반입반출 반출입사유 수정 
                    var orgObj = GetExportImport(exportImportID, true);
                    var newObj = orgObj.Clone();
                    newObj.Reason = Reason; // 반출입사유
                    newObj.UpdateDate = DateTime.Now;
                    ExportImportData.Update(newObj);
                    ExportImportData.Save();

                    // 1.1 반입반출히스토리 입력: 반출입사유만 수정시에는 히스토리 입력 안함. 
                }
            }
            else if (mode.Equals("RepairCompany"))
            { // 수리업체지정
                if (exportImportID > 0 && ExportImportData != null)
                {
                    // 1. 반입반출 반출입사유 수정 
                    var orgObj = GetExportImport(exportImportID, true);
                    var newObj = orgObj.Clone();
                    newObj.CompanySabun = CompanySabun; //회원사번(외부업체인수자)
                    newObj.CompanyContactName = CompanyContactName; //담당자이름 
                    newObj.CompanyName = CompanyName; //업체명
                    newObj.BizRegNo = BizRegNo; //사업자번호
                    newObj.CompanyTel = CompanyTel; //전화번호

                    newObj.UpdateDate = DateTime.Now;

                    ExportImportData.Update(newObj);
                    ExportImportData.Save();

                    // 1.1 반입반출히스토리 입력: 반출입사유만 수정시에는 히스토리 입력 안함. 
                }
            }
            return RedirectToAction("Detail", new { exportImportID, culture = GetLang() });
        }

        /// <summary>
        /// 노트북 자가결재
        /// 마스터와 IT팀만 추가/삭제
        /// 마스터관리자 : 자가결재 추가/삭제 , 목록조회
        /// IT팀 : 자가결재 추가/삭제
        /// 일반관리자 : 목록조회
        /// 임직원 : 목록조회
        /// (2023년 6월 15일 유선통화) 노트북자가결재 등록페이지 없이 “추가” 버튼 클릭 시 사원검색 팝업창 띄우고 사원을 선택하면 바로 추가되도록 처리. 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        /// searchLocation searchName  searchCompanyName searchOrgNameKor
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchLocation?}/{searchName?}/{searchCompanyName?}/{searchOrgNameKor?}")]
        public IActionResult SelfApproval(NotebookSelfApprovalGridData values, string mode = "")
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }
            var accessible = true; // 2023.09.11 모두 가능하도록 수정 IsMaster() || IsGeneralManager();// || IsITTeam();
            if (accessible == false)
            {
                return View("_Inaccessible");
            }
            NotebookSelfApprovalGridData filterValues = (NotebookSelfApprovalGridData)FilterGridData(values);
            ViewBag.IsEditable = true; // 2023.09.11 모두 가능하도록 수정 IsMaster();// || IsITTeam(); // master or IT Team
            NotebookSelfApprovalListViewModel vm = new()
            {
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CurrentRoute = values,
                SearchRoute = filterValues,
            };
            if (NotebookSelfApprovalData != null)
            {
                var options = new QueryOptions<NotebookSelfApproval>
                {
                    PageNumber = values.PageNumber,
                    PageSize = values.PageSize,
                    OrderByDirection = values.SortDirection,
                };
                // Where = a => a.IsDelete == "N",
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                if (filterValues.SearchLocation > 0)
                {
                    options.MultipleWhere.Add(a => a.Location == filterValues.SearchLocation.ToString());
                }
                if (!string.IsNullOrEmpty(filterValues.SearchName))
                {
                    options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%" + filterValues.SearchName + "%"));
                }
                if (!string.IsNullOrEmpty(filterValues.SearchCompanyName))
                {
                    options.MultipleWhere.Add(a => a.CompanyName != null && EF.Functions.Like(a.CompanyName, "%" + filterValues.SearchCompanyName + "%"));
                }
                if (!string.IsNullOrEmpty(filterValues.SearchOrgNameKor))
                {
                    options.MultipleWhere.Add(a => a.OrgNameKor != null && EF.Functions.Like(a.OrgNameKor, "%" + filterValues.SearchOrgNameKor + "%"));
                    options.MultipleWhere.Add(a => a.OrgNameEng != null && EF.Functions.Like(a.OrgNameEng, "%" + filterValues.SearchOrgNameKor + "%"));
                }
                // if ()
                // searchName searchLocation searchCompanyName searchOrgNameKor
                vm.NotebookSelfApprovals = NotebookSelfApprovalData.List(options);
                vm.TotalPages = values.GetTotalPages(NotebookSelfApprovalData.Count);
                vm.TotalCnt = NotebookSelfApprovalData.Count;
            }
            return View(vm);
        }

        /// <summary>
        /// 마스터와 IT팀만 추가/삭제
        /// </summary>
        /// <param name="values"></param>
        /// <param name="notebookSelfApproval"></param>
        /// <param name="NotebookSelfApprovalID"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchexportlocation?}/{searchimportlocation?}/{searchexportimportstatus?}/{searchexporttype?}/{searchstartinsertdate?}/{searchendinsertdate?}/{searchinsertname?}/{searchname?}/{searchexportimportid?}/{searchinsertorgnamekor?}")]
        public IActionResult SelfApproval(NotebookSelfApprovalGridData values, NotebookSelfApproval notebookSelfApproval, int NotebookSelfApprovalID, string mode = "")
        {
            if (IsAccess() == false)
            {
                return View("_Inaccessible");
            }

            var today = DateTime.Now;
            if (mode == "D")
            {
                WriteLog("mode: " + mode + ", NotebookSelfApprovalID: " + NotebookSelfApprovalID);
                if (NotebookSelfApprovalID > 0)
                {
                    var orgObj = GetNotebookSelfApproval(NotebookSelfApprovalID, true);
                    if (orgObj != null)
                    {
                        var newObj = orgObj.Clone();
                        newObj.IsDelete = "Y";
                        newObj.UpdateDate = today;
                        NotebookSelfApprovalData?.Update(newObj);
                        NotebookSelfApprovalData?.Save();
                    }
                }
            }
            else if (mode == "A" && NotebookSelfApprovalData != null)
            {
                WriteLog("mode: " + mode + ", notebookSelfApproval: " + Dump(notebookSelfApproval));
                PersonDTO my = GetMe();

                // 기 등록된 사원인지 검색
                var options = new QueryOptions<NotebookSelfApproval>
                {
                    // Includes = "",
                    PageNumber = values.PageNumber,
                    PageSize = values.PageSize,
                    OrderByDirection = values.SortDirection,
                    Where = a => a.Sabun == notebookSelfApproval.Sabun && a.IsDelete == "N",
                };
                List<NotebookSelfApproval> list = (List<NotebookSelfApproval>)NotebookSelfApprovalData.List(options);
                if (list.Count > 0)
                { // 갱신
                    NotebookSelfApproval orgObj = list[0];
                    orgObj.UpdateDate = today;
                    NotebookSelfApprovalData.Update(orgObj);
                    NotebookSelfApprovalData.Save();
                }
                else
                { // 신규 등록
                    notebookSelfApproval.InsertSabun = my.Sabun;
                    notebookSelfApproval.InsertName = my.Name;
                    notebookSelfApproval.InsertOrgID = my.OrgID;
                    notebookSelfApproval.InsertOrgNameKor = my.OrgNameKor;
                    notebookSelfApproval.InsertOrgNameEng = my.OrgNameEng;
                    notebookSelfApproval.UpdateDate = today;
                    notebookSelfApproval.InsertDate = today;
                    NotebookSelfApprovalData.Insert(notebookSelfApproval);
                    NotebookSelfApprovalData.Save();
                }
            }
            // return new EmptyResult();
            if (values != null && values.ToDictionary() != null)
            {
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                return RedirectToAction("SelfApproval", dict);
            }
            return RedirectToAction("SelfApproval", new { culture = GetLang() });
        }

        //##### 공통 메소드들 #####
        private int GetExportImportNo()
        {
            int exportImportCntByToday = 0;
            var today = DateTime.Now;
            var options = new QueryOptions<ExportImport>
            {
                Where = a => a.InsertDate.Year == today.Year && a.InsertDate.Month == today.Month && a.InsertDate.Day == today.Day, // 오늘날짜  
            };
            if (ExportImportData != null)
            {
                IEnumerable<ExportImport> exportImport = ExportImportData.List(options) ?? new List<ExportImport>();
                exportImportCntByToday = ExportImportData.Count;
            }
            // options.Max = a => (int?)a.PersonID??0;
            // WriteLog("exportImportCntByToday: "+exportImportCntByToday);
            // maxPersonIDByLocation=personData.Max;

            int maxExportImportCntByToday = exportImportCntByToday + 1;
            return maxExportImportCntByToday;
        }

        private dynamic GetExportImportList(ExportImportGridData values, bool isAll = false)
        {
            var list = new List<ExportImport>();
            if (ExportImportData != null)
            {
                var options = new QueryOptions<ExportImport>
                {
                    PageNumber = values.PageNumber,
                    PageSize = values.PageSize,
                    OrderByDirection = values.SortDirection,
                };
                options.MultipleWhere.Add(a => a.IsDelete == "N");
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsSecurity()
                         || IsPurchasing()) //2023.09.27 dsyoo 구매팀 추가
                    {
                        options.MultipleWhere.Add(x => x.ExportLocation == my.Location || x.ImportLocation == my.Location);
                    }
                    else if (IsEmployee())
                    {
                        // x.Sabun 반입사업장 인수자, x.InsertSabun 등록자, x.ApprovalSabun 결재자, x.ContactSabun 부담당자
                        options.MultipleWhere.Add(x => x.Sabun == my.Sabun || x.InsertSabun == my.Sabun || x.ApprovalSabun == my.Sabun || x.ContactSabun == my.Sabun);
                    }
                    else if (IsPartnerNonresidentManager())
                    {
                        options.MultipleWhere.Add(x => x.BizRegNo == GetCompanyBizRegNo(my.CompanyID)); // 비상주업체 관리자 신청건
                        // 외부에서 신청건에 대해서 회사명 LIKE 검색으로 처리할 지는 논의 중.
                    }
                }

                if (values.SearchExportLocation > -1)
                {
                    options.MultipleWhere.Add(a => a.ExportLocation == values.SearchExportLocation.ToString());
                }
                if (values.SearchImportLocation > -1)
                {
                    options.MultipleWhere.Add(a => a.ImportLocation == values.SearchImportLocation.ToString());
                }
                if (values.SearchExportImportStatus > -1)
                {
                    options.MultipleWhere.Add(a => a.ExportImportStatus == values.SearchExportImportStatus);
                }
                if (values.SearchExportType > -1)
                {
                    options.MultipleWhere.Add(a => a.ExportType == values.SearchExportType); //2023.10.06 신인아 바로잡음
                }
                if (values.SearchExportImportApplyStatus > -1)
                {
                    options.MultipleWhere.Add(a => a.ExportImportApplyStatus == values.SearchExportImportApplyStatus);
                }
                try
                {
                    if (!string.IsNullOrEmpty(values.SearchStartInsertDate))
                    {
                        // string d = values.SearchStartInsertDate + " 00:00:01";
                        string d = values.SearchStartInsertDate; //2023.10.06 신인아
                        options.MultipleWhere.Add(x => (DateTime)(object)x.InsertDate >= Convert.ToDateTime(d));
                    }
                    if (!string.IsNullOrEmpty(values.SearchEndInsertDate))
                    {
                        // string d = values.SearchEndInsertDate + " 23:59:59";
                        string d = values.SearchEndInsertDate; //2023.10.06 신인아
                        options.MultipleWhere.Add(x => (DateTime)(object)x.InsertDate <= Convert.ToDateTime(d));
                    }
                }
                catch (Exception e)
                {
                    WriteError(e.ToString());
                }
                if (!string.IsNullOrEmpty(values.SearchInsertName))
                {
                    options.MultipleWhere.Add(a => a.InsertName != null && EF.Functions.Like(a.InsertName, "%" + values.SearchInsertName + "%"));
                }
                if (!string.IsNullOrEmpty(values.SearchName))
                {
                    //인수자 
                    //ExportType - 0 외부업체 일때  CompanyContactName CompanyName 
                    //ExportType - 1 공장간이동 일때  Name 	OrgNameKor 
                    //ExportType - 2 외부업체경유공장간이동 일때  Name 	OrgNameKor 
                    // options.MultipleWhere.Add(a => a.Name != null && EF.Functions.Like(a.Name, "%"+values.SearchName+"%"));
                    options.MultipleWhere.Add(a => (a.Name != null && EF.Functions.Like(a.Name, "%" + values.SearchName + "%")) || (a.CompanyContactName != null && EF.Functions.Like(a.CompanyContactName, "%" + values.SearchName + "%")));
                }
                if (!string.IsNullOrEmpty(values.SearchInsertOrgNameKor))
                {
                    options.MultipleWhere.Add(a => a.InsertOrgNameKor == values.SearchInsertOrgNameKor);
                }
                if (!string.IsNullOrEmpty(values.SearchExportImportID))
                {
                    options.MultipleWhere.Add(a => a.ExportImportNo == values.SearchExportImportID);
                }
                options.OrderBy = a => a.ExportImportID;
                list = (List<ExportImport>)ExportImportData.List(options);
                return new { a = list, b = ExportImportData.Count };
            }
            return new { a = list, b = 0 };
        }

        private ExportImport GetExportImport(int exportImportID = -1, bool isNoTracking = false)
        {
            ExportImport exportImport = null;
            if (exportImportID > 0)
            {
                var options = new QueryOptions<ExportImport>
                {
                    Where = a => a.ExportImportID == exportImportID && a.IsDelete == "N",
                };
                if (isNoTracking)
                {
                    exportImport = ExportImportData.GetNT(exportImportID);
                }
                else
                {
                    exportImport = ExportImportData.Get(exportImportID);
                }
            }
            return exportImport;
        }


        private NotebookSelfApproval? GetNotebookSelfApproval(int notebookSelfApprovalID = -1, bool isNoTracking = false)
        {
            NotebookSelfApproval? notebookSelfApproval = null;
            if (notebookSelfApprovalID > 0)
            {
                var options = new QueryOptions<NotebookSelfApproval>
                {
                    Where = a => a.NotebookSelfApprovalID == notebookSelfApprovalID && a.IsDelete == "N",
                };
                if (isNoTracking)
                {
                    notebookSelfApproval = NotebookSelfApprovalData?.GetNT(notebookSelfApprovalID);
                }
                else
                {
                    notebookSelfApproval = NotebookSelfApprovalData?.Get(notebookSelfApprovalID);
                }
            }
            return notebookSelfApproval;
        }

        /// <summary>
        /// 테스트 검색어: 자재이름 - PH
        /// SAPITEMLIST 에서 자재 정보 조회: Field4, Field5, Field6, Field7
        /// FAB1 / FAB2 검색 조건 미수령
        /// Field1 11 / 22 / 33 
        /// Field2 사업장 FAB1: 2000, FAB2: 3000 (확인 필요)
        /// Field3
        /// Field4 자재코드 
        /// Field5 자재이름  FAB1 FAB2
        /// Field6 규격
        /// Field7 단위
        /// Field8 날짜
        /// Field9, Field10 사용 안함.
        /// </summary>
        /// <param name="itemName">자재 이름</param>
        /// <param name="itemCode">자재 코드</param>
        /// <param name="fab">FAB1 | FAB2</param>
        /// <returns></returns>
        public JsonResult? GetItemList(string itemName = "", string itemCode = "", string fab = "FAB1")
        {
            if (IsAccessAPI() == false)
            {
                return default;
            }
            WriteLog("itemName: " + itemName + ", itemCode: " + itemCode);
            IQueryable<Sapitemlist> sapitemlists = DbPASSPORT.Sapitemlists;
            if (!string.IsNullOrEmpty(itemName))
            {
                WriteLog("itemName: " + itemName);
                sapitemlists = sapitemlists.Where(x => EF.Functions.Like(x.Field5, "%" + itemName + "%"));
            }
            if (!string.IsNullOrEmpty(itemCode))
            {
                sapitemlists = sapitemlists.Where(x => x.Field4 == itemCode);
            }
            List<Sapitemlist> list = sapitemlists.ToList();
            return Json(list);
            // return null;
        }

        public JsonResult? VendorCheck(string itemCode = "MEP0078R", string location = "2000")
        {
            string connectionString = "AppServerHost=10.145.2.31; SystemNumber=00; User=S0000014; Password=informatica9; Client=100; Language=EN; PoolSize=5; Trace=8";
            SapConnection? connection = null;
            RFCZMMVisitorVendorCheckResult? result = null;
            try
            {
                connection = new SapConnection(connectionString);
                connection.Connect();
                if (connection.IsValid)
                {
                    // WriteLog("connection:"+Dump(connection));
                    using var someFunction = connection.CreateFunction("Z_MM_VISITOR_VENDOR_CHECK");
                    result = someFunction.Invoke<RFCZMMVisitorVendorCheckResult>(new RFCZMMVisitorVendorCheckParameters
                    {
                        I_WERKS = location,
                        I_MATNR = itemCode,
                    });

                    // {"E_LIFNR":"","E_SUBRC":"E","E_MESSAGE":"\uC800\uC7A5\uB41C \uBCA4\uB354\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4."}
                    WriteLog("result:" + Dump(result));
                    WriteLog("result string:" + result.E_MESSAGE.ToString());
                }
            }
            catch (Exception e)
            {
                WriteError(e.ToString());
            }
            finally
            {
                connection?.Disconnect();
            }
            return Json(result);
        }

        public JsonResult? GetItemListByPR(string prcode = "1000150932")
        {
            string connectionString = "AppServerHost=10.145.2.31; SystemNumber=00; User=S0000014; Password=informatica9; Client=100; Language=EN; PoolSize=5; Trace=8";
            SapConnection? connection = null;
            RFCZMMVisitorPRCheckResult? result = null;
            try
            {
                connection = new SapConnection(connectionString);
                connection.Connect();
                if (connection.IsValid)
                {
                    using var someFunction2 = connection.CreateFunction("Z_MM_VISITOR_PR_CHECK");
                    // BANFN="1000150932",
                    result = someFunction2.Invoke<RFCZMMVisitorPRCheckResult>(new RFCZMMVisitorPRCheckParameters
                    {
                        BANFN = prcode,
                    });
                    // 1000236674 1000169868
                    // Test="1000150932"
                    // {"E_LIFNR":"","E_SUBRC":"E","E_MESSAGE":"\uC800\uC7A5\uB41C \uBCA4\uB354\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4."}
                    WriteLog("result:" + Dump(result));
                    // WriteLog("result string:"+result2.E_MESSAGE.ToString());
                    // msg = Dump(result);
                    // msg = result.E_MESSAGE.ToString();
                }
            }
            catch (Exception e)
            {
                WriteError(e.ToString());
            }
            finally
            {
                connection?.Disconnect();
            }
            return Json(result);
        }

    }
}
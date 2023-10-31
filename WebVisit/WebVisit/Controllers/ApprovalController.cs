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
using WebVisit.Models.DomainModels.LPR;

namespace WebVisit.Controllers
{

    [Route("[controller]/[action]")]
    public class ApprovalController : BaseController
    {
        private Repository<PorteLog>? PorteLogData { get; set; }

        private Repository<ExportImport>? ExportImportData { get; set; }
        private Repository<ExportImportHistory>? ExportImportHistoryData { get; set; }
        private Repository<ExportImportItem>? ExportImportItemData { get; set; }
        
        private Repository<CarryItemApply>? CarryItemApplyData { get; set; }
        private Repository<CarryItemApplyHistory>? CarryItemApplyHistoryData { get; set; }
        private Repository<CarryItemInfo>? CarryItemInfoData { get; set; }        
        private Repository<CarryItemInfoHistory>? CarryItemInfoHistoryData { get; set; }

        // private Repository<Company>? CompanyData { get; set; }
        // private Repository<Person>? PersonData { get; set; }
        private Repository<WorkApply>? WorkApplyData { get; set; }
        private Repository<WorkApplyAttachFile>? WorkApplyAttachFileData { get; set; }


        public ApprovalController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer) : base(logger, configuration, localizer) { }
        protected override void Init() {
            if (DbSVISIT != null) {
                PorteLogData ??= new Repository<PorteLog>(DbSVISIT);

                ExportImportData ??= new Repository<ExportImport>(DbSVISIT);
                ExportImportHistoryData ??= new Repository<ExportImportHistory>(DbSVISIT);
                ExportImportItemData ??= new Repository<ExportImportItem>(DbSVISIT);

                CarryItemApplyData ??= new Repository<CarryItemApply>(DbSVISIT);
                CarryItemApplyHistoryData ??= new Repository<CarryItemApplyHistory>(DbSVISIT);
                CarryItemInfoData ??= new Repository<CarryItemInfo>(DbSVISIT);
                CarryItemInfoHistoryData ??= new Repository<CarryItemInfoHistory>(DbSVISIT);

                CompanyData ??= new Repository<Company>(DbSVISIT);
                PersonData ??= new Repository<Person>(DbSVISIT);

                WorkApplyData ??= new Repository<WorkApply>(DbSVISIT);
                WorkApplyAttachFileData ??= new Repository<WorkApplyAttachFile>(DbSVISIT);
            }
            if (DbPASSPORT != null) {
                // ViewAccesseventListData ??= new Repository<ViewAccesseventList>(DbPASSPORT);
            }
        }
        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("ExportImport");
        }

        /*
출입관리_자산반입확인	DM_IMS	ims_aioc	https://ims.dbhitek.com/approval/aioc
출입관리_휴대물품 반입 신청	DM_IMS	ims_cios	http://ims.dbhitek.com/approval/cios
출입관리_비상주협력사신청	DM_IMS	ims_cmps	https://ims.dbhitek.com/approval/cmps
출입관리_공사등록신청	DM_IMS	ims_works	https://ims.dbhitek.com/approval/works
출입관리_선입고신청	DM_IMS	ims_pios	https://ims.dbhitek.com/approval/pios        

출입관리_평가샘플반입신청	DM_IMS	ims_sios	http://ims.dbhitek.com/approval/sios
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportImportID"></param>
        /// <param name="eiType">1: 반출 신청, 2: 반입 신청</param>
        /// <returns></returns>
        public IActionResult ExportImport(int exportImportID, int eiType = 1){
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("ExportImportID:" + exportImportID);
            ViewBag.ApprType = 1;
            ExportImportDetailViewModel vm = new();
            if (ExportImportData != null && ExportImportItemData != null && ExportImportHistoryData != null){
                var exportImport = ExportImportData.Get(new QueryOptions<ExportImport>{
                    Where = a => a.ExportImportID == exportImportID,
                }) ?? new ExportImport();
                if(exportImport != null && exportImport.ExportImportID > 0){
                    var options = new QueryOptions<ExportImportItem>
                    {
                        Where = a => a.ExportImportID == exportImportID && a.IsDelete == "N",
                    };

                    var options2 = new QueryOptions<ExportImportHistory>
                    {
                        Where = a => a.ExportImportID == exportImportID,
                    };
                    vm.ExportImport = exportImport;
                    vm.ExportImportItems = ExportImportItemData.List(options);
                    vm.ExportImportHistory = ExportImportHistoryData.List(options2);
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    vm.CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                    // 반출입구분 ExportImportType 0 자산, 1 수리, 2 노트북 
                    vm.CodeExportImportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportType);
                    // 반출구분 ExportType : 0 외부업체, 1 공장간이동, 2 외부업체경유공장간이동
                    vm.CodeExportType = GetCommonCodes((int)VisitEnum.CommonCode.ExportType);
                    // 반출입목적 ExportImportPurposeType: 0 수리, 1 리클레임 2 판매 3 세정(재생) 4 교환  5 이체(이동) 6 기타-(X)  7 분석  8 Demo 9 우수협력업체-(X) 10 Warranty 11 반환  12 태블릿 
                    vm.CodeExportImportPurposeType = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportPurposeType);
                    // 반입구분 ImportType :0 반입요  1 반입불요 2 매각 3 반입불요(무상) 4 대체품반입요 5 대체품선입고
                    vm.CodeImportType = GetCommonCodes((int)VisitEnum.CommonCode.ImportType);
                    // 반출물전달방법 DeliveryMethodType : 0	방문수령, 1	우편/택배, 2	대리인수령, 3	핸드캐리, 4	물류차량
                    vm.CodeDeliveryMethodType = GetCommonCodes((int)VisitEnum.CommonCode.DeliveryMethodType);
                    // ExportImportStatus 반출입상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
                    vm.CodeExportImportStatus = GetCommonCodes((int)VisitEnum.CommonCode.ExportImportStatus);

                    // title 출입관리_자산반출입신청
                    // userid password
                    // systemid DM_IMS 
                    // businessid ims_aios
                    // legacy_apprlines 김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1
                    WriteLog("exportImport:" + Dump(exportImport));
                    var l = GetUnionPerson(null, exportImport.ApprovalSabun);
                    // var l = GetUnionPerson(null, "P9000058"); 
                    if(l != null && l.a != null && l.a.Count > 0){
                        StringBuilder sb = new();
                        sb.Append(exportImport.ApprovalName);
                        sb.Append('^');
                        sb.Append(exportImport.ApprovalSabun);
                        sb.Append('^');
                        Person person = l.a[0];
                        sb.Append(person.Email);
                        sb.Append('^');
                        sb.Append('2');
                        sb.Append('^');
                        sb.Append('1');
                        string apprLines = sb.ToString();
                        apprLines = EncMD5(apprLines);
                        WriteLog("apprLines:" + apprLines);
                        ApprovalRecvData approvalRecvData = new()
                        {
                            ID = exportImport.ExportImportID,
                        };
                        string title = "출입관리_자산반출입신청";
                        string bizId = "ims_aios";
                        if(eiType == 2){
                            title = "출입관리_자산반입확인";
                            bizId = "ims_aioc";
                        }
                        ApprovalRequest approvalRequest = new()
                        {
                            Legacyout = Dump(approvalRecvData),
                            Title=title,
                            BusinessId = bizId,
                            LegacyApprLines = apprLines,
                            DevLegacyApprLines = sb.ToString(),
                        };
                        var l2 = GetUnionPerson(null, exportImport.InsertSabun);
                        WriteLog("l2:"+Dump(l2));
                        if (l2 != null && l2.a != null && l2.a.Count > 0)
                        {
                            Person person2 = l2.a[0];
                            WriteLog("person2:"+Dump(person2));
                            WriteLog("porte:" + person2.PorteID + ", "+person2.Password);
                            // http://localhost:5010/approval/exportimport?ExportImportID=3
                            approvalRequest.UserId = person2.PorteID;
                            approvalRequest.Password = person2.Password;
                            // dongju.hyun, 8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg=
                        }
                        ViewBag.ApprovalRequest = approvalRequest;
                    }
                }
            }
            return View(vm);
        }

        /// <summary>
        /// 출입관리_자산반출입신청
        /// [event]
        /// 1   : 상신 : (기안자, 신청부서 기안자) IsDelete = N ?
        /// 2   : 기안회수 : (기안자)
        /// 4   : 중간결재 : (기안자, 협조자, 합의자)
        /// 8   : 일반결재 완료 : (전결자, 대결자, 결재자, 1인결재자)
        /// 16  : 반려 : (결재자, 협조자, 전결자 ,대결자)
        /// 32  : 이중결재 신청부서완료 : (신청부서 최종결재자 완결 시)
        /// 64   : 이중결재 주관부서 상신 : (주관부서 기안자 상신 시)
        /// 128 : 이중결재 주관부서 완료 : (주관부서 최종결재자 완결 시)
        /// 256 : 임시저장 : (기안자가 임시저장 시)
        /// 512 : 승인보류 : (결재자가 승인보류 시)
        /// 1024 : 삭제 : (휴지통에 있는 상신이전의 문서 삭제 시)
        /*  docid = 49263
            userid = eunji86
            systemid = DM_IMS
            businessid = ims_aios
            username = %bc%db%c0%ba%c1%f6
            event = 1
            employeeid = P9000058
            deptcode = DM02_DA20001
            legacyin = {"ID":3}
            title = %c3%e2%c0԰%fc%b8%ae_%c0ڻ%ea%b9%dd%c3%e2%c0Խ%c5û
            opinion = 
            signdate = 2023-08-30 21:40:17 */
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Aios(ApprovalResponse approvalResponse)
        {
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            PrintPostFormData();
            var isSuccess = "N";
            var rs = _Dump<ApprovalRecvData>(approvalResponse.Legacyin);
            if (rs != null && ExportImportData != null && ExportImportItemData != null && ExportImportHistoryData != null)
            {
                var exportImport = ExportImportData.Get(new QueryOptions<ExportImport>
                {
                    Where = a => a.ExportImportID == rs.ID,
                }) ?? new ExportImport();
                WriteLog("ApprovalRecvData:" + Dump(rs));
                if(exportImport != null &&exportImport.ExportImportID > 0){
                    isSuccess = "Y";
                    bool isUpdate = false;
                    if (approvalResponse.Event == 1){ // 상신
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 64; //2023.09.17 dsyoo 반출상신 
                    }else if (approvalResponse.Event == 8){ // 결재 완료
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 1;
                    }else if (approvalResponse.Event == 16){ // 반려
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 2;
                    }
                    else if (approvalResponse.Event == 4 || approvalResponse.Event == 32) //2023.09.17 dsyoo
                    {
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 3;
                    }else if (approvalResponse.Event == 128){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 4;
                    }else if (approvalResponse.Event == 64){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 64;
                    }else if (approvalResponse.Event == 256){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 256;
                    }else if (approvalResponse.Event == 512){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 512;
                    }
                    else if (approvalResponse.Event == 2 || approvalResponse.Event == 1024) //2023.09.17 dsyoo
                    {
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 1024;
                    }
                    if(isUpdate) {
                        ExportImportData.Update(exportImport);
                        ExportImportData.Save();
                    }
                }
            }

            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }
        
        /// <summary>
        /// 출입관리_자산반입확인
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Aioc(ApprovalResponse approvalResponse)
        {
            PrintPostFormData();
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            var isSuccess = "N";
            var rs = _Dump<ApprovalRecvData>(approvalResponse.Legacyin);
            if (rs != null && ExportImportData != null && ExportImportItemData != null && ExportImportHistoryData != null)
            {
                var exportImport = ExportImportData.Get(new QueryOptions<ExportImport>
                {
                    Where = a => a.ExportImportID == rs.ID,
                }) ?? new ExportImport();
                WriteLog("ApprovalRecvData:" + Dump(rs));
                if(exportImport != null &&exportImport.ExportImportID > 0){
                    isSuccess = "Y";
                    bool isUpdate = false;
                    if (approvalResponse.Event == 1){ // 상신
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 64;
                        // exportImport.ExportImportStatus = 8; // 반입상신
                    }else if (approvalResponse.Event == 8){ // 결재 완료
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 1;
                        // exportImport.ExportImportStatus = 2; // 반입 결재
                    }else if (approvalResponse.Event == 16){ // 반려
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 2;
                        // exportImport.ExportImportStatus = 3; // 반입대기 반려
                    }
                    else if (approvalResponse.Event == 4 || approvalResponse.Event == 32) //2023.09.17 dsyoo
                    {
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 3;
                    }else if (approvalResponse.Event == 128){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 4;
                    }else if (approvalResponse.Event == 64){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 64;
                    }else if (approvalResponse.Event == 256){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 256;
                    }else if (approvalResponse.Event == 512){
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 512;
                    }
                    else if (approvalResponse.Event == 2 || approvalResponse.Event == 1024) //2023.09.17 dsyoo
                    {
                        isUpdate = true;
                        exportImport.ExportImportApplyStatus = 1024;
                    }
                    if(isUpdate) {
                        ExportImportData.Update(exportImport);
                        ExportImportData.Save();
                    }
                }
            }

            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carryItemApplyID"></param>
        /// <returns></returns>
        public IActionResult CarryItem(int carryItemApplyID)
        {
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("carryItemApplyID:" + carryItemApplyID);
            ViewBag.ApprType = 3; // 3: 일반, 4: 선입고

            CarryItemDetailViewModel vm = new();
            if (CarryItemApplyData != null && CarryItemInfoData != null && CarryItemApplyHistoryData != null){
                var carryItemApply = CarryItemApplyData.Get(new QueryOptions<CarryItemApply>{
                    Where = a => a.CarryItemApplyID == carryItemApplyID,
                }) ?? new CarryItemApply();
                if(carryItemApply != null && carryItemApply.CarryItemApplyID > 0 && !string.IsNullOrEmpty(carryItemApply.InsertSabun)){
                    var options = new QueryOptions<CarryItemInfo>
                    {
                        Where = a => a.CarryItemApplyID == carryItemApplyID && a.IsDelete == "N",
                    };

                    var optionsHistory = new QueryOptions<CarryItemApplyHistory>
                    {
                        Where = a => a.CarryItemApplyID == carryItemApplyID,
                    };
                    // carryItemApply.ImportPurposeCodeID  == 4 선입고
                    vm.CarryItemApply = carryItemApply;
                    vm.CarryItemInfos = CarryItemInfoData.List(options);
                    vm.CarryItemApplyHistoryList = CarryItemApplyHistoryData.List(optionsHistory);
                    // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                    vm.CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                    //반입목적 ImportPurpose: 0 Set Up, 1 장비교체, 2 데모, 3 납품, 4 기타
                    vm.CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose);
                    // 반입장소 Place: 0 공통-통제구역-적색-클린룸, 1 공통-제한구역-황색-건물내부, 2 공통-일반구역-청색-건물외부/접견실/납품 ....... 
                    vm.CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place);
                    // 휴대물품 구분 CarryItem : 0 노트북, 1 PC, 2 카메라, 3 기타 
                    vm.CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem);
                    // 단위 Unit : 0: 개, 1: 대, 2: EA, 3: SIK
                    vm.CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit);
                    // 휴대물품신청상태  CarryItemApplyStatus : 0 승인대기, 1 승인완료, 2 반려, 3 결재중, 4 결재완료
                    vm.CodeCarryItemApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemApplyStatus);
                    // 휴대물품반입구분 CarryItemImportType 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
                    vm.CodeCarryItemImportType = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemImportType);
                    vm.CodeCarryItemStatus = GetCommonCodes((int)VisitEnum.CommonCode.CarryItemStatus);

                    // title 출입관리_휴대물품 반입 신청
                    // userid password
                    // systemid DM_IMS 
                    // businessid ims_cios
                    // legacy_apprlines 김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1
                    WriteLog("carryItemApply:" + Dump(carryItemApply));
                    var l = GetUnionPerson(null, carryItemApply.ApprovalSabun);
                    // var l = GetUnionPerson(null, "P9000058"); 
                    if(l != null && l.a != null && l.a.Count > 0){
                        StringBuilder sb = new();
                        sb.Append(carryItemApply.ApprovalName);
                        sb.Append('^');
                        sb.Append(carryItemApply.ApprovalSabun);
                        sb.Append('^');
                        Person person = l.a[0];
                        sb.Append(person.Email);
                        sb.Append('^');
                        sb.Append('2');
                        sb.Append('^');
                        sb.Append('1');
                        string apprLines = sb.ToString();
                        apprLines = EncMD5(apprLines);
                        WriteLog("apprLines:" + apprLines);
                        ApprovalRecvData approvalRecvData = new()
                        {
                            ID = carryItemApply.CarryItemApplyID,
                        };
                        string title = "출입관리_휴대물품반입신청";
                        string bizId = "ims_cios";
                        // if(eiType == 2){//타입추가 필요시 수정 
                        //     title = "출입관리_휴대물품반입신청";
                        //     bizId = "ims_cios";
                        // }
                        ApprovalRequest approvalRequest = new()
                        {
                            Legacyout = Dump(approvalRecvData),
                            Title=title,
                            BusinessId = bizId,
                            LegacyApprLines = apprLines,
                            DevLegacyApprLines = sb.ToString(),
                        };
                        var l2 = GetUnionPerson(null, carryItemApply.InsertSabun);
                        WriteLog("InsertSabun:" + carryItemApply.InsertSabun);
                        // WriteLog("l2:"+Dump(l2));
                        if (l2 != null && l2.a != null && l2.a.Count > 0)
                        {
                            Person person2 = l2.a[0];
                            // WriteLog("person2:"+Dump(person2));
                            WriteLog("porte:" + person2.PorteID + ", "+person2.Password);
                            // http://localhost:5010/approval/exportimport?ExportImportID=3
                            approvalRequest.UserId = person2.PorteID;
                            approvalRequest.Password = person2.Password;
                            // dongju.hyun, 8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg=
                        }
                        ViewBag.ApprovalRequest = approvalRequest;
                    }
                    
                }else{
                    return new EmptyResult();
                }
            }else{
                return new EmptyResult();
            }
            return View(vm);
        }

        /// <summary>
        /// 출입관리_휴대물품 반입 신청 ims_cios
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cios(ApprovalResponse approvalResponse)
        {
            PrintPostFormData();
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            var isSuccess = "Y";

            var rs = _Dump<ApprovalRecvData>(approvalResponse.Legacyin);
            if (rs != null && CarryItemApplyData != null && CarryItemInfoData != null && CarryItemApplyHistoryData != null)
            {
                WriteLog("ApprovalRecvData:" + Dump(rs));
                var carryItemApply = CarryItemApplyData.Get(new QueryOptions<CarryItemApply>{
                    Where = a => a.CarryItemApplyID == rs.ID,
                }) ?? new CarryItemApply();

                if(carryItemApply != null && carryItemApply.CarryItemApplyID > 0 && !string.IsNullOrEmpty(carryItemApply.InsertSabun)){
                    isSuccess = "Y";
                    bool isUpdate = false;
                    if (approvalResponse.Event == 1){ // 상신
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 64;
                        // exportImport.ExportImportStatus = 8; // 반입상신
                    }else if (approvalResponse.Event == 8){ // 결재 완료
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 1;
                        // exportImport.ExportImportStatus = 2; // 반입 결재
                    }else if (approvalResponse.Event == 16){ // 반려
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 2;
                        // exportImport.ExportImportStatus = 3; // 반입대기 반려
                    }else if (approvalResponse.Event == 4 || approvalResponse.Event == 32){
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 3;
                    }else if (approvalResponse.Event == 128){
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 4;
                    }else if (approvalResponse.Event == 64){
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 64;
                    }else if (approvalResponse.Event == 256){
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 256;
                    }else if (approvalResponse.Event == 512){
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 512;
                    }else if (approvalResponse.Event == 2 || approvalResponse.Event == 1024){
                        isUpdate = true;
                        carryItemApply.CarryItemApplyStatus = 1024;
                    }
                    if (isUpdate)
                    {
                        CarryItemApplyData.Update(carryItemApply);
                        CarryItemApplyData.Save();
                        //2023.09.22 dsyoo
                        SaveCarryElApproveHistory(carryItemApply.CarryItemApplyID, carryItemApply.CarryItemApplyStatus, approvalResponse);
                    }
                }
            }
            
            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }
        #region SaveCarryELApproveHistory  2023.09.21 휴대물품 승인상태정보를 저장한다.
        private void SaveCarryElApproveHistory(int CarryItemApplyID, int CarryItemApplyStatus, ApprovalResponse approvalResponse)
        {
            //2023.09.22 dsyoo 휴대물품 전자결재 정보 저장하기
            var newObj = CarryItemApplyData.Get(new QueryOptions<CarryItemApply>
            {
                Where = a => a.CarryItemApplyID == CarryItemApplyID,
            }) ?? new CarryItemApply();
            var OrgName = "";
            //VImsOrgInfo org = GetDeptInfo(approvalResponse.Deptcode);
            //if (org != null)
            //{
            //    OrgName = org.OrgName;
            //}
            var ResponseName = "";
            VImsUserInfo userInfo = GetUserInfo(approvalResponse.Employeeid);
            if (userInfo != null)
            {
                ResponseName = userInfo.UserName;
                OrgName = userInfo.DeptName;
            }

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


                Memo = approvalResponse.Opinion, // 비고
                UpdateSabun = approvalResponse.Employeeid,//등록자가 방문자가 아닐경우 회원정보 입력 
                UpdateName = ResponseName,
                UpdateOrgID = approvalResponse.Deptcode,
                UpdateOrgNameKor = OrgName,
                UpdateOrgNameEng = OrgName,
                InsertUpdateDate = DateTime.Now
            };
            CarryItemApplyHistoryData.Insert(carryItemApplyHistory);
            CarryItemApplyHistoryData.Save();

        }
        #endregion
        #region GetDeptInfo 2023.09.21 조직정보를 받아온다.
        private VImsOrgInfo GetDeptInfo(string DeptCode)
        {
            VImsOrgInfo orgInfo = null;
            if (DbPASSPORT != null && !string.IsNullOrEmpty(DeptCode))
            {
                List<VImsOrgInfo> list = new();
                List<string> strWhere = new();
                strWhere.Add("SAP_ORG_CODE = '" + DeptCode + "'");

                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.V_IMS_ORG_INFO ");
                if (strWhere.Count > 0)
                {
                    sb.Append(" WHERE ");
                    bool isFist = true;
                    foreach (var m in strWhere)
                    {
                        if (!isFist)
                        {
                            sb.Append(" AND ");
                        }
                        sb.Append(" ( ");
                        sb.Append(m);
                        sb.Append(" ) ");
                        isFist = false;
                    }
                }
                // sb.Append(" OFFSET ");
                // var offset = (PageNumber -1) * PageSize;
                // sb.Append(offset);
                // sb.Append(" ROWS FETCH NEXT ");
                // sb.Append(PageSize);
                // sb.Append(" ROWS ONLY");
                var fs2 = FormattableStringFactory.Create(sb.ToString());
                WriteLog("query: " + sb.ToString());
                list = DbPASSPORT.VImsOrgInfos
                    .FromSql(fs2)
                    .ToList();
                WriteLog("list: " + Dump(list));
                if (list.Count > 0)
                {
                    orgInfo = new VImsOrgInfo();
                    orgInfo.SapOrgCode = list[0].SapOrgCode;
                    orgInfo.OrgName = list[0].SapOrgCode;
                    orgInfo.SapOrgParentCode = list[0].SapOrgParentCode;
                    orgInfo.IsDeleted = list[0].IsDeleted;
                }
            }
            return orgInfo;
        }
        #endregion
        #region GetUserInfo 2023.09.21 조직정보를 받아온다.
        private VImsUserInfo GetUserInfo(string EmployeeID)
        {
            VImsUserInfo userInfo = null;
            if (DbPASSPORT != null && !string.IsNullOrEmpty(EmployeeID))
            {
                List<VImsUserInfo> list = new();
                List<string> strWhere = new();
                strWhere.Add("EMPLOYEE_ID = '" + EmployeeID + "'");

                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.V_IMS_USER_INFO ");
                if (strWhere.Count > 0)
                {
                    sb.Append(" WHERE ");
                    bool isFist = true;
                    foreach (var m in strWhere)
                    {
                        if (!isFist)
                        {
                            sb.Append(" AND ");
                        }
                        sb.Append(" ( ");
                        sb.Append(m);
                        sb.Append(" ) ");
                        isFist = false;
                    }
                }
                // sb.Append(" OFFSET ");
                // var offset = (PageNumber -1) * PageSize;
                // sb.Append(offset);
                // sb.Append(" ROWS FETCH NEXT ");
                // sb.Append(PageSize);
                // sb.Append(" ROWS ONLY");
                var fs2 = FormattableStringFactory.Create(sb.ToString());
                WriteLog("query: " + sb.ToString());
                list = DbPASSPORT.VImsUserInfos
                    .FromSql(fs2)
                    .ToList();
                WriteLog("list: " + Dump(list));
                if (list.Count > 0)
                {
                    userInfo = new VImsUserInfo();
                    userInfo.SapDeptCode = list[0].SapDeptCode;
                    userInfo.UserName = list[0].UserName;
                    userInfo.DeptName = list[0].DeptName;
                    userInfo.IsDeleted = list[0].IsDeleted;
                }
            }
            return userInfo;
        }
        #endregion

        /// <summary>
        /// 출입관리_선입고신청 ims_pios (휴대물품)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Pios(ApprovalResponse approvalResponse)
        {
            PrintPostFormData();
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            var isSuccess = "Y";
            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }

        public IActionResult Company(int companyID)
        {
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("companyID:" + companyID);
            ViewBag.ApprType = 5;
            
            CompanyDetailViewModel vm = new();
            // WriteLog("CompanyData:" + Dump(CompanyData));
            // WriteLog("PersonData:" + Dump(PersonData));
            if(companyID > 0 && CompanyData != null && PersonData != null){
                var company = CompanyData.Get(companyID);
                // if (!string.IsNullOrEmpty(company.BizFileData)) {
                //     FileDTO? ff = _Dump<FileDTO>(company.BizFileData);
                //     ViewBag.file1Name = ff?.FileName;
                // }
                Person? p = PersonData.Get(new QueryOptions<Person>{
                    Where = a => a.CompanyID == company.CompanyID  
                })??new Person();
                vm.CodeCompanyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CompanyStatus);
                vm.Company = company;
                vm.Person = p;

                WriteLog("company:" + Dump(company));
                // title 출입관리_비상주협력사신청
                // userid password
                // systemid DM_IMS 
                // businessid ims_cmps
                // legacy_apprlines 김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1
                // WriteLog("company:" + Dump(company));
                var l = GetUnionPerson(null, company.ApprovalSabun);
                
                // var l = GetUnionPerson(null, "P9000058"); 
                if(l != null && l.a != null && l.a.Count > 0){
                    StringBuilder sb = new();
                    sb.Append(company.ApprovalName);
                    sb.Append('^');
                    sb.Append(company.ApprovalSabun);
                    sb.Append('^');
                    Person person = l.a[0];
                    sb.Append(person.Email);
                    sb.Append('^');
                    sb.Append('2');
                    sb.Append('^');
                    sb.Append('1');
                    WriteLog("Approval person:" + Dump(person));
                    string apprLines = sb.ToString();
                    apprLines = EncMD5(apprLines);
                    WriteLog("apprLines:" + apprLines);
                    ApprovalRecvData approvalRecvData = new()
                    {
                        ID = company.CompanyID,
                    };
                    string title = "출입관리_비상주협력사신청";
                    string bizId = "ims_cmps";
                    ApprovalRequest approvalRequest = new()
                    {
                        Legacyout = Dump(approvalRecvData),
                        Title=title,
                        BusinessId = bizId,
                        LegacyApprLines = apprLines,
                        DevLegacyApprLines = sb.ToString(),
                    };
                    var l2 = GetUnionPerson(null, company.InsertSabun);
                    WriteLog("l2:"+Dump(l2));
                    if (l2 != null && l2.a != null && l2.a.Count > 0)
                    {
                        Person person2 = l2.a[0];
                        WriteLog("person2:"+Dump(person2));
                        WriteLog("porte:" + person2.PorteID + ", "+person2.Password);
                        // http://localhost:5010/approval/exportimport?ExportImportID=3
                        approvalRequest.UserId = person2.PorteID;
                        approvalRequest.Password = person2.Password;
                        // dongju.hyun, 8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg=
                    }
                    ViewBag.ApprovalRequest = approvalRequest;
                }
            }
            return View(vm);
        }
        
        /// <summary>
        /// 출입관리_비상주협력사신청   ims_cmps
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cmps(ApprovalResponse approvalResponse)
        {
            PrintPostFormData();
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }

            var isSuccess = "N";
            var rs = _Dump<ApprovalRecvData>(approvalResponse.Legacyin);
            if (rs != null && CompanyData != null)
            {
                var orgObj = GetCompany(rs.ID, true);
                if(orgObj != null &&orgObj.CompanyID > 0){
                    isSuccess = "Y";
                    var newObj = orgObj.Clone();
                    var CompanyStatus = 0; // 0: 승인대기, 1: 승인완료, 2: 반려
                    bool isUpdate = false;
                    if (approvalResponse.Event == 1){ // 상신
                        isUpdate = true;
                        CompanyStatus = 0; // 반입상신
                    }else if (approvalResponse.Event == 8){ // 결재 완료
                        isUpdate = true;
                        CompanyStatus = 1; // 반입 결재
                    }else if (approvalResponse.Event == 16){ // 반려
                        isUpdate = true;
                        CompanyStatus = 2; // 반입대기 반려
                    }
                    if(isUpdate) {
                        newObj.CompanyStatus = CompanyStatus;
                        newObj.UpdateDate = DateTime.Now;
                        CompanyData.Update(newObj);
                        CompanyData.Save();
                        WriteLog("ApprovalRecvData:" + Dump(rs));
                    }
                }
            }            

            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }

        public IActionResult Work(int workApplyID)
        {
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("workApplyID:" + workApplyID);
            ViewBag.ApprType = 6;
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
                return View(vm);
            }
            return new EmptyResult();
        }

        /// <summary>
        /// 출입관리_공사등록신청   ims_works
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Works(ApprovalResponse approvalResponse)
        {
            PrintPostFormData();
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            var isSuccess = "Y";
            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }

        /// <summary>
        /// Not Yet
        /// </summary>
        /// <param name="sampleID"></param>
        /// <returns></returns>
        public IActionResult Sample(int sampleID)
        {
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("sampleID:" + sampleID);
            ViewBag.ApprType = 7;

            // return View(vm);
            return new EmptyResult();
        }

        /// <summary>
        /// 출입관리_평가샘플반입신청   ims_sios
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Sios(ApprovalResponse approvalResponse)
        {
            PrintPostFormData();
            if (IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            var isSuccess = "Y";
            if(approvalResponse != null && PorteLogData != null){
                var strApprovalResponse = Dump(approvalResponse);
                WriteLog("strApprovalResponse:" + strApprovalResponse);
                PorteLog porteLog = new()
                {
                    BusinessID = approvalResponse.Businessid,
                    ResponseData = strApprovalResponse,
                    IsSuccess = isSuccess,
                    InsertDate = DateTime.Now,
                };
                PorteLogData.Insert(porteLog);
                PorteLogData.Save();
            }
            if(isSuccess.Equals("Y")){
                return Content("<REPLY><REPLY_CODE>1</REPLY_CODE><DESCRIPTION>Success</DESCRIPTION></REPLY>", "text/html");
            } else {
                return Content("<REPLY><REPLY_CODE>0</REPLY_CODE><DESCRIPTION>Failed</DESCRIPTION></REPLY>", "text/html");
            }
        }

    }

    public class ApprovalRequest {
        public string Title { get; set; } = "출입관리_자산반출입신청";    // title 출입관리_자산반출입신청
        public string UserId { get; set; } = "eunji86";
        public string Password { get; set; } = "8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg="; // 암호화된 비밀번호
        public string SystemId { get; set; } = "DM_IMS";
        public string BusinessId { get; set; } = "ims_aios";
        public string LegacyApprLines { get; set; } = string.Empty;    //legacy_apprlines 김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1
        public string Legacyout { get; set; } = "N";
        public string DevLegacyApprLines { get; set; } = string.Empty;
    }

    public class ApprovalRecvData {
        public int ID { get; set;} = 0;
    }

    public class ApprovalResponse {
        public int Docid { get; set; } = -1;
        public string Userid { get; set; } = string.Empty;
        public string  Systemid { get; set; } = string.Empty;
        public string  Businessid { get; set; } = string.Empty;
        public string  Username { get; set; } = string.Empty;
        public int Event { get; set; } = -1;
        public string  Employeeid { get; set; } = string.Empty;
        public string  Deptcode { get; set; } = string.Empty;
        public string  Legacyin { get; set; } = string.Empty;
        public string  Title { get; set; } = string.Empty;
        public string  Opinion { get; set; } = string.Empty;
        public string  Signdate { get; set; } = string.Empty;
    }
    /*  docid = 49263
        userid = eunji86
        systemid = DM_IMS
        businessid = ims_aios
        username = %bc%db%c0%ba%c1%f6
        event = 1
        employeeid = P9000058
        deptcode = DM02_DA20001
        legacyin = {"ID":3}
        title = %c3%e2%c0԰%fc%b8%ae_%c0ڻ%ea%b9%dd%c3%e2%c0Խ%c5û
        opinion = 
        signdate = 2023-08-30 21:40:17 */        
}

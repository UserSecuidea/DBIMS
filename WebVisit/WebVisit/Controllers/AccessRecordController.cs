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
    //test
    [Route("[controller]/[action]")]
    public class AccessRecordController : BaseController
    {
        private Repository<ViewAccesseventList>? ViewAccesseventListData { get; set; }
        
        public AccessRecordController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer){ }
        protected override void Init(){
            if (DbSVISIT != null) {
            }
            if (DbPASSPORT != null){
                ViewAccesseventListData ??= new Repository<ViewAccesseventList>(DbPASSPORT);
            }
        }
        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");

            return RedirectToAction("PersonList");
        }

        [HttpGet("{tabIdx?}/{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstarthour?}/{searchstartminute?}/{searchendhour?}/{searchendminute?}/{searchlocation?}/{searchstartdate?}/{searchenddate?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult PersonList (AccessEventPersonGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }

            PersonDTO my = GetMe();
            AccessEventPersonGridData filterhValue = (AccessEventPersonGridData) FilterGridData(values);
            var m = GetViewAccesseventList(filterhValue);
            AccessEventPersonListViewModel vm =new(){
                ViewAccesseventLists = m.a,
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType), // 출입증구분 0: 일반, 1: 방문증 
                // CodePersonType = GetCommonCodes((int)VisitEnum.CommonCode.PersonType),
                // CodeCardIssueType = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueType), 
                // CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus), // 출입증발급상태: 미발급(0)/발급(1)               
                // CodeCardApplyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardApplyStatus),
                CurrentRoute = values,
                SearchRoute=filterhValue,
                TotalPages = values.GetTotalPages(m.b),
                TotalCnt = m.b,
            };
            return View(vm);
        }

        [HttpPost("{tabIdx?}/{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstarthour?}/{searchstartminute?}/{searchendhour?}/{searchendminute?}/{searchlocation?}/{searchstartdate?}/{searchenddate?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult? ExcelDownload(AccessEventPersonGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            PersonDTO my = GetMe();

            DataTable dt = new(ExTitle);                   
            // 시간	카드번호	성명	직급	부서명	사번	기기명	위치	출입상태
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Hour2"]), new DataColumn(@Localizer["Card Number"]), new DataColumn(@Localizer["Name"]),
                                        new DataColumn(@Localizer["Position"]), new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Sabun"]), 
                                        new DataColumn(@Localizer["Device Name"]), new DataColumn(@Localizer["Location"]),new DataColumn(@Localizer["Access Status"]), });         
            
            AccessEventPersonGridData filterhValue = (AccessEventPersonGridData) FilterGridData(values);            
            var v = GetViewAccesseventList(filterhValue, false);
            if (v != null){
                // List<CommonCode> CodeCardType = GetCommonCodes((int)VisitEnum.CommonCode.CardType); // 출입증구분 0: 일반, 1: 방문증 
                // List< CommonCode > CodeCardIssueStatus = GetCommonCodes((int)VisitEnum.CommonCode.CardIssueStatus); // 출입증발급상태: 미발급(0)/발급(1)               
                // List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                foreach(ViewAccesseventList viewAccesseventList in v.a){
                        // var location = viewCardPerson.PersonUser2;
                        var eventDateTime = "";                       
                        if(viewAccesseventList?.EventDateTime != null){
                            eventDateTime=string.Format("{0:yyyy-MM-dd HH:mm:ss}", viewAccesseventList.EventDateTime);
                        }
                    dt.Rows.Add(eventDateTime, viewAccesseventList?.CardNo, viewAccesseventList?.PersonName, viewAccesseventList?.GradeName, 
                        viewAccesseventList?.OrgName, viewAccesseventList?.Sabun, viewAccesseventList?.EqName, viewAccesseventList?.LocationName, viewAccesseventList?.StateName);
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

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchstarthour?}/{searchstartminute?}/{searchendhour?}/{searchendminute?}/{searchlocation?}/{searchstartdate?}/{searchenddate?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult? ExcelDownload2(AccessEventPersonGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            PersonDTO my = GetMe();

            DataTable dt = new(ExTitle);                   
            // 시간	카드번호	성명	직급	부서명	사번	기기명	위치	출입상태
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Hour2"]), new DataColumn(@Localizer["Card Number"]), new DataColumn(@Localizer["Name"]),
                                        new DataColumn(@Localizer["Position"]), new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Sabun"]), 
                                        new DataColumn(@Localizer["Device Name"]), new DataColumn(@Localizer["Location"]),new DataColumn(@Localizer["Access Status"]), });
            
            AccessEventPersonGridData filterhValue = (AccessEventPersonGridData) FilterGridData(values);

            WriteLog("ExportImport is null");
            return new EmptyResult();  
        }
        ///1/10/cardissueapplyid/desc//2023-08-16/?culture=ko
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchdate?}")]
        public IActionResult CarList (CarListGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            CarListGridData filterhValue = (CarListGridData) FilterGridData(values);
            var l = GetCarList(filterhValue);
            CarListViewModel vm = new()
            {
                PassedVehicleLogs = l.a,
                CurrentRoute = values,
                SearchRoute = filterhValue,
                TotalPages = values.GetTotalPages(l.b),
                TotalCnt = l.b,
            };
            return View(vm);
        }

        private dynamic GetCarList(CarListGridData values){
            var count = 0;
            // 출입위치	입문	출문	입문-출문	입문 ( 전일 )	출문 ( 전일 )	입문-출문 ( 전일 )	입문 ( 전전일 )	출문 ( 전전일 )	입문-출문 ( 전전일 )
            List<PassedVehicleLog> list = new();
            bool isConnected = false;            
            try
            {
                if (DbLPR != null && DbLPR.Database.CanConnect() && !string.IsNullOrEmpty(values.SearchDate))
                {
                    StringBuilder sb = new();
                    // GatePermission = NULL: 전체, 1: 부천 정문 입구, 2: 부천 정문 출구, 3: 부천 민원 입구, 4: 부천 민원 출구, 5: 상우 정문 입구, 6: 상우 정문 출구
                    // BusinessLocation = NULL: 전체,  부천	2000, 미국	5000, 상우	3000	중국	7000, 판교	6000	싱가포르	8000, 대만	4000	일본	9000
                    // VehiclePurpose = 전체	NULL, 개인용	1, 영업용	2, 내방용	3, 내부등록	, 미등록차량	
                    // ByDay = 1: 일별, 0: 전체
                    DateTime end = DateTime.Parse(values.SearchDate);
                    // DateTime start = end.AddDays(-2);
                    sb.Append("exec SP_SELECT_COUNT_PASSEDVEHICLELOG @StartDate='");
                    sb.Append(end.ToString("yyyyMMdd"));
                    sb.Append(" 00:00:00");
                    sb.Append("', @EndDate='");
                    sb.Append(end.ToString("yyyyMMdd"));
                    sb.Append(" 023:59:59");
                    sb.Append("', @ByDay=1;");
                    using var command = DbLPR.Database.GetDbConnection().CreateCommand();
                    WriteLog("query: " + sb.ToString());
                    command.CommandText = sb.ToString();
                    command.CommandType = CommandType.Text;
                    // EMPLOYEE_TYPE: S:정규직, U:계약직, C: 협력직
                    // PersonTypeID: 0 임직원(S, U), 1: 상주(C), 2:비상주 관리자, 3: 비상주 직원
                    DbLPR.Database.OpenConnection(); // DbSVISIT.Database.GetDbConnection()
                    isConnected = true;
                    using var reader = command.ExecuteReader();
                    if(reader.HasRows){
                        while (reader.Read()){
                            // Person p = new();
                            WriteLog("----------");
                            PassedVehicleLog p = new();
                            for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                            {
                                if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value){
                                    var n = reader.GetName(fieldCount);
                                    var v = reader[fieldCount];
                                    if (v != null){
                                        try{
                                            // PassedTime=2023-08-09 차량 용도=내부등록 
                                            // 방문객입구=17 방문객출구=19 부천정문입구=108 부천정문출구=401 상우정문입구=81 상우정문출구=301
                                            // CountVisitorEntry CountVisitorExit Count1Entry Count1Exit Count2Entry Count2Exit
                                            WriteLog("read data["+fieldCount+"] "+n+"="+v);
                                            if(fieldCount == 0) {
                                                p.PassedTime = v.ToString()??"";
                                            } else if(fieldCount == 1) {
                                                p.VehiclePurpose = v.ToString()??"";
                                            } else if(fieldCount == 2) {
                                                p.CountVisitorEntry = int.Parse(v.ToString()??"0");
                                            } else if(fieldCount == 3) {
                                                p.CountVisitorExit = int.Parse(v.ToString()??"0");
                                            } else if(fieldCount == 4) {
                                                p.Count1Entry = int.Parse(v.ToString()??"0");
                                            } else if(fieldCount == 5) {
                                                p.Count1Exit = int.Parse(v.ToString()??"0");
                                            } else if(fieldCount == 6) {
                                                p.Count2Entry = int.Parse(v.ToString()??"0");
                                            } else if(fieldCount == 7) {
                                                p.Count2Exit = int.Parse(v.ToString()??"0");
                                            }
                                            // WriteLog("read data: "+n.GetType()+"="+v.GetType());
                                            // typeof(Person).GetProperty(n)?.SetValue(p, v);
                                        }catch(Exception e){
                                            WriteLog(e.ToString());
                                        }
                                    }
                                }
                            }
                            list.Add(p);
                            // WriteLog("PassedVehicleLog:" + Dump(p));
                        }
                    }
                    DbLPR.Database.CloseConnection();      
                }          
            }catch(Exception e) {
                WriteLog(e.ToString());
            }finally{
                DbLPR?.Database.CloseConnection();                
            }
            return new { a= list, b=count, c=isConnected};
        }

        public IActionResult StatisticsList (string eventDate)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            if (string.IsNullOrEmpty(eventDate)){
                eventDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            ViewBag.EventDate = eventDate;
            var l = GetViewPersonInout(eventDate);
            WriteLog("person inout:" + Dump(l));

            return View(l);
        }

        private List<ViewPersonInout> GetViewPersonInout(string eventDate){
            List<ViewPersonInout> list = new();
            if (DbPASSPORT != null)
            {
                StringBuilder sb = new();
                sb.Append(@"SELECT [EventDate]
                    ,[DIR]
                    ,[LOCATION_NAME]
                    ,[Cnt]
                FROM [PASSPORT].[dbo].[VIEW_PERSON_INOUT]");
                sb.Append("WHERE EventDate = '");
                sb.Append(eventDate); // 날짜
                sb.Append("'");
                WriteLog("query:" + sb.ToString());
                var fs1 = FormattableStringFactory.Create(sb.ToString());
                list = DbPASSPORT.ViewPersonInouts
                    .FromSql(fs1).ToList();
            }
            return list;
        }
        
        /// <summary>
        /// PASSPORT.VIEW_ACCESSEVENT_LIST 에서 검색 조건에 맞게 리스트 가져오기
        /// </summary>
        /// <param name="filterhValue"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        private dynamic GetViewAccesseventList(AccessEventPersonGridData filterhValue, bool isAll = false){
            List<ViewAccesseventList> list = new();
            var cnt = 0;
            WriteLog("filterhValue:" + Dump(filterhValue));
            if (DbPASSPORT != null){
                List<string> strWhere = new();
                if(filterhValue.TabIdx == 2){
                    strWhere.Add("Success = '0'"); // VIEW_ACCESSEVENT_LIST 에서 Location ID를 줄 경우 실행
                }
                if (filterhValue.SearchLocation > -1) {
                    strWhere.Add("LOCATION_CODE = '"+filterhValue.SearchLocation+"'"); // VIEW_ACCESSEVENT_LIST 에서 Location ID를 줄 경우 실행
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)){
                    strWhere.Add(" OrgName = '"+filterhValue.SearchOrgNameKor+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchName)){
                    strWhere.Add("PersonName = '"+filterhValue.SearchName+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchStartDate)){
                    DateTime s = DateTime.Parse(filterhValue.SearchStartDate + " " + filterhValue.SearchStartHour + ":" + filterhValue.SearchStartMinute + ":00");
                    strWhere.Add(" EventDateTime >= '"+s.ToString("yyyy-MM-dd HH:mm:ss")+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchEndDate)){
                    DateTime s = DateTime.Parse(filterhValue.SearchEndDate + " " + filterhValue.SearchEndHour + ":" + filterhValue.SearchEndMinute + ":00");
                    strWhere.Add(" EventDateTime <= '"+s.ToString("yyyy-MM-dd HH:mm:ss")+"'");
                }
                if(!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)){
                    strWhere.Add(" OrgName = '"+filterhValue.SearchOrgNameKor+"'");
                }

                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.VIEW_ACCESSEVENT_LIST ");
                WriteLog("strWhere:" + Dump(strWhere));
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

                WriteLog("query: " + sb.ToString());
                if(isAll == false){
                    var fs1 = FormattableStringFactory.Create(sb.ToString());
                    cnt = DbPASSPORT.ViewAccesseventLists
                        .FromSql(fs1)
                        .Count();
                }
                WriteLog("cnt: "+cnt);

                sb.Append(" ORDER BY AlarmID ");
                if (isAll == false) {
                    sb.Append(" OFFSET ");
                    var offset = (filterhValue.PageNumber -1) * filterhValue.PageSize;
                    sb.Append(offset);
                    sb.Append(" ROWS FETCH NEXT ");
                    sb.Append(filterhValue.PageSize);
                    sb.Append(" ROWS ONLY");
                }
                var fs2 = FormattableStringFactory.Create(sb.ToString());
                WriteLog("query: " + sb.ToString());
                list = DbPASSPORT.ViewAccesseventLists
                    .FromSql(fs2)
                    .ToList();
                WriteLog("list: "+Dump(list));
            }
            return new { a = list, b = cnt };
        }
        
    }
}
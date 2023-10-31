using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebVisit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Text;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace WebVisit.Controllers
{

    [Route("[controller]/[action]")]
    public class MealController : BaseController
    {
        private Repository<MealTerm>? MealTermData { get; set; }
        private Repository<MealSchedule>? MealScheduleData { get; set; }
        private Repository<MealPrice>? MealPriceData { get; set; }
        private Repository<MealAccess>? MealAccessData { get; set; }
        private Repository<MealLog>? MealLogData { get; set; }

        public MealController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer)
        {     
        }
        
        protected override void Init(){
            if (DbSVISIT != null) {
                MealTermData ??= new Repository<MealTerm>(DbSVISIT);
                MealScheduleData ??= new Repository<MealSchedule>(DbSVISIT);
                MealPriceData ??= new Repository<MealPrice>(DbSVISIT);
                MealAccessData ??= new Repository<MealAccess>(DbSVISIT);
                MealLogData ??= new Repository<MealLog>(DbSVISIT);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("List");
        }

        
        [HttpGet("{tabIdx?}/{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}/{searchstartmealymd?}/{searchendmealymd?}")]
        public IActionResult List (MealLogGridData values, string mode="")
        {

            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsHR() || IsDietitian();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            // WriteLog("MealLogGridData:" + Dump(values));
            var my = GetMe();
            ViewBag.tabIdx=values.TabIdx;
            MealLogGridData filterhValue = (MealLogGridData) FilterGridData(values);
            var rs = GetMealLogList(filterhValue);
            MealLogListViewModel vm = new() {
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // MealLogs = MealLogData.List(options),
                MealLogDTOs = rs.a,
                CurrentRoute = values,
                SearchRoute = filterhValue,
                TotalPages = values.GetTotalPages(rs.b),
                TotalCnt = rs.b,
            };
            return View(vm);
        }

        private dynamic GetMealLogList(MealLogGridData filterhValue){
            // IsManual: 수기등록구분: 식수리더(0),수기등록(1)
            List<MealLogDTO> list = new();
            int totalCount = 0;
            if (DbSVISIT != null){
                IQueryable<MealLog> q = DbSVISIT.MealLog;
                // q = q.Where(x => x.IsManual == 0); // 로그 데이터가 없어 임시로 주석 처리.
                if (filterhValue.SearchLocation > -1){
                    q = q.Where(x => x.Location == filterhValue.SearchLocation.ToString());
                }
                if (IsMaster() == false)
                {
                    var my = GetMe();
                    if (IsGeneralManager() || IsHR() || IsDietitian())
                    {
                        q = q.Where(x => x.Location == my.Location);
                    }
                }                
                try {
                    if (!string.IsNullOrEmpty(filterhValue.SearchStartMealYMD)){
                        var s = DateTime.Parse(filterhValue.SearchStartMealYMD + " 00:00:00");
                        q = q.Where(x => x.MealYMD >= s);
                    }
                    if (!string.IsNullOrEmpty(filterhValue.SearchEndMealYMD)){
                        var e = DateTime.Parse(filterhValue.SearchEndMealYMD + " 23:59:59");
                        q = q.Where(x => x.MealYMD <= e);
                    }
                } catch (Exception e) {
                    WriteError(e.ToString());
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchName)) {
                    q = q.Where(x => x.Name == filterhValue.SearchName);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchCompanyName)) {
                    if(filterhValue.SearchCompanyName.Equals("DBHiTek")){
                        q = q.Where(x => x.CompanyID == 0);
                    } else {
                        q = q.Where(x => x.CompanyName == filterhValue.SearchCompanyName);
                    }
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)) {
                    q = q.Where(x => x.OrgNameKor == filterhValue.SearchOrgNameKor || x.OrgNameEng == filterhValue.SearchOrgNameKor);
                }
                if (filterhValue.TabIdx == 1){
                    // 일자 별: 일자	회사	회사명	부서명	성명	사번	직급	 조식	중식	석식	야식	건수	금액
                    // 전체 리스트 수 가져오기
                    if (filterhValue.PageSize > 0) {
                        var countList = q.GroupBy(x => new { x.MealYMD.Year, x.MealYMD.Month, x.MealYMD.Day, x.Sabun }).ToList();
                        totalCount = countList.Count();
                        // WriteLog("meallog:" + countList.Count());
                    }
                    // 리스트 가져오기
                    dynamic l = null;
                    if (filterhValue.PageSize > 0) {
                        l = q.GroupBy(x => new { x.MealYMD.Year, x.MealYMD.Month, x.MealYMD.Day, x.Sabun, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).Skip((filterhValue.PageNumber - 1) * filterhValue.PageSize).Take(filterhValue.PageSize).ToList();
                    } else {
                        l = q.GroupBy(x => new { x.MealYMD.Year, x.MealYMD.Month, x.MealYMD.Day, x.Sabun, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).ToList();
                    }
                    // WriteLog("meallog:" + Dump(l));
                    MealLogDTO mealLogDTO = new();
                    var PrevKey = string.Empty;
                    var total = 0;
                    var price = 0;
                    foreach (var v in l) {
                        // 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                        var NewKey = v.m?.Sabun + "/" + v.k.Year + "" + v.k.Month + "" + v.k.Day;
                        // WriteLog("NewKey:" + NewKey);
                        if (string.IsNullOrEmpty(PrevKey) || !NewKey.Equals(PrevKey)) {
                            if (!string.IsNullOrEmpty(PrevKey) && !NewKey.Equals(PrevKey)) {
                                mealLogDTO.CountTotal = total;
                                mealLogDTO.PriceTotal = price;
                                list.Add(mealLogDTO);
                            }
                            total = 0;
                            price = 0;
                            mealLogDTO = new()
                            {
                                MealLog = v.m ?? new MealLog(),
                            };
                            // Year = v.k.Year Month = v.k.Month
                        }
                        if (v.k.MealGB == 0) {
                            mealLogDTO.Count0 = v.c;
                        } else if (v.k.MealGB == 1) {
                            mealLogDTO.Count1 = v.c;
                            total += v.c;
                            price += v.s ?? 0; //v.m?.Price??0;
                        } else if (v.k.MealGB == 2) {
                            mealLogDTO.Count2 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 3) {
                            mealLogDTO.Count3 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 4) {
                            mealLogDTO.Count4 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        }
                        PrevKey = NewKey;
                    }
                    if (!string.IsNullOrEmpty(mealLogDTO.MealLog.Sabun)) {
                        mealLogDTO.CountTotal = total;
                        mealLogDTO.PriceTotal = price;
                        list.Add(mealLogDTO);
                        // WriteLog("meallog2:" + Dump(list));
                    }
                } else if (filterhValue.TabIdx == 2) {
                    // 개인 별:       회사	회사명	부서명	성명	사번	직급	 조식	중식	석식	야식	건수	금액
                    // 전체 리스트 수 가져오기
                    if (filterhValue.PageSize > 0) {
                        var countList = q.GroupBy(x => new { x.Sabun }).ToList();
                        totalCount = countList.Count();
                        // WriteLog("meallog:" + countList.Count());
                    }
                    // 리스트 가져오기
                    dynamic l = null;
                    if (filterhValue.PageSize > 0) {
                        l = q.GroupBy(x => new { x.Sabun, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).Skip((filterhValue.PageNumber - 1) * filterhValue.PageSize).Take(filterhValue.PageSize).ToList();
                    } else {
                        l = q.GroupBy(x => new { x.Sabun, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).ToList();
                    }
                    // WriteLog("meallog:" + Dump(l));
                    MealLogDTO mealLogDTO = new();
                    var PrevKey = string.Empty;
                    var total = 0;
                    var price = 0;
                    foreach (var v in l) {
                        // 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                        var NewKey = v.m?.Sabun;
                        // WriteLog("NewKey:" + NewKey);
                        if (string.IsNullOrEmpty(PrevKey) || !NewKey.Equals(PrevKey)) {
                            if (!string.IsNullOrEmpty(PrevKey) && !NewKey.Equals(PrevKey)) {
                                mealLogDTO.CountTotal = total;
                                mealLogDTO.PriceTotal = price;
                                list.Add(mealLogDTO);
                            }
                            total = 0;
                            price = 0;
                            mealLogDTO = new()
                            {
                                MealLog = v.m ?? new MealLog(),
                            };
                            // Year = v.k.Year Month = v.k.Month
                        }
                        if (v.k.MealGB == 0) {
                            mealLogDTO.Count0 = v.c;
                        } else if (v.k.MealGB == 1) {
                            mealLogDTO.Count1 = v.c;
                            total += v.c;
                            price += v.s ?? 0; //v.m?.Price??0;
                        } else if (v.k.MealGB == 2) {
                            mealLogDTO.Count2 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 3) {
                            mealLogDTO.Count3 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 4) {
                            mealLogDTO.Count4 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        }
                        PrevKey = NewKey;
                    }
                    if (!string.IsNullOrEmpty(mealLogDTO.MealLog.Sabun)) {
                        mealLogDTO.CountTotal = total;
                        mealLogDTO.PriceTotal = price;
                        list.Add(mealLogDTO);
                        // WriteLog("meallog2:" + Dump(list));
                    }
                } else if (filterhValue.TabIdx == 3) {
                    // 회사 별: 일자	회사	회사명	                                조식	중식	석식	야식	건수	금액
                    // 전체 리스트 수 가져오기
                    if (filterhValue.PageSize > 0) {
                        var countList = q.GroupBy(x => new { x.CompanyID }).ToList();
                        totalCount = countList.Count();
                        // WriteLog("meallog:" + countList.Count());
                    }
                    // 리스트 가져오기
                    dynamic l = null;
                    if (filterhValue.PageSize > 0) {
                        l = q.GroupBy(x => new { x.CompanyID, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).Skip((filterhValue.PageNumber - 1) * filterhValue.PageSize).Take(filterhValue.PageSize).ToList();
                    } else {
                        l = q.GroupBy(x => new { x.CompanyID, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).ToList();
                    }
                    // WriteLog("meallog:" + Dump(l));
                    MealLogDTO mealLogDTO = new();
                    var PrevKey = string.Empty;
                    var total = 0;
                    var price = 0;
                    foreach (var v in l) {
                        // 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                        var NewKey = v.k?.CompanyID+"";
                        // WriteLog("NewKey:" + NewKey);
                        if (string.IsNullOrEmpty(PrevKey) || !NewKey.Equals(PrevKey)) {
                            if (!string.IsNullOrEmpty(PrevKey) && !NewKey.Equals(PrevKey)) {
                                mealLogDTO.CountTotal = total;
                                mealLogDTO.PriceTotal = price;
                                list.Add(mealLogDTO);
                            }
                            total = 0;
                            price = 0;
                            mealLogDTO = new()
                            {
                                MealLog = v.m ?? new MealLog(),
                            };
                            // Year = v.k.Year Month = v.k.Month
                        }
                        if (v.k.MealGB == 0) {
                            mealLogDTO.Count0 = v.c;
                        } else if (v.k.MealGB == 1) {
                            mealLogDTO.Count1 = v.c;
                            total += v.c;
                            price += v.s ?? 0; //v.m?.Price??0;
                        } else if (v.k.MealGB == 2) {
                            mealLogDTO.Count2 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 3) {
                            mealLogDTO.Count3 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 4) {
                            mealLogDTO.Count4 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        }
                        PrevKey = NewKey;
                    }
                    if (!string.IsNullOrEmpty(mealLogDTO.MealLog.Sabun)) {
                        mealLogDTO.CountTotal = total;
                        mealLogDTO.PriceTotal = price;
                        list.Add(mealLogDTO);
                        // WriteLog("meallog2:" + Dump(list));
                    }                
                } else if (filterhValue.TabIdx == 4) {
                    // 사업장별: 일자	회사	 조식	조식(수기)	중식	중식(수기)	석식	석식(수기)	야식 야식(수기)	건수	금액
                    // 전체 리스트 수 가져오기
                    if (filterhValue.PageSize > 0) {
                        var countList = q.GroupBy(x => new { x.Location, x.IsManual }).ToList();
                        totalCount = countList.Count();
                        // WriteLog("meallog:" + countList.Count());
                    }
                    // 리스트 가져오기
                    dynamic l = null;
                    if (filterhValue.PageSize > 0) {
                        l = q.GroupBy(x => new { x.Location, x.IsManual, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).Skip((filterhValue.PageNumber - 1) * filterhValue.PageSize).Take(filterhValue.PageSize).ToList();
                    } else {
                        l = q.GroupBy(x => new { x.Location, x.IsManual, x.MealGB }).Select(g => new
                        {
                            k = g.Key,
                            c = g.Count(),
                            s = g.Sum(s => s.Price),
                            m = g.FirstOrDefault()
                        }).ToList();
                    }
                    // WriteLog("meallog:" + Dump(l));
                    MealLogDTO mealLogDTO = new();
                    var PrevKey = string.Empty;
                    var total = 0;
                    var price = 0;
                    foreach (var v in l) {
                        // 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                        var NewKey = v.k.Location+"";
                        // WriteLog("NewKey:" + NewKey);
                        if (string.IsNullOrEmpty(PrevKey) || !NewKey.Equals(PrevKey)) {
                            if (!string.IsNullOrEmpty(PrevKey) && !NewKey.Equals(PrevKey)) {
                                mealLogDTO.CountTotal = total;
                                mealLogDTO.PriceTotal = price;
                                list.Add(mealLogDTO);
                            }
                            total = 0;
                            price = 0;
                            mealLogDTO = new()
                            {
                                MealLog = v.m ?? new MealLog(),
                            };
                            // Year = v.k.Year Month = v.k.Month
                        }
                        if (v.k.MealGB == 0) {
                            mealLogDTO.Count0 = v.c;
                        } else if (v.k.MealGB == 1) {
                            if (v.m?.IsManual == 1)
                                mealLogDTO.Count5 = v.c;
                            else
                                mealLogDTO.Count1 = v.c;
                            total += v.c;
                            price += v.s ?? 0; //v.m?.Price??0;
                        } else if (v.k.MealGB == 2) {
                            if (v.m?.IsManual == 1)
                                mealLogDTO.Count6 = v.c;
                            else
                                mealLogDTO.Count2 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 3) {
                            if (v.m?.IsManual == 1)
                                mealLogDTO.Count7 = v.c;
                            else
                                mealLogDTO.Count3 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        } else if (v.k.MealGB == 4) {
                            if (v.m?.IsManual == 1)
                                mealLogDTO.Count8 = v.c;
                            else
                                mealLogDTO.Count4 = v.c;
                            total += v.c;
                            price += v.s ?? 0;
                        }
                        PrevKey = NewKey;
                    }
                    if (!string.IsNullOrEmpty(mealLogDTO.MealLog.Sabun)) {
                        mealLogDTO.CountTotal = total;
                        mealLogDTO.PriceTotal = price;
                        list.Add(mealLogDTO);
                        // WriteLog("meallog2:" + Dump(list));
                    }                
                }
            }
            return new { a=list, b=totalCount };
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult? ExcelDownload(MealLogGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            PersonDTO my = GetMe();
            MealLogGridData filterhValue = (MealLogGridData) FilterGridData(values);
            filterhValue.PageSize = 0;
            var l = GetMealLogList(filterhValue);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            DataTable dt = new(ExTitle);
            if(filterhValue.TabIdx == 1) {
                // 일자 별: 일자	회사	회사명	부서명	성명	사번	직급	 조식	중식	석식	야식	건수	금액
                dt.Columns.AddRange(new DataColumn[13] { new DataColumn(@Localizer["Date"]), new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]),
                    new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Sabun"]),
                    new DataColumn(@Localizer["Position"]),new DataColumn(@Localizer["Breakfast"]), new DataColumn(@Localizer["Lunch"]), 
                    new DataColumn(@Localizer["Dinner"]),new DataColumn(@Localizer["LatenightMeal"]),new DataColumn(@Localizer["Number"]),new DataColumn(@Localizer["Price"]),
                    });
                if (l != null)
                {
                    foreach(var m in l.a){ 
                        MealLog mealLog = m.MealLog;
                        var location = "";
                        if (mealLog.Location != null && CodeLocation != null) {
                            foreach(var m1 in CodeLocation) {
                                if (m1.SubCodeDesc != null && m1.SubCodeDesc.Equals(mealLog.Location)) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        location = m1.CodeNameKor;
                                    }else {
                                        location = m1.CodeNameEng;
                                    }
                                }
                            }
                        }
                        string mealYMD = string.Format("{0:yyyy-MM-dd}", mealLog.MealYMD);
                        string? companyName;
                        if (string.IsNullOrEmpty(mealLog.CompanyName)){
                            companyName = "DBHiTek";
                        }else{
                            companyName = mealLog.CompanyName;
                        }
                        dt.Rows.Add(mealYMD, location, companyName, mealLog.OrgNameKor, mealLog.Name, mealLog.Sabun,
                            mealLog.GradeName, m.Count1, m.Count2, m.Count3, m.Count4, m.CountTotal, m.PriceTotal.ToString("N0"));
                    }
                    using XLWorkbook wb = new();
                    wb.Worksheets.Add(dt);
                    using MemoryStream stream = new();
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
                }
            } else if(filterhValue.TabIdx == 2) {
                // 개인 별:       회사	회사명	부서명	성명	사번	직급	 조식	중식	석식	야식	건수	금액
                dt.Columns.AddRange(new DataColumn[12] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]),
                    new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Sabun"]),
                    new DataColumn(@Localizer["Position"]),new DataColumn(@Localizer["Breakfast"]), new DataColumn(@Localizer["Lunch"]), 
                    new DataColumn(@Localizer["Dinner"]),new DataColumn(@Localizer["LatenightMeal"]),new DataColumn(@Localizer["Number"]),new DataColumn(@Localizer["Price"]),
                    });
                if (l != null)
                {
                    foreach(var m in l.a){ 
                        MealLog mealLog = m.MealLog;
                        var location = "";
                        if (mealLog.Location != null && CodeLocation != null) {
                            foreach(var m1 in CodeLocation) {
                                if (m1.SubCodeDesc != null && m1.SubCodeDesc.Equals(mealLog.Location)) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        location = m1.CodeNameKor;
                                    }else {
                                        location = m1.CodeNameEng;
                                    }
                                }
                            }
                        }
                        string mealYMD = string.Format("{0:yyyy-MM-dd}", mealLog.MealYMD);
                        string? companyName;
                        if (string.IsNullOrEmpty(mealLog.CompanyName)){
                            companyName = "DBHiTek";
                        }else{
                            companyName = mealLog.CompanyName;
                        }
                        dt.Rows.Add(location, companyName, mealLog.OrgNameKor, mealLog.Name, mealLog.Sabun,
                            mealLog.GradeName, m.Count1, m.Count2, m.Count3, m.Count4, m.CountTotal, m.PriceTotal.ToString("N0"));
                    }
                    using XLWorkbook wb = new();
                    wb.Worksheets.Add(dt);
                    using MemoryStream stream = new();
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
                }
            } else if(filterhValue.TabIdx == 3) {
                // 회사 별: 일자	회사	회사명	                                조식	중식	석식	야식	건수	금액
                dt.Columns.AddRange(new DataColumn[9] { new DataColumn(@Localizer["Date"]), new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]),
                    new DataColumn(@Localizer["Breakfast"]), new DataColumn(@Localizer["Lunch"]), 
                    new DataColumn(@Localizer["Dinner"]),new DataColumn(@Localizer["LatenightMeal"]),new DataColumn(@Localizer["Number"]),new DataColumn(@Localizer["Price"]),
                    });
                if (l != null)
                {
                    foreach(var m in l.a){ 
                        MealLog mealLog = m.MealLog;
                        var location = "";
                        if (mealLog.Location != null && CodeLocation != null) {
                            foreach(var m1 in CodeLocation) {
                                if (m1.SubCodeDesc != null && m1.SubCodeDesc.Equals(mealLog.Location)) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        location = m1.CodeNameKor;
                                    }else {
                                        location = m1.CodeNameEng;
                                    }
                                }
                            }
                        }
                        string mealYMD = string.Format("{0:yyyy-MM-dd}", mealLog.MealYMD);
                        string? companyName;
                        if (string.IsNullOrEmpty(mealLog.CompanyName)){
                            companyName = "DBHiTek";
                        }else{
                            companyName = mealLog.CompanyName;
                        }
                        dt.Rows.Add(mealYMD, location, companyName, m.Count1, m.Count2, m.Count3, m.Count4, m.CountTotal, m.PriceTotal.ToString("N0"));
                    }
                    using XLWorkbook wb = new();
                    wb.Worksheets.Add(dt);
                    using MemoryStream stream = new();
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
                }
            } else if(filterhValue.TabIdx == 4) {
                // 사업장별: 일자	회사	                                        조식	조식(수기)	중식	중식(수기)	석식	석식(수기)	야식 야식(수기)	건수	금액
                dt.Columns.AddRange(new DataColumn[12] { new DataColumn(@Localizer["Date"]), new DataColumn(@Localizer["Place Of Business"]),
                    new DataColumn(@Localizer["Breakfast"]), new DataColumn(@Localizer["Breakfast"]+" "+@Localizer["Menual"]), 
                    new DataColumn(@Localizer["Lunch"]), new DataColumn(@Localizer["Lunch"]+" "+@Localizer["Menual"]), 
                    new DataColumn(@Localizer["Dinner"]), new DataColumn(@Localizer["Dinner"]+" "+@Localizer["Menual"]),
                    new DataColumn(@Localizer["LatenightMeal"]), new DataColumn(@Localizer["LatenightMeal"]+" "+@Localizer["Menual"]),
                    new DataColumn(@Localizer["Number"]),new DataColumn(@Localizer["Price"]),
                    });
                if (l != null)
                {
                    foreach(var m in l.a){ 
                        MealLog mealLog = m.MealLog;
                        var location = "";
                        if (mealLog.Location != null && CodeLocation != null) {
                            foreach(var m1 in CodeLocation) {
                                if (m1.SubCodeDesc != null && m1.SubCodeDesc.Equals(mealLog.Location)) {
                                    if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                        location = m1.CodeNameKor;
                                    }else {
                                        location = m1.CodeNameEng;
                                    }
                                }
                            }
                        }
                        string mealYMD = string.Format("{0:yyyy-MM-dd}", mealLog.MealYMD);
                        string? companyName;
                        if (string.IsNullOrEmpty(mealLog.CompanyName)){
                            companyName = "DBHiTek";
                        }else{
                            companyName = mealLog.CompanyName;
                        }
                        dt.Rows.Add(mealYMD, location, m.Count1, m.Count5, m.Count2, m.Count6, m.Count3, m.Count7, m.Count4, m.Count8, m.CountTotal, m.PriceTotal.ToString("N0"));
                    }
                    using XLWorkbook wb = new();
                    wb.Worksheets.Add(dt);
                    using MemoryStream stream = new();
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
                }
            }
            return new EmptyResult();            
        }
        
        [HttpGet("{tabIdx?}/{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}/{searchstartmealymd?}/{searchendmealymd?}")]
        public IActionResult Detail (MealLogGridData filterhValue, MealLogDTO mealLogDTO)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mealLogDTO:" + Dump(mealLogDTO));
            if(MealLogData != null && mealLogDTO != null && mealLogDTO.MealLogID != null){
                PersonDTO my = GetMe();
                MealLog mealLog = MealLogData.Get(mealLogDTO.MealLogID);
                if (mealLog != null){
                    mealLogDTO.MealLog = mealLog;
                    mealLogDTO.TabIdx = filterhValue.TabIdx;
                }
                mealLogDTO.CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            }

            return View(mealLogDTO);
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}/{searchstartmealymd?}/{searchendmealymd?}")]
        public IActionResult InvalidList (MealInvalidGridData values, string mode="")
        {
            
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsHR() || IsDietitian();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();
            MealInvalidGridData filterhValue = (MealInvalidGridData) FilterGridData(values);
            var options = new QueryOptions<MealLog>
            {
                PageNumber = filterhValue.PageNumber,
                PageSize = filterhValue.PageSize,
                OrderByDirection = filterhValue.SortDirection,
            };
            // Where = a => a.MealGB == 0, //a.IsDelete == "N" && 
            if (IsMaster() == false)
            {
                if (IsGeneralManager() || IsHR() || IsDietitian())
                {
                    options.MultipleWhere.Add(x => x.Location == my.Location);
                }
            }            
            options.MultipleWhere.Add(a => a.MealGB == 0);
            if (filterhValue.SearchLocation > -1){
                options.MultipleWhere.Add(x => x.Location == filterhValue.SearchLocation.ToString());
            }
            try {
                if (!string.IsNullOrEmpty(filterhValue.SearchStartMealYMD)){
                    WriteLog("searchDate: "+filterhValue.SearchStartMealYMD);
                    var s = DateTime.Parse(filterhValue.SearchStartMealYMD + " 00:00:00");
                    options.MultipleWhere.Add(x => x.MealYMD >= s);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndMealYMD)){
                    var e = DateTime.Parse(filterhValue.SearchEndMealYMD + " 23:59:59");
                    options.MultipleWhere.Add(x => x.MealYMD <= e);
                }
            } catch (Exception e) {
                WriteError(e.ToString());
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchName)) {
                options.MultipleWhere.Add(x => x.Name == filterhValue.SearchName);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchCompanyName)) {
                if(filterhValue.SearchCompanyName.Equals("DBHiTek")){
                    options.MultipleWhere.Add(x => x.CompanyID == 0);
                } else {
                    options.MultipleWhere.Add(x => x.CompanyName == filterhValue.SearchCompanyName);
                }
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)) {
                options.MultipleWhere.Add(x => x.OrgNameKor == filterhValue.SearchOrgNameKor || x.OrgNameEng == filterhValue.SearchOrgNameKor);
            }

            MealLogInvalidListViewModel vm =new(){
                MealLogs = MealLogData.List(options),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                
                CurrentRoute = values,
                SearchRoute = filterhValue,
                TotalPages = values.GetTotalPages(MealLogData.Count),
                TotalCnt = MealLogData.Count,
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}/{searchstartmealymd?}/{searchendmealymd?}")]
        public IActionResult? InvalidExcelDownload(MealInvalidGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);                                  
            // 사업장	회사명	부서명	성명	사번	직급	조식	중식	석식	야식
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn(@Localizer["Date"]), new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]), 
                new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Sabun"]), new DataColumn(@Localizer["Position"]),
                new DataColumn(@Localizer["Status"]),});
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            MealInvalidGridData filterhValue = (MealInvalidGridData) FilterGridData(values);
            filterhValue.PageSize = 0;
            var options = new QueryOptions<MealLog>
            {
                PageNumber = filterhValue.PageNumber,
                PageSize = filterhValue.PageSize,
                OrderByDirection = filterhValue.SortDirection,
            };
            if (IsMaster() == false)
            {
                if (IsGeneralManager() || IsHR() || IsDietitian())
                {
                    options.MultipleWhere.Add(x => x.Location == my.Location);
                }
            }             
            // Where = a => a.MealGB == 0, //a.IsDelete == "N" && 
            options.MultipleWhere.Add(a => a.MealGB == 0);
            if (filterhValue.SearchLocation > -1){
                options.MultipleWhere.Add(x => x.Location == filterhValue.SearchLocation.ToString());
            }
            try {
                if (!string.IsNullOrEmpty(filterhValue.SearchStartMealYMD)){
                    WriteLog("searchDate: "+filterhValue.SearchStartMealYMD);
                    var s = DateTime.Parse(filterhValue.SearchStartMealYMD + " 00:00:00");
                    options.MultipleWhere.Add(x => x.MealYMD >= s);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndMealYMD)){
                    var e = DateTime.Parse(filterhValue.SearchEndMealYMD + " 23:59:59");
                    options.MultipleWhere.Add(x => x.MealYMD <= e);
                }
            } catch (Exception e) {
                WriteError(e.ToString());
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchName)) {
                options.MultipleWhere.Add(x => x.Name == filterhValue.SearchName);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchCompanyName)) {
                if(filterhValue.SearchCompanyName.Equals("DBHiTek")){
                    options.MultipleWhere.Add(x => x.CompanyID == 0);
                } else {
                    options.MultipleWhere.Add(x => x.CompanyName == filterhValue.SearchCompanyName);
                }
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)) {
                options.MultipleWhere.Add(x => x.OrgNameKor == filterhValue.SearchOrgNameKor || x.OrgNameEng == filterhValue.SearchOrgNameKor);
            }
            List<MealLog> l = (List<MealLog>) MealLogData.List(options);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            if (l != null)
            {
                foreach(var mealLog in l){
                    var location = "";
                    if (mealLog.Location != null && CodeLocation != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(mealLog.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng;
                                }
                            }
                        }
                    }
                    var mealYMD=string.Format("{0:yyyy-MM-dd}", mealLog.MealYMD);
                    var companyName = mealLog.CompanyName;
                    if(string.IsNullOrEmpty(companyName)){
                        companyName = "DB HiTek";
                    }
                    dt.Rows.Add(mealYMD, location, companyName, mealLog.OrgNameKor, mealLog.Name, mealLog.Sabun, mealLog.GradeName, mealLog.StateName);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            }
            return new EmptyResult();
        }

        /// <summary>MealLog
        /// 관리자 : 식수권한정보조회
        /// 임직원 : 자신의식수권한정보조회
        /// 비상주업체 : 자사의 식수권한정보조회
        /// 인사팀 : 해당사업장의 식수권한정보조회
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult PermissionList (MealPermissionGridData values, string mode="")
        {
            
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager() || IsHR();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            MealPermissionGridData filterhValue = (MealPermissionGridData) FilterGridData(values);
            var options = new QueryOptions<Person>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = values.SortDirection,
                Where = a => a.IsDelete == "N" && a.PersonTypeID != null && a.PersonStatusID == 0,
            };
            // PersonTypeID 회원구분: 임직원(0)/상주직원(1)/비상주관리자(2)/비상주직원(3)
            // PersonStatusID 재직상태: 재직(0)/휴직(1)/퇴직(2)
            // WriteLog("Person:"+Dump(PersonData.List(options)));
            var l = GetPermissionList(filterhValue);
            // WriteLog("list:" + Dump(l));
            MealPermissionListViewModel vm =new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CurrentRoute = values,
                SearchRoute=filterhValue,
            };
            if (l != null && l.a != null) {
                vm.MealAccessDTOs = l.a;
                vm.TotalPages = values.GetTotalPages(l.b);
                vm.TotalCnt = l.b;
            } else {
                vm.TotalPages = 0;
                vm.TotalCnt = 0;
            }
            return View(vm);
        }

        private dynamic GetPermissionList(MealPermissionGridData values){
            PersonDTO my = GetMe();
            var count = 0;
            List<MealAccessDTO> list = new();
            bool isConnected = false;
            try{
                if (DbSVISIT != null && DbSVISIT.Database.CanConnect()){
                    int offset = 0;
                    if (values != null){
                        offset = (values.PageNumber - 1) * values.PageSize; // COALESCE(a.IS_DELETED, b.PersonTypeID) as 
                    }
                    StringBuilder sb = new();
                    sb.Append(@"SELECT Location, OrgNameKor, OrgNameEng, e.Name, e.Sabun, GradeName, EMPLOYEE_TYPE, PersonTypeID, CompanyName, 
                        MealAccessID, COALESCE(MealGB1, 1) as MealGB1, COALESCE(MealGB2, 1) as MealGB2, COALESCE(MealGB3, 1) as MealGB3, COALESCE(MealGB4, 1) as MealGB4, COALESCE(MealGB5, 1) as MealGB5, COALESCE(MealGB6, 1) as MealGB6, UpdateDate, InsertDate FROM 
                        (SELECT Location, OrgNameKor, OrgNameEng, c.Name, Sabun, GradeName, EMPLOYEE_TYPE, PersonTypeID, COALESCE(CompanyName, 'DB HiTek') as CompanyName FROM (
                            SELECT b.PersonID, COALESCE(a.EMPLOYEE_ID, b.sabun, 'N') as Sabun, COALESCE(a.USER_NAME, b.Name, 'N') as Name, 
                                COALESCE(a.Location, b.Location, 'N') as Location, b.CompanyID, COALESCE(b.PersonTypeID, 0) as PersonTypeID, 
                                b.OrgID as OrgID, COALESCE(a.DEPT_NAME, b.OrgNameKor, 'N') as OrgNameKor, COALESCE(b.OrgNameEng, 'N') as OrgNameEng, 
                                COALESCE(b.LevelCodeID, 3) as LevelCodeID, b.GradeID as GradeID, COALESCE(a.SAP_TITLE_NAME, b.GradeName) as GradeName, 
                                COALESCE(a.PASSWORD, b.Password, 'N') as Password, b.PWUpdateDate, b.BirthDate, b.Gender, COALESCE(a.MOBILE, b.Mobile) as Mobile, 
                                COALESCE(b.Tel, '') as Tel, COALESCE(a.Email, b.Email) as Email, b.ImageData, b.ImageDataHash, COALESCE(CONVERT(int, a.IS_DELETED), b.PersonStatusID, 0) as PersonStatusID, 
                                b.HomeAddr, b.HomeDetailAddr, b.HomeZipcode, COALESCE(b.IsForeigner, 0) as IsForeigner, b.Nationality, b.ImmStatusCodeID, 
                                b.ImmStartDate, b.ImmEndDate, b.IsAllowSMS, b.WorkTypeCodeID, b.CarAllowCnt, b.CarRegCnt, b.TermsPrivarcyFileData, 
                                b.TermsPrivarcyFileDataHash, b.CardIssueStatus, b.CardCreateCnt, CONVERT(CHAR(10),b.VisitorEduLastDate,23) as VisitorEduLastDate, 
                                CONVERT(CHAR(10), b.CleanEduLastDate,23) as CleanEduLastDate, CONVERT(CHAR(10),b.SafetyEduLastDate,23) as SafetyEduLastDate, 
                                CONVERT(CHAR(10),b.VisitLastDate,23) as VisitLastDate, CONVERT(CHAR(10),b.ValidDate,23) as ValidDate, b.UpdateIP, b.CardID, b.CardNo, b.IsRecTempCard, 
                                CONVERT(CHAR(10),b.TempCardRecDate,23) as TempCardRecDate, COALESCE(b.InsertSabun,'N') as InsertSabun, b.InsertName, b.InsertOrgID, 
                                b.InsertOrgNameKor, b.InsertOrgNameEng, CONVERT(CHAR(10), b.UpdateDate,23) as UpdateDate, CONVERT(CHAR(10),b.InsertDate,23) as InsertDate, 
                                COALESCE(b.IsDelete,'N') as IsDelete, b.PID, a.EMPLOYEE_TYPE 
                            FROM (SELECT * FROM PASSPORT.dbo.V_IMS_USER_INFO  WHERE USER_NAME IS NOT NULL");
                    if (IsMaster() == false)
                    {
                        if(IsGeneralManager() || IsHR()){
                            sb.Append(" AND Location='"+my.Location+"'");
                        }
                    }
                    sb.Append(@") a FULL OUTER JOIN 
                                (SELECT * from Person WHERE IsDelete = 'N' ");
                    if (IsMaster() == false)
                    {
                        if(IsGeneralManager() || IsHR()){
                            sb.Append(" AND Location='"+my.Location+"'");
                        }
                    }
                    sb.Append("  ) b ON (a.EMPLOYEE_ID = b.sabun) ");
                    // where AND Location='2000'
                    StringBuilder sbWhere = new();
                    sbWhere.Append("WHERE (b.PersonStatusID = '0' OR a.IS_DELETED = '0') ");
                    // a: V_IMS_USER_INFO, b: Person
                    if (values != null) {
                        // 레벨별 권한 설정.
                        // if (IsMaster() == false){
                        //     WriteLog("LevelCodeID:" + my.LevelCodeID);
                        //     if (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager || my.LevelCodeID == (int)VisitEnum.LevelType.Security) {
                        //         // 일반관리자 : 해당 사업장의 사원정보조회 / 엑셀다운로드 Location
                        //         // 보안실 : 해당 사업장의 사원정보 조회 / 엑셀다운로드
                        //         sbWhere.Append("AND (a.Location = '");
                        //         sbWhere.Append(my.Location);
                        //         sbWhere.Append("' OR b.Location = '");
                        //         sbWhere.Append(my.Location);
                        //         sbWhere.Append("') ");
                        //     } else if (my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager){ // 비상주업체 : 자사 임직원 사원정보 조회 / 엑셀다운로드
                        //         sbWhere.Append("AND b.CompanyID = '");
                        //         sbWhere.Append(my.CompanyID);
                        //         sbWhere.Append("' ");
                        //     } else if (IsEmployee(my.LevelCodeID) && !string.IsNullOrEmpty(my.Sabun)){ // 임직원 : 자신의 사원정보 조회
                        //         sbWhere.Append("AND (a.EMPLOYEE_ID = '");
                        //         sbWhere.Append(my.Sabun);
                        //         sbWhere.Append("' OR b.Sabun = '");
                        //         sbWhere.Append(my.Sabun);
                        //         sbWhere.Append("') ");
                        //     }
                        // }
                        // 검색: SearchLocation SearchCompanyName SearchOrgNameKor SearchName
                        if (values.SearchLocation >-1) { // 회원 구분
                            sbWhere.Append("AND (b.Location = '");
                            sbWhere.Append(values.SearchLocation);
                            sbWhere.Append("' OR a.Location = '");
                            sbWhere.Append(values.SearchLocation);
                            sbWhere.Append("') ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchCompanyName) && !values.SearchCompanyName.Equals("DB HiTek")) { // 회사
                            int companyID = GetCompanyID(values.SearchCompanyName);
                            if (companyID == -1) {
                                return new { a= list, b=0, c=isConnected};
                            }
                            sbWhere.Append("AND b.CompanyID = '");
                            sbWhere.Append(companyID);
                            sbWhere.Append("' ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchOrgNameKor) && !values.SearchOrgNameKor.Equals("+") && !values.SearchOrgNameKor.Equals("%20")) { // 부서명
                            WriteLog("[Search] OrgName:" + values.SearchOrgNameKor);
                            sbWhere.Append("AND (b.OrgNameEng = '");
                            sbWhere.Append(values.SearchOrgNameKor);
                            sbWhere.Append("' OR ");
                            sbWhere.Append(" b.OrgNameKor = '");
                            sbWhere.Append(values.SearchOrgNameKor);
                            sbWhere.Append("' OR a.DEPT_NAME = '");
                            sbWhere.Append(values.SearchOrgNameKor);
                            sbWhere.Append("') ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchName) && !values.SearchName.Equals("+") && !values.SearchName.Equals("%20")) { // 이름
                            WriteLog("[Search] Name:" + values.SearchName);
                            sbWhere.Append("AND (b.Name = '");
                            sbWhere.Append(values.SearchName);
                            sbWhere.Append("' OR a.USER_NAME = '");
                            sbWhere.Append(values.SearchName);
                            sbWhere.Append("') ");
                        }
                    }
                    sb.Append(sbWhere);
                    sb.Append(") c LEFT OUTER JOIN Company d ON (c.CompanyID = d.CompanyID) ");
                    if (values != null){
                        // sb.Append("OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY ");
                        if (values.PageSize > 0){ // 0: 전체 가져오기
                            // order by
                            sb.Append("ORDER By  Name ");
                            // offset
                            sb.Append(" OFFSET ");
                            sb.Append(offset);
                            sb.Append(" ROWS FETCH NEXT ");
                            sb.Append(values.PageSize);
                            sb.Append(" ROWS ONLY ");
                        }
                        if (values.PageSize > 0)
                        {
                            StringBuilder sb2 = new();
                            sb2.Append(@"SELECT count(*) as cnt
                                FROM (SELECT * FROM PASSPORT.dbo.V_IMS_USER_INFO  WHERE USER_NAME IS NOT NULL");
                            if (IsMaster() == false)
                            {
                                if(IsGeneralManager() || IsHR()){
                                    sb2.Append(" AND Location='"+my.Location+"'");
                                }
                            }
                            sb2.Append(@") a FULL OUTER JOIN 
                                    (SELECT * from Person WHERE IsDelete = 'N'");
                            if (IsMaster() == false)
                            {
                                if(IsGeneralManager() || IsHR()){
                                    sb2.Append(" AND Location='"+my.Location+"'");
                                }
                            }
                            sb2.Append(" ) b ON (a.EMPLOYEE_ID = b.sabun)");
                            sb2.Append(sbWhere);
                            var fs1 = FormattableStringFactory.Create(sb2.ToString());
                            count = DbSVISIT.Database.SqlQuery<int>(fs1).ToList()[0];
                            WriteLog("count:" + count);
                        }
                    }
                    sb.Append(") e LEFT OUTER JOIN MealAccess f ON (e.Sabun = f.Sabun) ");
                    if (values != null && values.PageSize == 0)
                    {
                        sb.Append("ORDER BY Name");
                    }
                    using var command = DbSVISIT.Database.GetDbConnection().CreateCommand();
                    WriteLog("query: " + sb.ToString());
                    command.CommandText = sb.ToString();
                    command.CommandType = CommandType.Text;
                    // EMPLOYEE_TYPE: S:정규직, U:계약직, C: 협력직
                    // PersonTypeID: 0 임직원(S, U), 1: 상주(C), 2:비상주 관리자, 3: 비상주 직원
                    DbSVISIT.Database.OpenConnection(); // DbSVISIT.Database.GetDbConnection()
                    isConnected = true;
                    using var reader = command.ExecuteReader();
                    if(reader.HasRows){
                        while (reader.Read()){
                            MealAccessDTO p = new();
                            for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                            {
                                if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value){
                                    var n = reader.GetName(fieldCount);
                                    var v = reader[fieldCount];
                                    if (v != null){
                                        try{
                                            // WriteLog("read data: "+n+"="+v);
                                            // WriteLog("read data: "+n.GetType()+"="+v.GetType());
                                            typeof(MealAccessDTO).GetProperty(n)?.SetValue(p, v);
                                        }catch(Exception e){
                                            WriteLog(e.ToString());
                                        }
                                    }
                                }
                            }
                            list.Add(p);
                            // WriteLog("Person:" + Dump(p));
                        }
                    }
                    DbSVISIT.Database.CloseConnection();      
                }          
            }catch(Exception e) {
                WriteLog(e.ToString());
            }finally{
                DbSVISIT.Database.CloseConnection();                
            }
            return new { a= list, b=count, c=isConnected};
        }

        // int personID, string Sabun, string Name, string OrgID, string OrgNameKor, string OrgNameEng, int MealGB1, int MealGB2, int MealGB3, int MealGB4,
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult PermissionList (MealPermissionGridData values, MealAccess mealAccess, string mode = "")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            if(mode.Equals("EMealPermission")){ //식수권한관리 수정 
                // 1. 식수권한 (MealAccess) 정보 입력 
                mealAccess.MealGB1 ??= 0;//조식권한: 있음(1), 없음(0)
                mealAccess.MealGB2 ??= 0;//중식권한: 있음(1), 없음(0)
                mealAccess.MealGB3 ??= 0;//석식권한: 있음(1), 없음(0)
                mealAccess.MealGB4 ??= 0;//야식권한: 있음(1), 없음(0)
                // mealAccess.MealGB5 ??= 0;
                // mealAccess.MealGB6 ??= 0;
                if (mealAccess.MealAccessID == null) {
                    mealAccess.InsertDate = DateTime.Now;
                    MealAccessData.Insert(mealAccess);
                } else {
                    mealAccess.UpdateDate = DateTime.Now;
                    MealAccessData.Update(mealAccess);
                }
                WriteLog("mealAccess:" + Dump(mealAccess));
                MealAccessData.Save();
                // return new EmptyResult();
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                return RedirectToAction("PermissionList", dict);
            }
            return RedirectToAction("PermissionList", new { culture = GetLang() });
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchcompanyname?}/{searchorgnamekor?}/{searchname?}")]
        public IActionResult? PermissionExcelDownload(MealPermissionGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);                                  
            // 사업장	회사명	부서명	성명	사번	직급	조식	중식	석식	야식
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn(@Localizer["Place Of Business"]), new DataColumn(@Localizer["Company Name"]), new DataColumn(@Localizer["Department Name"]),
                                        new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Sabun"]), new DataColumn(@Localizer["Position"]),
                                        new DataColumn(@Localizer["Breakfast"]),new DataColumn(@Localizer["Lunch"]), new DataColumn(@Localizer["Dinner"]), new DataColumn(@Localizer["LatenightMeal"]),});
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            MealPermissionGridData filterhValue = (MealPermissionGridData) FilterGridData(values);
            filterhValue.PageSize = 0;
            var l = GetPermissionList(filterhValue);
            List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            WriteLog("permisssions: success");
            if (l != null)
            {
                foreach(var mealAccessDTO in l.a){
                    var location = "";
                    if (mealAccessDTO.Location != null && CodeLocation != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(mealAccessDTO.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng;
                                }
                            }
                        }
                    }
                    dt.Rows.Add(location, mealAccessDTO.CompanyName, mealAccessDTO.OrgNameKor, mealAccessDTO.Name, mealAccessDTO.Sabun, mealAccessDTO.GradeName,
                        mealAccessDTO.MealGB1 == 1?"Y":"N", mealAccessDTO.MealGB2 == 1?"Y":"N", mealAccessDTO.MealGB3 == 1?"Y":"N", mealAccessDTO.MealGB4 == 1?"Y":"N");
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            }            
            return new EmptyResult();            
        }

        [HttpGet]
        public IActionResult InfoList (int tabIdx)
        {
            WriteLog("tabIdx:"+tabIdx);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            ViewBag.tabIdx=tabIdx;
            var options = new QueryOptions<MealTerm>
            {
                Where = a => a.IsDelete == "N",
            };
            var options2 = new QueryOptions<MealSchedule>
            {
                Where = a => a.IsDelete == "N",
            };
            var options3 = new QueryOptions<MealPrice>
            {
                Where = a => a.IsDelete == "N",
            };
            MealInfoListViewModel vm =new(){
                MealTerm = MealTermData.List(options),
                MealSchedule = MealScheduleData.List(options2),
                MealPrice = MealPriceData.List(options3),
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeTermType = GetCommonCodes((int)VisitEnum.CommonCode.TermType),
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult InfoList (string mode, 
            string StartDate0, string EndDate0, string StartDate1, string EndDate1, 
            int Term, 
            string StartTime1, string EndTime1, string StartTime2, string EndTime2, string StartTime3, string EndTime3, string StartTime4, string EndTime4, 
            string Location, int MealGB, string StartTime, string EndTime, int Price
        )
        {
            WriteLog("mode: "+mode+", Term: "+Term+", StartTime1: "+StartTime1+", EndTime1: "+EndTime1+", StartTime2: "+StartTime2+", EndTime2: "+EndTime2);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            
            PersonDTO my = GetMe();
            int tabIdx = 0;

            if (mode.Equals("MealTerm")){ // 식수계절구분
                
                // 1. 식수계절구분 (MealTerm) 수정
                // 하계
                var orgObj0 = GetMealTerm(0); // Term: 동계(0), 하계(1) 
                var newObj0 = orgObj0.Clone();
                newObj0.StartDate = StartDate0;
                newObj0.EndDate = EndDate0;
                // newObj0.UpdateIP = UpdateIP; //입력한장치의IP
                newObj0.UpdateDate = DateTime.Now;
                MealTermData.Update(newObj0);
                MealTermData.Save();
                
                // 동계
                var orgObj1 = GetMealTerm(1); // Term: 동계(0), 하계(1)
                var newObj1 = orgObj1.Clone();
                newObj1.StartDate = StartDate1;
                newObj1.EndDate = EndDate1;
                // newObj1.UpdateIP = UpdateIP; //입력한장치의IP
                newObj1.UpdateDate = DateTime.Now;
                MealTermData.Update(newObj1);
                MealTermData.Save();
            }else if(mode.Equals("MealSchedule")){ // 식수스케줄 
                if(Term > -1){
                    // 1. 식수스케줄 (MealSchedule) 수정 
                    //조식 
                    var orgObj01 = GetMealSchedule(Term, 1); // Term: 동계(0), 하계(1), MealGB: 조식(1),중식(2),석식(3),야식(4)
                    var newObj01 = orgObj01.Clone();
                    newObj01.StartTime = StartTime1; //시작시간 HHMM
                    newObj01.EndTime = EndTime1; //종료시간 HHMM
                    // newObj01.UpdateIP = UpdateIP; //입력한장치의IP
                    newObj01.UpdateDate = DateTime.Now;
                    MealScheduleData.Update(newObj01);
                    MealScheduleData.Save();
                    //조식 
                    var orgObj02 = GetMealSchedule(Term, 2); // Term: 동계(0), 하계(1), MealGB: 조식(1),중식(2),석식(3),야식(4)
                    var newObj02 = orgObj02.Clone();
                    newObj02.StartTime = StartTime2; //시작시간 HHMM
                    newObj02.EndTime = EndTime2; //종료시간 HHMM
                    // newObj02.UpdateIP = UpdateIP; //입력한장치의IP
                    newObj02.UpdateDate = DateTime.Now;
                    MealScheduleData.Update(newObj02);
                    MealScheduleData.Save();
                    //조식 
                    var orgObj03 = GetMealSchedule(Term, 3); // Term: 동계(0), 하계(1), MealGB: 조식(1),중식(2),석식(3),야식(4)
                    var newObj03 = orgObj03.Clone();
                    newObj03.StartTime = StartTime3; //시작시간 HHMM
                    newObj03.EndTime = EndTime3; //종료시간 HHMM
                    // newObj03.UpdateIP = UpdateIP; //입력한장치의IP
                    newObj03.UpdateDate = DateTime.Now;
                    MealScheduleData.Update(newObj03);
                    MealScheduleData.Save();
                    //조식 
                    var orgObj04 = GetMealSchedule(Term, 4); // Term: 동계(0), 하계(1), MealGB: 조식(1),중식(2),석식(3),야식(4)
                    var newObj04 = orgObj04.Clone();
                    newObj04.StartTime = StartTime4; //시작시간 HHMM
                    newObj04.EndTime = EndTime4; //종료시간 HHMM
                    // newObj04.UpdateIP = UpdateIP; //입력한장치의IP
                    newObj04.UpdateDate = DateTime.Now;
                    MealScheduleData.Update(newObj04);
                    MealScheduleData.Save();
                }
            }else if(mode.Equals("MealPrice")){ // 식수단가
                tabIdx = 1;

                // 1. 식수단가 (MealPrice) 등록 
                MealPrice mealPrice = new()
                {
                    Location = Location, //식당명 
                    Price = Price, //가격 
                    
                    UpdateIP = GetUserIP(), // 입력한장치의IP
                    UpdateSabun = my.Sabun,//등록자 
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now
                };
                MealPriceData.Insert(mealPrice);
                MealPriceData.Save();
                WriteLog("mealPrice: "+Dump(mealPrice));
            }
            return RedirectToAction("InfoList", new { tabIdx, culture=GetLang()});
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchisvisitor?}/{searchstartmealymd?}/{searchendmealymd?}/{searchvisitantcompany?}/{searchorgnamekor?}/{searchvisitantname?}/{searchmealgb1?}/{searchmealgb2?}/{searchmealgb3?}/{searchmealgb4?}")]
        public IActionResult ManualList (MealManualGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            var accessible = IsMaster() || IsGeneralManager() || IsHR() || IsDietitian();
            if (accessible == false) {
                return View("_Inaccessible");
            }

            MealManualGridData filterhValue = (MealManualGridData) FilterGridData(values);
            var options = new QueryOptions<MealLog>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = values.SortDirection,
                // Where = a => a.IsManual == 1, //a.IsDelete == "N" && 
            };
            var my = GetMe();
            if (IsMaster() == false)
            {
                if (IsGeneralManager() || IsHR() || IsDietitian())
                {
                    options.MultipleWhere.Add(x => x.Location == my.Location);
                }
            }
            options.MultipleWhere.Add(a => a.IsManual == 1);
            if (filterhValue.SearchLocation > -1){
                options.MultipleWhere.Add(x => x.Location == filterhValue.SearchLocation.ToString());
            }
            if (filterhValue.SearchIsVisitor > -1) {
                options.MultipleWhere.Add(x => x.IsVisitor == filterhValue.SearchIsVisitor);
            }
            try {
                if (!string.IsNullOrEmpty(filterhValue.SearchStartMealYMD)){
                    WriteLog("searchDate: "+filterhValue.SearchStartMealYMD);
                    var s = DateTime.Parse(filterhValue.SearchStartMealYMD + " 00:00:00");
                    options.MultipleWhere.Add(x => x.MealYMD >= s);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndMealYMD)){
                    var e = DateTime.Parse(filterhValue.SearchEndMealYMD + " 23:59:59");
                    options.MultipleWhere.Add(x => x.MealYMD <= e);
                }
            } catch (Exception e) {
                WriteError(e.ToString());
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchVisitantCompany)) {
                if(filterhValue.SearchVisitantCompany.Equals("DBHiTek")){
                    options.MultipleWhere.Add(x => x.CompanyID == 0);
                } else {
                    options.MultipleWhere.Add(x => x.CompanyName == filterhValue.SearchVisitantCompany);
                }
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)) {
                options.MultipleWhere.Add(x => x.OrgNameKor == filterhValue.SearchOrgNameKor || x.OrgNameEng == filterhValue.SearchOrgNameKor);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchVisitantName)) {
                options.MultipleWhere.Add(x => x.VisitantName == filterhValue.SearchVisitantName);
            }
            List<int> mealGbList = new();
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB1) && filterhValue.SearchMealGB1.Equals("1")) {
                mealGbList.Add(1);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB2) && filterhValue.SearchMealGB2.Equals("1")) {
                mealGbList.Add(2);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB3) && filterhValue.SearchMealGB3.Equals("1")) {
                mealGbList.Add(3);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB4) && filterhValue.SearchMealGB4.Equals("1")) {
                mealGbList.Add(4);
            }
            if (mealGbList.Count > 0){
                options.MultipleWhere.Add(x => x.MealGB != null && mealGbList.Contains((int)x.MealGB));
            }

            MealManualListViewModel vm =new(){
                MealLog = MealLogData.List(options),
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // 방문자구분 VisitorType 0 임직원, 1 방문자 
                CodeVisitorType = GetCommonCodes((int)VisitEnum.CommonCode.VisitorType),
                // 식사구분 MealType 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                CodeMealType = GetCommonCodes((int)VisitEnum.CommonCode.MealType),

                CurrentRoute = values,
                SearchRoute=filterhValue,
                TotalPages = values.GetTotalPages(MealLogData.Count),
                TotalCnt = MealLogData.Count,
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchisvisitor?}/{searchstartmealymd?}/{searchendmealymd?}/{searchvisitantcompany?}/{searchorgnamekor?}/{searchvisitantname?}/{searchmealgb1?}/{searchmealgb2?}/{searchmealgb3?}/{searchmealgb4?}")]
        public IActionResult? ManualExcelDownload(MealManualGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }
            DataTable dt = new(ExTitle);
            // 식사일자	등록일	사업장	부서명	성명	사번	등록구분	회사명	직급	식사구분
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn(@Localizer["Meal Date"]), new DataColumn(@Localizer["Registration Date"]), new DataColumn(@Localizer["Place Of Business"]), 
                                        new DataColumn(@Localizer["Department Name"]), new DataColumn(@Localizer["Name"]), new DataColumn(@Localizer["Sabun"]), new DataColumn(@Localizer["Registration Classify"]),
                                        new DataColumn(@Localizer["Company Name"]),new DataColumn(@Localizer["Position"]), new DataColumn(@Localizer["Meal Classify"]),});
            
            // WriteLog(Dump(values));
            PersonDTO my = GetMe();
            MealManualGridData filterhValue = (MealManualGridData) FilterGridData(values);
            filterhValue.PageSize = 0;

            var options = new QueryOptions<MealLog>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = values.SortDirection,
                // Where = a => a.IsManual == 1, //a.IsDelete == "N" && 
            };
            if (IsMaster() == false)
            {
                if (IsGeneralManager() || IsHR() || IsDietitian())
                {
                    options.MultipleWhere.Add(x => x.Location == my.Location);
                }
            }
            options.MultipleWhere.Add(a => a.IsManual == 1);
            if (filterhValue.SearchLocation > -1){
                options.MultipleWhere.Add(x => x.Location == filterhValue.SearchLocation.ToString());
            }
            if (filterhValue.SearchIsVisitor > -1) {
                options.MultipleWhere.Add(x => x.IsVisitor == filterhValue.SearchIsVisitor);
            }
            try {
                if (!string.IsNullOrEmpty(filterhValue.SearchStartMealYMD)){
                    WriteLog("searchDate: "+filterhValue.SearchStartMealYMD);
                    var s = DateTime.Parse(filterhValue.SearchStartMealYMD + " 00:00:00");
                    options.MultipleWhere.Add(x => x.MealYMD >= s);
                }
                if (!string.IsNullOrEmpty(filterhValue.SearchEndMealYMD)){
                    var e = DateTime.Parse(filterhValue.SearchEndMealYMD + " 23:59:59");
                    options.MultipleWhere.Add(x => x.MealYMD <= e);
                }
            } catch (Exception e) {
                WriteError(e.ToString());
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchVisitantCompany)) {
                if(filterhValue.SearchVisitantCompany.Equals("DBHiTek")){
                    options.MultipleWhere.Add(x => x.CompanyID == 0);
                } else {
                    options.MultipleWhere.Add(x => x.CompanyName == filterhValue.SearchVisitantCompany);
                }
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchOrgNameKor)) {
                options.MultipleWhere.Add(x => x.OrgNameKor == filterhValue.SearchOrgNameKor || x.OrgNameEng == filterhValue.SearchOrgNameKor);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchVisitantName)) {
                options.MultipleWhere.Add(x => x.VisitantName == filterhValue.SearchVisitantName);
            }
            List<int> mealGbList = new();
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB1) && filterhValue.SearchMealGB1.Equals("1")) {
                mealGbList.Add(1);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB2) && filterhValue.SearchMealGB2.Equals("1")) {
                mealGbList.Add(2);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB3) && filterhValue.SearchMealGB3.Equals("1")) {
                mealGbList.Add(3);
            }
            if (!string.IsNullOrEmpty(filterhValue.SearchMealGB4) && filterhValue.SearchMealGB4.Equals("1")) {
                mealGbList.Add(4);
            }
            if (mealGbList.Count > 0){
                options.MultipleWhere.Add(x => x.MealGB != null && mealGbList.Contains((int)x.MealGB));
            }
            List<MealLog> l = (List<MealLog>) MealLogData.List(options);
            if (l != null)
            {
                List<CommonCode> CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
                // 방문자구분 VisitorType 0 임직원, 1 방문자 
                List<CommonCode> CodeVisitorType = GetCommonCodes((int)VisitEnum.CommonCode.VisitorType);
                    // 식사구분 MealType 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                List< CommonCode > CodeMealType = GetCommonCodes((int)VisitEnum.CommonCode.MealType);
                foreach(var mealLog in l){
                    var location = "";
                    var visitorTypeName = "";
                    string mealYMD="";
                    var mealTypeName = "";
                    if (mealLog.Location != null && CodeLocation != null && CodeLocation != null) {
                        foreach(var m in CodeLocation) {
                            if (m.SubCodeDesc != null && m.SubCodeDesc.Equals(mealLog.Location)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    location = m.CodeNameKor;
                                }else {
                                    location = m.CodeNameEng;
                                }
                            }
                        }
                    }        
                    if (mealLog.IsVisitor != null && CodeVisitorType != null) {
                        foreach(var a in CodeVisitorType) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(mealLog.IsVisitor)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    visitorTypeName = a.CodeNameKor;
                                }else {
                                    visitorTypeName = a.CodeNameEng;
                                }
                            }
                        }
                    }
                    if (mealLog.MealGB != null && CodeMealType != null) {
                        foreach(var a in CodeMealType) {
                            if (a.SubCodeID != null && a.SubCodeID.Equals(mealLog.MealGB)) {
                                if (CultureInfo.CurrentCulture.ToString().Equals("ko")) {
                                    mealTypeName = a.CodeNameKor;
                                }else {
                                    mealTypeName = a.CodeNameEng;
                                }
                            }
                        }
                    }        
                    // if(mealLog.InsertDate != null){
                    mealYMD=string.Format("{0:yyyy-MM-dd}", mealLog.MealYMD);
                    // }
                    // 식사일자	등록일	사업장	부서명	성명	사번	등록구분	회사명	직급	식사구분
                    dt.Rows.Add(mealYMD, mealLog.InsertDate, location, mealLog.OrgNameKor, mealLog.Name, mealLog.Sabun, visitorTypeName, mealLog.VisitantCompany, mealLog.VisitantGrade, mealTypeName);
                }
                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
            }            
            return new EmptyResult(); 
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchisvisitor?}/{searchstartmealymd?}/{searchendmealymd?}/{searchvisitantcompany?}/{searchorgnamekor?}/{searchvisitantname?}/{searchmealgb1?}/{searchmealgb2?}/{searchmealgb3?}/{searchmealgb4?}")]
        public IActionResult ManualReg ()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            MealManualRegViewModel vm =new(){
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // 방문자구분 VisitorType 0 임직원, 1 방문자 
                CodeVisitorType = GetCommonCodes((int)VisitEnum.CommonCode.VisitorType),
                // 식사구분 MealType 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                CodeMealType = GetCommonCodes((int)VisitEnum.CommonCode.MealType),
            };
            return View(vm);
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchisvisitor?}/{searchstartmealymd?}/{searchendmealymd?}/{searchvisitantcompany?}/{searchorgnamekor?}/{searchvisitantname?}/{searchmealgb1?}/{searchmealgb2?}/{searchmealgb3?}/{searchmealgb4?}")]
        public IActionResult ManualReg(string mode, 
            int IsManual, int IsVisitor, string VisitantName, string Location, int MealGB, string VisitantCompany, string VisitantGrade, string Comment, 
            string Sabun, string Name, string OrgID, string OrgNameKor, string OrgNameEng, string GradeName
        )
        { 

            WriteLog("mode: "+mode+", IsManual: "+IsManual+", MealGB: "+MealGB+", IsVisitor: "+IsVisitor+", VisitantName: "+VisitantName);
           
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("A")) // 식수 수기등록
            {
                PersonDTO my = GetMe();
                var mealPriceObj = GetMealPrice(Location); //사업장의 가격(식대) 가져오기 
                WriteLog("mealPriceObj: "+Dump(mealPriceObj));

                // 1. 식수로그 (MealLog) 정보 입력 
                MealLog mealLog = new()
                {   
                    Sabun = Sabun,//식사자
                    Name = Name,
                    // OrgID = OrgID,
                    OrgNameKor = OrgNameKor,
                    OrgNameEng = OrgNameEng,
                    GradeName = GradeName,

                    MealYMD = DateTime.Now, //식수일
                    Location = Location,
                    MealGB = MealGB, //식수구분: 조식(1),중식(2),석식(3),야식(4)
                    Price = mealPriceObj.Price, //가격(식대) 
                    CompanyID = 0, // DB HiTek
                    IsManual = IsManual, // 수기등록구분: 식수리더(0),수기등록(1)
                    IsVisitor = IsVisitor, // 방문자구분: 임직원(0)/방문자(1)
                    VisitantName = VisitantName, //성명
                    VisitantCompany = VisitantCompany, //회사명
                    VisitantGrade = VisitantGrade, //직급
                    Comment = Comment, //기타
                    UpdateIP = GetUserIP(), // 입력한장치의IP
                    UpdateSabun = my.Sabun,//등록자: 영양사 수기 등록시 입력 영양사의 사번
                    // InsertName = my.Name,
                    // InsertOrgID = my.OrgID,
                    // InsertOrgNameKor = my.OrgNameKor,
                    // InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now
                };
                MealLogData.Insert(mealLog);
                MealLogData.Save();
                WriteLog("mealLog: "+Dump(mealLog));

            }
            // return new EmptyResult();
            return RedirectToAction("ManualList", new { culture=GetLang() });          
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchisvisitor?}/{searchstartmealymd?}/{searchendmealymd?}/{searchvisitantcompany?}/{searchorgnamekor?}/{searchvisitantname?}/{searchmealgb1?}/{searchmealgb2?}/{searchmealgb3?}/{searchmealgb4?}")]
        public IActionResult ManualDetail (int mealLogID, string slug="") 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mealLogID:" + mealLogID + ", slug" + slug);

            var mealLog = MealLogData.Get(new QueryOptions<MealLog>{
                Where = a => a.MealLogID == mealLogID,
            }) ?? new MealLog();

            MealManualDetailViewModel vm = new(){
                MealLog = mealLog,
                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // 방문자구분 VisitorType 0 임직원, 1 방문자 
                CodeVisitorType = GetCommonCodes((int)VisitEnum.CommonCode.VisitorType),
                // 식사구분 MealType 조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
                CodeMealType = GetCommonCodes((int)VisitEnum.CommonCode.MealType),
            };
            return View(vm);
        }

        //식수계절정보 가져오기 
         private MealTerm? GetMealTerm(int term)
        {
            WriteLog("term: " + term);
            var options = new QueryOptions<MealTerm>
            {
                Where = a => a.Term == term && a.IsDelete == "N",
            };
            if (MealTermData != null){
                var mealTerm = MealTermData.GetNT(options);
                return mealTerm;
            }
            return null;
        }
        //식수스케줄정보 가져오기 
         private MealSchedule? GetMealSchedule(int term, int mealGB)
        {
            WriteLog("term: " + term);
            var options = new QueryOptions<MealSchedule>
            {
                Where = a => a.Term == term && a.MealGB == mealGB && a.IsDelete == "N",
            };
            if (MealScheduleData != null){
                var mealSchedule = MealScheduleData.GetNT(options);
                return mealSchedule;
            }
            return null;
        }
        //식수단가 가져오기 
         private MealPrice? GetMealPrice(string location)
        {
            WriteLog("location: " + location);
            var options = new QueryOptions<MealPrice>
            {
                Where = a => a.Location == location && a.IsDelete == "N",
            };
            if (MealPriceData != null){
                var mealPrice = MealPriceData.GetNT(options);
                return mealPrice;
            }
            return null;
        }
    }
}
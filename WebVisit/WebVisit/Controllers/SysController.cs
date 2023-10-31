using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebVisit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class SysController : BaseController
    {
        // 메뉴 관리
        private Repository<Menu>? MenuData { get; set; }
        private Repository<MenuHistory>? MenuHistoryData { get; set; }

        // 레벨 관리
        // private Repository<Level>? LevelData { get; set; }
        private Repository<LevelHistory>? LevelHistoryData { get; set; }
        private Repository<MenuLevel>? MenuLevelData { get; set; }

        // 결재 경로 관리

        private Repository<Approval>? ApprovalData { get; set; }
        private Repository<ApprovalHistory>? ApprovalHistoryData { get; set; }
        private Repository<ApprovalLine>? ApprovalLineData { get; set; }
        private Repository<ApprovalLineHistory>? ApprovalLineHistoryData { get; set; }

        // 공통 코드 관리
        private Repository<CommonCodeHistory>? CommonCodeHistoryData { get; set; }

        //공휴일 관리
        private Repository<Holiday>? HolidayData { get; set; }

        public SysController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer)
        {
        }
        
        protected override void Init(){
            if (DbSVISIT != null) {
                MenuData ??= new Repository<Menu>(DbSVISIT);
                MenuHistoryData ??= new Repository<MenuHistory>(DbSVISIT);

                LevelData ??= new Repository<Level>(DbSVISIT);
                LevelHistoryData ??= new Repository<LevelHistory>(DbSVISIT);
                MenuLevelData ??= new Repository<MenuLevel>(DbSVISIT);

                ApprovalData ??= new Repository<Approval>(DbSVISIT);
                ApprovalHistoryData ??= new Repository<ApprovalHistory>(DbSVISIT);
                ApprovalLineData ??= new Repository<ApprovalLine>(DbSVISIT);
                ApprovalLineHistoryData ??= new Repository<ApprovalLineHistory>(DbSVISIT);

                CommonCodeHistoryData ??= new Repository<CommonCodeHistory>(DbSVISIT);
                HolidayData ??= new Repository<Holiday>(DbSVISIT);
            }
        }

        // [Route("~/{controller}")]
        [Route("~/{controller}/{action}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("MenuList", new { culture = GetLang() });
        }

        // ### 메뉴 관리 ###
        [HttpGet("{groupNo:int?}/{menuID:int?}/{slug?}")]
        public IActionResult MenuList(int groupNo, int menuID, string slug="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            WriteLog("groupNo:" + groupNo + ", menuID:" + menuID + ", slug" + slug);
            if (menuID>0){
                var ml = GetMenuLevelList(menuID:menuID);
                ViewBag.menuLevels = ml;
                // WriteLog(Dump(ml));
            }
            
            // Level
            var options = new QueryOptions<Level>
            {
                OrderBy = a => a.LevelID,
                Where = a => a.IsDelete == "N"
            };
            IEnumerable<Level> levels = LevelData.List(options);
            ViewBag.levels = levels;
            
            ViewBag.GroupNo = groupNo;
            ViewBag.MenuID = menuID;
            var m = GetMenuList();

            var m2 = m.ToList();
            foreach(var a in m2) {
                // WriteLog(a.MenuNameKor+" groupNo:"+a.GroupNo+", orderNo:"+a.OrderNo);
                if (a.MenuID == menuID){
                    ViewBag.MenuNameKor = a.MenuNameKor;
                    ViewBag.MenuNameEng = a.MenuNameEng;
                    ViewBag.URL = a.URL;
                    ViewBag.IconImg = a.IconImg;
                    ViewBag.Depth = a.Depth;
                    if (a.Depth == 0) {
                        ViewBag.URLDisabled = "readOnly";
                        ViewBag.IconImgDisabled = "";
                    } else {
                        ViewBag.URLDisabled = "";
                        ViewBag.IconImgDisabled = "readOnly";
                    }
                }
            }
            // WriteLog("m:" + Dump(m));
            // WriteLog("m2:" + Dump(m2));
            return View(m2);
        }

        /// <summary>
        /// 메뉴 추가 / 수정 / 삭제
        /// </summary>
        /// <param name="groupNo">groupNo</param>
        /// <param name="menuID">menuID</param>
        /// <param name="mode">A:샌규 입력, E:수정, D:삭제</param>
        /// <param name="MenuNameKor">공통 한글 이름</param>
        /// <param name="MenuNameEng">공통 영문 이름</param>
        /// <param name="URL">URL</param>
        /// <param name="IconImg">아이콘 URL</param>IconImg
        /// 레벨별 메뉴 이름         
        /// <param name="MenuLevelID">레벨별 한글 이름 Array, 없으면 0</param>
        /// <param name="MenuNameKorByLevel">레벨별 한글 이름 Array</param>
        /// <param name="MenuNameEngByLevel">레벨별 영문 이름 Array</param>
        /// <returns></returns>
        [HttpPost("{groupNo:int?}/{menuID:int?}/{slug?}")]
        public IActionResult MenuList(int groupNo, int menuID, string mode, 
            string MenuNameKor, string MenuNameEng,string URL, string IconImg,
            string[] LevelID, string[] MenuLevelID, string[] MenuNameKorByLevel, string[] MenuNameEngByLevel, string slug="")
        {
            if(IsAccessAPI() == false) {
                return View("_Inaccessible");
            }
            WriteLog("mode: "+mode+", groupNo:" + groupNo + ", menuID:" + menuID);
            if (menuID>0){
                var ml = GetMenuLevelList(menuID:menuID);
            }
            // MenuHistory
            PersonDTO my = GetMe();
            if (mode.Equals("A")) // 메뉴 추가
            {
                Menu menu = new()
                {
                    GroupNo = groupNo,
                    MenuNameKor = MenuNameKor,
                    MenuNameEng = MenuNameEng,
                    URL = URL,
                    IconImg = IconImg,
                    Depth = 0,
                    OrderNo = 1,
                };
                if(groupNo>0) { // 하위 메뉴 추가
                    _ = GetMenuList(groupNo: groupNo);
                    menu.Depth = 1;
                    menu.OrderNo = MenuData.Max + 1;

                    MenuData.Insert(menu);
                    MenuData.Save();

                    for(var i =0; i< MenuNameKorByLevel.Length; i++) {
                        var nameK = MenuNameKorByLevel[i]??MenuNameKor;
                        var nameE = MenuNameEngByLevel[i]??MenuNameEng;

                        MenuLevel menuLevel = new()
                        {
                            MenuID = menu.MenuID,
                            LevelID = int.Parse(LevelID[i]),
                            MenuNameKor = nameK,
                            MenuNameEng = nameE??"",
                            IsAccess = "N",
                            IsDisplay = "N",
                            InsertSabun = my.Sabun,
                            InsertOrgID = my.OrgID,
                            InsertOrgNameKor = my.OrgNameKor,
                            InsertOrgNameEng = my.OrgNameEng,
                            InsertDate = DateTime.Now,
                            IsDelete = "N"
                        };
                        MenuLevelData.Insert(menuLevel);
                        WriteLog("menuLevel: "+Dump(menuLevel));
                    }
                } else { // 최 상위 메뉴 추가
                    MenuData.Insert(menu);
                    MenuData.Save();
                    
                    menu.GroupNo = menu.MenuID;
                    MenuData.Update(menu);
                    MenuData.Save();

                    WriteLog("pMenu: "+Dump(menu));
                }
                MenuLevelData.Save();                
            }else if (mode.Equals("E")) // 수정
            {
                if (menuID > 0)
                {
                    var orgObj = GetMenu(menuID, true);
                    var newObj = orgObj.Clone();
                    newObj.MenuNameKor = MenuNameKor;
                    newObj.MenuNameEng = MenuNameEng;
                    newObj.URL = URL;
                    newObj.IconImg = IconImg;
                    newObj.UpdateDate = DateTime.Now;
                    try
                    {
                        WriteLog("update menu:"+Dump(newObj));
                        MenuData.Update(newObj);
                        MenuData.Save();
                        MenuHistory menuHistory = new()
                        {
                            MenuID = newObj.MenuID,
                            GroupNo = newObj.GroupNo,
                            Depth = newObj.Depth,
                            OrderNo = newObj.OrderNo,
                            MenuNameKor = newObj.MenuNameKor,
                            MenuNameEng = newObj.MenuNameEng,
                            URL = newObj.URL,
                            IconImg = newObj.IconImg,
                            IsDisplay = newObj.IsDisplay,
                            UpdateSabun = my.Sabun,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = DateTime.Now,
                        };
                        MenuHistoryData.Insert(menuHistory);
                        MenuHistoryData.Save();
                    }
                    catch (Exception e)
                    {
                        WriteError(e.ToString());
                    }
                    bool isSave = false;

                    for (var i = 0; i < MenuNameKorByLevel.Length; i++)
                    {
                        // WriteLog("i:"+i+"MenuNameKorByLevel length:"+MenuNameKorByLevel.Length+" / MenuNameEngByLevel length:"+MenuNameEngByLevel.Length + "/ MenuLevelID length:"+MenuLevelID.Length);
                        var nameK = MenuNameKorByLevel[i] ?? MenuNameKor;
                        var nameE = MenuNameEngByLevel[i] ?? MenuNameEng;
                        // WriteLog("MenuNameKorByLevel:" + Dump(MenuNameKorByLevel));
                        // WriteLog("MenuNameKorByLevel:" + Dump(MenuNameKorByLevel));
                        if (MenuLevelID != null && MenuLevelID.Length > i && !String.IsNullOrEmpty(MenuLevelID[i]))
                        {
                            int mid = int.Parse(MenuLevelID[i]);
                            if (mid == 0) { // insert
                                MenuLevel menuLevel = new()
                                {
                                    MenuID = menuID,
                                    LevelID = int.Parse(LevelID[i]),
                                    MenuNameKor = nameK,
                                    MenuNameEng = nameE??"",
                                    IsAccess = "N",
                                    IsDisplay = "N",
                                    InsertSabun = my.Sabun,
                                    InsertOrgID = my.OrgID,
                                    InsertOrgNameKor = my.OrgNameKor,
                                    InsertOrgNameEng = my.OrgNameEng,
                                    InsertDate = DateTime.Now,
                                    IsDelete = "N"
                                };
                                MenuLevelData.Insert(menuLevel);
                                isSave = true;
                                WriteLog("2");
                            } else {
                                var menuLevel = GetMenuLevel(menuLevelID: mid, true);
                                menuLevel.MenuNameKor = nameK;
                                menuLevel.MenuNameEng = nameE??"";
                                MenuLevelData.Update(menuLevel);
                                WriteLog("update menuLevel: " + Dump(menuLevel));
                                isSave = true;
                            }
                        }
                    }
                    if(isSave)
                        DbSVISIT.SaveChanges();
                }
            }else if (mode.Equals("D")){ // 삭제
                if(menuID> 0) {
                    if (menuID == groupNo) {
                        // group 삭제
                        List<Menu> menuList = GetMenuList(groupNo: groupNo);
                        foreach (var m in menuList) {
                            m.IsDelete = "Y";
                            m.UpdateDate = DateTime.Now;
                            MenuData.Update(m);
                        }
                    } else {
                        var orgObj = GetMenu(menuID, true);
                        WriteLog(Dump(orgObj));
                        // var newObj = orgObj.Clone();
                        // newObj.MenuID = menu.CommonCodeID;
                        // newObj.IsDelete = "Y";
                        // newObj.UpdateDate = DateTime.Now;
                        // menuData.Update(newObj);
                        orgObj.IsDelete = "Y";
                        orgObj.UpdateDate = DateTime.Now;
                        MenuData.Update(orgObj);
                    }
                    var menuLevelList = GetMenuLevelList(menuID: menuID);
                    if (menuLevelList != null) {
                        foreach(var m in menuLevelList) {
                            m.IsDelete = " Y";
                            m.UpdateDate = DateTime.Now;
                            MenuLevelData.Update(m);
                        }
                        MenuData.Save();
                    }
                }
                return RedirectToAction("MenuList", new { culture=GetLang() });
            }

            if (groupNo > 0) {
                if (menuID > 0) {
                    return RedirectToAction("MenuList", new { groupNo, menuID, culture=GetLang() });
                }
                return RedirectToAction("MenuList", new { groupNo, culture=GetLang() });
            }
            return RedirectToAction("MenuList", new { culture = GetLang() });
            // return View();
        }

        private List<Menu> GetMenuList(int groupNo = -1){
            var options = new QueryOptions<Menu>
            {
                PageNumber = 0,
                PageSize = 0,
                OrderByDirection = "asc",
                Where = a => a.IsDelete == "N" && a.Depth < 2,
            };
            if (groupNo > 0) {
                options = new QueryOptions<Menu>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.GroupNo == groupNo && a.IsDelete == "N",
                    Max = a => (int?)a.OrderNo ?? 0
                };
            }
            options.MultipleOrderBy.Add(a => a.GroupNo, "asc");
            options.MultipleOrderBy.Add(a => a.OrderNo, "asc");
            // OrderBy = a => a.GroupNo,
            List<Menu> m = (List<Menu>)MenuData.List(options);
            return m;
        }

        private Menu GetMenu(int menuID=-1, bool isNoTracking = false)
        {
            Menu menu = null;
            if(menuID > 0) {
                var options = new QueryOptions<Menu>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.MenuID == menuID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    menu = MenuData.GetNT(menuID);
                } else {
                    menu = MenuData.Get(menuID);
                }
            }
            return menu;
        }

        // ### 레벨 관리 ###
        [HttpGet("{id:int?}/{slug?}")]
        public IActionResult LevelList(int id)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            // Level
            var options = new QueryOptions<Level>
            {
                OrderBy = a => a.LevelName,
                Where = a => a.IsDelete == "N"
            };
            IEnumerable<Level> levels = LevelData.List(options);
            
            levels = levels.Append(new Level { LevelID = -1, LevelName = "레벨추가(+)" });
            ViewBag.levels = levels;
            if (id == 0) {
                id = levels.First().LevelID;
            }
            ViewBag.id = id;

            //MenuLevel
            List<MenuLevel> menuLevelList = new();
            List<Menu> menuList = new();
            // var query = db.Menu
            //     .Where(x => x.IsDelete == "N" && x.Depth < 2)
            //     .OrderByDescending(x => x.GroupNo)
            //     .GroupJoin(
            //         db.MenuLevel.Where(x => x.LevelID == id),
            //         a => a.MenuID, // Menu
            //         b => b.MenuID, // MenuLevel
            //         (a, b) => new { a, b });

            // var menuLevels = query.ToList();
            // ViewBag.menuLevels = menuLevels;
            var m = GetMenuLevelList(id);
            ViewBag.menuLevels = m;
            // WriteLog("sharedMenuList3" + Dump(ViewBag.menuLevels));
            // foreach (var r in m) {
            //     WriteLog("a1:"+r.GetType().ToString());
            //     if (r.b != null){
            //         WriteLog("b1:"+r.b.GetType().ToString());
            //     }
            // }
            // WriteLog("start level:" + menuLevels.Count + ", id: " + id);
            // foreach (var r in menuLevels) // Menu
            // {
            //     WriteLog("a2:"+r.GetType().ToString());
            //     if (r.b != null){
            //         WriteLog("b2:"+r.b.GetType().ToString());
            //     }
            // }
            return View();
        }

        [HttpPost("{id:int?}/{slug?}")]
        public IActionResult LevelList(int id, string mode, string LevelName, string[] MenuIDs)
        {
            WriteLog("post start mode:" + mode);
            if(IsAccessAPI() == false) {
                return View("_Inaccessible");
            }

            if (mode.Equals("M") && MenuIDs != null) { // 레벨별 메뉴 권한 추가/수정
                // foreach (var menuID in MenuIDs) {
                //     WriteLog("save:" + menuID);
                // }
                // var m = GetMenuLevelList(id);
                SetMenuLevelList(id, MenuIDs);
            } else if (mode.Equals("L")) { // 레벨 추가
                WriteLog("new LevelName:" + LevelName);
                PersonDTO my = GetMe();
                Level level = new()
                {
                    LevelName = LevelName,
                    InsertSabun = my.Sabun,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now,
                    IsDelete = "N"
                };
                LevelData.Insert(level);
                LevelData.Save();

                LevelHistory levelHistory = new()
                {
                    LevelID = level.LevelID,
                    LevelName = LevelName,
                    UpdateSabun = my.Sabun,
                    UpdateOrgID = my.OrgID,
                    UpdateOrgNameKor = my.OrgNameKor,
                    UpdateOrgNameEng = my.OrgNameEng,
                    InsertUpdateDate = DateTime.Now,
                };
                LevelHistoryData.Insert(levelHistory);
                LevelHistoryData.Save();
            }
            return RedirectToAction("LevelList", new { id,culture = GetLang() });
        }

        private dynamic SetMenuLevelList(int LevelID, string[] MenuIDs){
            List<int> selectedMenuLevelIDs = new();
            List<int> selectedMenuIDs = new();
            foreach(var a in MenuIDs) {
                string[] t = a.Split("_");
                if(t.Length == 2) {
                    if (!t[1].Trim().Equals("")) { // !t[1].Trim().Equals("0")
                        WriteLog("t:" + Dump(t));
                        try{
                            int b = int.Parse(t[1].Trim());
                            WriteLog("b:" + b);
                            selectedMenuLevelIDs.Add(int.Parse(t[1].Trim()));

                            selectedMenuIDs.Add(int.Parse(t[0].Trim()));
                        }catch(Exception e){
                            WriteError(e.ToString());
                        }
                    }
                }
            }
            WriteLog("selectedMenuLevelIDs:" + Dump(selectedMenuLevelIDs));
            WriteLog("selectedMenuIDs:" + Dump(selectedMenuIDs));
            PersonDTO my = GetMe();
            WriteLog("my:" + Dump(my));
            
            List<MenuLevel> menuLevelList = new();
            List<Menu> menuList = new();
            var query = DbSVISIT.Menu
                .Where(x => x.IsDelete == "N" && x.Depth < 2)
                .OrderByDescending(x => x.GroupNo)
                .GroupJoin(
                    DbSVISIT.MenuLevel.Where(x=>x.LevelID == LevelID),
                    a => a.MenuID, // Menu
                    b => b.MenuID, // MenuLevel
                    (a, b) => new { a, b });

            var menuLevels = query.ToList();
            menuLevels.ForEach(x => {
                var mlID = 0;
                if (x.b != null && x.b.Any()) { // update - data 존재
                    try{
                        mlID = x.b.FirstOrDefault().MenuLevelID;
                        if (selectedMenuLevelIDs.Contains(mlID)){
                            // update Y
                            WriteLog("update Y:" + mlID);
                            foreach(var c in x.b) {
                                c.IsAccess = "Y";
                            }
                        } else {
                            // update N
                            WriteLog("update N:" + mlID);
                            foreach(var c in x.b) {
                                c.IsAccess = "N";
                            }
                        }
                    }catch (Exception e) {
                        WriteError(e.ToString());
                    }
                } else { // insert
                    if (x.a.Depth > 0) {
                        string IsAccess = "N";
                        // InsertSabun InsertOrgID InsertOrgNameKor InsertOrgNameEng UpdateDate InsertDate IsDelete
                        if (selectedMenuIDs.Contains(x.a.MenuID)){
                            IsAccess = "Y";
                        }
                        WriteLog("insert" + x.a.MenuID+", "+x.a.MenuNameKor +", "+x.a.MenuNameEng+", "+IsAccess);
                        MenuLevel menuLevel = new()
                        {
                            MenuID = x.a.MenuID,
                            LevelID = LevelID,
                            MenuNameKor = x.a.MenuNameKor,
                            MenuNameEng = x.a.MenuNameEng??"",
                            IsAccess = IsAccess,
                            IsDisplay = "N",
                            InsertSabun = my.Sabun,
                            InsertOrgID = my.OrgID,
                            InsertOrgNameKor = my.OrgNameKor,
                            InsertOrgNameEng = my.OrgNameEng,
                            InsertDate = DateTime.Now,
                            IsDelete = "N"
                        };
                        MenuLevelData.Insert(menuLevel);
                        WriteLog("insert" + Dump(menuLevel));
                        // menuLevelData.Save();
                    }
                }
            });
            DbSVISIT.SaveChanges();
            return menuLevels;
        }

        private MenuLevel GetMenuLevel(int menuLevelID, bool isNoTracking = false)
        {
            MenuLevel menuLevel = null;
            if(menuLevelID > 0) {
                if(isNoTracking) {
                    menuLevel = MenuLevelData.GetNT(menuLevelID);
                } else {
                    menuLevel = MenuLevelData.Get(menuLevelID);
                }
            }
            return menuLevel;

        }

        private dynamic? GetMenuLevelList(int levelID = -1, int menuID = -1){
            WriteLog("levelID:" + levelID + ", menuID:" + menuID);
            // List<MenuLevel> menuLevelList = new();
            // List<Menu> menuList = new();
            if (levelID > 0) {
                var query = DbSVISIT.Menu
                    .Where(x => x.IsDelete == "N" && x.Depth < 2)
                    .OrderBy(x => x.GroupNo)
                    // .OrderByDescending(x => x.GroupNo)
                    .GroupJoin(
                        DbSVISIT.MenuLevel.Where(x=>x.IsDelete == "N" && x.LevelID == levelID),
                        a => a.MenuID, // Menu
                        b => b.MenuID, // MenuLevel
                        (a, b) => new { a, b });
                var menuLevels = query.ToList();
                // WriteLog("sharedMenuList2:>>" + Dump(menuLevels));
                return menuLevels;
            } else if (menuID > 0) {
                var query = DbSVISIT.Level
                    .Where(x => x.IsDelete == "N")
                    .OrderBy(x => x.LevelID)
                    .GroupJoin(
                        DbSVISIT.MenuLevel.Where(x=>x.IsDelete == "N" && x.MenuID == menuID),
                        a => a.LevelID, // Level
                        b => b.LevelID, // MenuLevel
                        (a, b) => new { a, b });
                var menuLevels = query.ToList();
                return menuLevels;
            }
            return null;
        }

        // ### 결재경로 관리 ###
        public IActionResult Approval (ApprovalGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            return View();
        }
        
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchlocation?}/{searchapprovaltype?}")]
        public IActionResult ApprovalList (ApprovalGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var vm = GetApprovalListViewModel(values);
            // WriteLog("ApprovalList:"+Dump(vm));
            return View(vm);
        }

        [HttpGet("{id:int?}/{slug?}")]
        public IActionResult ApprovalDetailList (ApprovalLineGridData values, string id="", string slug="") //string location="", string approvalType="", 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            ViewBag.parentReload = false;

            if (String.IsNullOrEmpty(id) || id.Equals("0")){
                // return View("_BlankApprovalLine");
                var vm = GetApprovalLineListViewModel(values);
                // var vm = new ApprovalLineListViewModel
                // {
                //     CurrentRoute = values,
                //     TotalPages = 0,
                // };
                return View(vm);
            } else {
                ViewBag.ApprovalID = id;
                // ViewBag.Location = location;
                // ViewBag.ApprovalType = approvalType;
                ViewBag.ApprovalLineName = slug??"";
                WriteLog(Dump(values));
                WriteLog("id: "+id);
                var vm = GetApprovalLineListViewModel(values, id);
                WriteLog(Dump(vm));
                return View(vm);
            }
        }

        [HttpPost("{id:int?}/{slug?}")]
        public IActionResult ApprovalDetailList(ApprovalGridData values, ApprovalLineGridData values2, string id, string saveAction, int ApprovalID,
            string ApprovalSabun, string ApprovalName, string ApprovalOrgID, string ApprovalOrgNameKor, string ApprovalOrgNameEng, 
            string Location, int ApprovalType, int ApprovalSysType, int ApprovalLineID
        )
        {
            // string maxSubCodeID = "1";
            // string saveAction = "A";
            //, string maxSubCodeID
            WriteLog("saveAction: "+saveAction+", id: "+id+", ApprovalID: "+ApprovalID+", ApprovalSabun: "+ApprovalSabun+", ApprovalName: "+ApprovalName);
            WriteLog("Location: "+Location+", ApprovalType: "+ApprovalType+", ApprovalSysType: "+ApprovalSysType);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // if (saveAction.Equals("A") && String.IsNullOrEmpty(commonCode.InsertSabun)){
            // }
            // WriteLog(Dump(commonCode));
            // if (ModelState.IsValid)
            // {
            //     WriteLog("ModelState.IsValid:"+saveAction);
                var approvalID = 0;
                var today = DateTime.Now;
                ViewBag.parentReload = false;
                if (saveAction.Equals("D")) // delete
                {
                    WriteLog("delete...");
                    var orgObj = GetApprovalLineID(ApprovalLineID, true);
                    WriteLog(Dump(orgObj));
                    var newObj = orgObj.Clone();
                    newObj.ApprovalLineID = ApprovalLineID;

                    newObj.IsDelete = "Y";
                    newObj.UpdateDate = today;
                    ApprovalLineData.Update(newObj);
                    ApprovalLineData.Save();
                } else if (saveAction.Equals("U") || saveAction.Equals("A")){
                    PersonDTO my = GetMe();
                    if (saveAction.Equals("U")) // update
                    {
                        // 1. 결재경로 (Approval) 수정
                        var orgObj2 = GetApprovalID(ApprovalID, true); 
                        var newObj2 = orgObj2.Clone();
                        newObj2.Location = Location; //사업장
                        newObj2.ApprovalType = ApprovalType; //결재유형
                        newObj2.UpdateDate = today;
                        try{
                            ApprovalData.Update(newObj2);
                            ApprovalData.Save();
                        }catch (Exception e) {
                            WriteError(e.ToString());
                        }
                        // 1.1 결재경로히스토리 (ApprovalLineHistory) 등록 
                        ApprovalHistory approvalHistory = new()
                        {
                            ApprovalID = ApprovalID,//결재경로ID
                            Location = Location, //사업장
                            ApprovalType = ApprovalType,//결재유형
                            
                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = today
                        };
                        ApprovalHistoryData.Insert(approvalHistory);
                        ApprovalHistoryData.Save();
                        WriteLog("approvalHistory: "+Dump(approvalHistory));

                        // 2. 결재라인 (ApprovalLine) 수정  
                        WriteLog("[update]"+ApprovalID);
                        var orgObj = GetApprovalLineID(ApprovalLineID, true); 
                        var newObj = orgObj.Clone();
                        newObj.ApprovalSysType = ApprovalSysType; //결재자유형
                        
                        newObj.ApprovalSabun = ApprovalSabun;//결재자
                        newObj.ApprovalName = ApprovalName;
                        newObj.ApprovalOrgID = ApprovalOrgID;
                        newObj.ApprovalOrgNameKor = ApprovalOrgNameKor;
                        newObj.ApprovalOrgNameEng = ApprovalOrgNameEng;
                        newObj.UpdateDate = today;
                        try{
                            ApprovalLineData.Update(newObj);
                            ApprovalLineData.Save();
                        }catch (Exception e) {
                            WriteError(e.ToString());
                        }
                        // 2.1 결재라인히스토리 (ApprovalLineHistory) 등록 
                        ApprovalLineHistory approvalLineHistory = new()
                        {
                            ApprovalID = ApprovalID,//결재경로ID
                            ApprovalSysType = ApprovalSysType, //결재자유형
                            
                            ApprovalSabun = ApprovalSabun,//결재자
                            ApprovalName = ApprovalName,
                            ApprovalOrgID = ApprovalOrgID,
                            ApprovalOrgNameKor = ApprovalOrgNameKor,
                            ApprovalOrgNameEng = ApprovalOrgNameEng,
                            
                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = today
                        };
                        ApprovalLineHistoryData.Insert(approvalLineHistory);
                        ApprovalLineHistoryData.Save();
                        WriteLog("approvalLineHistory: "+Dump(approvalLineHistory));

                    } else if (saveAction.Equals("A")) { // insert
                        // WriteLog("[insert]ApprovalSabun: "+ApprovalSabun+", ApprovalName:"+ApprovalName+", Location:"+Location+", ApprovalType:"+ApprovalType+", ApprovalSysType:"+ApprovalSysType);
                        var approvalInfo = GetApprovalID(Location, ApprovalType);
                        if (approvalInfo != null){ // 기존 방문 회원
                           approvalID = approvalInfo.ApprovalID;
                        }else{
                            // 1. 결재경로 (Approval) 정보 입력 
                            Approval approval = new()
                            {
                                Location = Location, //사업장
                                ApprovalType = ApprovalType,//결재유형

                                InsertSabun = my.Sabun,//등록자
                                InsertName = my.Name,
                                InsertOrgID = my.OrgID,
                                InsertOrgNameKor = my.OrgNameKor,
                                InsertOrgNameEng = my.OrgNameEng,
                                InsertDate = today
                            };
                            ApprovalData.Insert(approval);
                            ApprovalData.Save();
                            // WriteLog("approval: "+Dump(approval));
                            approvalID = approval.ApprovalID;
                        }
                        // 1.1 결재경로히스토리 (ApprovalHistory) 정보 입력 
                        ApprovalHistory approvalHistory = new()
                        {
                            ApprovalID = approvalID,//결재경로ID
                            Location = Location, //사업장
                            ApprovalType = ApprovalType,//결재유형
                            
                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = today
                        };
                        ApprovalHistoryData.Insert(approvalHistory);
                        ApprovalHistoryData.Save();
                        // WriteLog("approvalHistory: "+Dump(approvalHistory));

                        // 2. 결재라인 (ApprovalLine) 정보 입력 
                        ApprovalLine approvalLine = new()
                        {
                            ApprovalID = approvalID,//결재경로ID
                            ApprovalSysType = ApprovalSysType, //결재자유형
                            
                            ApprovalSabun = ApprovalSabun,//결재자
                            ApprovalName = ApprovalName,
                            ApprovalOrgID = ApprovalOrgID,
                            ApprovalOrgNameKor = ApprovalOrgNameKor,
                            ApprovalOrgNameEng = ApprovalOrgNameEng,

                            InsertSabun = my.Sabun,//등록자
                            InsertName = my.Name,
                            InsertOrgID = my.OrgID,
                            InsertOrgNameKor = my.OrgNameKor,
                            InsertOrgNameEng = my.OrgNameEng,
                            InsertDate = today
                        };
                        ApprovalLineData.Insert(approvalLine);
                        ApprovalLineData.Save();
                        // WriteLog("approvalLine: "+Dump(approvalLine));

                        // 2.1 결재라인히스토리 (ApprovalLineHistory) 정보 입력 
                        ApprovalLineHistory approvalLineHistory = new()
                        {
                            ApprovalID = approvalID,//결재경로ID
                            ApprovalSysType = ApprovalSysType, //결재자유형
                            
                            ApprovalSabun = ApprovalSabun,//결재자
                            ApprovalName = ApprovalName,
                            ApprovalOrgID = ApprovalOrgID,
                            ApprovalOrgNameKor = ApprovalOrgNameKor,
                            ApprovalOrgNameEng = ApprovalOrgNameEng,
                            
                            UpdateSabun = my.Sabun,//등록자
                            UpdateName = my.Name,
                            UpdateOrgID = my.OrgID,
                            UpdateOrgNameKor = my.OrgNameKor,
                            UpdateOrgNameEng = my.OrgNameEng,
                            InsertUpdateDate = today
                        };
                        ApprovalLineHistoryData.Insert(approvalLineHistory);
                        ApprovalLineHistoryData.Save();
                        ViewBag.parentReload = true;
                        // WriteLog("approvalLineHistory: "+Dump(approvalLineHistory));
                    }
                // }

            } else {
                PrintModelSateError();
            }

            WriteLog(Dump(values));
            ViewBag.ApprovalID = approvalID;
            var vm = GetApprovalLineListViewModel(values2, id);
            // var vm = GetApprovalListViewModel(values, id);
            WriteLog(Dump(vm));
            return View(vm);
        }

        private Approval? GetApprovalID(string location, int approvalType)
        {
            WriteLog("location: " + location + ", approvalType: " + approvalType);
            var options = new QueryOptions<Approval>
            {
                Where = a => a.Location == location && a.ApprovalType == approvalType && a.IsDelete == "N",
            };
            if (ApprovalData != null){
                var approval = ApprovalData.GetNT(options);
                return approval;
            }
            return null;
        }
        private Approval GetApprovalID(int ApprovalID, bool isNoTracking = false){
            WriteLog("Sub Page > ApprovalID:"+ApprovalID);
            Approval? approval = null;
            if (ApprovalData != null){
                if (isNoTracking) {
                    WriteLog("No Tracking");
                    approval = ApprovalData.GetNT(ApprovalID);
                } else {
                    approval = ApprovalData.Get(ApprovalID);
                }
            }
            if (approval == null){
                approval = new Approval();
            }
            return approval;
        }
        private ApprovalLine GetApprovalLineID(int ApprovalLineID, bool isNoTracking = false){
            WriteLog("Sub Page > ApprovalLineID:"+ApprovalLineID);
            ApprovalLine? approvalLine = null;
            if (ApprovalLineData != null){
                if (isNoTracking) {
                    WriteLog("No Tracking");
                    approvalLine = ApprovalLineData.GetNT(ApprovalLineID);
                } else {
                    approvalLine = ApprovalLineData.Get(ApprovalLineID);
                }
            }
            if (approvalLine == null){
                approvalLine = new ApprovalLine();
            }
            return approvalLine;
        }

        private ApprovalListViewModel GetApprovalListViewModel(ApprovalGridData values, string ApprovalType="-1", string ApprovalID="-1")
        {
            ApprovalGridData filterhValue = (ApprovalGridData) FilterGridData(values);
            // WriteLog("Sub Page > ApprovalLineGridData:"+Dump(values)+", values.OrderNo:"+values.OrderNo);
            var options = new QueryOptions<Approval>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = "asc",
                // Where = a => a.IsDelete == "N" //&& a.OrderNo == values.OrderNo && a.IsSys == "N"
            };
            // searchlocation searchapprovaltype
            options.MultipleWhere.Add(a => a.IsDelete == "N");
            if(values.SearchLocation > -1) {
                options.MultipleWhere.Add(a => a.Location == values.SearchLocation.ToString());
            }
            if(values.SearchApprovalType > -1){
                options.MultipleWhere.Add(a => a.ApprovalType == values.SearchApprovalType);
            }
            // if(!String.IsNullOrEmpty(ApprovalID) && !ApprovalID.Equals("-1")){ // 상세
            //     // WriteLog("Sub Page > GroupNo:"+GroupNo);
            //     options = new QueryOptions<ApprovalLine>
            //     {
            //         PageNumber = values.PageNumber,
            //         PageSize = values.PageSize,
            //         OrderByDirection = "asc",
            //         // OrderByDirection = values.SortDirection,
            //         Where = a => a.ApprovalID == Int32.Parse(ApprovalID) && a.IsDelete == "N" //&& a.OrderNo >0  && a.IsSys == "N"
            //     };
            // }

            // if (values.IsSortByApprovalID)
            //     options.OrderBy = a => a.ApprovalID;

            // options.Max = a => (int?)a.SubCodeID??0;
            var vm = new ApprovalListViewModel
            {
                Approval = ApprovalData.List(options),

                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // 결재유형 ApprovalType-0: 사원추가, 1: 카드추가, 2: 반출입신청, 3: 공사신청, 4: 안전교육신청
                CodeApprovalType = GetCommonCodes((int)VisitEnum.CommonCode.ApprovalType),
                

                CurrentRoute = values,
                SearchRoute=filterhValue,
                TotalPages = values.GetTotalPages(ApprovalData.Count),
                // MaxSubCodeID = ApprovalData.Max,
            };
            // WriteLog("ApprovalData:"+Dump(ApprovalData.List(options)));
            return vm;
        }

        private ApprovalLineListViewModel GetApprovalLineListViewModel(ApprovalLineGridData values, string ApprovalID="-1") //string ApprovalType="-1", 
        {
            WriteLog("Sub Page > ApprovalLineGridData:"+Dump(values)+", ApprovalID:"+ApprovalID);
            var options = new QueryOptions<ApprovalLine>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = "asc",
                // OrderByDirection = values.SortDirection,
                Where = a => a.ApprovalID == Int32.Parse(ApprovalID) && a.IsDelete == "N"  //&& a.OrderNo == values.OrderNo && a.IsSys == "N"
            };
            if(!String.IsNullOrEmpty(ApprovalID) && !ApprovalID.Equals("-1")){ // 상세
                // WriteLog("Sub Page > GroupNo:"+GroupNo);
                options = new QueryOptions<ApprovalLine>
                {
                    PageNumber = values.PageNumber,
                    PageSize = values.PageSize,
                    OrderByDirection = "asc",
                    // OrderByDirection = values.SortDirection,
                    Where = a => a.ApprovalID == Int32.Parse(ApprovalID) && a.IsDelete == "N" //&& a.OrderNo >0  && a.IsSys == "N"
                };
            }

            // if (values.IsSortByApprovalID)
            //     options.OrderBy = a => a.ApprovalID;

            // options.Max = a => (int?)a.SubCodeID??0;
            var vm = new ApprovalLineListViewModel
            {
                ApprovalLine = ApprovalLineData.List(options),

                // 사업장 Location : 1000 서울, 2000 부천, 3000 상우, 6000 글로벌칩스
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                // 결재유형 ApprovalType-0: 사원추가, 1: 카드추가, 2: 반출입신청, 3: 공사신청, 4: 안전교육신청
                CodeApprovalType = GetCommonCodes((int)VisitEnum.CommonCode.ApprovalType),
                // 결재자유형 ApprovalSysType-0: 통문관리시스템, 1: ERP, 2: 사용자설정              
                CodeApprovalSysType = GetCommonCodes((int)VisitEnum.CommonCode.ApprovalSysType),  
                

                CurrentRoute = values,
                TotalPages = values.GetTotalPages(ApprovalLineData.Count),
                // MaxSubCodeID = ApprovalLineData.Max,
            };
            WriteLog("ApprovalLineData:"+Dump(ApprovalLineData.List(options)));
            return vm;
        }

        // ### 공통코드 관리 ###
        public IActionResult CommonCode (CommonCodeGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CommonCodeList(CommonCodeGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var vm = GetCommonCodeListViewModel(values);
            WriteLog(Dump(vm));
            return View(vm);
        }

        [HttpPost]
        public IActionResult CommonCodeList(CommonCodeGridData values, string saveAction, CommonCode commonCode)
        {
            WriteLog(saveAction);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // if (saveAction.Equals("A") && String.IsNullOrEmpty(commonCode.InsertSabun)){
            // }
            WriteLog(Dump(commonCode));
            if (ModelState.IsValid && CommonCodeData != null && CommonCodeHistoryData != null)
            {
                WriteLog("ModelState.IsValid:"+saveAction);
                if (saveAction.Equals("D")) // delete
                {
                    WriteLog("delete...");
                    commonCode.IsDelete = "Y";
                    commonCode.UpdateDate = DateTime.Now;
                    CommonCodeData.Update(commonCode);
                    // commonCodeData.Delete(commonCode);
                    CommonCodeData.Save();
                } else if (saveAction.Equals("U") || saveAction.Equals("A")){
                    PersonDTO my = GetMe();
                    if (saveAction.Equals("U")) // update
                    {
                        WriteLog("update...");                        
                        commonCode.UpdateDate = DateTime.Now;
                        commonCode.IsDelete = "N";
                        CommonCodeData.Update(commonCode);
                        CommonCodeData.Save();
                    }
                    else if (saveAction.Equals("A")) { // insert
                        WriteLog("insert...");
                        commonCode.InsertSabun = my.Sabun;
                        commonCode.InsertDate = DateTime.Now;
                        commonCode.IsDelete = "N";
                        commonCode.InsertOrgID = my.OrgID;
                        commonCode.InsertOrgNameKor = my.OrgNameKor;
                        commonCode.InsertOrgNameEng = my.OrgNameEng;
                        CommonCodeData.Insert(commonCode);
                        CommonCodeData.Save();

                        commonCode.GroupNo = commonCode.CommonCodeID;
                        CommonCodeData.Update(commonCode);
                        CommonCodeData.Save();

                    }
                    // Save a history
                    CommonCodeHistory commonCodeHistory = new()
                    {
                        CommonCodeID = commonCode.CommonCodeID,
                        GroupNo = commonCode.GroupNo,
                        CodeNameKor = commonCode.CodeNameKor,
                        CodeNameEng = commonCode.CodeNameEng,
                        SubCodeID = commonCode.SubCodeID,
                        SubCodeDesc = commonCode.SubCodeDesc,
                        OrderNo = commonCode.OrderNo,
                        IsActive = commonCode.IsActive,
                        IsSys = commonCode.IsSys,
                        Memo = commonCode.Memo,
                        UpdateSabun = my.Sabun,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now,
                    };
                    CommonCodeHistoryData.Insert(commonCodeHistory);
                    CommonCodeHistoryData.Save();
                }

            } else {
                PrintModelSateError();
            }

            WriteLog(Dump(values));
            var vm = GetCommonCodeListViewModel(values);
            WriteLog(Dump(vm));
            return View(vm);
        }

        // [HttpGet("~/{controller}/{action}/{id:int?}/{slug?}")]
        [HttpGet("{id:int?}/{slug?}")]
         public IActionResult CommonCodeDetailList(CommonCodeGridData values, string id="", string slug="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("id:"+id+", slug:"+slug);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (String.IsNullOrEmpty(id) || id.Equals("0")){
                return View("_Blank");
                // var vm = new CommonCodeListViewModel
                // {
                //     CurrentRoute = values,
                //     TotalPages = 0,
                // };                
                // return View(vm);
            } else {
                ViewBag.GroupNo = id;
                ViewBag.GroupName = slug??"";
                WriteLog(Dump(values));
                var vm = GetCommonCodeListViewModel(values, id);
                WriteLog(Dump(vm));
                return View(vm);
            }
        }

        [HttpPost("{id:int?}/{slug?}")]
        public IActionResult CommonCodeDetailList(CommonCodeGridData values, string id, string saveAction, string maxSubCodeID, CommonCode commonCode)
        {
            // string maxSubCodeID = "1";
            // string saveAction = "A";
            //, string maxSubCodeID
            WriteLog("saveAction: "+saveAction+" , maxSubCodeID: "+maxSubCodeID+", id: "+commonCode.GroupNo);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            // if (saveAction.Equals("A") && String.IsNullOrEmpty(commonCode.InsertSabun)){
            // }
            WriteLog(Dump(commonCode));
            if (ModelState.IsValid)
            {
                WriteLog("ModelState.IsValid:"+saveAction);
                if (saveAction.Equals("D")) // delete
                {
                    WriteLog("delete...");
                    var orgObj = GetCommonCode(commonCode.CommonCodeID, true);
                    WriteLog(Dump(orgObj));
                    var newObj = orgObj.Clone();
                    newObj.CommonCodeID = commonCode.CommonCodeID;

                    newObj.IsDelete = "Y";
                    newObj.UpdateDate = DateTime.Now;
                    CommonCodeData.Update(newObj);
                    // commonCodeData.Delete(newObj);
                    CommonCodeData.Save();
                } else if (saveAction.Equals("U") || saveAction.Equals("A")){
                    PersonDTO my = GetMe();
                    if (saveAction.Equals("U")) // update
                    {
                        WriteLog("[update]"+commonCode.CommonCodeID);                        
                        var orgObj = GetCommonCode(commonCode.CommonCodeID, true);
                        // WriteLog(Dump(commonCode));
                        // WriteLog("-----");
                        // WriteLog(Dump(orgObj));
                        var newObj = orgObj.Clone();
                        newObj.CommonCodeID = commonCode.CommonCodeID;
                        newObj.CodeNameKor = commonCode.CodeNameKor;
                        newObj.CodeNameEng = commonCode.CodeNameEng;
                        newObj.UpdateDate = DateTime.Now;
                        try{
                            CommonCodeData.Update(newObj);
                            CommonCodeData.Save();
                        }catch (Exception e) {
                            WriteError(e.ToString());
                        }
                        commonCode = newObj.Clone();
                    }
                    else if (saveAction.Equals("A")) { // insert
                        WriteLog("[insert]GroupNo: "+commonCode.GroupNo+", CommonCodeID:"+commonCode.CommonCodeID);
                        // var parentObj = GetCommonCode(commonCode.GroupNo, true);
                        // if(parentObj != null) {
                        //     commonCode.GroupNameKor = parentObj.GroupNameKor;
                        //     commonCode.GroupNameEng = parentObj.GroupNameEng;
                        // }
                        commonCode.InsertSabun = my.Sabun;
                        commonCode.InsertDate = DateTime.Now;
                        commonCode.InsertOrgID = my.OrgID;
                        commonCode.InsertOrgNameKor = my.OrgNameKor;
                        commonCode.InsertOrgNameEng = my.OrgNameEng;
                        commonCode.IsDelete = "N";

                        commonCode.SubCodeID = Int32.Parse(maxSubCodeID) + 1;
                        commonCode.OrderNo = Int32.Parse(maxSubCodeID) + 2;
                        // WriteLog("commonCode: "+Dump(commonCode));
                        CommonCodeData.Insert(commonCode);
                        CommonCodeData.Save();
                    }
                    // Save a history
                    CommonCodeHistory commonCodeHistory = new()
                    {
                        CommonCodeID = commonCode.CommonCodeID,
                        GroupNo = commonCode.GroupNo,
                        CodeNameKor = commonCode.CodeNameKor,
                        CodeNameEng = commonCode.CodeNameEng,
                        SubCodeID = commonCode.SubCodeID,
                        SubCodeDesc = commonCode.SubCodeDesc,
                        OrderNo = commonCode.OrderNo,
                        IsActive = commonCode.IsActive,
                        IsSys = commonCode.IsSys,
                        Memo = commonCode.Memo,
                        UpdateSabun = my.Sabun,
                        UpdateOrgID = my.OrgID,
                        UpdateOrgNameKor = my.OrgNameKor,
                        UpdateOrgNameEng = my.OrgNameEng,
                        InsertUpdateDate = DateTime.Now,
                    };
                    CommonCodeHistoryData.Insert(commonCodeHistory);
                    CommonCodeHistoryData.Save();
                }

            } else {
                PrintModelSateError();
            }

            WriteLog(Dump(values));
            ViewBag.GroupNo = id;
            var vm = GetCommonCodeListViewModel(values, id);
            // var vm = GetCommonCodeListViewModel(values);
            WriteLog(Dump(vm));
            return View(vm);
        }
        private CommonCode GetCommonCode(int CommonCodeID, bool isNoTracking = false){
            WriteLog("Sub Page > CommonCodeID:"+CommonCodeID);
            CommonCode? commonCode = null;
            if (CommonCodeData != null){
                if (isNoTracking) {
                    WriteLog("No Tracking");
                    commonCode = CommonCodeData.GetNT(CommonCodeID);
                } else {
                    commonCode = CommonCodeData.Get(CommonCodeID);
                }
            }
            if (commonCode == null){
                commonCode = new CommonCode();
            }
            return commonCode;
        }
        private CommonCodeListViewModel GetCommonCodeListViewModel(CommonCodeGridData values, string GroupNo="-1", string CommonCodeID="-1")
        {
            // WriteLog("Sub Page > CommonCodeGridData:"+Dump(values)+", values.OrderNo:"+values.OrderNo);
            var options = new QueryOptions<CommonCode>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = "asc",
                // OrderByDirection = values.SortDirection,
                Where = a => a.OrderNo == values.OrderNo && a.IsDelete == "N" && a.IsSys == "N"
            };
            if(!String.IsNullOrEmpty(GroupNo) && !GroupNo.Equals("-1")){ // 상세
                // WriteLog("Sub Page > GroupNo:"+GroupNo);
                options = new QueryOptions<CommonCode>
                {
                    PageNumber = values.PageNumber,
                    PageSize = values.PageSize,
                    OrderByDirection = "asc",
                    // OrderByDirection = values.SortDirection,
                    Where = a => a.GroupNo == Int32.Parse(GroupNo) && a.OrderNo >0 && a.IsDelete == "N"  && a.IsSys == "N"
                };
            }
            if (values.IsSortByCommonCodeID)
                options.OrderBy = a => a.CodeNameKor;
            else if (values.IsSortByCommonCodeID)
                options.OrderBy = a => a.CommonCodeID;
            else 
                options.OrderBy = a => a.CodeNameKor;

            options.Max = a => (int?)a.SubCodeID??0;
            var vm = new CommonCodeListViewModel
            {
                CommonCodes = CommonCodeData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(CommonCodeData.Count),
                MaxSubCodeID = CommonCodeData.Max,
            };
            return vm;
        }

        // ### 공휴일 관리 ###
        /// <summary>
        /// 공휴일정보 가져오기시 해당년도 수기로 추가하거나삭제된 정보는 리셋됨
        /// </summary>
        /// <param name="values"></param>
        /// <param name="searchYear"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchyear?}")]
        public async Task<IActionResult> HolidayList (HolidayGridData values, int holidayID, string searchYear="", string mode="H")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            //https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getHoliDeInfo?serviceKey=PmrvwpgHPpFANBG8iHH3GucFlL%2FMRzG44leu3jYMg4OmzPyQ1k14T%2FOYXUZ4Vb%2BJ83KYL%2FzRD87pCMPmI0TYPg%3D%3D&solYear=2023&solMonth=08
            HolidayGridData filterhValue = (HolidayGridData) FilterGridData(values);

            //https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getHoliDeInfo?serviceKey=PmrvwpgHPpFANBG8iHH3GucFlL%2FMRzG44leu3jYMg4OmzPyQ1k14T%2FOYXUZ4Vb%2BJ83KYL%2FzRD87pCMPmI0TYPg%3D%3D&solYear=2023&solMonth=08
            if(mode.Equals("H") && values.SearchYear > 0){
                StringBuilder sb = new StringBuilder();
                sb.Append("https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getHoliDeInfo?serviceKey=PmrvwpgHPpFANBG8iHH3GucFlL%2FMRzG44leu3jYMg4OmzPyQ1k14T%2FOYXUZ4Vb%2BJ83KYL%2FzRD87pCMPmI0TYPg%3D%3D&numOfRows=100&solYear=");
                sb.Append(values.SearchYear);
                // sb.Append("&solMonth=");
                // sb.Append("08");
                WriteLog("api url:" + sb.ToString());
                var rs = await GenerateHolydayByAPI(sb.ToString(), values.SearchYear);

                values.SearchYear = -1;
            } else if(mode.Equals("D") && holidayID > 0) {
                if (HolidayData != null){
                    var m = HolidayData.Get(holidayID);
                    // WriteLog("m.HolidayID:" + Dump(m));
                    if(m != null && m.HolidayID > 0){
                        HolidayData.Delete(m);
                        HolidayData.Save();
                    }
                }
            }
            var options = new QueryOptions<Holiday>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderBy = a => a.HolidayDate,
                OrderByDirection = values.SortDirection,
                Where = a => a.IsDelete == "N",
            };
            HolidayListViewModel vm =new(){
                CurrentRoute = values,
                SearchRoute=filterhValue,
            };
            if(HolidayData != null){
                vm.Holiday = HolidayData.List(options);
                vm.TotalPages = values.GetTotalPages(HolidayData.Count);
                vm.TotalCnt = HolidayData.Count;
            }
            return View(vm);
        }

        /*  <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <response>
                <header>
                    <resultCode>00</resultCode>
                    <resultMsg>NORMAL SERVICE.</resultMsg>
                </header>
                <body>
                    <items>
                        <item>
                            <dateKind>01</dateKind>
                            <dateName>광복절</dateName>
                            <isHoliday>Y</isHoliday>
                            <locdate>20230815</locdate>
                            <seq>1</seq>
                        </item>
                    </items>
                    <numOfRows>10</numOfRows>
                    <pageNo>1</pageNo>
                    <totalCount>1</totalCount>
                </body>
            </response> */
        private async Task<bool> GenerateHolydayByAPI(string url, int year){
            // https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getHoliDeInfo?serviceKey=PmrvwpgHPpFANBG8iHH3GucFlL%2FMRzG44leu3jYMg4OmzPyQ1k14T%2FOYXUZ4Vb%2BJ83KYL%2FzRD87pCMPmI0TYPg%3D%3D&solYear=2023&solMonth=08
            // http req https://www.yogihosting.com/aspnet-core-consume-api/
            // xml parse https://stackoverflow.com/questions/364253/how-to-deserialize-xml-document
            if (HolidayData != null)
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url);
                string apiResponse = await response.Content.ReadAsStringAsync();
                WriteLog("apiResponse:" + apiResponse);
                XDocument doc = XDocument.Parse(apiResponse);
                XElement? items = doc?.Element("response")?.Element("body")?.Element("items");
                List<XElement>? itemList = items?.Elements("item")?.ToList();
                if (itemList != null && itemList.Count > 0)
                {
                    PersonDTO my = GetMe();
                    //기존 데이터 지우기
                    var options = new QueryOptions<Holiday>
                    {
                        PageNumber = 0,
                        PageSize = 0,
                        Where = a => a.IsDelete == "N" && EF.Functions.Like(a.HolidayDate, year+"%"),
                    };
                    var list = HolidayData.List(options);
                    foreach(var m in list){
                        HolidayData.Delete(m);
                    }
                    foreach (var m in itemList)
                    {
                        // WriteLog("itemList:dateName>" + m.Element("dateName")?.Value);
                        // WriteLog("itemList:locdate>" + m.Element("locdate")?.Value);
                        var strName = m.Element("dateName")?.Value??"";
                        var strDate = m.Element("locdate")?.Value;
                        WriteLog("itemList:strName>" +strName);
                        WriteLog("itemList:strDate>" +strDate);
                        if(!string.IsNullOrEmpty(strDate)){
                            var d = DateTime.ParseExact(strDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                            // 1. 공휴일 (Holiday) 정보 입력 
                            Holiday holiday = new()
                            { 
                                // HolidayDate	HolidayName	IsManual
                                HolidayDate = d.ToString("yyyy-MM-dd"), //공휴일자
                                HolidayName = strName, //공휴일명
                                IsManual = "Y", //수기등록여부 
                                InsertSabun = my.Sabun,//등록자
                                InsertName = my.Name,
                                InsertOrgID = my.OrgID,
                                InsertOrgNameKor = my.OrgNameKor,
                                InsertOrgNameEng = my.OrgNameEng,
                                InsertDate = DateTime.Now,
                                IsDelete = "N", 
                            };
                            HolidayData.Insert(holiday);
                            WriteLog("holiday:" +Dump(holiday));
                            HolidayData.Save();                        
                        }
                    }
                }
            }
            return false;
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchyear?}")]
        public IActionResult HolidayReg (HolidayGridData values, string mode="")
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            return View();
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchyear?}")]
        public IActionResult HolidayReg (string mode, string HolidayDate, string HolidayName )
        {
            WriteLog("mode: "+mode+", HolidayDate: "+HolidayDate+", HolidayName: "+HolidayName);
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            if (mode.Equals("A")) // 공휴일 추가
            {
                PersonDTO my = GetMe();
                // 1. 공휴일 (Holiday) 정보 입력 
                Holiday holiday = new()
                { 
                    // HolidayDate	HolidayName	IsManual
                    HolidayDate = HolidayDate, //공휴일자
                    HolidayName = HolidayName, //공휴일명
                    IsManual = "Y", //수기등록여부 

                    InsertSabun = my.Sabun,//등록자
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now,
                    IsDelete = "N", 
                };
                HolidayData.Insert(holiday);
                HolidayData.Save();
                WriteLog("holiday: "+Dump(holiday));
            }
            return RedirectToAction("HolidayList", new { culture=GetLang() });
        }
    }
}
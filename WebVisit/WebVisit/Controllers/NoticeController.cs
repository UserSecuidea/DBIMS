using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebVisit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class NoticeController : BaseController
    {
        private Repository<Notice>? NoticeData { get; set; }

        public NoticeController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer)
        { 
        }
        
        protected override void Init(){
            if (DbSVISIT != null) {
                NoticeData ??= new Repository<Notice>(DbSVISIT);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("List", new { culture = GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchfield?}/{searchkeyword?}")]
        public IActionResult List (NoticeGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            var accessible = IsMaster() || IsGeneralManager();
            if (accessible == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            WriteLog("NoticeGridData:" + Dump(values));
            var options = new QueryOptions<Notice>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                Where = a=> a.IsDelete == "N",
                OrderByDirection = "desc",
                OrderBy = a => a.NoticeID,
            };
            if (!String.IsNullOrEmpty(values.SearchField) && !String.IsNullOrEmpty(values.SearchKeyword)){
                if (values.SearchField.Equals("Title")) 
                    options.Where = a => a.IsDelete == "N" && EF.Functions.Like(a.Title, "%"+values.SearchKeyword+"%");
                else if (values.SearchField.Equals("Contents")) 
                    options.Where = a => a.IsDelete == "N" && EF.Functions.Like(a.Contents, "%"+values.SearchKeyword+"%");
            }

            NoticeListViewModel vm = new()
            {
                Notices = NoticeData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(NoticeData.Count),
                TotalCount = NoticeData.Count,
                IsMaster = my.LevelCodeID == (int)VisitEnum.LevelType.Master,
            };
            // WriteLog("notice list:" + Dump(vm));
            return View(vm);
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchfield?}/{searchkeyword?}")]
        public IActionResult Detail (NoticeGridData values, Notice? notice)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }

            if (notice == null ){
                return RedirectToAction("List", new { culture = GetLang() });
            }
            // var base64 = Convert.ToBase64String(notice.FileDataHash2);
            // ViewBag.img1= String.Format("data:image/gif;base64,{0}", base64);
            // WriteLog("NoticeGridData:" + Dump(values));
            // WriteLog("Notice 1:" + Dump(notice));
            PersonDTO my = GetMe();
            notice = NoticeData?.Get(notice.NoticeID);
            if (!string.IsNullOrEmpty(notice?.FileData1)) {
                WriteLog("notice.FileData1:" + notice.FileData1);
                FileDTO? ff = _Dump<FileDTO>(notice.FileData1);
                ViewBag.file1Name = ff?.FileName;
            }
            if (!string.IsNullOrEmpty(notice?.FileData2)) {
                FileDTO? ff = _Dump<FileDTO>(notice.FileData2);
                ViewBag.file2Name = ff?.FileName;
            }
            NoticeViewModel vm = new()
            {
                Notice = notice??new Notice(),
                CurrentRoute = values,
                IsMaster = my.LevelCodeID == (int)VisitEnum.LevelType.Master,
            };
            return View(vm);
        }

        /// <summary>
        /// var base64 = Convert.ToBase64String(notice.FileDataHash2);
        /// ViewBag.img1= String.Format("data:image/gif;base64,{0}", base64);
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <param name="NoticeID"></param>
        /// <param name="InsertName"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="FormFile"></param>
        /// <param name="Title"></param>
        /// <param name="Contents"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchfield?}/{searchkeyword?}")]
        public async Task<IActionResult> Detail (NoticeGridData values, string mode, string NoticeID, string InsertName, 
            string StartDate, string EndDate, IFormFile? FormFile1, IFormFile? FormFile2, string Title, string Contents)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("HttpPost NoticeID:" + NoticeID);
            WriteLog("mode:"+mode+", InsertName:"+ InsertName+", StartDate:"+ StartDate);
            PersonDTO my = GetMe();
            if (my.LevelCodeID != (int)VisitEnum.LevelType.Master) 
            {
                WriteLog("Do not have a write/eidit permmision.");
                return RedirectToAction("List", new { culture = GetLang() });
            }
            if (string.IsNullOrEmpty(NoticeID)) {
                return RedirectToAction("List", new { culture = GetLang() });
            }
            Notice? notice = NoticeData?.Get(int.Parse(NoticeID));
            if(notice != null) {
                notice.UpdateDate = DateTime.Now;
                if (mode.Equals("D")) {
                    notice.IsDelete = "Y";
                } else {
                    notice.StartDate = DateTime.Parse(StartDate+" 00:00:00");
                    notice.EndDate = DateTime.Parse(EndDate+" 23:59:59");
                    notice.EndDate.AddHours(23);
                    notice.EndDate.AddMinutes(59);
                    notice.EndDate.AddSeconds(59);
                    notice.Title = Title;
                    notice.Contents = Contents;

                    if(notice!= null){
                        if (FormFile1 != null) {
                            FileData fileData = await GetFileData(FormFile1);
                            notice.FileDataHash1 = fileData.Hash;
                            if (!String.IsNullOrEmpty(fileData.Meta)) {
                                notice.FileData1 = fileData.Meta;
                            }
                        }
                        if (FormFile2 != null) {
                            FileData fileData = await GetFileData(FormFile2);
                            notice.FileDataHash2 = fileData.Hash;
                            if (!String.IsNullOrEmpty(fileData.Meta)) {
                                notice.FileData2 = fileData.Meta;
                            }
                        }
                    }
                }
                NoticeData?.Update(notice);
                NoticeData?.Save();
            }
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                if (mode.Equals("D"))
                {
                    return RedirectToAction("List", dict);
                } else {
                    dict.Add("NoticeID", NoticeID);
                    return RedirectToAction("Detail", dict);
                }
            }
            return RedirectToAction("Detail", new { NoticeID, culture = GetLang() });
        }

        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchfield?}/{searchkeyword?}")]
        public IActionResult Reg (NoticeGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            Notice notice = new Notice();
            // List<CommonCode> commonCodes = GetCommonCodes((int)VisitEnum.CommonCode.ApprovalType);
            // WriteLog("commonCodes: >> " + Dump(commonCodes));
            WriteLog("NoticeGridData:" + Dump(values));

            return View(notice);
        }

        // public IActionResult Reg (NoticeGridData values, Notice notice)
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchfield?}/{searchkeyword?}")]
        public async Task<IActionResult> Reg (NoticeGridData values, Notice notice)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            WriteLog("Post Reg:");
            WriteLog("NoticeGridData:" + Dump(values));
            PersonDTO my = GetMe();
            if (my.LevelCodeID != (int)VisitEnum.LevelType.Master) 
            {
                WriteLog("Do not have a write/eidit permmision.");
                return RedirectToAction("List", new { culture = GetLang() });
            }
            // Handle Files
            if(notice!= null && notice.FormFile != null){
                int i = 0;
                foreach (var formFile in notice.FormFile)
                {
                    FileData fileData = await GetFileData(formFile);
                    if (i ==0) {
                        notice.FileDataHash1 = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta)) {
                            notice.FileData1 = fileData.Meta;
                        }
                        // var base64 = Convert.ToBase64String(notice.FileDataHash1);
                        // ViewBag.img1= String.Format("data:image/gif;base64,{0}", base64);
                    } else {
                        notice.FileDataHash2 = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta)) {
                            notice.FileData2 = fileData.Meta;
                        }
                    }
                    i++;
                }
            }
            // Notice Data
            if (notice != null && !String.IsNullOrEmpty(notice.Title) && !String.IsNullOrEmpty(notice.Contents)) {
                try {
                    notice.IsPublish = "Y";
                    notice.InsertSabun = my.Sabun;
                    notice.InsertName = my.Name;
                    notice.InsertOrgID = my.OrgID;
                    notice.InsertOrgNameKor = my.OrgNameKor;
                    notice.InsertOrgNameEng = my.OrgNameEng;
                    notice.InsertDate = DateTime.Now;
                    // notice.UpdateDate = DateTime.Now;
                    notice.IsDelete = "N";

                    notice.EndDate=notice.EndDate.AddHours(23);
                    notice.EndDate=notice.EndDate.AddMinutes(59);
                    notice.EndDate=notice.EndDate.AddSeconds(59);
                    WriteLog("insert notice:"+ Dump(notice));
                    NoticeData.Insert(notice);
                    NoticeData.Save();
                }catch(Exception e) {
                    WriteError(e.ToString());
                }
            }
            // Route
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                return RedirectToAction("List", dict);
            }
            return RedirectToAction("List", new { culture = GetLang() });
        }
        
        [HttpPost]
        public FileResult? Download (string NoticeID, string FileIdx){
            if (IsAccessAPI() == false) {
                return null;
            }
            // WriteLog("start download> NoticeID:"+NoticeID+" , FileIdx:" + FileIdx);
            if (!String.IsNullOrEmpty(NoticeID) && !String.IsNullOrEmpty(FileIdx)) 
            {
                int id = int.Parse(NoticeID);
                int fileIdx = int.Parse(FileIdx);
                Notice n = NoticeData.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (n!= null)
                {
                    if (fileIdx == 0) {
                        if (n.FileDataHash1 != null && n.FileData1 != null) {
                            fileData = n.FileDataHash1;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(n.FileData1);
                        } else {
                            return null;
                        }
                    } else { // 1
                        if (n.FileDataHash2 != null && n.FileData2 != null) {
                            fileData = n.FileDataHash2;
                            meta =_Dump<FileDTO>(n.FileData2);
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
    } // end class
} // end namespace

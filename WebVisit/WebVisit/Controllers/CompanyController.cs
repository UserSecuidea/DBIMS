using System.Text.Json;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using ClosedXML.Excel;
using WebVisit.Models;
using WebVisit.Models.DomainModels.Passport;
using System.Globalization;
using System.Text;
using System.Runtime.CompilerServices;

namespace WebVisit.Controllers
{
    [Route("[controller]/[action]")]
    public class CompanyController : BaseController
    {
        private Repository<MealAccess>? MealAccessData { get; set; }

        public CompanyController(ILogger<PersonController> logger, IConfiguration configuration, IStringLocalizer<PersonController> localizer):base(logger, configuration, localizer)
        {
        }

        protected override void Init(){
            if (DbSVISIT != null) {
                CompanyData ??= new Repository<Company>(DbSVISIT);
                MealAccessData ??= new Repository<MealAccess>(DbSVISIT);
                // PersonData ??= new Repository<Person>(db);
            }
        }

        [Route("~/{controller}")]
        [Route("")]
        public RedirectToActionResult Index() {
            WriteLog("Index");
            return RedirectToAction("List");
        }
        // public IActionResult Index() => RedirectToAction("List");

        /// <summary>
        /// 관리자: 조회/승인변경
        /// 임직원: 조회
        /// 보안실: 조회
        /// 비상주업체: 조회
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public IActionResult List (CompanyGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            ViewBag.ExcelDownloadable = IsEmployee(my.LevelCodeID); // 엑셀 다운로드 권한
            ViewBag.IsMaster = IsMaster();
            ViewBag.IsManager = IsMaster() || IsGeneralManager();
            ViewBag.Registable = IsMaster() || IsGeneralManager();
            // WriteLog("CompanyGridData:"+Dump(values));
            // WriteLog("bizRegNo Uinique:" +IsUniqueBizRegNo("002-31-23123"));
            var options = new QueryOptions<Company>
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                OrderByDirection = values.SortDirection,
            };
            if (values.IsSortByCompanyName)
                options.OrderBy = a => a.CompanyName;
            else
                options.OrderBy = a => a.CompanyID;
            options.MultipleWhere.Add(a => a.IsDelete == "N");
            if(IsPartnerNonresidentManager() && my.CompanyID >0){
                options.MultipleWhere.Add(a => a.CompanyID == my.CompanyID);
            }
            if (!string.IsNullOrEmpty(values.SearchCompanyName)) {
                options.MultipleWhere.Add(a => EF.Functions.Like(a.CompanyName, "%"+values.SearchCompanyName+"%"));
            }
            if (!string.IsNullOrEmpty(values.SearchCompanyStatus)) {
                var s = int.Parse(values.SearchCompanyStatus);
                if (s >= 0) {
                    options.MultipleWhere.Add(a => a.CompanyStatus == s);
                }
            }
            var vm = new CompanyListViewModel
            {
                Companies = CompanyData.List(options),
                CodeCompanyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CompanyStatus),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(CompanyData.Count),
                TotalCnt = CompanyData.Count,
            };
            return View(vm);
        }

        /// <summary>
        /// 등록: 마스터, 일반관리자, 임직원
        /// 조회는 모두 가능
        /// </summary>
        /// <param name="values"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyStatus"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public IActionResult List(CompanyGridData values, int CompanyID, int CompanyStatus, string mode = "")
        {
            if(IsAccess(false) == false){
                return View("_Inaccessible");
            }
            if(mode == "ECompanyStatus" && CompanyData != null){
                var orgObj = GetCompany(CompanyID, true);
                var newObj = orgObj.Clone();
                newObj.CompanyStatus = CompanyStatus;
                newObj.UpdateDate = DateTime.Now;
                CompanyData.Update(newObj);
                CompanyData.Save();
            }

            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                return RedirectToAction("List", dict);
            }
            return RedirectToAction("List", new { culture = GetLang() });
        }

        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public IActionResult? ExcelDownload(CompanyGridData values, string ExTitle="DBHiTek") {
            if(IsAccessAPI() == false){
                return default;
            }

            PersonDTO my = GetMe();
            DataTable dt = new(ExTitle);
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn(@Localizer["Company Name"]),
                                        new DataColumn(@Localizer["Classify"]),
                                        new DataColumn(@Localizer["Contact Name"]),
                                        new DataColumn(@Localizer["Contact Tel"]),
                                        new DataColumn(@Localizer["Company Address"]),
                                        new DataColumn(@Localizer["Approval Classify"]), });
            var options = new QueryOptions<Company>
            {
                PageNumber = 0,
                PageSize = 0,
                OrderByDirection = values.SortDirection,
            };
            if(IsPartnerNonresidentManager() && my.CompanyID >0){
                options.MultipleWhere.Add(a => a.CompanyID == my.CompanyID);
            }
            if (values.IsSortByCompanyName)
                options.OrderBy = a => a.CompanyName;
            else
                options.OrderBy = a => a.CompanyID;
            options.MultipleWhere.Add(a => a.IsDelete == "N");
            if (!string.IsNullOrEmpty(values.SearchCompanyName)) {
                options.MultipleWhere.Add(a => EF.Functions.Like(a.CompanyName, "%"+values.SearchCompanyName+"%"));
            }
            if (!string.IsNullOrEmpty(values.SearchCompanyStatus)) {
                var s = int.Parse(values.SearchCompanyStatus);
                if (s >= 0) {
                    options.MultipleWhere.Add(a => a.CompanyStatus == s);
                }
            }

            var list = CompanyData.List(options);
            if(list != null && list.Count()>0){
                List<CommonCode> CodeCompanyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CompanyStatus);
                // 회사명	구분	담당자명	담당자연락처	사업장주소	승인구분
                foreach(var m in list){
                    var CompanyStatus = "";
                    foreach(var m2 in CodeCompanyStatus) {
                        WriteLog("CompanyStatus:"+m2.SubCodeID +"=="+ m.CompanyStatus);
                        if (m2.SubCodeID == m.CompanyStatus) {
                            // WriteLog("CompanyStatus:"+m2.SubCodeID +"=="+ m.CompanyStatus);
                            if(CultureInfo.CurrentCulture.ToString().Equals("en")){
                                CompanyStatus = @m2.CodeNameEng;
                            }else {
                                CompanyStatus = @m2.CodeNameKor;
                            }
                            break;
                        }
                    }
                    dt.Rows.Add(m.CompanyName, "비상주협력사", m.CeoName, m.Tel, m.Address, CompanyStatus);
                }
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExTitle + ".xlsx");
        }
        // public ViewResult List (int PageNumber, int PageSize, string SortField, string SortDirection)
        // {
        //     // 1, 4, companyid, asc
        //     Console.WriteLine(TAG+">List, "+PageNumber+", "+PageSize+", "+SortField+", "+SortDirection);
        //     var vm = new CompanyListViewModel();
        //     return View(vm);
        // }

        /// <summary>
        /// 관리자 : 사업자번호/비밀번호 이외의 모든 정보 변경 가능 – 비밀번호는 초기화만 가능
        ///     - 일반 관리자는 자신의 사업장만 처리가능 ==> Company 는 사업장이 없으므로 이 기능을 구현 불가.
        /// 임직원 : 모든 정보 변경 불가
        /// 비상주업체 : 주소/담당자명/이메일/전화번호/연락처/비밀번호 변경가능
        /// </summary>
        /// <param name="values"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        // 1/10/companyname/desc/-1/aa/?culture=ko&CompanyID=1
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public IActionResult Detail(CompanyGridData values, Company? company) 
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            ModelState.Clear();
            PersonDTO my = GetMe();
            ViewBag.ErrorMsg = GetErrorMessage();
            // WriteLog("values:" + Dump(values));
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                ViewBag.prevDic = dict;
            }
            ViewBag.IsMaster = IsMaster();
            if (company != null && company.CompanyID > 0){
                company = CompanyData.Get(company.CompanyID);
                ViewBag.IsOwner = IsPartnerNonresidentManager() && my.CompanyID == company?.CompanyID;
                ViewBag.IsEditable = IsMaster(); // 2023.08.26 일반 관리자는 제외 || (my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager); //  && my.CompanyID == company.CompanyID
                // WriteLog("Company:" + Dump(company));
                // BizFileDataHash BizFileData
                if (!string.IsNullOrEmpty(company.BizFileData)) {
                    // WriteLog("company.BizFileData:" + company.BizFileData);
                    FileDTO? ff = _Dump<FileDTO>(company.BizFileData);
                    ViewBag.file1Name = ff?.FileName;
                }
                Person? p = PersonData.Get(new QueryOptions<Person>{
                    Where = a => a.CompanyID == company.CompanyID  
                })??new Person();
                CompanyDetailViewModel vm = new(){
                    CodeCompanyStatus = GetCommonCodes((int)VisitEnum.CommonCode.CompanyStatus),
                    Company = company,
                    Person = p,
                };
                return View(vm);
            } else {
                CompanyDetailViewModel vm = new();
                return View(vm);
            }
        }

        /// <summary>
        /// 관리자 : 사업자번호/비밀번호 이외의 모든 정보 변경 가능 – 비밀번호는 초기화만 가능
        ///     -일반관리자는 자신의 사업장만 처리가능
        /// 임직원 : 모든 정보 변경 불가
        /// 비상주업체 : 주소/담당자명/이메일/전화번호/연락처/비밀번호 변경가능
        /// </summary>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <param name="InitPassword"></param>
        /// <param name="CompanyID"></param>
        /// <param name="CompanyType"></param>
        /// <param name="CompanyStatus"></param>
        /// <param name="CompanyName"></param>
        /// <param name="CeoName"></param>
        /// <param name="BizRegNo"></param>
        /// <param name="Address"></param>
        /// <param name="ContactPersonName"></param>
        /// <param name="Email"></param>
        /// <param name="Tel"></param>
        /// <param name="ContactPersonTel"></param>
        /// <param name="Password"></param>
        /// <param name="FormFile1"></param>
        /// <returns></returns>
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public async Task<IActionResult> Detail(CompanyGridData values, string mode, string InitPassword, 
            int CompanyID, int CompanyType, int CompanyStatus, string CompanyName, string CeoName, 
            string BizRegNo, string Address, string ContactPersonName, string Email, string Tel, 
            string ContactPersonTel, string Password, IFormFile? FormFile1){
            if (IsAccess() == false){
                return View("_Inaccessible");
            }
            if(CompanyData == null || PersonData == null){
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            Company? company = CompanyData.Get(CompanyID);
            bool accessible = IsMaster() || (IsPartnerNonresidentManager() && my.CompanyID == company?.CompanyID);
            if(accessible == false){
                return View("_Inaccessible");
            }
            Person? p = PersonData.Get(new QueryOptions<Person>{
              Where = a => a.CompanyID == CompanyID && a.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager
            });
            if (InitPassword.Equals("Y") && company != null) {
                p.Password=company.BizRegNo.SHA256Encrypt();
                PersonData.Update(p);
                PersonData.Save();
                WriteLog("change password:" + Dump(p));            
            } else {
                // WriteLog("BizRegNo:" + company.BizRegNo + "=="+BizRegNo);
                if (company.BizRegNo.Equals(BizRegNo) || IsUniqueBizRegNo(BizRegNo)){
                    company.CompanyType = CompanyType;
                    company.CompanyStatus = CompanyStatus;
                    company.CompanyName = CompanyName ?? company.CompanyName;
                    company.CeoName = CeoName ?? company.CeoName;
                    company.BizRegNo = BizRegNo ?? company.BizRegNo;
                    company.Address = Address ?? company.Address;
                    company.ContactPersonName = ContactPersonName ?? company.ContactPersonName;
                    company.Email = Email ?? company.Email;
                    company.Tel = Tel ?? company.Tel;
                    company.ContactPersonTel = ContactPersonTel ?? company.ContactPersonTel;
                    // company.Password = Password;
                    company.UpdateDate = DateTime.Now;
                    WriteLog("Company FormFile1:" + FormFile1);
                    if(company!= null && FormFile1 != null){
                        FileData fileData = await GetFileData(FormFile1);
                        company.BizFileDataHash = fileData.Hash;
                        if (!String.IsNullOrEmpty(fileData.Meta)) {
                            company.BizFileData = fileData.Meta;
                        }
                        WriteLog("company.BizFileData:" + Dump(company.BizFileData));
                    }
                    CompanyData.Update(company);
                    CompanyData.Save();
                    // WriteLog("Company:" + Dump(company));

                    p.Sabun = company.BizRegNo;
                    p.Name = company.CompanyName + " 관리자";
                    if (!string.IsNullOrEmpty(Password)){ // null 이면 변경 안함.
                        var newPwd = Password.SHA256Encrypt();
                        if (!p.Password.Equals(newPwd)) { // 이전 비번과 다르면
                            p.Password=Password.SHA256Encrypt();
                        }
                    }
                    p.Email = company.Email;
                    p.Tel = company.Tel;
                    p.UpdateDate = DateTime.Now;
                    PersonData.Update(p);
                    PersonData.Save();
                } else {
                    SetErrorMessage(BizRegNo+Localizer["There is already a registered business registration number"]);
                    // SetErrorMessage(BizRegNo+" 이미 등록된 사업자등록번호가 있습니다");
                }
            }
            WriteLog("Person:" + Dump(p));
            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                dict.Add("companyID", CompanyID.ToString());
                return RedirectToAction("Detail", dict);
            }
            return RedirectToAction("Detail", new { CompanyID, culture = GetLang() });
        }

        /// <summary>
        /// 신규 등록은 마스터와 임직원만 등록 가능.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpGet("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public IActionResult Reg(CompanyGridData values)
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            Company company = new();
            var obj = GetErrorObject<Company>();
            if (obj != null) {
                company = obj;
            }
            WriteLog("ErrorMsg:" + GetErrorMessage()+ "obj:"+Dump(obj)+" company:"+Dump(company));
            ViewBag.ErrorMsg = GetErrorMessage();

            PersonDTO my = GetMe();
            CompanyRegViewModel vm = new();
            var isEditable = IsEmployee(my.LevelCodeID);
            if (isEditable) {
                // Company company = new Company();
                if (values != null && values.ToDictionary() != null){
                    var dict = values.ToDictionary();
                    dict.Add("culture", GetLang());
                    ViewBag.prevDic = dict;
                }
                List<ApprovalPerson> approvalPeople = GetApprovalPerson();
                WriteLog("approvalPeople:" + Dump(approvalPeople));
                vm =new(){
                    company=company,
                    ApprovalPeople = approvalPeople,
                };
                return View(vm);
            } else {
                if (values != null && values.ToDictionary() != null){
                    var dict = values.ToDictionary();
                    dict.Add("culture", GetLang());
                    return RedirectToAction("List", dict);
                }
                return RedirectToAction("List", new { culture = GetLang() });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">사번</param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        ///  int companyID, int CompanyType, int CompanyStatus, string CompanyName, string CeoName, string BizRegNo, string Address, string ContactPersonName, string Email, string Tel, string ContactPersonTel, string Password
        [HttpPost("{pagenumber?}/{pagesize?}/{sortfield?}/{sortdirection?}/{searchcompanystatus?}/{searchcompanyname?}")]
        public async Task<IActionResult> Reg(CompanyGridData values, string mode, Company company)
        {
            // mode CompanyType CompanyStatus CompanyName CeoName BizRegNo  Address ContactPersonName Email Tel ContactPersonTel
            WriteLog("mode: "+mode+" company:"+Dump(company));
            
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            PersonDTO my = GetMe();
            // var isEditable = IsMaster() || my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager;
            var isEditable = IsEmployee();
            if (!isEditable)
            {
                if (values != null && values.ToDictionary() != null){
                    var dict = values.ToDictionary();
                    dict.Add("culture", GetLang());
                    return RedirectToAction("List", dict);
                }
                return RedirectToAction("List", new { culture = GetLang() });
            }            
            if (mode.Equals("A") && IsUniqueBizRegNo(company.BizRegNo)) // 업체 추가, 사업자 등록 번호 중복체크
            {
                company.InsertSabun = my.Sabun;
                company.InsertName = my.Name;
                company.InsertOrgID = my.OrgID;
                company.InsertOrgNameKor = my.OrgNameKor;
                company.InsertOrgNameEng = my.OrgNameEng;
                company.InsertDate = DateTime.Now;
                company.IsDelete = "N";
                if(company.FormFile != null && company.FormFile.Count > 0){
                    FileData fileData = await GetFileData(company.FormFile[0]);
                    company.BizFileDataHash = fileData.Hash;
                    if (!String.IsNullOrEmpty(fileData.Meta)) {
                        company.BizFileData = fileData.Meta;
                    }
                }
                // WriteLog("company.FormFile:" + Dump(company));
                // WriteLog("company.FormFile:" + Dump(company.FormFile));
                CompanyData.Insert(company);
                CompanyData.Save();
                WriteLog("company: "+Dump(company));
                
                //PORTE ID 가 있는 사용자만 이용 가능
                if(!string.IsNullOrEmpty(my.PorteID)){
                    TempData["DATA.CompanyID"] = company.CompanyID;
                }
                
                Person p = new()
                {
                    Sabun = company.BizRegNo,
                    Name = company.CompanyName + " 관리자",
                    CompanyID = company.CompanyID,
                    PersonTypeID = 2, // 0:임직원, 1:상주직원, 2:비상주관리자, 3:비상주직원, 4:방문객
                    LevelCodeID = 9, // 레벨: 비상주 업체
                    Password=company.BizRegNo.SHA256Encrypt(),
                    Email = company.Email,
                    Tel = company.Tel,
                    IsForeigner = 0, // 외국인 아님
                    IsAllowSMS = "Y",
                    InsertSabun = my.Sabun,
                    InsertName = my.Name,
                    InsertOrgID = my.OrgID,
                    InsertOrgNameKor = my.OrgNameKor,
                    InsertOrgNameEng = my.OrgNameEng,
                    InsertDate = DateTime.Now,
                    UpdateIP = GetUserIP(),
                    IsDelete = "N"
                };
                PersonData.Insert(p);
                PersonData.Save();
                WriteLog("person: "+Dump(p));
                
                // 1. 식수권한 (MealAccess) 정보 입력 
                MealAccess mealAccess = new()
                {
                    Sabun = company.BizRegNo, //사번
                    // Name = company.CompanyName + " 관리자", //이름 
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

                // PROCI_PERSON_REG: PersonTypeID 2: 비상주 관리자, 3: 비상주 사원 CompanyID = company.CompanyID
                if (DbPASSPORT != null){
                    // var companyName = GetCompanyName(p.CompanyID??0);
                    StringBuilder sb = new();
                    sb.Append("PROCI_PERSON_REG @PageType='I', @Sabun='");
                    sb.Append(p.Sabun);
                    sb.Append("' , @Name='");
                    sb.Append(p.Name);
                    sb.Append("', @GradeName='"); 
                    sb.Append(p.GradeName); // 없음
                    sb.Append("', @CompanyName='"); 
                    sb.Append(company.CompanyName);
                    sb.Append("', @CompanyID='"); 
                    sb.Append(company.CompanyID);
                    sb.Append("', @OrgCode='"); 
                    sb.Append(p.OrgID); // 없음
                    sb.Append("', @Email='"); 
                    sb.Append(p.Email);
                    sb.Append("', @Tel='"); 
                    sb.Append(p.Tel);
                    sb.Append("', @Mobile='"); 
                    sb.Append(p.Mobile); // 없음
                    sb.Append("', @PersonTypeID='2', @PersonStatusID='0', @PhotFile=NULL, @PersonUser3=NULL, @PersonUser4=NULL, @PersonUser5=NULL, @ValidDate=NULL, @UpdateIP='");
                    sb.Append(p.UpdateIP);
                    sb.Append("'");
                    WriteLog("sp query:"+sb.ToString());
                    var fs1 = FormattableStringFactory.Create(sb.ToString());
                    try{
                        await DbPASSPORT.Database.ExecuteSqlAsync(fs1);
                    }catch(Exception e){
                        WriteError(e.ToString());
                    }
                }                
            } else {
                WriteLog("duplicate BizRegNo:"+company.BizRegNo);
                ModelState.Clear();
                SetErrorMessage(company.BizRegNo+Localizer["There is already a registered business registration number"], company);
                // SetErrorMessage(company.BizRegNo+" 이미 등록된 사업자등록번호가 있습니다", company);
                return RedirectToAction("Reg", new { culture = GetLang() });
                // AlertErrorMsg("이미 등록된 사업자등록번호가 있습니다.");
                // return View("_White");
            }

            if (values != null && values.ToDictionary() != null){
                var dict = values.ToDictionary();
                dict.Add("culture", GetLang());
                return RedirectToAction("List", dict);
            }
            return RedirectToAction("List", new { culture = GetLang() });

            // return RedirectToAction("List", "Person", new { culture=GetLang() });
            // return View();
        }
        

        private bool IsUniqueBizRegNo(string bizRegNo){
            var options = new QueryOptions<Company>
            {
                PageNumber = 1,
                PageSize = 1,
                Where = a=>a.BizRegNo == bizRegNo,
            };
            _ = (List<Company>) CompanyData.List(options);
            if (CompanyData.Count > 0){
                return false;
            }
            return true;
        }
        private Person GetCompanyPerson(int companyID=-1, bool isNoTracking = false)
        {
            Person? person = null;
            if(companyID > 0) {
                var options = new QueryOptions<Person>
                {
                    // PageNumber = 0,
                    // PageSize = 0,
                    // OrderByDirection = "asc",
                    Where = a => a.CompanyID == companyID && a.LevelCodeID == 9 && a.IsDelete == "N",
                };
                    WriteLog("options: "+options);
                if(isNoTracking) {
                    // person = personData.GetNT(options);
                } else {
                    person = PersonData?.Get(options);
                }
                // WriteLog("person: "+Dump(person));
            }
            return person;
        }

        [HttpPost]
        public FileResult? Download(string companyID, string FileIdx){
            if (IsAccess() == false) {
                return null;
            }
            WriteLog("start download> companyID:"+companyID+" , FileIdx:" + FileIdx);
            if (!string.IsNullOrEmpty(companyID) && !string.IsNullOrEmpty(FileIdx)) 
            {
                int id = int.Parse(companyID);
                int fileIdx = int.Parse(FileIdx);
                Company? company = CompanyData?.Get(id);
                byte[]? fileData = null;
                FileDTO? meta = null;
                // WriteLog("notice:" + Dump(n));
                if (company!= null)
                { 
                    WriteLog("company:"+Dump(company));
                    if (fileIdx == 0) {
                        if (company.BizFileDataHash != null && company.BizFileData != null) {
                            fileData = company.BizFileDataHash;
                            // ContentDisposition ContentType Headers.Content-Disposition Headers.Content-Type Length FileName
                            meta =_Dump<FileDTO>(company.BizFileData);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (IsAccess() == false) {
                return View("_Inaccessible");
            }
            return View("Error!");
        }

        /// <summary>
        /// 테스트 검색어: 알비엠 이상준
        /// 외부업체 인수자 검색
        /// </summary>
        /// <param name="isForeigner">0:내국인, 1:외국인</param>
        /// <param name="companyName"></param>
        /// <param name="companyContactName"></param>
        /// <returns></returns>
        public JsonResult? GetExternalCompany(string isForeigner, string companyName, string companyContactName) {
            
            if (IsAccessAPI() == false) {
                return default;
            }
            WriteLog("isForeigner:"+isForeigner+", companyName:" + companyName + ", companyContactName:" + companyContactName);
            // Name, Type, CompanyName, Position, Mobile, Email, BizRegNo, CompanyTel, chargerName
            List<ExternalCompanyRecord> list = new();
            if(DbSVISIT != null) {
                try{
                    IQueryable<Person> outer = DbSVISIT.Person.Where(x => x.IsDelete == "N");
                    if (!string.IsNullOrEmpty(isForeigner)) {
                        outer = outer.Where(x => x.IsForeigner == int.Parse(isForeigner));
                    }
                    if (!string.IsNullOrEmpty(companyContactName)){
                        outer = outer.Where(x => x.Name != null && EF.Functions.Like(x.Name, "%" + companyContactName + "%"));
                    }
                    IQueryable<Company> inner = DbSVISIT.Company.Where(x => x.IsDelete == "N");
                    if (!string.IsNullOrEmpty(companyName)){
                        inner = inner.Where(x => x.CompanyName != null && EF.Functions.Like(x.CompanyName, "%" + companyName + "%"));
                    }
                    var query = outer
                        .Join(
                            inner,
                            a => a.CompanyID, 
                            b => b.CompanyID,
                            (a, b) => new {a, b});
                    var l = query.ToList();
                    foreach(var m in l){
                        list.Add(new ExternalCompanyRecord(m.a.Sabun??"", m.a.Name??"", "업체", m.b.CompanyName??"", m.a.GradeName??"", m.a.Mobile??"", m.a.Email??"", m.b.BizRegNo??"", m.b.Tel??"", m.b.ContactPersonName??""));

                    }
                } catch(Exception e){
                    WriteError(e.ToString());
                }
            }
            /*
            // Verndor 검색
            // PersonData ??= new Repository<Person>(DbSVISIT);
            if (DbPASSPORT != null) {
                // "구분, 직급, 휴대전화" 정보 테이블에 없음.
                // PASSPORT.VW_TSR_VENDOR: [VENDOR_ID],[VENDOR_NO], 업체명:[VENDOR_NAME], 사업자등록번호:[BUSINESS_NO], 법인등록번호:[CORPORATION_NO],
                // 업태:[BUSINESS_TYPE], 업종:[BUSINESS_CLASS], 국가:[COUNTRY], 퇴출업체 여부:[VENDOR_BLOCK] - 기본 NULL, [REASON_DELETE], [UPDATEDATE],[INSERTDATE]
                // 대표명:[REPRESENT_NAME], 세금계산서담당자 이름:[ENTRY_NAME],[ENTRY_EMAIL],[ENTRY_TEL], 구매담당자 이름:[CHARGER_NAME],
                IQueryable<VwTsrVendor> vendor = DbPASSPORT.VwTsrVendors;
                if(!string.IsNullOrEmpty(companyName)) {
                    WriteLog("companyName: " + companyName);
                    vendor = vendor.Where(x => x.VendorName != null && EF.Functions.Like(x.VendorName, "%" + companyName + "%"));
                }
                if(!string.IsNullOrEmpty(companyContactName)) {
                    vendor = vendor.Where(x =>x.ChargerName == companyContactName);
                    // vendor = vendor.Where(x => x.VendorName != null && EF.Functions.Like(x.VendorName, "%" + companyName + "%"));
                }
                List<VwTsrVendor> l = vendor.ToList();
                // 성명	구분	회사명	직급	휴대전화	이메일 사업자번호 전화번호 담당자명
                // (주)DB하이텍	2118110771	NULL	조기석	NULL	NULL	NULL	권혁명	032-680-4160	001	이현주(HyunJu Lee)	NULL	NULL	NULL	2023-07-20 20:34:23.530
                // 100005	200004	알비엠	1042573244	NULL	이상준	도소매	사무기기	rbm@dbhitekdev.com	알비엠	02-776-5222	001	황미진	NULL	NULL	NULL	2023-07-20 20:34:23.530
                foreach(var m in l){
                    list.Add(new ExternalCompanyRecord(m.ChargerName??"", "업체", m.VendorName??"", "", "", m.EntryEmail??"", m.BusinessNo??"", m.EntryTel??"", m.ChargerName??""));
                    // list.Add(new ExternalCompanyRecord(m.EntryName??"", "업체", m.VendorName??"", "", "", m.EntryEmail??"", m.BusinessNo??"", m.EntryTel??"", m.ChargerName??""));
                }
                // list.Add(new ExternalCompanyRecord("홍길동", "업체", "포트마스", "", "", "hong@gmail.com", "222-95-969696", "02-965-1234", "김당당"));
                // list.Add(new ExternalCompanyRecord("김상희", "업체", "포트란", "", "", "potran@gmail.com", "549-95-12480", "02-965-9856", "이당당"));
                // list.Add(new ExternalCompanyRecord("홍길동", "업체", "포트마스", "", "", "hong@gmail.com", "123-05-58742", "031-456-1601", "박당당"));
            }
            */
            return Json(list);
        }
        internal record ExternalCompanyRecord(string Sabun, string Name, string Type, string CompanyName, string Position, string Mobile, string Email, string BizRegNo, string CompanyTel, string chargerName);

    }
}
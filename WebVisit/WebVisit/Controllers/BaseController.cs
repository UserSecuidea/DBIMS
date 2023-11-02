using System.Text;
using System.Text.Json;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using System.Runtime.CompilerServices;
using System.Web;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

using WebVisit.Models;
using WebVisit.Models.DomainModels.Passport;
using WebVisit.Models.DomainModels.MySQL;
using WebVisit.Models.DomainModels.LPR;
using System.Net;
// using WebVisit.Models.DomainModels.S1Access;

namespace WebVisit.Controllers
{
    /// <summary>
    /// 공통 팝업 등 모든 controller 에서 사용되는 methods or properties
    /// </summary>
    public class BaseController: Controller
    {
        // Constants
        private readonly string TEMPDATA_ERROR = "errMsg";
        private readonly string TEMPDATA_ERROR_OBJECT = "errObj";

        // Environment & Log
        private static readonly Object _lockObj = new();
        protected readonly int SERVER_IDX = 3; // 1: CONOZ, 2: SecuIDES, 3: DB Dev, 4: DB Prod
        protected readonly IStringLocalizer? Localizer;
        protected readonly ILogger? _logger;
        protected readonly IConfiguration? _configuration;

        // Session
        protected VisitSession? VisitSession { get; set; }

        // Base DB Context
        protected BaseSVISITContext? DbSVISIT { get; set; }
        protected BasePASSPORTContext? DbPASSPORT { get; set; }
        protected BaseLPRContext? DbLPR { get; set; }
        protected BaseMySQLWebVisitContext? DbMySQL { get; set; }
        // protected BaseS1ACCESSContext? DbS1ACCESS { get; set; }

        // CONOZ
        protected CzSVISITContext? CzSVISITContext => HttpContext.RequestServices.GetService(typeof(CzSVISITContext)) as CzSVISITContext;
        protected CzPASSPORTContext? CzPASSPORTContext => HttpContext.RequestServices.GetService(typeof(CzPASSPORTContext)) as CzPASSPORTContext;
        protected CzLPRContext? CzLPRContext => HttpContext.RequestServices.GetService(typeof(CzLPRContext)) as CzLPRContext;
        protected CzMySQLWebVisitContext? CzMySQLWebVisitContext => HttpContext.RequestServices.GetService(typeof(CzMySQLWebVisitContext)) as CzMySQLWebVisitContext;
        // protected CzS1ACCESSContext? CzS1ACCESSContext => HttpContext.RequestServices.GetService(typeof(CzS1ACCESSContext)) as CzS1ACCESSContext;

        //db hitek dev
        protected DBSVISITDevContext? DBSVISITDevContext => HttpContext.RequestServices.GetService(typeof(DBSVISITDevContext)) as DBSVISITDevContext;
        protected DBPASSPORTDevContext? DBPASSPORTDevContext => HttpContext.RequestServices.GetService(typeof(DBPASSPORTDevContext)) as DBPASSPORTDevContext;
        protected DBLPRDevContext? DBLPRDevContext => HttpContext.RequestServices.GetService(typeof(DBLPRDevContext)) as DBLPRDevContext;
        protected DBMySQLWebVisitDevContext? DBMySQLWebVisitDevContext => HttpContext.RequestServices.GetService(typeof(DBMySQLWebVisitDevContext)) as DBMySQLWebVisitDevContext;
        // protected DBS1ACCESSDevContext? DBS1ACCESSDevContext => HttpContext.RequestServices.GetService(typeof(DBS1ACCESSDevContext)) as DBS1ACCESSDevContext;

        //db hitek production 
        protected DBSVISITContext? DBSVISITContext => HttpContext.RequestServices.GetService(typeof(DBSVISITContext)) as DBSVISITContext;
        protected DBPASSPORTContext? DBPASSPORTContext => HttpContext.RequestServices.GetService(typeof(DBPASSPORTContext)) as DBPASSPORTContext;
        protected DBLPRContext? DBLPRContext => HttpContext.RequestServices.GetService(typeof(DBLPRContext)) as DBLPRContext;
        protected DBMySQLWebVisitContext? DBMySQLWebVisitContext => HttpContext.RequestServices.GetService(typeof(DBMySQLWebVisitContext)) as DBMySQLWebVisitContext;
        // protected DBS1ACCESSContext? DBS1ACCESSContext => HttpContext.RequestServices.GetService(typeof(DBS1ACCESSContext)) as DBS1ACCESSContext;

        // secuidea
        protected SVISITContext? SVISITContext => HttpContext.RequestServices.GetService(typeof(SVISITContext)) as SVISITContext;
        protected PASSPORTContext? PASSPORTContext => HttpContext.RequestServices.GetService(typeof(PASSPORTContext)) as PASSPORTContext;
        // protected S1ACCESSContext? S1ACCESSContext => HttpContext.RequestServices.GetService(typeof(S1ACCESSContext)) as S1ACCESSContext;

        // 공통 Repository

        protected Repository<CommonCode>? CommonCodeData { get; set; }
        protected Repository<WebVisit.Models.Person>? PersonData { get; set; }
        protected Repository<Company>? CompanyData { get; set; }
        protected Repository<Level>? LevelData { get; set; }
        protected Repository<NotebookSelfApproval>? NotebookSelfApprovalData { get; set; }

        //파일 확장자
        protected string[] permittedExtensions = new string[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".csv", ".txt", ".rtf", ".jpg", ".jpeg", ".png", ".gif" };
        protected readonly long _fileSizeLimit;

        public BaseController(ILogger logger, IConfiguration configuration, [FromServices] IStringLocalizer ㅣocalizer){
            Localizer = ㅣocalizer;
            _logger = logger;
            _configuration = configuration;
            _fileSizeLimit = _configuration.GetValue<long>("FileSizeLimit");
        }

        protected void SetDbContext(){
            if (SERVER_IDX == 1)
            {
                // 코노즈 개발 서버
                DbSVISIT ??= CzSVISITContext;
                DbPASSPORT ??= CzPASSPORTContext;
                DbLPR ??= CzLPRContext;
                DbMySQL = CzMySQLWebVisitContext;
                // DbS1ACCESS ??= CzS1ACCESSContext;
            }
            else if (SERVER_IDX == 2)
            {
                // 시큐이데아 개발 서버
                DbSVISIT ??= SVISITContext;
                DbPASSPORT ??= PASSPORTContext;
                // DbLPR ??= CzLPRContext;
                // DbMySQL = new CzMySQLWebVisitContext();
            }else if (SERVER_IDX == 3)
            {
                // DB 하이텍 개발 서버
                DbSVISIT ??= DBSVISITDevContext;
                DbPASSPORT ??= DBPASSPORTDevContext;
                DbLPR ??= DBLPRDevContext; 
                DbMySQL = DBMySQLWebVisitDevContext; //new CzMySQLWebVisitContext();
            }else if (SERVER_IDX == 4)
            {
                // DB 하이텍 Live 서버
                DbSVISIT ??= DBSVISITContext;
                DbPASSPORT ??= DBPASSPORTContext;
                DbLPR ??= DBLPRContext;
                DbMySQL = DBMySQLWebVisitContext; //new CzMySQLWebVisitContext();
            }
        }

        protected virtual void Init(){
            // implements in child
        }

        protected bool IsAccess(bool isLoadMenu = true){
            if (DbSVISIT == null || DbPASSPORT != null)
                SetDbContext();
            VisitSession = new(HttpContext);
            // menu list 불러오기
            if (isLoadMenu){
                SetMenuListByLevel();
            }
            if(DbSVISIT != null){
                if (CommonCodeData == null){
                    CommonCodeData = new Repository<CommonCode>(DbSVISIT);
                    PersonData ??= new Repository<WebVisit.Models.Person>(DbSVISIT);
                    CompanyData ??= new Repository<Company>(DbSVISIT);
                    LevelData ??= new Repository<Level>(DbSVISIT);
                    Init();
                    // To-do: level 정보를 가져와서 이 페이지의 접근 권한이 있는 지확인
                }
            }
            PersonDTO? personDTO = GetMe();
            ViewBag.UserName = personDTO?.Name??"";
            return true;
        }

        protected bool IsAccessAPI(){
            if (DbSVISIT == null || DbPASSPORT != null)
                SetDbContext();
            VisitSession = new(HttpContext);
            if (DbSVISIT != null)
            {
                CommonCodeData ??= new Repository<CommonCode>(DbSVISIT);
                Init();
            }
            return true;
        }

        /// <summary>
        /// 노트북자가결재 권한체크 
        /// </summary>
        /// <returns></returns>
        protected bool IsSelfApproval(){
            var my = GetMe();
            if (NotebookSelfApprovalData != null){
                var options = new QueryOptions<NotebookSelfApproval>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    Where = a => a.Sabun == my.Sabun && a.IsDelete == "N",
                };
                List<NotebookSelfApproval> selfApprovalList = (List<NotebookSelfApproval>) NotebookSelfApprovalData.List(options);
                if (selfApprovalList.Count > 0) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// SMS / 이메일 발송
        /// await SendMessageAsync("SMS", 1, "", "DB HiTek", "010-4610-0915", "이윤댕", "상우");
        /// await SendMessageAsync("MAIL", 24015, "", "DB HiTek", "choco@conoz.net", "심청이");
        /// </summary>
        /// <param name="MsgType">SMS | MAIL</param>
        /// <param name="TemplateID"></param>
        /// <param name="FromAddr"></param>
        /// <param name="FromName"></param>
        /// <param name="ToAddr"></param>
        /// <param name="ToName"></param>
        /// <param name="Param1"></param>
        /// <param name="Param2"></param>
        /// <param name="Param3"></param>
        /// <returns></returns>
        protected async Task SendMessageAsync(string MsgType, int TemplateID, string FromAddr, string FromName, string ToAddr, string ToName, string Param1 ="", string Param2 = "", string Param3 = ""){
            string Title = "";
            string Contents = "";
            string TMPL_CD = ""; // SMS:카카오 알림톡 템플릿 코드, MAIL:NULL
            
            if(DbPASSPORT != null){
                if(MsgType.Equals("SMS")){
                    // TMPL_CD = TemplateID + "";
                    IQueryable<TemplateSm> templateQuery = DbPASSPORT.TemplateSms.Where(x => x.MsgType == TemplateID);
                    var list = templateQuery.ToList();
                    if(list.Count > 0) {
                        TemplateSm templateSm = list[0];
                        WriteLog("templateSm:" + Dump(templateSm));
                        // WriteLog("templateSm:" + templateSm.Title + ", " + templateSm.Title);
                        if(!string.IsNullOrEmpty(templateSm.Msg) && !string.IsNullOrEmpty(templateSm.Title)){
                            if (TemplateID == 1 || TemplateID == 2 || TemplateID == 3) {
                                Title = templateSm.Title;
                                string locationName = "";
                                if(!string.IsNullOrEmpty(templateSm.TmplCd)){
                                  TMPL_CD = templateSm.TmplCd;
                                }
                                if(!string.IsNullOrEmpty(Param1)){
                                    locationName = Param1;
                                } else{
                                    locationName = GetLocationName(Param2);
                                }
                                Contents = String.Format(templateSm.Msg, ToName, locationName, "");
                            }
                        }
                    }
                } else if(MsgType.Equals("MAIL")){
                    string tID = TemplateID + "";
                    IQueryable<TemplateMail> templateQuery = DbPASSPORT.TemplateMails.Where(x => x.MailType == tID);
                    var list = templateQuery.ToList();
                    if(list.Count > 0) {
                        TemplateMail templateMail = list[0];
                        WriteLog("templateMail:" + templateMail.MailTitle + ", " + templateMail.MailContents);
                        if(!string.IsNullOrEmpty(templateMail.MailContents) && !string.IsNullOrEmpty(templateMail.MailTitle)){
                            Title = templateMail.MailTitle;
                            if (tID.Equals("24015")) {
                                // 24015: 이름, 출입관리시스템 URL
                                var url = "https://ims.dbhitek.com/";
                                Contents = String.Format(templateMail.MailContents, ToName, url);
                            }
                            // 24001: 이름, 업체명, 사유
                        }
                    }
                    Contents = Contents.Replace("{", "{{");
                    Contents = Contents.Replace("}", "}}");
                }
                WriteLog("Contents: " + Contents);
                // Contents = "<html>body {{margin:20;}}</html>";
                if (!string.IsNullOrEmpty(Title)){
                    StringBuilder sb = new();
                    sb.Append("PROC_MESSAGE_SEND_REQ @FromAddr='");
                    sb.Append(FromAddr);
                    sb.Append("' , @FromName='");
                    sb.Append(FromName);
                    sb.Append("', @ToAddr='"); 
                    sb.Append(ToAddr);
                    sb.Append("', @ToName='"); 
                    sb.Append(ToName);
                    sb.Append("', @Title='"); 
                    sb.Append(Title);
                    sb.Append("', @Contents='"); 
                    sb.Append(Contents);
                    sb.Append("', @MsgType='"); 
                    sb.Append(MsgType);
                    sb.Append("', @TMPL_CD='"); 
                    sb.Append(TMPL_CD);
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

        protected string GetVisitEduURL(){
            var url = $"{this.Request.Scheme}://{this.Request.Host}/visit/edu";
            return url;
        }

        /// <summary>
        /// 이름^사번^이메일^결재역할^주관부서상신
        /// SET @pi_intValue1 = '김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1'
        /// SET @pi_intValue2 = '98dee76cd19ba651dc5879d5ee75ed919bad54d8156137f49d189a7a259017d8762bac3789cb17b924b749a1d1225f1c3b44192799eee067'
        /// select [PASSPORT].dbo.FuncEncrypt(@pi_intValue1)
        /// select [PASSPORT].dbo.FuncDecrypt(@pi_intValue2)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string EncMD5(string value){
            string strEnc = "";
            if(DbPASSPORT != null){
                StringBuilder sb = new();
                sb.Append("select [PASSPORT].dbo.FuncEncrypt('");
                sb.Append(value);
                sb.Append("')");
                var fs1 = FormattableStringFactory.Create(sb.ToString());
                strEnc = DbPASSPORT.Database.SqlQuery<string>(fs1).ToList()[0];
                WriteLog("strEnc:"+strEnc);
            }
            return strEnc;
        }

        protected string DecMD5(string value){
            string strDec = "";
            if(DbPASSPORT != null){
                StringBuilder sb = new();
                sb.Append("select [PASSPORT].dbo.FuncDecrypt('");
                sb.Append(value);
                sb.Append("')");
                var fs1 = FormattableStringFactory.Create(sb.ToString());
                strDec = DbPASSPORT.Database.SqlQuery<string>(fs1).ToList()[0];
                WriteLog("strEnc:"+strDec);
            }
            return strDec;
        }

        /// <summary>
        /// deprecated
        /// param: legacy_apprlines
        /// 성명^부서코드^메일주소^결재역할^주관부서 상신부서 구분;
        ///	ex)홍길동^AAA000^hong@dongbu.com^2^1;홍길이^AAA100^hong@dongbu.com^2^1
        ///	1인결재	0,결재	2,참조	4,전결	6,결재안함(위임)	8,통보	10,순차합의	12,병렬합의	13,심사	15
        ///	상신부서	2, 주관부서	1
        ///	김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1
        ///	98dee76cd19ba651dc5879d5ee75ed919bad54d8156137f49d189a7a259017d8762bac3789cb17b924b749a1d1225f1c3b44192799eee067
        ///	
        /// param: legacyout        결재이후 return 받을 값
        /// param: bsavedocument    결재시 문서관리에 저장할 여부를 물음
        /// param: bmodifylines     문서정보 변경가능 여부
        /// param: bfinalline
        /// param: bfixapprover
        /// </summary>
        /// <returns></returns>
        protected async Task<string> SendPorte(){
            var url = "http://login.dbhitekdev.com/login_sso.aspx"; // https://login.dbhitekdev.com/login_sso.aspx
            Uri uri = new Uri("http://dbhitekdev.com");//approval.dbhitekdev.com
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies
            };
            using var httpClient = new HttpClient(handler);
            // httpClient.DefaultRequestHeaders.Add("Content-Type","application/x-www-form-urlencoded");
            httpClient.DefaultRequestHeaders.Add("Accept-Charset","euc-kr");
            httpClient.DefaultRequestHeaders.Add("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            // httpClient.DefaultRequestHeaders.Add("Accept-Encoding","gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Accept-Language","ko-KR,ko;q=0.8,en-US;q=0.5,en;q=0.3");
            httpClient.DefaultRequestHeaders.Add("User-Agent","Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:109.0) Gecko/20100101 Firefox/116.0");
            httpClient.DefaultRequestHeaders.Add("Connection","keep-alive");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site","cross-site");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest","document");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode","navigate");
            httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests","1");
            var value = new Dictionary<string, string>
            {
                { "userid", "eunji86" },
                { "password", "8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg=" },
                { "systemid", "DM_IMS" },
                { "businessid", "ims_aios" },
                { "bodytype", "html" },
                { "title", "[신통문] 신청 테스트입니다" },
                { "legacy_apprlines", "98dee76cd19ba651dc5879d5ee75ed919bad54d8156137f49d189a7a259017d8762bac3789cb17b924b749a1d1225f1c3b44192799eee067" },
                { "legacyout", "<XML_DATA><CHECK_INOUT_NO>1234</CHECK_INOUT_NO><CARRY_OUT_CLASS>2000</CARRY_OUT_CLASS><CHECK_INOUT_PURPOSE>1</CHECK_INOUT_PURPOSE><oneApproval>N</oneApproval></XML_DATA>" },
                { "bsavedocument", "N" },
                { "bmodifylines", "N" },
                { "bfinalline", "" },
                { "bfixapprover", "N" },
                { "bodies", "<html><body>test 한글 테스트</body></html>" },
            };
            var requestContent = new FormUrlEncodedContent(value);
            // var d = await requestContent.ReadAsStringAsync();
            // WriteLog("requestContent:" + d);

            using var response = httpClient.PostAsync(url, requestContent);
            // using var response = await httpClient.GetAsync(url);
            string resMsg = await response.Result.Content.ReadAsStringAsync();
            // string apiResponse = await response.Content.ReadAsStringAsync();
            WriteLog("resMsg:" + resMsg);
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
                Console.WriteLine("getCookie:"+cookie.Name + ": " + cookie.Value);
            var headersEnumerator = response.Result.Headers.GetEnumerator();
            
            string cookieValue = "";
            while (headersEnumerator.MoveNext())
            {
                WriteLog(headersEnumerator.Current.Key+":" + String.Join(" ", headersEnumerator.Current.Value));
                if(headersEnumerator.Current.Key.Equals("Set-Cookie")){
                    cookieValue = String.Join(" ", headersEnumerator.Current.Value);
                }
            }
            cookieValue = "Login_Culture=ko; ASP.NET_SessionId=4cafq1xymg4sxtcypo1q3euu; cps_ck6=4haeve2T9fFtn7WvCQTyj6nIw+IfuvHJSim6pm4vFqhKsDRWvwp5R7JmmdU+KEnS; Login_GMT=9; eNovator=UserID=zPz4Utv1n9QZ1W8b73SJYA==&Email=BQgqf4kyV9xMVMa7Jyim0oPXJ8BeQ8qE7mn0dc9zWXiODln5U49LEaRhhq6dxjpj&FrontName=YXBwcm92YWwuZGJoaXRla2Rldi5jb20=&ProhibitSendQuota=MjA5NzE1Mg==&IssueWarningQuota=MTk5MjI5NQ==&UserName=7Iah7J2A7KeA&UserName_ENG=RVVOSkkgU09ORw==&UserNick=&DeptID=REEyMDAwMQ==&DeptName=7ZWY7J207YWN7KCV67O07ISc67mE7Iqk7YyA&DeptName_ENG=7ZWY7…0MDc3ZTNiNDQxOTI3OTllZWUwNjc=&SSO_EMPNO=Y2IxNzVjZDZkNzg4OTZlYTViZGQ4NjQzOTFiZTg4OWMzYjQ0MTkyNzk5ZWVlMDY3&SSO_LANG=ZWJjMGE0ODNiOTE2NTA4M2JjNDY2ZmE5NmQ0YjViZWMzYjQ0MTkyNzk5ZWVlMDY3&SSO_SAPID=Y2IxNzVjZDZkNzg4OTZlYTViZGQ4NjQzOTFiZTg4OWMzYjQ0MTkyNzk5ZWVlMDY3&SSO_EXTEND_ATTR1=YTRjOTcyNTcyNjZiZmE5YTE1MDcyMTQ0Y2JjZGYxYjczYjQ0MTkyNzk5ZWVlMDY3&SSO_POS_CODE=MDUzZTM1NDg4Y2NhYTc0ODIwN2NkZjZiZTg1OGE3MWEzYjQ0MTkyNzk5ZWVlMDY3&SSO_DUTY_CODE=MWVkN2Y1ODUwYzZkZGUyMjQzMDQzNDFiMjM3ZTNkMzkzYjQ0MTkyNzk5ZWVlMDY3; cps_ucd1=RE0wMg==";
            cookieValue = String.Join(" ", Encoding.ASCII.GetBytes(cookieValue));
            cookies.Add(uri, new Cookie("cps_ck6","4haeve2T9fFtn7WvCQTyj6nIw+IfuvHJSim6pm4vFqhKsDRWvwp5R7JmmdU+KEnS"));
            cookies.Add(uri, new Cookie("Login_Culture","ko"));
            cookies.Add(uri, new Cookie("ASP.NET_SessionId","4cafq1xymg4sxtcypo1q3euu"));
            cookies.Add(uri, new Cookie("Login_GMT","9"));
            cookies.Add(uri, new Cookie("eNovator","UserID=zPz4Utv1n9QZ1W8b73SJYA==&Email=BQgqf4kyV9xMVMa7Jyim0oPXJ8BeQ8qE7mn0dc9zWXiODln5U49LEaRhhq6dxjpj&FrontName=YXBwcm92YWwuZGJoaXRla2Rldi5jb20=&ProhibitSendQuota=MjA5NzE1Mg==&IssueWarningQuota=MTk5MjI5NQ==&UserName=7Iah7J2A7KeA&UserName_ENG=RVVOSkkgU09ORw==&UserNick=&DeptID=REEyMDAwMQ==&DeptName=7ZWY7J207YWN7KCV67O07ISc67mE7Iqk7YyA&DeptName_ENG=7ZWY7…0MDc3ZTNiNDQxOTI3OTllZWUwNjc=&SSO_EMPNO=Y2IxNzVjZDZkNzg4OTZlYTViZGQ4NjQzOTFiZTg4OWMzYjQ0MTkyNzk5ZWVlMDY3&SSO_LANG=ZWJjMGE0ODNiOTE2NTA4M2JjNDY2ZmE5NmQ0YjViZWMzYjQ0MTkyNzk5ZWVlMDY3&SSO_SAPID=Y2IxNzVjZDZkNzg4OTZlYTViZGQ4NjQzOTFiZTg4OWMzYjQ0MTkyNzk5ZWVlMDY3&SSO_EXTEND_ATTR1=YTRjOTcyNTcyNjZiZmE5YTE1MDcyMTQ0Y2JjZGYxYjczYjQ0MTkyNzk5ZWVlMDY3&SSO_POS_CODE=MDUzZTM1NDg4Y2NhYTc0ODIwN2NkZjZiZTg1OGE3MWEzYjQ0MTkyNzk5ZWVlMDY3&SSO_DUTY_CODE=MWVkN2Y1ODUwYzZkZGUyMjQzMDQzNDFiMjM3ZTNkMzkzYjQ0MTkyNzk5ZWVlMDY3"));
            cookies.Add(uri, new Cookie("cps_ucd1","RE0wMg=="));
            using var httpClient2 = new HttpClient(handler);
            url = "http://approval.dbhitekdev.com/eNovator/GSWF/WebPage/Legacy/Legacyinterface2.aspx?AppParamID=19430&FixApprover=";
            httpClient2.DefaultRequestHeaders.Clear();
            httpClient2.DefaultRequestHeaders.Add("Accept-Charset","euc-kr");
            // httpClient2.DefaultRequestHeaders.Add("Set-Cookie",cookieValue);
            using var response2 = httpClient2.PostAsync(url, null);
            string resMsg2 = await response2.Result.Content.ReadAsStringAsync();
            WriteLog("resMsg2:" + resMsg2);
            return resMsg;
        }

        protected bool IsValidFileExtension(string fileName)
        {
            return IsValidFileExtension(fileName, permittedExtensions);
        }

        protected bool IsValidFileExtension(string fileName, string[] permittedExtensions) {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            // WriteLog("ext:" + ext + ", permittedExtensions:" + Dump(permittedExtensions));
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                WriteLog("false");
                return false;
            }
            WriteLog("true");
            return true;
        }

        protected async Task<FileData> GetFileData(IFormFile formFile)
        {
            FileData fileData = new();
            if (formFile.Length > 0)
            {
                var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                // WriteLog("_configuration:" + _fileSizeLimit);
                if (memoryStream.Length < _fileSizeLimit) // Upload the file if less than 10 MB, 2097152 = 2 MB
                {
                    // WriteLog("fileName:"+formFile.FileName);
                    // string meta = Dump(formFile);
                    // WriteLog("formFile 1:"+meta.Length);
                    // WriteLog("formFile 2:"+_Dump(typeof(IFormFile), meta));
                    if (IsValidFileExtension(formFile.FileName))
                    {
                        FileDTO fileDTO = new()
                        {
                            ContentType = formFile.ContentType,
                            ContentDisposition = formFile.ContentDisposition,
                            Length = formFile.Length,
                            FileName = formFile.FileName,
                        };
                        string meta = Dump(fileDTO);
                        // WriteLog("formFile 1:" + meta.Length);
                        // WriteLog("formFile 2:" + _Dump<FileDTO>(meta));
                        fileData.Hash = memoryStream.ToArray();
                        if (!String.IsNullOrEmpty(meta))
                        {
                            fileData.Meta = meta;
                        }
                    } else {
                        fileData.ErrorCode = -1;
                        fileData.ErrorMessage = "Invalid file extension";
                    }
                } else{
                    fileData.ErrorCode = -2;
                    fileData.ErrorMessage = "File size exceeded";
                }
            } else {
                    fileData.ErrorCode = -3;
                    fileData.ErrorMessage = "File is empty";
            }
            return fileData;
        }

        /// <summary>
        /// 레벨에 따른 메뉴 리스트
        /// </summary>
        /// <param name="levelID"></param>
        /// <returns></returns>
        // public dynamic? GetMenuListByLevel() {
        protected void SetMenuListByLevel() {
            int levelID = GetMe().LevelCodeID;
            // .OrderByDescending(x => x.GroupNo)
            string uri = GetURI();
            string lang = GetLang();
            // WriteLog("GetURI:" + GetURI());
            if (DbSVISIT != null){
                var query = DbSVISIT?.Menu
                    .Where(x => x.IsDelete == "N" && x.Depth < 2)
                    .OrderBy(x => x.GroupNo)
                    .GroupJoin(
                        inner: DbSVISIT.MenuLevel.Where(x => x.IsDelete == "N" && x.LevelID == levelID),
                        outerKeySelector: a => a.MenuID, // Menu
                        innerKeySelector: b => b.MenuID, // MenuLevel
                        resultSelector: (a, b) => new { a, b });
                var menuLevels = query?.ToList();
                menuLevels = menuLevels?.Where(x => x.a.Depth == 0 || (x.b != null && x.b.Any() &&  x.b.All(c => c.IsAccess == "Y"))).ToList();
                // WriteLog("menuLevels:" + Dump(menuLevels));
                // var t = menuLevels.GroupBy(x => x.a.GroupNo).Select(g => new { groupNo = g.Key, count = g.Count() });
                // WriteLog("groupby t:>>" + Dump(t));
                // 자식 메뉴가 없는 대메뉴 
                var noChildMenuList = menuLevels?.GroupBy(x => x.a.GroupNo ).Select(g => new { groupNo=g.Key, count = g.Count()}).Where(x=>x.count < 2);
                // WriteLog("groupby:>>" + Dump(noChildMenuList));
                if (menuLevels != null) {
                    var toRemove = new HashSet<dynamic>();
                    foreach(var m in menuLevels)
                    {
                        if (noChildMenuList != null && HasChildMenu(noChildMenuList, m.a.GroupNo) == false)
                            toRemove.Add(m);
                        if (m.a.Depth > 0 && !m.b.Any()){
                            toRemove.Add(m);
                        }
                        // WriteLog("compare uri:" + m.a.URL+", "+uri);
                        if (!String.IsNullOrEmpty(m.a.URL) && m.a.URL.Equals(uri)) {
                            // WriteLog("current title:" + m.a.MenuNameKor+" /"+m.b.FirstOrDefault()?.MenuNameKor);
                            // ViewBag.MenuNameKor = m.b.FirstOrDefault()?.MenuNameKor;
                            // ViewBag.MenuNameEng = m.b.FirstOrDefault()?.MenuNameEng;
                            ViewBag.CurrentMenuGroupNo = m.a.GroupNo;
                            ViewBag.CurrentMenuID = m.a.MenuID;
                            VisitSession?.SetMenuGroup(m.a.GroupNo);
                            VisitSession?.SetMenuID(m.a.MenuID);
                            // WriteLog("Set ViewBag.CurrentMenuGroupNo:" + m.a.GroupNo);
                            if (!String.IsNullOrEmpty(lang)) {
                                if (lang.Equals("ko")) {
                                    ViewData["Title"] = m.b.FirstOrDefault()?.MenuNameKor;
                                } else {
                                    ViewData["Title"] = m.b.FirstOrDefault()?.MenuNameEng;
                                }
                            }else{
                                ViewData["Title"] = m.b.FirstOrDefault()?.MenuNameKor;
                            }
                        }
                    }
                    menuLevels.RemoveAll(toRemove.Contains);
                    // WriteLog("toRemove:" + Dump(toRemove));
                    // WriteLog("sharedMenuList:>>" + Dump(menuLevels));
                }
                ViewBag.sharedMenuList = menuLevels;
            }
            if (ViewBag.CurrentMenuGroupNo == null) {
                // WriteLog("ViewBag.CurrentMenuGroupNo:" + VisitSession?.GetMenuGroup());
                ViewBag.CurrentMenuGroupNo = VisitSession?.GetMenuGroup();
            }
            if (ViewBag.CurrentMenuID == null) {
                ViewBag.CurrentMenuID = VisitSession?.GetMenuID();
            }
            // return default;
        }

        private bool HasChildMenu(dynamic noChildMenuList, int groupNo) {
            foreach(var m in noChildMenuList) {
                if(m.groupNo == groupNo) {
                    return false;
                }
            }
            return true;
        }
        
        private string GetURI(){
            string? c = this.RouteData?.Values["Controller"]?.ToString()?.ToLower();
            string? a = this.RouteData?.Values["Action"]?.ToString()?.ToLower();
            return "/"+c + "/" + a;
            // StackTrace stackTrace = new();
            // MethodBase methodBase = stackTrace.GetFrame(1)?.GetMethod()!;
            // return (this.GetType().Name +"/"+ methodBase.Name).ToLower();
        }


        protected string GetLang() 
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            if (rqf == null) {
                return "ko";
            }
            return rqf.RequestCulture.Culture.ToString();
        }
       
       /// <summary>
       /// Test 용 메소드
       /// </summary>
       /// <returns></returns>
        private WebVisit.Models.Person GetMyInfo(){
            // PersonID = 1, Sabun = "master", CompanyID=0, LevelCodeID = 1, Password ="123456".SHA256Encrypt(), 
            // Name = "마스터관리자", InsertSabun="1", OrgNameKor="마스터", OrgNameEng="master"
            WebVisit.Models.Person person = new()
            {
                PersonID=0,
                Sabun = "master",
                Name="마스터관리자",
                CompanyID=0,
                OrgID="",
                OrgNameKor="마스터",
                OrgNameEng="master",
                LevelCodeID = 1,
            };
            return person;
        }

        protected PersonDTO GetMe()
        {
            PersonDTO? personDTO = VisitSession?.GetPersonDTO();
            ViewBag.personDTO = personDTO;
            WriteLog("personDTO:" + Dump(personDTO));
            if(personDTO == null || personDTO?.PersonID == -1) {
                //     personDTO = new PersonDTO(GetMyInfo());
                string? c = this.RouteData?.Values["Controller"]?.ToString()?.ToLower();
                if (!c.Equals("home")) {
                    Response.Redirect("/");
                }
                // WriteLog("controller:"+c);
            }
            return personDTO??new PersonDTO();
        }

        /// <summary>
        /// 임직원 여부 확인
        /// 비상주업체 관리자나 비상주업체 직원을 제외한 모든 레벨
        /// </summary>
        /// <param name="LevelCodeID"></param>
        /// <returns></returns>
        protected bool IsEmployee(int LevelCodeID = 0){
            if (LevelCodeID == 0) {
                LevelCodeID = GetMe().LevelCodeID;
            }
            return LevelCodeID != (int)VisitEnum.LevelType.PartnerNonresident && LevelCodeID != (int)VisitEnum.LevelType.PartnerNonresidentManager;
        }

        protected bool IsEmployeeOnly(int LevelCodeID = 0){
            if (LevelCodeID == 0) {
                LevelCodeID = GetMe().LevelCodeID;
            }
            return LevelCodeID == (int)VisitEnum.LevelType.Employee;
        }

        /// <summary>
        /// 비상주 협력사 관리자
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        protected bool IsPartnerNonresidentManager(int CompanyID = 0){
            var my = GetMe();
            if (CompanyID < 1){
                return my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager;
            } else{
                return my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager && my.CompanyID == CompanyID;
            }
        }


        /// <summary>
        /// 사업장 관리자
        /// </summary>
        /// <param name="CompanyID">사업장</param>
        /// <returns></returns>
        protected bool IsGeneralManager(string? Location="") {
            var my = GetMe();
            if (my != null){
                if (string.IsNullOrEmpty(Location)){
                    return my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager;
                } else{
                    return my.LevelCodeID == (int)VisitEnum.LevelType.GeneralManager && my.Location != null && my.Location.Equals(Location);
                }
            }
            return false;
        }

        protected bool IsMaster(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.Master;
        }

        protected bool IsMasterWithSabun(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.Master && !GetMe().Sabun.Equals("master");
        }

        protected bool IsESH(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.EmployeeESH;
        }

        protected bool IsHR(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.EmployeeHR;
        }

        /// <summary>
        /// 보안실 여부
        /// </summary>
        /// <returns></returns>
        protected bool IsSecurity(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.Security;
        }

        /// <summary>
        /// 영양사 여부
        /// </summary>
        /// <returns></returns>
        protected bool IsDietitian(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.EmployeeDietitian;
        }

        protected bool IsPurchasing(){
            return GetMe().LevelCodeID == (int)VisitEnum.LevelType.EmployeePurchasing;
        }

        /// <summary>
        /// IT팀 인지 여부
        /// </summary>
        /// <returns></returns>
        protected bool IsITTeam(){
            if (GetMe() != null && GetMe().OrgNameKor != null) {
                return GetMe().OrgNameKor.Equals("IT팀");
            }
            return false;
        }

        protected GridData FilterGridData(GridData values){
            if (values == null) return default;
            GridData values2 = values.Clone();
            foreach (var propertyInfo in values2.GetType().GetProperties(BindingFlags.Public  | BindingFlags.Instance)){
                // WriteLog("propertyInfo: "+ propertyInfo.Name + " / " + propertyInfo.PropertyType +" / "+ propertyInfo.GetValue(values));
                if (propertyInfo.GetValue(values2) != null && propertyInfo.PropertyType == typeof(string)){
                    // WriteLog("propertyInfo: "+ propertyInfo.Name + " / " + propertyInfo.PropertyType +" / "+ propertyInfo.GetValue(values2));
                    if (propertyInfo.GetValue(values2) != null && propertyInfo.GetValue(values2).Equals("+")){
                        propertyInfo.SetValue(values2, "");
                    }
                }
            }
            return values2;
        }

        /// <summary>
        /// example:
        ///    List<CommonCode> commonCodes = GetCommonCodes(208);
        ///    WriteLog("commonCodes:" + Dump(commonCodes));
        ///    List<CommonCode> commonCodes = GetCommonCodes((int)VisitEnum.CommonCode.ApprovalType);
        ///    WriteLog("commonCodes: >> " + Dump(commonCodes));
        /// </summary>
        /// <param name="GroupNo"></param>
        /// <returns></returns>
        protected List<CommonCode> GetCommonCodes(int GroupNo)
        {
            if (CommonCodeData != null){
                var options = new QueryOptions<CommonCode>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderBy = a => a.OrderNo,
                    OrderByDirection = "asc",
                    // OrderByDirection = values.SortDirection,
                    Where = a => a.GroupNo == GroupNo && a.OrderNo >0 && a.IsDelete == "N"
                };
                return (List<CommonCode>)CommonCodeData.List(options);
            } else {
                return new List<CommonCode>();
            }
        }

        protected List<Company> Companies = new();
        protected List<Company> GetCompanyList(){
            if (Companies != null && Companies.Count > 0){
                return Companies;
            }
            if (CompanyData != null){
                var options2 = new QueryOptions<Company>
                {
                    Where = a => a.CompanyStatus == 1 && a.IsDelete == "N",
                };
                Companies = (List<Company>) CompanyData.List(options2);
                return Companies;
            }
            return new List<Company>();
        }

        protected string GetCompanyName(int companyID){
            var list = GetCompanyList();
            foreach(var m in list){
                if (m.CompanyID == companyID){
                    return m.CompanyName;
                }
            }
            return "";
        }
        protected int GetCompanyID(string companyName){
            if (CompanyData != null){
                var options2 = new QueryOptions<Company>
                {
                    Where = a => a.CompanyStatus == 1 && a.IsDelete == "N" && a.CompanyName != null && EF.Functions.Like(a.CompanyName, "%"+companyName+"%"),
                };
                var companies = (List<Company>) CompanyData.List(options2);
                WriteLog("companies:" + Dump(companies));
                if (companies.Count > 0) {
                    return companies[0].CompanyID;
                }
            }
            return -1;
        }

        protected Company GetCompany(int companyID=-1, bool isNoTracking = false)
        {
            Company company = null;
            // WriteLog("companyID:" + companyID);
            if(companyID > 0 && CompanyData != null) {
                var options = new QueryOptions<Company>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.CompanyID == companyID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    company = CompanyData.GetNT(companyID);
                } else {
                    company = CompanyData.Get(companyID);
                }
            }
            return company;
        }
        #region GetCompanyBizRegNo 2023.10.30 dsyoo 업체 사업자번호찾기
        protected string GetCompanyBizRegNo(int? companyID)
        {
            var list = GetCompanyList();
            foreach (var m in list)
            {
                if (m.CompanyID == companyID)
                {
                    return m.BizRegNo;
                }
            }
            return "";
        }
        #endregion
        protected string GetLocationName(string Location, int langIdx = 1 ){
            List< CommonCode > CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location);
            foreach(var m in CodeLocation){
                if(m.SubCodeDesc.Equals(Location)) {
                    if(langIdx == 1){
                        return m.CodeNameKor;
                    }else{
                        return m.CodeNameEng;
                    }
                }
            }
            return string.Empty;
        }

        protected Company GetCompanyByID(int companyID){
            if (CompanyData != null){
                var company = CompanyData.Get(new QueryOptions<Company>{
                    Where = a => a.CompanyID == companyID,
                }) ?? new Company();
                return company;
            }
            return new Company();
        }        

        protected string GetUserIP(){
            // var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress;
            if (HttpContext.Connection.RemoteIpAddress != null){
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6){
                    remoteIp = remoteIp.MapToIPv4();
                }
                return remoteIp.ToString();
            }
            return "";
        }

        protected string? GetPorteID(string sabun) {
            string? porteID = null;
            if (DbPASSPORT != null && !string.IsNullOrEmpty(sabun)) {
                List<VImsUserInfo> list = new ();
                List<string> strWhere = new();
                strWhere.Add("EMPLOYEE_ID = '"+sabun+"'");

                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.V_IMS_USER_INFO ");
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
                WriteLog("list: "+Dump(list));
                if(list.Count>0){
                    porteID= list[0].PorteId;
                }
            }
            return porteID;
        }

        protected dynamic GetUnionPerson(PersonGridData? values, string? sabun="", string? passowrd=""){
            var count = 0;
            List<Person> list = new();
            bool isConnected = false;
            try{
                if (DbSVISIT != null && DbSVISIT.Database.CanConnect()){
                    int offset = 0;
                    if (values != null){
                        offset = (values.PageNumber - 1) * values.PageSize; // COALESCE(a.IS_DELETED, b.PersonTypeID) as 
                    }
                    StringBuilder sb = new();
                    sb.Append(@"SELECT b.PersonID, COALESCE(a.EMPLOYEE_ID, b.sabun, 'N') as Sabun, COALESCE(a.USER_NAME, b.Name, 'N') as Name, 
                        COALESCE(a.Location, b.Location, 'N') as Location, b.CompanyID, COALESCE(b.PersonTypeID, 0) as PersonTypeID, 
                        b.OrgID as OrgID, COALESCE(a.DEPT_NAME, b.OrgNameKor, 'N') as OrgNameKor, COALESCE(b.OrgNameEng, 'N') as OrgNameEng, 
                        COALESCE(b.LevelCodeID, 3) as LevelCodeID, COALESCE(b.GradeID, 0) as GradeID, COALESCE(a.SAP_TITLE_NAME, b.GradeName) as GradeName, 
                        COALESCE(a.PASSWORD, b.Password, 'N') as Password, b.PWUpdateDate, b.BirthDate, b.Gender, COALESCE(a.MOBILE, b.Mobile) as Mobile, 
                        COALESCE(b.Tel, '') as Tel, COALESCE(a.Email, b.Email) as Email, b.ImageData, b.ImageDataHash, COALESCE(CONVERT(int, a.IS_DELETED), b.PersonStatusID, 0) as PersonStatusID, 
                        b.HomeAddr, b.HomeDetailAddr, b.HomeZipcode, COALESCE(b.IsForeigner, 0) as IsForeigner, b.Nationality, b.ImmStatusCodeID, 
                        b.ImmStartDate, b.ImmEndDate, b.IsAllowSMS, b.WorkTypeCodeID, b.CarAllowCnt, b.CarRegCnt, b.TermsPrivarcyFileData, 
                        b.TermsPrivarcyFileDataHash, b.CardIssueStatus, b.CardCreateCnt, CONVERT(CHAR(10),b.VisitorEduLastDate,23) as VisitorEduLastDate, 
                        CONVERT(CHAR(10), b.CleanEduLastDate,23) as CleanEduLastDate, CONVERT(CHAR(10),b.SafetyEduLastDate,23) as SafetyEduLastDate, 
                        CONVERT(CHAR(10),b.VisitLastDate,23) as VisitLastDate, CONVERT(CHAR(10),b.ValidDate,23) as ValidDate, b.UpdateIP, b.CardID, b.CardNo, b.IsRecTempCard, 
                        CONVERT(CHAR(10),b.TempCardRecDate,23) as TempCardRecDate, COALESCE(b.InsertSabun,'N') as InsertSabun, b.InsertName, b.InsertOrgID, 
                        b.InsertOrgNameKor, b.InsertOrgNameEng, CONVERT(CHAR(10), b.UpdateDate,23) as UpdateDate, CONVERT(CHAR(10),b.InsertDate,23) as InsertDate, 
                        COALESCE(b.IsDelete,'N') as IsDelete, b.PID, a.PORTE_ID as PorteID, a.SAP_DEPT_CODE as SapDeptCode
                        FROM (SELECT * FROM PASSPORT.dbo.V_IMS_USER_INFO ");
                    // COALESCE(a.OFFICE_TEL, b.Tel) as Tel
                    sb.Append(" WHERE USER_NAME IS NOT NULL"); //  AND IS_DELETED = 0
                    sb.Append(") a FULL OUTER JOIN (SELECT * from Person");
                    sb.Append(" WHERE IsDelete = 'N' ");
                    sb.Append(") b ON (a.EMPLOYEE_ID = b.sabun)");
                    // where
                    StringBuilder sbWhere = new();
                    bool useWhere = false;
                    if (values != null) {
                        // 레벨별 권한 설정.
                        if (IsMaster() == false){
                            PersonDTO my = GetMe();
                            WriteLog("LevelCodeID:" + my.LevelCodeID);
                            if (IsGeneralManager() || IsSecurity() || IsDietitian() || IsHR() || IsESH()) {
                                // 일반관리자 : 해당 사업장의 사원정보조회 / 엑셀다운로드 Location
                                // 보안실 : 해당 사업장의 사원정보 조회 / 엑셀다운로드
                                if(useWhere){
                                    sbWhere.Append(" AND ");
                                }else {
                                    sbWhere.Append(" WHERE ");
                                    useWhere = true;
                                }
                                sbWhere.Append(" (a.Location = '");
                                sbWhere.Append(my.Location);
                                sbWhere.Append("' OR b.Location = '");
                                sbWhere.Append(my.Location);
                                sbWhere.Append("') ");
                            } else if (my.LevelCodeID == (int)VisitEnum.LevelType.PartnerNonresidentManager){ // 비상주업체 : 자사 임직원 사원정보 조회 / 엑셀다운로드
                                if(useWhere){
                                    sbWhere.Append(" AND ");
                                }else {
                                    sbWhere.Append(" WHERE ");
                                    useWhere = true;
                                }
                                sbWhere.Append(" b.CompanyID = '");
                                sbWhere.Append(my.CompanyID);
                                sbWhere.Append("' ");
                            } else if (IsEmployee(my.LevelCodeID) && !string.IsNullOrEmpty(my.Sabun)){ // 임직원 : 자신의 사원정보 조회
                                if(useWhere){
                                    sbWhere.Append(" AND ");
                                }else {
                                    sbWhere.Append(" WHERE ");
                                    useWhere = true;
                                }
                                sbWhere.Append(" (a.EMPLOYEE_ID = '");
                                sbWhere.Append(my.Sabun);
                                sbWhere.Append("' OR b.Sabun = '");
                                sbWhere.Append(my.Sabun);
                                sbWhere.Append("') ");
                            }
                        }
                        // 검색
                        if (int.Parse(values.SearchPersonTypeID) >-1) { // 회원 구분
                            if(useWhere){
                                sbWhere.Append(" AND ");
                            }else {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            if (int.Parse(values.SearchPersonTypeID) > 1)
                            { // 2 3 비상주 관리자, 비상주 직원
                                if (int.Parse(values.SearchPersonTypeID) == 100){
                                    sbWhere.Append(" (a.EMPLOYEE_TYPE = 'C' OR a.EMPLOYEE_TYPE = 'S' OR a.EMPLOYEE_TYPE = 'U')");
                                } else {
                                    sbWhere.Append(" b.PersonTypeID = '");
                                    sbWhere.Append(int.Parse(values.SearchPersonTypeID));
                                    sbWhere.Append("' ");
                                }
                            }else { // 0 1 임직원, 상주직원
                                if (int.Parse(values.SearchPersonTypeID) == 1)
                                {
                                    sbWhere.Append(" a.EMPLOYEE_TYPE = 'C' ");// * (S:정규직, U:계약직), C: 협력직
                                }
                                else
                                {
                                    sbWhere.Append(" (a.EMPLOYEE_TYPE = 'S' OR a.EMPLOYEE_TYPE = 'U') ");
                                }
                            }
                        }

                        // if (int.Parse(values.SearchCompanyID) >-1) { // 회사
                        //     if(useWhere){
                        //         sbWhere.Append(" AND ");
                        //     }else {
                        //         sbWhere.Append(" WHERE ");
                        //         useWhere = true;
                        //     }
                        //     sbWhere.Append(" b.CompanyID = '");
                        //     sbWhere.Append(int.Parse(values.SearchCompanyID));
                        //     sbWhere.Append("' ");
                        // }
                        if (!string.IsNullOrEmpty(values.SearchCompanyName) && !values.SearchCompanyName.Equals("+") && !values.SearchCompanyName.Equals("%20")) { // 회사명
                            WriteLog("[Search] CompanyName:" + values.SearchCompanyName);
                            var cid = GetCompanyID(values.SearchCompanyName);
                            // if(cid > 0){
                            if(useWhere){
                                sbWhere.Append(" AND ");
                            }else {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.CompanyID = '");
                            sbWhere.Append(cid);
                            sbWhere.Append("') ");
                            // }
                        }
                        if (int.Parse(values.SearchPersonStatusID) >-1) { // 재직 상태
                            // WriteLog("[Search] PersonStatusID:" + values.SearchPersonStatusID);
                            if(useWhere){
                                sbWhere.Append(" AND ");
                            }else {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.PersonStatusID = '");
                            sbWhere.Append(int.Parse(values.SearchPersonStatusID));
                            sbWhere.Append("' OR a.IS_DELETED = '");
                            sbWhere.Append(int.Parse(values.SearchPersonStatusID));
                            sbWhere.Append("') ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchOrgName) && !values.SearchOrgName.Equals("+") && !values.SearchOrgName.Equals("%20")) { // 부서명
                            WriteLog("[Search] OrgName:" + values.SearchOrgName);
                            if(useWhere){
                                sbWhere.Append(" AND ");
                            }else {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.OrgNameEng = '");
                            sbWhere.Append(values.SearchOrgName);
                            sbWhere.Append("' OR ");
                            sbWhere.Append(" b.OrgNameKor = '");
                            sbWhere.Append(values.SearchOrgName);
                            sbWhere.Append("' OR a.DEPT_NAME = '");
                            sbWhere.Append(values.SearchOrgName);
                            sbWhere.Append("') ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchName) && !values.SearchName.Equals("+") && !values.SearchName.Equals("%20")) { // 이름
                            WriteLog("[Search] Name:" + values.SearchName);
                            if(useWhere){
                                sbWhere.Append(" AND ");
                            }else {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.Name = '");
                            sbWhere.Append(values.SearchName);
                            sbWhere.Append("' OR a.USER_NAME = '");
                            sbWhere.Append(values.SearchName);
                            sbWhere.Append("') ");
                        }
                    }
                    // 사번으로 가져오기
                    if (!string.IsNullOrEmpty(sabun)){
                        useWhere = true;
                        sbWhere.Clear();
                        sbWhere.Append(" WHERE (a.EMPLOYEE_ID = '");
                        sbWhere.Append(sabun);
                        sbWhere.Append("' OR b.sabun = '");
                        sbWhere.Append(sabun);
                        sbWhere.Append("') ");

                        if (!string.IsNullOrEmpty(passowrd)){
                            sbWhere.Append(" AND (a.PASSWORD = '");
                            sbWhere.Append(passowrd);
                            sbWhere.Append("' OR b.Password = '");
                            sbWhere.Append(passowrd);
                            sbWhere.Append("') ");
                        }
                    }
                    
                    if(useWhere){
                        sb.Append(sbWhere);
                    }                    
                    // order by
                    sb.Append(" ORDER BY a.USER_NAME ");
                    // offset
                    if (values != null){
                        if (values.PageSize > 0){ // 0: 전체 가져오기
                            sb.Append(" OFFSET ");
                            sb.Append(offset);
                            sb.Append(" ROWS FETCH NEXT ");
                            sb.Append(values.PageSize);
                            sb.Append(" ROWS ONLY ");
                        }
                        if (values.PageSize > 0)
                        {
                            StringBuilder sb2 = new();
                            sb2.Append(@"select count(*) as cnt
                                from (select * from PASSPORT.dbo.V_IMS_USER_INFO where USER_NAME IS NOT NULL) a 
                                full outer join (select * from person where IsDelete = 'N') b on (a.EMPLOYEE_ID = b.sabun)");
                            if (useWhere)
                            {
                                sb2.Append(sbWhere);
                            }
                            var fs1 = FormattableStringFactory.Create(sb2.ToString());
                            count = DbSVISIT.Database.SqlQuery<int>(fs1).ToList()[0];
                            WriteLog("count:" + count);
                        }
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
                            Person p = new();
                            for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                            {
                                if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value){
                                    var n = reader.GetName(fieldCount);
                                    var v = reader[fieldCount];
                                    if (v != null){
                                        try{
                                            // WriteLog("read data: "+n+"="+v);
                                            // WriteLog("read data: "+n.GetType()+"="+v.GetType());
                                            if(n.Equals("UpdateDate") || n.Equals("InsertDate") || n.Equals("PWUpdateDate")
                                                || n.Equals("VisitorEduLastDate")|| n.Equals("CleanEduLastDate")|| n.Equals("VisitLastDate")
                                                || n.Equals("ValidDate")|| n.Equals("TempCardRecDate")|| n.Equals("SafetyEduLastDate")) {
                                                var t = v.ToString();
                                                if(!string.IsNullOrEmpty(t)){
                                                    v = DateTime.Parse(t);
                                                }
                                            }
                                            typeof(Person).GetProperty(n)?.SetValue(p, v);
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
                DbSVISIT?.Database.CloseConnection();                
            }
            return new { a= list, b=count, c=isConnected};
        }        

        protected Person? GetPerson(int personID=-1, bool isNoTracking = false)
        {
            if(personID > 0 && PersonData != null) {
                var options = new QueryOptions<Person>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.PersonID == personID && a.IsDelete == "N",
                };
                if(isNoTracking) {
                    return PersonData.GetNT(personID);
                } else {
                    return PersonData.Get(personID);
                }
            }
            return null;
        }

        protected Person? GetPersonBySabun(string sabun, bool isNoTracking = false)
        {
            if(!string.IsNullOrEmpty(sabun) && PersonData != null) {
                var options = new QueryOptions<Person>
                {
                    PageNumber = 0,
                    PageSize = 0,
                    OrderByDirection = "asc",
                    Where = a => a.Sabun == sabun && a.IsDelete == "N",
                };
                Person person;
                if(isNoTracking) {
                    person = PersonData.GetNT(options);
                } else {
                    person = PersonData.Get(options);
                }
                if (person == null)
                {
                    WriteLog("mode: insert");
                    PersonDTO? my = GetMe();
                    person = new();
                    var l = GetUnionPerson(null, sabun, null);
                    if (l != null && l.a != null && l.a.Count > 0)
                    {
                        person = l.a[0];
                    }
                    else
                    {
                        person.Sabun = sabun;
                    }
                    person.InsertSabun = my.Sabun;
                    person.InsertOrgID = my.OrgID;
                    person.InsertOrgNameKor = my.OrgNameKor;
                    person.InsertOrgNameEng = my.OrgNameEng;
                    person.InsertName = my.Name;

                    PersonData.Insert(person);
                    PersonData.Save();
                }
                return person;
            }
            return null;
        }

        protected List<ApprovalPerson> GetApprovalPerson(){
            List<ApprovalPerson> list = new();
            var my = GetMe();
            try
            {
                if (DbPASSPORT != null && DbPASSPORT.Database.CanConnect() && !string.IsNullOrEmpty(my.SapDeptCode))
                {
                    StringBuilder sb = new();
                    sb.Append("exec PROC_FIND_APPROVER @DEPT_CD='");
                    sb.Append(my.SapDeptCode);
                    sb.Append("';");
                    using var command = DbPASSPORT.Database.GetDbConnection().CreateCommand();
                    WriteLog("query: " + sb.ToString());
                    command.CommandText = sb.ToString();
                    command.CommandType = CommandType.Text;
                    DbPASSPORT.Database.OpenConnection(); // DbSVISIT.Database.GetDbConnection()
                    using var reader = command.ExecuteReader();
                    if(reader.HasRows){
                        while (reader.Read()){
                            ApprovalPerson p = new(){
                                DeptCode = my.SapDeptCode,
                            };
                            for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                            {
                                if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value){
                                    var n = reader.GetName(fieldCount);
                                    var v = reader[fieldCount];
                                    if (v != null){
                                        try{
                                            // WriteLog("read data["+fieldCount+"] "+n+"="+v);
                                            if(fieldCount == 0) {
                                                p.IsInformalPartManager = v.ToString()??"";
                                            } else if(fieldCount == 1) {
                                                p.MemberID = v.ToString()??"";
                                            } else if(fieldCount == 2) {
                                                p.ConfirmStaffNo = v.ToString()??"";
                                            } else if(fieldCount == 3) {
                                                p.MemberName = v.ToString()??"";
                                            } else if(fieldCount == 4) {
                                                p.CompanyName = v.ToString()??"";
                                            } else if(fieldCount == 5) {
                                                p.Position = v.ToString()??"";
                                            } else if(fieldCount == 6) {
                                                p.Email = v.ToString()??"";
                                            } else if(fieldCount == 7) {
                                                p.IsPersonal = v.ToString()??"";
                                            } else if(fieldCount == 8) {
                                                p.MobileNo = v.ToString()??"";
                                            } else if(fieldCount == 9) {
                                                p.TelNo = v.ToString()??"";
                                            } else if(fieldCount == 10) {
                                                p.BirthDate = v.ToString()??"";
                                            } else if(fieldCount == 11) {
                                                p.ResignationDate = v.ToString()??"";
                                            } else if(fieldCount == 12) {
                                                p.CompanyRegistrationNo = v.ToString()??"";
                                            } else if(fieldCount == 13) {
                                                p.MemberType = v.ToString()??"";
                                            } else if(fieldCount == 14) {
                                                p.MemberTypeName = v.ToString()??"";
                                            } else if(fieldCount == 15) {
                                                p.DeptName = v.ToString()??"";
                                            }
                                            // WriteLog("read data: "+n.GetType()+"="+v.GetType());
                                        }catch(Exception e){
                                            WriteLog(e.ToString());
                                        }
                                    }
                                }
                            }
                            list.Add(p);
                        }
                    }
                    DbPASSPORT.Database.CloseConnection();      
                }          
            }catch(Exception e) {
                WriteLog(e.ToString());
            }finally{
                DbPASSPORT?.Database.CloseConnection();                
            }
            return list;
        }

        protected List<SeniorPerson> GetSeniorPerson()
        {
            List<SeniorPerson> list = new();
            var my = GetMe();
            try
            {
                if (DbPASSPORT != null && DbPASSPORT.Database.CanConnect() && !string.IsNullOrEmpty(my.SapDeptCode))
                {
                    StringBuilder sb = new();
                    sb.Append("exec PROC_FIND_SENIOR @DEPT_CD='");
                    sb.Append(my.SapDeptCode);
                    sb.Append("';");
                    using var command = DbPASSPORT.Database.GetDbConnection().CreateCommand();
                    WriteLog("query: " + sb.ToString());
                    command.CommandText = sb.ToString();
                    command.CommandType = CommandType.Text;
                    DbPASSPORT.Database.OpenConnection(); // DbSVISIT.Database.GetDbConnection()
                    using var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SeniorPerson p = new()
                            {
                                DeptCode = my.SapDeptCode,
                            };
                            for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                            {
                                if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value)
                                {
                                    var n = reader.GetName(fieldCount);
                                    var v = reader[fieldCount];
                                    if (v != null)
                                    {
                                        try
                                        {
                                            // WriteLog("read data["+fieldCount+"] "+n+"="+v);
                                            if (fieldCount == 0)
                                            {
                                                p.SapPositionName = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 1)
                                            {
                                                p.SapPositionCode = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 2)
                                            {
                                                p.SapDeptCode = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 3)
                                            {
                                                p.Objid = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 4)
                                            {
                                                p.MemberId = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 5)
                                            {
                                                p.MemberName = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 6)
                                            {
                                                p.CompanyName = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 7)
                                            {
                                                p.Position = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 8)
                                            {
                                                p.Email = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 9)
                                            {
                                                p.IsPersonnel = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 10)
                                            {
                                                p.MobileNo = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 11)
                                            {
                                                p.TelephoneNo = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 12)
                                            {
                                                p.BirthDate = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 13)
                                            {
                                                p.ResignationDate = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 14)
                                            {
                                                p.CompanyRegistrationNo = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 15)
                                            {
                                                p.MemberType = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 16)
                                            {
                                                p.MemberTypeName = v.ToString() ?? "";
                                            }
                                            else if (fieldCount == 17)
                                            {
                                                p.DeptName = v.ToString() ?? "";
                                            }
                                            // WriteLog("read data: "+n.GetType()+"="+v.GetType());
                                        }
                                        catch (Exception e)
                                        {
                                            WriteLog(e.ToString());
                                        }
                                    }
                                }
                            }
                            list.Add(p);
                        }
                    }
                    DbPASSPORT.Database.CloseConnection();
                }
            }
            catch (Exception e)
            {
                WriteLog(e.ToString());
            }
            finally
            {
                DbPASSPORT?.Database.CloseConnection();
            }
            return list;
        }

        protected List<ViewDuty> GetViewDudies(string? location){
            List<ViewDuty> list = new();
            var cnt = 0;
            if (DbPASSPORT != null){
                List<string> strWhere = new();
                if(!string.IsNullOrEmpty(location)){
                    strWhere.Add("LOCATION_CODE = '"+location+"'");
                }

                StringBuilder sb = new();
                sb.Append(@"SELECT * FROM dbo.VIEW_DUTIES ");
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
                // sb.Append(" OFFSET ");
                // var offset = (PageNumber -1) * PageSize;
                // sb.Append(offset);
                // sb.Append(" ROWS FETCH NEXT ");
                // sb.Append(PageSize);
                // sb.Append(" ROWS ONLY");
                var fs2 = FormattableStringFactory.Create(sb.ToString());
                WriteLog("query: " + sb.ToString());
                list = DbPASSPORT.ViewDuties
                    .FromSql(fs2)
                    .ToList();
                WriteLog("list: "+Dump(list));
            }
            return list;
        }

        protected string GetTag() => this.GetType().Name;

        protected void SetErrorMessage(string msg, object? obj = null) {
            TempData[TEMPDATA_ERROR] = msg;
            // WriteLog("TempData:" + msg);
            if (obj != null) {
                // WriteLog("TempData Object:" + Dump(obj));
                TempData[TEMPDATA_ERROR_OBJECT] = JsonSerializer.Serialize(obj);
                // WriteLog("TempData Object:" + TempData[TEMPDATA_ERROR_OBJECT]);
            }
        }
        
        protected string? GetErrorMessage() {
            string? msg = TempData[TEMPDATA_ERROR]?.ToString();
            // TempData.Remove(TEMPDATA_ERROR);
            // WriteLog("TempData:" + msg);
            return msg;
        }

        protected T? GetErrorObject<T>()
        {
            try {
                string? msg = TempData[TEMPDATA_ERROR_OBJECT]?.ToString();
                WriteLog("TempData Object:" + msg);
                if (msg != null)
                    return JsonSerializer.Deserialize<T>(msg);
            } catch (Exception e) {
                WriteLog(e.ToString());
            }
            return default;
            // if (TempData[TEMPDATA_ERROR] != null) {
            //     return TempData[TEMPDATA_ERROR];
            // }
            // return null;
        }

        /// <summary>
        /// alert popup
        /// AlertErrorMsg("error test");
        /// return RedirectToAction("Detail", new {id=1});
        /// </summary>
        /// <param name="msg"></param>
        protected void AlertErrorMsg(string msg)
        {
            // WriteLog("HttpContext:" + HttpContext.Request);
            ModelState.AddModelError("Error", msg);
        }

        /// <summary>
        /// object to json string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string Dump(Object? obj) {
            try {
                return JsonSerializer.Serialize(obj);
            } catch (Exception e) {
                WriteLog(e.ToString());
            }
            return string.Empty;
        }

        public T? _Dump<T>(string obj)
        {
            try {
                return JsonSerializer.Deserialize<T>(obj);
            } catch (Exception e) {
                WriteLog(e.ToString());
            }
            return default;
        }

        protected T? Des<T>(string obj)
        {
            try {
                return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(obj, typeof(T));
            } catch (Exception e) {
                WriteLog(e.ToString());
            }
            return default;
        }


        protected void WriteLog(string str) 
        {
            // MethodBase.GetCurrentMethod().Name;
            StackTrace stackTrace = new();
            MethodBase methodBase = stackTrace.GetFrame(1)?.GetMethod()!;
            Console.WriteLine(this.GetType().Name+">"+methodBase.Name+">"+str);
        }

        protected void WriteError(string str) 
        {
            lock (_lockObj)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // MethodBase.GetCurrentMethod().Name;
                StackTrace stackTrace = new StackTrace();
                MethodBase methodBase = stackTrace.GetFrame(1)?.GetMethod()!;
                Console.WriteLine("[CzError]"+ this.GetType().Name + ">" + methodBase.Name);
                Console.WriteLine(str);
                Console.ForegroundColor = ConsoleColor.White;
                // Console.ResetColor();
            }
        }

        protected void PrintModelSateError(){
            foreach (ModelStateEntry modelState in ViewData.ModelState.Values) {
                foreach (ModelError error in modelState.Errors) {
                    
                    WriteLog(error.ErrorMessage);
                    WriteLog(Dump(error));
                    // WriteLog(error.ToString());
                    // WriteLog(error.Exception.StackTrace);
                }
            }
        }

        protected void PrintPostFormData(){
            StackTrace stackTrace = new();
            MethodBase methodBase = stackTrace.GetFrame(1)?.GetMethod()!;
            var formData = Request.Form;
            foreach(var data in formData)
            {
                string keyName = data.Key;
                string keyValue = data.Value;
                Console.WriteLine(this.GetType().Name+">"+methodBase.Name+">formData:"+keyName+" = "+keyValue);
                // WriteLog("formData: "+keyName+" = "+keyValue);
            }
        }

        //2023.09.21 dsyoo 임직원+비상주협력사 조회
        protected dynamic GetAvailabePerson(PersonGridData? values)
        {
            var count = 0;
            List<Person> list = new();
            bool isConnected = false;
            try
            {
                if (DbSVISIT != null && DbSVISIT.Database.CanConnect())
                {
                    int offset = 0;
                    if (values != null)
                    {
                        offset = (values.PageNumber - 1) * values.PageSize; // COALESCE(a.IS_DELETED, b.PersonTypeID) as 
                    }
                    StringBuilder sb = new();
                    sb.Append(@"SELECT b.PersonID, COALESCE(a.EMPLOYEE_ID, b.sabun, 'N') as Sabun, COALESCE(a.USER_NAME, b.Name, 'N') as Name, 
                        COALESCE(a.Location, b.Location, 'N') as Location, b.CompanyID, COALESCE(b.PersonTypeID, 0) as PersonTypeID, 
                        b.OrgID as OrgID, COALESCE(a.DEPT_NAME, b.OrgNameKor, 'N') as OrgNameKor, COALESCE(b.OrgNameEng, 'N') as OrgNameEng, 
                        COALESCE(b.LevelCodeID, 3) as LevelCodeID, COALESCE(b.GradeID, 0) as GradeID, COALESCE(a.SAP_TITLE_NAME, b.GradeName) as GradeName, 
                        COALESCE(a.PASSWORD, b.Password, 'N') as Password, b.PWUpdateDate, b.BirthDate, b.Gender, COALESCE(a.MOBILE, b.Mobile) as Mobile, 
                        COALESCE(b.Tel, '') as Tel, COALESCE(a.Email, b.Email) as Email, b.ImageData, b.ImageDataHash, COALESCE(CONVERT(int, a.IS_DELETED), b.PersonStatusID, 0) as PersonStatusID, 
                        b.HomeAddr, b.HomeDetailAddr, b.HomeZipcode, COALESCE(b.IsForeigner, 0) as IsForeigner, b.Nationality, b.ImmStatusCodeID, 
                        b.ImmStartDate, b.ImmEndDate, b.IsAllowSMS, b.WorkTypeCodeID, b.CarAllowCnt, b.CarRegCnt, b.TermsPrivarcyFileData, 
                        b.TermsPrivarcyFileDataHash, b.CardIssueStatus, b.CardCreateCnt, CONVERT(CHAR(10),b.VisitorEduLastDate,23) as VisitorEduLastDate, 
                        CONVERT(CHAR(10), b.CleanEduLastDate,23) as CleanEduLastDate, CONVERT(CHAR(10),b.SafetyEduLastDate,23) as SafetyEduLastDate, 
                        CONVERT(CHAR(10),b.VisitLastDate,23) as VisitLastDate, CONVERT(CHAR(10),b.ValidDate,23) as ValidDate, b.UpdateIP, b.CardID, b.CardNo, b.IsRecTempCard, 
                        CONVERT(CHAR(10),b.TempCardRecDate,23) as TempCardRecDate, COALESCE(b.InsertSabun,'N') as InsertSabun, b.InsertName, b.InsertOrgID, 
                        b.InsertOrgNameKor, b.InsertOrgNameEng, CONVERT(CHAR(10), b.UpdateDate,23) as UpdateDate, CONVERT(CHAR(10),b.InsertDate,23) as InsertDate, 
                        COALESCE(b.IsDelete,'N') as IsDelete, b.PID, a.PORTE_ID as PorteID, a.SAP_DEPT_CODE as SapDeptCode
                        FROM (SELECT * FROM PASSPORT.dbo.V_IMS_USER_INFO ");
                    // COALESCE(a.OFFICE_TEL, b.Tel) as Tel
                    sb.Append(" WHERE USER_NAME IS NOT NULL"); //  AND IS_DELETED = 0
                    sb.Append(") a FULL OUTER JOIN (SELECT * from Person");
                    sb.Append(" WHERE IsDelete = 'N' ");
                    sb.Append(") b ON (a.EMPLOYEE_ID = b.sabun)");
                    // where
                    StringBuilder sbWhere = new();
                    bool useWhere = false;
                    if (values != null)
                    {
                        // 레벨별 권한 설정.
                        if (IsMaster() == false)
                        {
                            PersonDTO my = GetMe();
                            WriteLog("LevelCodeID:" + my.LevelCodeID);

                            // 일반관리자 : 해당 사업장의 사원정보조회 / 엑셀다운로드 Location
                            // 보안실 : 해당 사업장의 사원정보 조회 / 엑셀다운로드
                            if (useWhere)
                            {
                                sbWhere.Append(" AND ");
                            }
                            else
                            {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (a.Location = '");
                            sbWhere.Append(my.Location);
                            sbWhere.Append("' OR b.Location = '");
                            sbWhere.Append(my.Location);
                            sbWhere.Append("') ");



                        }
                        // 검색
                        if (int.Parse(values.SearchPersonTypeID) > -1)
                        { // 회원 구분
                            if (useWhere)
                            {
                                sbWhere.Append(" AND ");
                            }
                            else
                            {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            if (int.Parse(values.SearchPersonTypeID) > 1)
                            { // 2 3 비상주 관리자, 비상주 직원
                                if (int.Parse(values.SearchPersonTypeID) == 100)
                                {
                                    sbWhere.Append(" (a.EMPLOYEE_TYPE = 'C' OR a.EMPLOYEE_TYPE = 'S' OR a.EMPLOYEE_TYPE = 'U')");
                                }
                                else
                                {
                                    sbWhere.Append(" b.PersonTypeID = '");
                                    sbWhere.Append(int.Parse(values.SearchPersonTypeID));
                                    sbWhere.Append("' ");
                                }
                            }
                            else
                            { // 0 1 임직원, 상주직원
                                if (int.Parse(values.SearchPersonTypeID) == 1)
                                {
                                    sbWhere.Append(" a.EMPLOYEE_TYPE = 'C' ");// * (S:정규직, U:계약직), C: 협력직
                                }
                                else
                                {
                                    sbWhere.Append(" (a.EMPLOYEE_TYPE = 'S' OR a.EMPLOYEE_TYPE = 'U') ");
                                }
                            }
                        }

                        // if (int.Parse(values.SearchCompanyID) >-1) { // 회사
                        //     if(useWhere){
                        //         sbWhere.Append(" AND ");
                        //     }else {
                        //         sbWhere.Append(" WHERE ");
                        //         useWhere = true;
                        //     }
                        //     sbWhere.Append(" b.CompanyID = '");
                        //     sbWhere.Append(int.Parse(values.SearchCompanyID));
                        //     sbWhere.Append("' ");
                        // }
                        if (!string.IsNullOrEmpty(values.SearchCompanyName) && !values.SearchCompanyName.Equals("+") && !values.SearchCompanyName.Equals("%20"))
                        { // 회사명
                            WriteLog("[Search] CompanyName:" + values.SearchCompanyName);
                            var cid = GetCompanyID(values.SearchCompanyName);
                            // if(cid > 0){
                            if (useWhere)
                            {
                                sbWhere.Append(" AND ");
                            }
                            else
                            {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.CompanyID = '");
                            sbWhere.Append(cid);
                            sbWhere.Append("') ");
                            // }
                        }
                        if (int.Parse(values.SearchPersonStatusID) > -1)
                        { // 재직 상태
                            // WriteLog("[Search] PersonStatusID:" + values.SearchPersonStatusID);
                            if (useWhere)
                            {
                                sbWhere.Append(" AND ");
                            }
                            else
                            {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.PersonStatusID = '");
                            sbWhere.Append(int.Parse(values.SearchPersonStatusID));
                            sbWhere.Append("' OR a.IS_DELETED = '");
                            sbWhere.Append(int.Parse(values.SearchPersonStatusID));
                            sbWhere.Append("') ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchOrgName) && !values.SearchOrgName.Equals("+") && !values.SearchOrgName.Equals("%20"))
                        { // 부서명
                            WriteLog("[Search] OrgName:" + values.SearchOrgName);
                            if (useWhere)
                            {
                                sbWhere.Append(" AND ");
                            }
                            else
                            {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.OrgNameEng = '");
                            sbWhere.Append(values.SearchOrgName);
                            sbWhere.Append("' OR ");
                            sbWhere.Append(" b.OrgNameKor = '");
                            sbWhere.Append(values.SearchOrgName);
                            sbWhere.Append("' OR a.DEPT_NAME = '");
                            sbWhere.Append(values.SearchOrgName);
                            sbWhere.Append("') ");
                        }
                        if (!string.IsNullOrEmpty(values.SearchName) && !values.SearchName.Equals("+") && !values.SearchName.Equals("%20"))
                        { // 이름
                            WriteLog("[Search] Name:" + values.SearchName);
                            if (useWhere)
                            {
                                sbWhere.Append(" AND ");
                            }
                            else
                            {
                                sbWhere.Append(" WHERE ");
                                useWhere = true;
                            }
                            sbWhere.Append(" (b.Name = '");
                            sbWhere.Append(values.SearchName);
                            sbWhere.Append("' OR a.USER_NAME = '");
                            sbWhere.Append(values.SearchName);
                            sbWhere.Append("') ");
                        }
                    }
                    //// 사번으로 가져오기
                    //if (!string.IsNullOrEmpty(sabun))
                    //{
                    //    useWhere = true;
                    //    sbWhere.Clear();
                    //    sbWhere.Append(" WHERE (a.EMPLOYEE_ID = '");
                    //    sbWhere.Append(sabun);
                    //    sbWhere.Append("' OR b.sabun = '");
                    //    sbWhere.Append(sabun);
                    //    sbWhere.Append("') ");

                    //    if (!string.IsNullOrEmpty(passowrd))
                    //    {
                    //        sbWhere.Append(" AND (a.PASSWORD = '");
                    //        sbWhere.Append(passowrd);
                    //        sbWhere.Append("' OR b.Password = '");
                    //        sbWhere.Append(passowrd);
                    //        sbWhere.Append("') ");
                    //    }
                    //}

                    if (useWhere)
                    {
                        sb.Append(sbWhere);
                    }
                    // order by
                    sb.Append(" ORDER BY a.USER_NAME ");
                    // offset
                    if (values != null)
                    {
                        if (values.PageSize > 0)
                        { // 0: 전체 가져오기
                            sb.Append(" OFFSET ");
                            sb.Append(offset);
                            sb.Append(" ROWS FETCH NEXT ");
                            sb.Append(values.PageSize);
                            sb.Append(" ROWS ONLY ");
                        }
                        if (values.PageSize > 0)
                        {
                            StringBuilder sb2 = new();
                            sb2.Append(@"select count(*) as cnt
                                from (select * from PASSPORT.dbo.V_IMS_USER_INFO where USER_NAME IS NOT NULL) a 
                                full outer join (select * from person where IsDelete = 'N') b on (a.EMPLOYEE_ID = b.sabun)");
                            if (useWhere)
                            {
                                sb2.Append(sbWhere);
                            }
                            var fs1 = FormattableStringFactory.Create(sb2.ToString());
                            count = DbSVISIT.Database.SqlQuery<int>(fs1).ToList()[0];
                            WriteLog("count:" + count);
                        }
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
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Person p = new();
                            for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                            {
                                if (reader.GetName(fieldCount) != null && reader[fieldCount] != null && reader[fieldCount] != DBNull.Value)
                                {
                                    var n = reader.GetName(fieldCount);
                                    var v = reader[fieldCount];
                                    if (v != null)
                                    {
                                        try
                                        {
                                            // WriteLog("read data: "+n+"="+v);
                                            // WriteLog("read data: "+n.GetType()+"="+v.GetType());
                                            if (n.Equals("UpdateDate") || n.Equals("InsertDate") || n.Equals("PWUpdateDate")
                                                || n.Equals("VisitorEduLastDate") || n.Equals("CleanEduLastDate") || n.Equals("VisitLastDate")
                                                || n.Equals("ValidDate") || n.Equals("TempCardRecDate") || n.Equals("SafetyEduLastDate"))
                                            {
                                                var t = v.ToString();
                                                if (!string.IsNullOrEmpty(t))
                                                {
                                                    v = DateTime.Parse(t);
                                                }
                                            }
                                            typeof(Person).GetProperty(n)?.SetValue(p, v);
                                        }
                                        catch (Exception e)
                                        {
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
            }
            catch (Exception e)
            {
                WriteLog(e.ToString());
            }
            finally
            {
                DbSVISIT?.Database.CloseConnection();
            }
            return new { a = list, b = count, c = isConnected };
        }
        #region 2023.10.30 dsyoo 문자열에서 특정 문자 삭제하기 ex)122-86-11111 => 1228611111
        protected string RemoveCharFromString(string input, char charItem)
        {
            int indexOfChar = input.IndexOf(charItem);
            if (indexOfChar < 0)
            {
                return input;
            }
            return RemoveCharFromString(input.Remove(indexOfChar, 1), charItem);
        }
        #endregion
    }

    public class  FileData {
        public string Meta { get; set; } = string.Empty;
        public byte[]? Hash { get; set; }
        public int ErrorCode { get; set; } = 1;
        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
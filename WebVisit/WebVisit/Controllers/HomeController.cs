using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebVisit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Dynamic;
using WebVisit.Utilities;
using System.Text.Json;
using SapNwRfc;
using System.Text;

namespace WebVisit.Controllers
{
    public class HomeController : BaseController
    {
        private Repository<Notice>? NoticeData { get; set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IStringLocalizer<HomeController> localizer):base(logger, configuration, localizer){}

        protected override void Init(){
            // VisitSession = new( HttpContext);
            if (DbSVISIT != null) {
                NoticeData ??= new Repository<Notice>(DbSVISIT);
                PersonData ??= new Repository<Person>(DbSVISIT);
                CommonCodeData ??= new Repository<CommonCode>(DbSVISIT);
                LoadLastNoticeList();
            }
        }

        private void LoadLastNoticeList(){
            WriteLog("load notice 1");
            if(NoticeData != null)
            {
                var today = DateTime.Now;
                var options = new QueryOptions<Notice>
                {
                    PageNumber = 1,
                    PageSize = 3,
                    Where = a=> a.IsDelete == "N" && a.StartDate <= today && a.EndDate >= today ,
                    OrderByDirection = "desc",
                    OrderBy = a => a.NoticeID,
                };
                List<Notice> notices = (List<Notice>) NoticeData.List(options);
                ViewBag.notices= notices;
            }
        }

        public IActionResult Index()
        {
            if (!IsAccessAPI()){
            }
            WriteLog("TempData:"+TempData["message"]);
            WriteLog("master pwd: " + "abc1234$".SHA256Encrypt()+"/"+TempData["msg"]);
            // TempData["UI.AlertMsg"] = "alert 테스트22";
            IndexViewModel model = new(){
                CodeLocation = GetCommonCodes((int)VisitEnum.CommonCode.Location),
                CodeVisitPurpose = GetCommonCodes((int)VisitEnum.CommonCode.VisitPurpose),
                CodePlace = GetCommonCodes((int)VisitEnum.CommonCode.Place),
                CodeGenderType = GetCommonCodes((int)VisitEnum.CommonCode.GenderType),
                CodeImportPurpose = GetCommonCodes((int)VisitEnum.CommonCode.ImportPurpose),
                CodeCarryItem = GetCommonCodes((int)VisitEnum.CommonCode.CarryItem),
                CodeUnit = GetCommonCodes((int)VisitEnum.CommonCode.Unit),
            };
            PersonDTO? personDTO = GetMe();
            if (personDTO != null){
                WriteLog("personDTO:" + Dump(personDTO));
            }

            return View(model);
        }

        /// <summary>
        /// SSO Login
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="user_pass"></param>
        /// <param name="sabun"></param>
        /// <param name="epcompanycode"></param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SSOLogin(String user_id, String user_pass, string sabun, string epcompanycode, string deptid)
        {
            if (!IsAccess())
            {
                return RedirectToAction("Index", new { culture = GetLang() });
            }
            if (PersonData != null && !string.IsNullOrEmpty(sabun))
            {
                string UserId = DecMD5(sabun);
                string UserPwd = DecMD5(user_pass);
                var m = GetUnionPerson(values: null, sabun: UserId, passowrd: UserPwd);
                Person person = new();
                if(m != null && m.a != null && m.a.Count > 0){
                    person = m.a[0];
                } else {
                    person = null;
                }
                if (person != null) { // && person.PersonID >0
                    if(person.PersonTypeID == 2) {
                        var company = GetCompanyByID(person.CompanyID??0);
                        WriteLog("company:" + Dump(company));
                        if(company != null && company.CompanyStatus != 1){
                            TempData["errMsg"] = "Incorrect username or password.";
                            return RedirectToAction("Index", new { culture = GetLang()});
                        }
                    }
                    TempData["errMsg"] = "";
                    // 유효기간: 정규직일경우 2099년 12월 31일, 임시직은 상황에 맞게 - ValidDate
                    // set session
                    VisitSession?.SetPerson(person);
                    VisitSession?.SetLang("ko");
                    // person/myinfo
                    return RedirectToAction("MyInfo", "Person", new { culture = "ko" });
                } else {
                    TempData["errMsg"] = "Incorrect username or password.";
                    return RedirectToAction("Index", new { culture = GetLang()});
                }
            }
            return RedirectToAction("Index", new { culture = GetLang() });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserLang">ko | en</param>
        /// <param name="UserId"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(String UserLang, String UserId, String UserPwd)
        {
            // PrintPostFormData();
            if (!IsAccess()){
                return RedirectToAction("Index", new { culture = GetLang()});
            }
            if(PersonData!= null){
                // select personid, name, sabun, password, isdelete from person where sabun='master' and password='8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'
                // WriteLog("UserLang: " + UserLang + ", UserId: " + UserId + ", UserPwd: " + UserPwd+" / "+UserPwd.SHA256Encrypt());
                var m = GetUnionPerson(values:null, sabun: UserId, passowrd: UserPwd.SHA256Encrypt());
                Person person = new();
                if(m != null && m.a != null && m.a.Count > 0){
                    person = m.a[0];
                } else {
                    person = null;
                }
                // var person = PersonData.Get(new QueryOptions<Person>{
                //         Where = a => a.IsDelete == "N" && a.Sabun == UserId && a.Password == UserPwd.SHA256Encrypt() && a.LevelCodeID != (int)VisitEnum.LevelType.PartnerNonresident,
                //     }) ?? new Person();
                // WriteLog("m:" + Dump(m));
                // WriteLog("person:" + Dump(person));
                if (person != null) { // && person.PersonID >0
                    if(person.PersonTypeID == 2) {
                        var company = GetCompanyByID(person.CompanyID??0);
                        WriteLog("company:" + Dump(company));
                        if(company != null && company.CompanyStatus != 1){
                            TempData["errMsg"] = "Incorrect username or password.";
                            return RedirectToAction("Index", new { culture = GetLang()});
                        }
                    }
                    TempData["errMsg"] = "";
                    // 유효기간: 정규직일경우 2099년 12월 31일, 임시직은 상황에 맞게 - ValidDate
                    // set session
                    VisitSession?.SetPerson(person);
                    VisitSession?.SetLang(UserLang);
                    // person/myinfo
                    return RedirectToAction("MyInfo", "Person", new { culture = UserLang });
                    // return RedirectToAction("List", "Person", new { culture = UserLang });
                } else {
                    TempData["errMsg"] = "Incorrect username or password.";
                    return RedirectToAction("Index", new { culture = GetLang()});
                }
            }
            return RedirectToAction("Index", new { culture = GetLang()});
        }

        [HttpGet]
        public async Task<IActionResult> TTAsync(string msgType="SMS", int idx=1){
            WriteLog("ExpAsync");
            if(IsAccessAPI() == true) {
                // 문자, 이메일 보내기
                // await SendMessageAsync("SMS", 1, "", "출입관리시스템", "010-4610-0915", "이윤댕", "상우");
                // await SendMessageAsync("MAIL", 24015, "", "출입관리시스템", "choco@conoz.net", "심청이");

                // 인코딩 / 디코딩 
                // '김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1'
                // '98dee76cd19ba651dc5879d5ee75ed919bad54d8156137f49d189a7a259017d8762bac3789cb17b924b749a1d1225f1c3b44192799eee067'
                // string value = EncMD5("김동욱^51000504^donguk.kim1@dbhitekdev.com^2^1");
                // WriteLog("EncMD5:" + value);
                // string value2 = DecMD5("98dee76cd19ba651dc5879d5ee75ed919bad54d8156137f49d189a7a259017d8762bac3789cb17b924b749a1d1225f1c3b44192799eee067");
                // WriteLog("DecMD5:" + value2);

                // 결재 PORTE
                // var rs = await SendPorte();                
                // WriteLog("SendPorte:" + rs);
                // return View(rs);
                // rs= "<html><form name=\"form\" method=\"post\" action=\"https://approval.dbhitekdev.com/eNovator/GSWF/WebPage/Legacy/Legacyinterface2.aspx?AppParamID=19410&FixApprover=\"><input type=submit name=sb value='send' /></form></html>";
                // return Content(rs, "text/html");

                // PORTE ID 찾기
                // var porteID = GetPorteID("10006793");
                // if(string.IsNullOrEmpty(porteID)) { // PORTE ID 없음
                //     // todo: PORTE ID 있을 때 처리
                // } else { // PORTE ID 있음
                //     // todo: PORTE ID 있을 때 처리
                // }
                // WriteLog("porteID: 1)" + porteID);
                // porteID = GetPorteID("1000679355");
                // WriteLog("porteID: 2)" + porteID);

                // SSO 암호화
                /*
                <sso 암호화>    
                id: 87757f4ec602228e4304341b237e3d393b44192799eee067
                pwd: 8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg=
                sabun: cb175cd6d78896ea5bdd864391be889c3b44192799eee067
                epcompanycode: a2bf92b28643ad10dc13d6f817db8ef33b44192799eee067
                deptid: 537c3b5278e64d2bb15172977fa71ef528d7744393d487343b44192799eee067
                ssoid: cb175cd6d78896ea5bdd864391be889c3b44192799eee067
                
                <sso 복호화>
                id: eunji86
                pwd: 8l7Rc9KNTTJMd07DYycb8hPZVjC7U9AI9o66Op7fibg=
                sabun: P9000058
                ssoid: eunji86
                epcompanycode: DG02
                deptid: DG02_DA20001
                */
                string a = DecMD5("87757f4ec602228e4304341b237e3d393b44192799eee067");
                WriteLog("[SSO] id:" + a);
                string b = DecMD5("cb175cd6d78896ea5bdd864391be889c3b44192799eee067");
                WriteLog("[SSO] sabun:" + b);
            }
            // return View();
            return new EmptyResult();
        }

        [HttpGet]
        public async Task<IActionResult> IPAsync()
        {
            return Content(GetUserIP(), "text/html");
        }

        [HttpGet]
        public async Task<IActionResult> RFCAsync(string itemCode="MEP0078R", string location="2000", string prcode="1000150932")
        {
            string connectionString = "AppServerHost=10.145.2.31; SystemNumber=00; User=S0000014; Password=informatica9; Client=100; Language=EN; PoolSize=5; Trace=8";
            StringBuilder sb = new();
            sb.Append("CCC SAP TEST ZZZ\r\n");
            SapConnection? connection = null;
            try{
                connection = new SapConnection(connectionString);
                connection.Connect();
                if(connection.IsValid){
                    // WriteLog("connection:"+Dump(connection));
                    sb.Append("result 1\r\n");
                    using var someFunction = connection.CreateFunction("Z_MM_VISITOR_VENDOR_CHECK");
                    var result = someFunction.Invoke<RFCZMMVisitorVendorCheckResult>(new RFCZMMVisitorVendorCheckParameters
                    {
                        I_WERKS=location,
                        I_MATNR=itemCode,
                    });

                    // {"E_LIFNR":"","E_SUBRC":"E","E_MESSAGE":"\uC800\uC7A5\uB41C \uBCA4\uB354\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4."}
                    WriteLog("result:"+Dump(result));
                    WriteLog("result string:"+result.E_MESSAGE.ToString());
                    // msg = Dump(result);
                    sb.Append(Dump(result)); //result.E_MESSAGE.ToString();
                    sb.Append("\r\n");
                    sb.Append(result.E_MESSAGE);
                    sb.Append("\r\n\r\n");

                    sb.Append("result 2\r\n");
                    using var someFunction2 = connection.CreateFunction("Z_MM_VISITOR_PR_CHECK");
                    
                    // BANFN="1000150932",
                    var result2 = someFunction2.Invoke<RFCZMMVisitorPRCheckResult>(new RFCZMMVisitorPRCheckParameters
                    {
                        BANFN=prcode,
                    });
                    // 1000236674
                    // Test="1000150932" 1000169868
                    // {"E_LIFNR":"","E_SUBRC":"E","E_MESSAGE":"\uC800\uC7A5\uB41C \uBCA4\uB354\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4."}
                    WriteLog("result2:"+Dump(result2));
                    sb.Append(Dump(result2));
                    sb.Append("\r\n");
                    // WriteLog("result string:"+result2.E_MESSAGE.ToString());
                    // msg = Dump(result);
                    // msg = result.E_MESSAGE.ToString();
                }

                // SapServer.InstallGenericServerFunctionHandler(connectionString, (connection, function) => {
                //     var attributes = connection.GetAttributes();
                //     // Table:ZSMM0097 Table Name:[MM]VISITOR_PR_Information Structure Package: ZMMD
                //     switch(function.GetName()){
                //         case "ZSMM0097":
                //             var parameters = function.GetParameters<RFCZMMVisitorPRCheckParameters>();
                //             var result = new RFCZMMVisitorPRCheckResult();
                //             function.SetResult(result);
                //             WriteLog("restul ...");
                //             break;
                //     }
                // });
            }catch(Exception e){
                sb.Append(e.ToString());
                WriteError(e.ToString());
            }finally{
                connection?.Disconnect();
            }
            return Content(sb.ToString(), "text/plain;charset=utf-8");
        }

        [HttpPost]
        public IActionResult Logout() {
            WriteLog("Logout...");
            VisitSession?.SetPerson(null);            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VisitReg(){
            //방문신청: mode VisitorType VisitApplyType Location VisitStartDate VisitEndDate ContactSabun ContactName	ContactOrgID	ContactOrgNameKor ContactOrgNameEng PlaceCodeID VisitPurposeCodeID
            //방문신청회원 arr : Name BirthDate Gender Mobile CompanyName ckVisitorEdu ckCleanEdu CarNo
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Location">2000 부천, 3000 상우</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CleanRoomTest(string Location, string name, string companyName){
            // PopCleanTestB PopCleanTestS
            ViewBag.name = name;
            ViewBag.companyName = companyName;
            if (Location.Equals("2000")){
                return View("PopCleanTestB");
            } else {
                return View("PopCleanTestS");
            }
        }

        // ansCheck 
        /// <summary>
        /// </summary>
        /// <param name="ansCheck">
        /// [{"question":1,"answer":"4"},{"question":2,"answer":"3"},{"question":3,"answer":"1;2"},{"question":4,"answer":"1;5"},{"question":5,"answer":"4;5"},{"question":6,"answer":"4"},{"question":7,"answer":"3"},{"question":8,"answer":"2"},{"question":9,"answer":"1"},{"question":10,"answer":"4"}]
        /// </param>
        /// <param name="CLEANROOM_TRAINING_SITE">CLEANROOM_TRAINING_SITE:01003(상우 3000), 01002(부천 2000)</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult? CleanRoomAnswer(string ansCheck, string CLEANROOM_TRAINING_SITE) {
            // WriteLog("ansCheck:"+ansCheck+", CLEANROOM_TRAINING_SITE:"+CLEANROOM_TRAINING_SITE);
            var cleanRoomAnswerList = JsonSerializer.Deserialize<IList<CleanRoomAnswer>>(ansCheck);
            WriteLog("cleanRoomAnswerList:"+Dump(cleanRoomAnswerList));

            CleanRoomAnswerResponse cleanRoomAnswerResponse = new();
            List<CleanRoomAnswer> cleanRoomCorrectAnswers = new();
            int correctAnswerCount = 0;
            if(CLEANROOM_TRAINING_SITE.Equals("01003")) {
                // 상우
                cleanRoomCorrectAnswers = new()
                {
                    new CleanRoomAnswer()
                    {
                        question = 1,
                        answer = "3",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 2,
                        answer = "1",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 3,
                        answer = "1;4;5",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 4,
                        answer = "1;2;5",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 5,
                        answer = "4;5",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 6,
                        answer = "3",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 7,
                        answer = "2",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 8,
                        answer = "2",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 9,
                        answer = "4",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 10,
                        answer = "4",
                    },
                };
            } else if(CLEANROOM_TRAINING_SITE.Equals("01002")) {
                // 부천
                cleanRoomCorrectAnswers = new()
                {
                    new CleanRoomAnswer()
                    {
                        question = 1,
                        answer = "3",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 2,
                        answer = "3",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 3,
                        answer = "4",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 4,
                        answer = "4",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 5,
                        answer = "4",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 6,
                        answer = "3",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 7,
                        answer = "4",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 8,
                        answer = "2",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 9,
                        answer = "4",
                    },
                    new CleanRoomAnswer()
                    {
                        question = 10,
                        answer = "1",
                    },
                };
            }
            if(cleanRoomCorrectAnswers.Count > 0 ){
                var i = 0;
                foreach(var m in cleanRoomCorrectAnswers){
                    // cleanRoomAnswerList
                    if(cleanRoomAnswerList != null && cleanRoomAnswerList.Count > i) {
                        CleanRoomAnswer? a = cleanRoomAnswerList[i];
                        if(m.question == a.question && m.answer != null && m.answer.Equals(a.answer)) {
                            correctAnswerCount++;
                        }
                    }                    
                    i++;
                }
            }
            cleanRoomAnswerResponse.correctAnswerCount = correctAnswerCount;
            if (correctAnswerCount >= 8) {
                cleanRoomAnswerResponse.examPass = "success";
            }
            WriteLog("cleanRoomAnswerResponse:" + Dump(cleanRoomAnswerResponse));
            return Json(cleanRoomAnswerResponse);
            // return new EmptyResult();
        }
    }

    public class CleanRoomAnswerResponse
    {
        public string examPass { get; set; } = "fail"; // fail | success
        public int correctAnswerCount { get; set; } = 0;
    }

    public class CleanRoomAnswer
    {
        public int? question { get; set; }
        public string? answer { get; set; }
    }

}
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Pomelo.EntityFrameworkCore.MySql;
// using Microsoft.AspNetCore.Localization;
// using System.Globalization;

using WebVisit.Models;
using WebVisit.Models.DomainModels.MySQL;
using WebVisit.Models.DomainModels.MySQL;
using WebVisit.Models.DomainModels.LPR;
using WebVisit.Models.DomainModels.Passport;
using WebVisit.Models.DomainModels.S1Access;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

// Add services to session - AddControllersWithViews 이전에 선언되어야 함.
builder.Services.AddMemoryCache();

// builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    // options.IdleTimeout = TimeSpan.FromSeconds(60 * 60 * 24); // default 20 분
    options.IdleTimeout = TimeSpan.FromHours(24); // 24시간, default 20 분
    options.Cookie.HttpOnly = false; // default true, allows client-side script to access the cookie
    options.Cookie.IsEssential = true; // default false, GDPR
});

// Add services to the container. add MVC services
builder.Services.AddControllersWithViews(); 

// Add EF Core DI
// CONOZ
builder.Services.AddDbContext<CzSVISITContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CzSVISITContext")));
builder.Services.AddDbContext<CzS1ACCESSContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CzS1ACCESSContext")));
builder.Services.AddDbContext<CzPASSPORTContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CzPASSPORTContext")));
builder.Services.AddDbContext<CzLPRContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CzLPRContext")));
builder.Services.AddDbContext<CzMySQLWebVisitContext>(options =>
    {
        try{
            var connectionString = builder.Configuration.GetConnectionString("CzMySQLWebVisitContext");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }catch (Exception){}
    });

// 시큐이데아 개발 서버
builder.Services.AddDbContext<SVISITContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SVISITContext")));
builder.Services.AddDbContext<S1ACCESSContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("S1ACCESSContext")));
builder.Services.AddDbContext<PASSPORTContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PASSPORTContext")));

// DB - 개발서버
builder.Services.AddDbContext<DBSVISITDevContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBSVISITDevContext")));
builder.Services.AddDbContext<DBS1ACCESSDevContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBS1ACCESSDevContext")));
builder.Services.AddDbContext<DBPASSPORTDevContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBPASSPORTDevContext")));
builder.Services.AddDbContext<DBLPRDevContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBLPRDevContext")));
builder.Services.AddDbContext<DBMySQLWebVisitDevContext>(options =>
    {
        try{
            var connectionString = builder.Configuration.GetConnectionString("DBMySQLWebVisitDevContext");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }catch (Exception){}
    });

// DB - 운영서버
builder.Services.AddDbContext<DBSVISITContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBSVISITContext")));
builder.Services.AddDbContext<DBS1ACCESSContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBS1ACCESSContext")));
builder.Services.AddDbContext<DBPASSPORTContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBPASSPORTContext")));
builder.Services.AddDbContext<DBLPRContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBLPRContext")));
builder.Services.AddDbContext<DBMySQLWebVisitContext>(options =>
    {
        try{
            var connectionString = builder.Configuration.GetConnectionString("DBMySQLWebVisitContext");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }catch (Exception){}
    });

builder.Services.AddHttpContextAccessor();

// po for localize
builder.Services.AddPortableObjectLocalization(options => options.ResourcesPath = "Localization");

builder.Services
    .Configure<RequestLocalizationOptions>(options => options
        .AddSupportedCultures("ko", "en")
        .AddSupportedUICultures("ko", "en"));

builder.Services
    .AddRazorPages()
    .AddViewLocalization();

// javascript 에서 한글 깨지지 않게 하기 위한 처리
builder.Services.AddSingleton<HtmlEncoder>(
     HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization();

// for session. - map route 이전에 호출. 
app.UseSession();

app.UseRequestLocalization();

app.MapRazorPages();

// app.MapControllers();

// app.MapControllerRoute(
//     name: "page_sort",
//     pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");

// app.MapControllerRoute(
//     name: "page_sort2",
//     pattern: "{controller}/{action}/{pagenumber}/{pagesize}/{sortfield}/{sortdirection}/{searchtype?}/{searchkeyword?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.Run();

using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class BaseSVISITContext : DbContext {
        public BaseSVISITContext(DbContextOptions options): base(options) {
            Console.WriteLine("init BaseSVISITContext");
        }
        public DateTime Convert<T>(T a, string prm2) => throw new InvalidOperationException();

        // Table Name: PageInfos, CommonCode ...
        public DbSet<Menu> Menu { get; set; } = null!;
        public DbSet<MenuHistory> MenuHistory { get; set; } = null!;
        public DbSet<Level> Level { get; set; } = null!;
        public DbSet<LevelHistory> LevelHistory { get; set; } = null!;
        public DbSet<MenuLevel> MenuLevel { get; set; } = null!;
        public DbSet<MenuLevelHistory> MenuLevelHistory { get; set; } = null!;
        public DbSet<Approval> Approval { get; set; } = null!;
        public DbSet<ApprovalHistory> ApprovalHistory { get; set; } = null!;
        public DbSet<ApprovalLine> ApprovalLine { get; set; } = null!;
        public DbSet<ApprovalLineHistory> ApprovalLineHistory { get; set; } = null!;
        public DbSet<CommonCode> CommonCode { get; set; } = null!;
        public DbSet<CommonCodeHistory> CommonCodeHistory { get; set; } = null!;
        public DbSet<Holiday> Holiday { get; set; } = null!;
        public DbSet<Notice> Notice { get; set; } = null!;

        public DbSet<Company> Company { get; set; } = null!;
        public DbSet<CompanyHistory> CompanyHistory { get; set; } = null!;
        public DbSet<PasswordHistory> PasswordHistory { get; set; } = null!;
        public DbSet<Person> Person { get; set; } = null!;
        public DbSet<VisitPerson> VisitPerson { get; set; } = null!;
        public DbSet<VisitApply> VisitApply { get; set; } = null!;
        public DbSet<VisitApplyHistory> VisitApplyHistory { get; set; } = null!;

        public DbSet<VisitApplyPerson> VisitApplyPerson { get; set; } = null!;
        public DbSet<VisitApplyPersonHistory> VisitApplyPersonHistory { get; set; } = null!;

        public DbSet<SMS> SMS { get; set; } = null!;
        public DbSet<CarryItemApply> CarryItemApply { get; set; } = null!;
        public DbSet<CarryItemApplyHistory> CarryItemApplyHistory { get; set; } = null!;
        public DbSet<CarryItemInfo> CarryItemInfo { get; set; } = null!;
        public DbSet<CarryItemInfoHistory> CarryItemInfoHistory { get; set; } = null!;
        public DbSet<CarCardIssueApply> CarCardIssueApply { get; set; } = null!;
        public DbSet<CarCardIssueApplyHistory> CarCardIssueApplyHistory { get; set; } = null!;
        public DbSet<CardIssueApply> CardIssueApply { get; set; } = null!;
        public DbSet<CardIssueApplyHistory> CardIssueApplyHistory { get; set; } = null!;

        public DbSet<TempCardIssueApply> TempCardIssueApply { get; set; } = null!;
        public DbSet<TempCardIssueApplyHistory> TempCardIssueApplyHistory { get; set; } = null!;
        public DbSet<WorkApply> WorkApply { get; set; } = null!;
        public DbSet<WorkApplyAttachFile> WorkApplyAttachFile { get; set; } = null!;
        public DbSet<WorkApplyHistory> WorkApplyHistory { get; set; } = null!;
        public DbSet<WorkVisitApply> WorkVisitApply { get; set; } = null!;
        public DbSet<WorkVisitApplyHistory> WorkVisitApplyHistory { get; set; } = null!;
        public DbSet<WorkVisitPersonApply> WorkVisitPersonApply { get; set; } = null!;
        public DbSet<WorkVisitPersonApplyHistory> WorkVisitPersonApplyHistory { get; set; } = null!;
        public DbSet<SafetyEdu> SafetyEdu { get; set; } = null!;
        public DbSet<SafetyEduHistory> SafetyEduHistory { get; set; } = null!;
        public DbSet<SafetyEduApply> SafetyEduApply { get; set; } = null!;
        public DbSet<SafetyEduApplyHistory> SafetyEduApplyHistory { get; set; } = null!;
        public DbSet<SafetyViolationReason> SafetyViolationReason { get; set; } = null!;
        public DbSet<SafetyViolationReasonHistory> SafetyViolationReasonHistory { get; set; } = null!;
        public DbSet<SafetyViolation> SafetyViolation { get; set; } = null!;
        public DbSet<SafetyViolationHistory> SafetyViolationHistory { get; set; } = null!;
        public DbSet<SafetyViolationPerson> SafetyViolationPerson { get; set; } = null!;
        public DbSet<SafetyViolationPersonHistory> SafetyViolationPersonHistory { get; set; } = null!;
        public DbSet<BlackList> BlackList { get; set; } = null!;
        public DbSet<BlackListHistory> BlackListHistory { get; set; } = null!;

        public DbSet<ExportImport> ExportImport { get; set; } = null!;
        public DbSet<ExportImportHistory> ExportImportHistory { get; set; } = null!;
        public DbSet<ExportImportItem> ExportImportItem { get; set; } = null!;
        public DbSet<ExportImportItemHistory> ExportImportItemHistory { get; set; } = null!;
        public DbSet<NotebookSelfApproval> NotebookSelfApproval { get; set; } = null!;
        public DbSet<NotebookSelfApprovalHistory> NotebookSelfApprovalHistory { get; set; } = null!;

        public DbSet<MealTerm> MealTerm { get; set; } = null!;
        public DbSet<MealSchedule> MealSchedule { get; set; } = null!;
        public DbSet<MealPrice> MealPrice { get; set; } = null!;
        public DbSet<MealAccess> MealAccess { get; set; } = null!;
        public DbSet<MealLog> MealLog { get; set; } = null!;
        public DbSet<PorteLog> PorteLog { get; set; } = null!;

        // public DbSet<PageInfo> PageInfos { get; set; } = null!;        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
            modelBuilder.UseCollation("Korean_Wansung_CI_AS");
            
            modelBuilder.ApplyConfiguration(new ConfigurePerson());
            modelBuilder.ApplyConfiguration(new ConfigureMenu());
            modelBuilder.ApplyConfiguration(new ConfigureLevel());
            modelBuilder.ApplyConfiguration(new ConfigureMenuLevel());
            modelBuilder.ApplyConfiguration(new ConfigureCommonCode());
            modelBuilder.ApplyConfiguration(new ConfigureMaterial());
            modelBuilder.ApplyConfiguration(new ConfigureHoliday());
            modelBuilder.ApplyConfiguration(new ConfigureMealTerm());
            modelBuilder.ApplyConfiguration(new ConfigureMealSchedule());
            modelBuilder.ApplyConfiguration(new ConfigureMealPrice());
            
            // 초기 data seed
            // modelBuilder.Entity<PageInfo>().HasData(
            //     new PageInfo {
            //         PageID = 1,
            //         PageNameKor = "임직원 정보관리"
            //     },
            //     new PageInfo {
            //         PageID = 2,
            //         PageNameKor = "업체 정보관리"
            //     }
            // );
        }
    }
}
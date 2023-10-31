using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport
{
    public class BasePASSPORTContext : DbContext
    {
        public BasePASSPORTContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("init BasePASSPORTContext");
        }
        public DateTime Convert<T>(T a, string prm2) => throw new InvalidOperationException();

        public virtual DbSet<Accessevent> Accessevents { get; set; }

        public virtual DbSet<Approverposition> Approverpositions { get; set; }

        public virtual DbSet<Dblocation> Dblocations { get; set; }

        public virtual DbSet<Dblocationname2code> Dblocationname2codes { get; set; }

        public virtual DbSet<Envsetting> Envsettings { get; set; }

        public virtual DbSet<GViZthr0100> GViZthr0100s { get; set; }

        public virtual DbSet<GVwTsrVendor> GVwTsrVendors { get; set; }

        public virtual DbSet<Hrperson> Hrpeople { get; set; }

        public virtual DbSet<Hrphoto> Hrphotos { get; set; }

        public virtual DbSet<Interfacelog> Interfacelogs { get; set; }

        public virtual DbSet<MailMessage> MailMessages { get; set; }

        public virtual DbSet<MdmInstalled> MdmInstalleds { get; set; }

        public virtual DbSet<MdmRaw> MdmRaws { get; set; }

        public virtual DbSet<MtsAtalkMsg> MtsAtalkMsgs { get; set; }

        public virtual DbSet<MtsAtalkMsgLog> MtsAtalkMsgLogs { get; set; }

        public virtual DbSet<MtsMmsContent> MtsMmsContents { get; set; }

        public virtual DbSet<MtsMmsContentsLog> MtsMmsContentsLogs { get; set; }

        public virtual DbSet<MtsMmsMsg> MtsMmsMsgs { get; set; }

        public virtual DbSet<MtsMmsMsgLog> MtsMmsMsgLogs { get; set; }

        public virtual DbSet<MtsSmsMsg> MtsSmsMsgs { get; set; }

        public virtual DbSet<MtsSmsMsgLog> MtsSmsMsgLogs { get; set; }

        public virtual DbSet<Nonregidentuser> Nonregidentusers { get; set; }

        public virtual DbSet<Sapitemlist> Sapitemlists { get; set; }

        public virtual DbSet<SmsMessage> SmsMessages { get; set; }

        public virtual DbSet<TemplateMail> TemplateMails { get; set; }

        public virtual DbSet<TemplateSm> TemplateSms { get; set; }

        public virtual DbSet<TmpSapitemlist> TmpSapitemlists { get; set; }

        public virtual DbSet<VGateBirthday> VGateBirthdays { get; set; }

        public virtual DbSet<VImsOrgInfo> VImsOrgInfos { get; set; }

        public virtual DbSet<VImsUserInfo> VImsUserInfos { get; set; }

        public virtual DbSet<ViZtad001> ViZtad001s { get; set; }

        public virtual DbSet<ViZthr0100> ViZthr0100s { get; set; }

        public virtual DbSet<ViewAccesseventList> ViewAccesseventLists { get; set; }

        public virtual DbSet<ViewApproverList> ViewApproverLists { get; set; }

        public virtual DbSet<ViewCardPerson> ViewCardPeople { get; set; }

        public virtual DbSet<ViewDuty> ViewDuties { get; set; }

        public virtual DbSet<ViewPerson> ViewPeople { get; set; }

        public virtual DbSet<ViewPersonCard> ViewPersonCards { get; set; }

        public virtual DbSet<ViewPersonInfo> ViewPersonInfos { get; set; }

        public virtual DbSet<ViewPersonInout> ViewPersonInouts { get; set; }

        public virtual DbSet<ViewTempCard> ViewTempCards { get; set; }

        public virtual DbSet<ViewVisitorCard> ViewVisitorCards { get; set; }

        public virtual DbSet<VwTsrVendor> VwTsrVendors { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DBPASSPORTDevContext");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Korean_Wansung_CI_AS");

            modelBuilder.Entity<Accessevent>(entity =>
            {
                entity.Property(e => e.AlarmId).ValueGeneratedNever();
                entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Dblocation>(entity =>
            {
                entity.HasKey(e => e.LocationCode).HasName("PK__DBLOCATI__E8889593587D1E9F");
            });

            modelBuilder.Entity<GVwTsrVendor>(entity =>
            {
                entity.Property(e => e.Country).IsFixedLength();
                entity.Property(e => e.Insertdate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MdmInstalled>(entity =>
            {
                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SmsMessage>(entity =>
            {
                entity.Property(e => e.Idx).ValueGeneratedOnAdd();
                entity.Property(e => e.MsgType).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TemplateMail>(entity =>
            {
                entity.HasKey(e => e.MailType).HasName("PK__TEMPLATE__665AE85EB0B9D0F2");

                entity.Property(e => e.Insertdate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<VGateBirthday>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_V_IMS_USER_INFO");

                entity.Property(e => e.Insertdate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ViZtad001>(entity =>
            {
                entity.Property(e => e.Insertdate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ViewAccesseventList>(entity =>
            {
                entity.ToView("VIEW_ACCESSEVENT_LIST");
            });

            modelBuilder.Entity<ViewCardPerson>(entity =>
            {
                entity.ToView("VIEW_CARD_PERSON");
            });

            modelBuilder.Entity<ViewPersonCard>(entity =>
            {
                entity.ToView("VIEW_PERSON_CARD");
            });

            modelBuilder.Entity<ViewTempCard>(entity =>
            {
                entity.ToView("VIEW_TEMP_CARD");
            });

            modelBuilder.Entity<ViewVisitorCard>(entity =>
            {
                entity.ToView("VIEW_VISITOR_CARD");
            });

            modelBuilder.Entity<VwTsrVendor>(entity =>
            {
                entity.Property(e => e.Country).IsFixedLength();
                entity.Property(e => e.Insertdate).HasDefaultValueSql("(getdate())");
            });
        }
    }
}
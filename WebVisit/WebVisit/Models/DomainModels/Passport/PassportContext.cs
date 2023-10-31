using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

public partial class PassportContext : DbContext
{
    public PassportContext()
    {
    }

    public PassportContext(DbContextOptions<PassportContext> options)
        : base(options)
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:CzPASSPORTContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Korean_Wansung_CI_AS");

        modelBuilder.Entity<Accessevent>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Approverposition>(entity =>
        {
            entity.HasKey(e => e.SapPositionCode).HasName("PK__APPROVER__E8B7F4594CB53F1E");
        });

        modelBuilder.Entity<Dblocation>(entity =>
        {
            entity.HasKey(e => e.LocationCode).HasName("PK__DBLOCATI__E8889593587D1E9F");
        });

        modelBuilder.Entity<Dblocationname2code>(entity =>
        {
            entity.HasKey(e => e.LocationName).HasName("PK_SMS_MESSAGE");
        });

        modelBuilder.Entity<Envsetting>(entity =>
        {
            entity.HasKey(e => e.SettingName).HasName("PK__ENVSETTI__40EF4F9CB4FE9F91");
        });

        modelBuilder.Entity<GVwTsrVendor>(entity =>
        {
            entity.Property(e => e.Country).IsFixedLength();
            entity.Property(e => e.Insertdate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Hrperson>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Hrphoto>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Interfacelog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__INTERFAC__3214EC0759C7C7D4");

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<MailMessage>(entity =>
        {
            entity.Property(e => e.Idx).ValueGeneratedOnAdd();
            entity.Property(e => e.Inserted).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MsgType).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MdmInstalled>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<MtsAtalkMsg>(entity =>
        {
            entity.HasKey(e => e.TranPr).HasName("pk_MTS_ATALK_MSG");

            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranNationPhone).HasDefaultValueSql("('82')");
            entity.Property(e => e.TranReplaceType)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsAtalkMsgLog>(entity =>
        {
            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranNationPhone).HasDefaultValueSql("('82')");
            entity.Property(e => e.TranReplaceType).IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsMmsContent>(entity =>
        {
            entity.HasKey(e => new { e.TranPr, e.ContentSeq }).HasName("pk_MTS_MMS_CONTENTS");

            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsMmsContentsLog>(entity =>
        {
            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsMmsMsg>(entity =>
        {
            entity.HasKey(e => e.TranPr).HasName("pk_MTS_MMS_MSG");

            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsMmsMsgLog>(entity =>
        {
            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsSmsMsg>(entity =>
        {
            entity.HasKey(e => e.TranPr).HasName("pk_MTS_SMS_MSG");

            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranRslt).IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
        });

        modelBuilder.Entity<MtsSmsMsgLog>(entity =>
        {
            entity.Property(e => e.TranLog)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TranRslt).IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
        });

        modelBuilder.Entity<Nonregidentuser>(entity =>
        {
            entity.HasKey(e => e.Idx).HasName("PK__NONREGID__C496003E218D9E0E");

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

        modelBuilder.Entity<TemplateSm>(entity =>
        {
            entity.HasKey(e => e.MsgType).HasName("PK__TEMPLATE__2037ADEA8F10918C");

            entity.Property(e => e.MsgType).ValueGeneratedNever();
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

        modelBuilder.Entity<ViewApproverList>(entity =>
        {
            entity.ToView("VIEW_APPROVER_LIST");
        });

        modelBuilder.Entity<ViewCardPerson>(entity =>
        {
            entity.ToView("VIEW_CARD_PERSON");
        });

        modelBuilder.Entity<ViewDuty>(entity =>
        {
            entity.ToView("VIEW_DUTIES");
        });

        modelBuilder.Entity<ViewPerson>(entity =>
        {
            entity.ToView("VIEW_PERSON");
        });

        modelBuilder.Entity<ViewPersonCard>(entity =>
        {
            entity.ToView("VIEW_PERSON_CARD");
        });

        modelBuilder.Entity<ViewPersonInfo>(entity =>
        {
            entity.ToView("VIEW_PERSON_INFO");
        });

        modelBuilder.Entity<ViewPersonInout>(entity =>
        {
            entity.ToView("VIEW_PERSON_INOUT");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

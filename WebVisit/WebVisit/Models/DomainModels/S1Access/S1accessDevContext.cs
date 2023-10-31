using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

public partial class S1accessDevContext : DbContext
{
    public S1accessDevContext()
    {
    }

    public S1accessDevContext(DbContextOptions<S1accessDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessCardToEqMaster> AccessCardToEqMasters { get; set; }

    public virtual DbSet<AccessCardToEqMasterHistory> AccessCardToEqMasterHistories { get; set; }

    public virtual DbSet<AccessCommonToEqMaster> AccessCommonToEqMasters { get; set; }

    public virtual DbSet<AccessCommonToEqMasterHistory> AccessCommonToEqMasterHistories { get; set; }

    public virtual DbSet<AccessExcelToEqMaster> AccessExcelToEqMasters { get; set; }

    public virtual DbSet<AccessExcelToEqMasterHistory> AccessExcelToEqMasterHistories { get; set; }

    public virtual DbSet<AccessGroupToEqMaster> AccessGroupToEqMasters { get; set; }

    public virtual DbSet<AccessGroupToEqMasterHistory> AccessGroupToEqMasterHistories { get; set; }

    public virtual DbSet<AccessLocation> AccessLocations { get; set; }

    public virtual DbSet<AccessOrgToEqMaster> AccessOrgToEqMasters { get; set; }

    public virtual DbSet<AccessOrgToEqMasterHistory> AccessOrgToEqMasterHistories { get; set; }

    public virtual DbSet<AccessPattern> AccessPatterns { get; set; }

    public virtual DbSet<AccessPatternEqStatus> AccessPatternEqStatuses { get; set; }

    public virtual DbSet<AccessPatternObject> AccessPatternObjects { get; set; }

    public virtual DbSet<AccessPatternPerson> AccessPatternPeople { get; set; }

    public virtual DbSet<AccessPatternPersonLog> AccessPatternPersonLogs { get; set; }

    public virtual DbSet<AccessRightUiinfo> AccessRightUiinfos { get; set; }

    public virtual DbSet<AccessUpdate> AccessUpdates { get; set; }

    public virtual DbSet<AccessUpdateHistory> AccessUpdateHistories { get; set; }

    public virtual DbSet<AccessUpload> AccessUploads { get; set; }

    public virtual DbSet<AgentConf> AgentConfs { get; set; }

    public virtual DbSet<Alarm> Alarms { get; set; }

    public virtual DbSet<Alarm202211> Alarm202211s { get; set; }

    public virtual DbSet<Alarm202212> Alarm202212s { get; set; }

    public virtual DbSet<Alarm202301> Alarm202301s { get; set; }

    public virtual DbSet<Alarm202302> Alarm202302s { get; set; }

    public virtual DbSet<Alarm202303> Alarm202303s { get; set; }

    public virtual DbSet<Alarm202304> Alarm202304s { get; set; }

    public virtual DbSet<Alarm202305> Alarm202305s { get; set; }

    public virtual DbSet<Alarm202306> Alarm202306s { get; set; }

    public virtual DbSet<AlarmBak> AlarmBaks { get; set; }

    public virtual DbSet<AlarmPriority> AlarmPriorities { get; set; }

    public virtual DbSet<AlarmProcess> AlarmProcesses { get; set; }

    public virtual DbSet<AlarmProcess202211> AlarmProcess202211s { get; set; }

    public virtual DbSet<AlarmProcess202212> AlarmProcess202212s { get; set; }

    public virtual DbSet<AlarmProcess202301> AlarmProcess202301s { get; set; }

    public virtual DbSet<AlarmProcess202302> AlarmProcess202302s { get; set; }

    public virtual DbSet<AlarmProcess202303> AlarmProcess202303s { get; set; }

    public virtual DbSet<AlarmProcess202304> AlarmProcess202304s { get; set; }

    public virtual DbSet<AlarmProcess202305> AlarmProcess202305s { get; set; }

    public virtual DbSet<AlarmProcess202306> AlarmProcess202306s { get; set; }

    public virtual DbSet<AlarmProcessBak> AlarmProcessBaks { get; set; }

    public virtual DbSet<AlarmProcessPlan> AlarmProcessPlans { get; set; }

    public virtual DbSet<AlarmProcessType> AlarmProcessTypes { get; set; }

    public virtual DbSet<AlarmReasonType> AlarmReasonTypes { get; set; }

    public virtual DbSet<Announce> Announces { get; set; }

    public virtual DbSet<AnnounceToCr> AnnounceToCrs { get; set; }

    public virtual DbSet<AuthServer> AuthServers { get; set; }

    public virtual DbSet<BatchList> BatchLists { get; set; }

    public virtual DbSet<BatchLog> BatchLogs { get; set; }

    public virtual DbSet<Cad2D> Cad2Ds { get; set; }

    public virtual DbSet<Cad3D> Cad3Ds { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardKind> CardKinds { get; set; }

    public virtual DbSet<CardStatus> CardStatuses { get; set; }

    public virtual DbSet<CardToEqMaster> CardToEqMasters { get; set; }

    public virtual DbSet<CardToEqMasterInactive> CardToEqMasterInactives { get; set; }

    public virtual DbSet<CardToEqmasterHistory> CardToEqmasterHistories { get; set; }

    public virtual DbSet<CardToFloor> CardToFloors { get; set; }

    public virtual DbSet<CardToFloorChanged> CardToFloorChangeds { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<Carlendar> Carlendars { get; set; }

    public virtual DbSet<CarlendarHoliday> CarlendarHolidays { get; set; }

    public virtual DbSet<ConsoleObject> ConsoleObjects { get; set; }

    public virtual DbSet<DashBoard> DashBoards { get; set; }

    public virtual DbSet<DashBoardObject> DashBoardObjects { get; set; }

    public virtual DbSet<DayOfWeekString> DayOfWeekStrings { get; set; }

    public virtual DbSet<DoorAccessModeName> DoorAccessModeNames { get; set; }

    public virtual DbSet<DoorAccessModeSchedule> DoorAccessModeSchedules { get; set; }

    public virtual DbSet<DoorAgtAlarmState> DoorAgtAlarmStates { get; set; }

    public virtual DbSet<DoorModeProperty> DoorModeProperties { get; set; }

    public virtual DbSet<DoorModePropertyDataId> DoorModePropertyDataIds { get; set; }

    public virtual DbSet<DoorRunModeName> DoorRunModeNames { get; set; }

    public virtual DbSet<DoorRunSchedule> DoorRunSchedules { get; set; }

    public virtual DbSet<Dvrtype> Dvrtypes { get; set; }

    public virtual DbSet<ElevatorIf> ElevatorIfs { get; set; }

    public virtual DbSet<EqCodeList> EqCodeLists { get; set; }

    public virtual DbSet<EqCommand> EqCommands { get; set; }

    public virtual DbSet<EqMaster> EqMasters { get; set; }

    public virtual DbSet<EqMasterGroup> EqMasterGroups { get; set; }

    public virtual DbSet<EqMasterGroupLink> EqMasterGroupLinks { get; set; }

    public virtual DbSet<EqMasterLoopSecurityLink> EqMasterLoopSecurityLinks { get; set; }

    public virtual DbSet<EqStatus> EqStatuses { get; set; }

    public virtual DbSet<EqType> EqTypes { get; set; }

    public virtual DbSet<EqUser> EqUsers { get; set; }

    public virtual DbSet<EqUserLevel> EqUserLevels { get; set; }

    public virtual DbSet<EqUserRecentMenu> EqUserRecentMenus { get; set; }

    public virtual DbSet<EvsyncDatum> EvsyncData { get; set; }

    public virtual DbSet<ExcelCardToEqMaster> ExcelCardToEqMasters { get; set; }

    public virtual DbSet<ExcelEqMaster> ExcelEqMasters { get; set; }

    public virtual DbSet<ExcelLocation> ExcelLocations { get; set; }

    public virtual DbSet<ExcelOrg> ExcelOrgs { get; set; }

    public virtual DbSet<ExcelPersonCard> ExcelPersonCards { get; set; }

    public virtual DbSet<ExcelPersonGroup> ExcelPersonGroups { get; set; }

    public virtual DbSet<ExcelPersonGroupToEqMaster> ExcelPersonGroupToEqMasters { get; set; }

    public virtual DbSet<FaceAuthType> FaceAuthTypes { get; set; }

    public virtual DbSet<FaceDetectKeyDatum> FaceDetectKeyData { get; set; }

    public virtual DbSet<FaceDetectParam> FaceDetectParams { get; set; }

    public virtual DbSet<FaceDetectPersonPicture> FaceDetectPersonPictures { get; set; }

    public virtual DbSet<FaceEqMaster> FaceEqMasters { get; set; }

    public virtual DbSet<FaceReaderActionSetting> FaceReaderActionSettings { get; set; }

    public virtual DbSet<FaceReaderAuthMode> FaceReaderAuthModes { get; set; }

    public virtual DbSet<FaceReaderAuthType> FaceReaderAuthTypes { get; set; }

    public virtual DbSet<FaceReaderEtcFunction> FaceReaderEtcFunctions { get; set; }

    public virtual DbSet<FaceReaderServerSetting> FaceReaderServerSettings { get; set; }

    public virtual DbSet<FaceReaderStatus> FaceReaderStatuses { get; set; }

    public virtual DbSet<FaceReaderTemplate> FaceReaderTemplates { get; set; }

    public virtual DbSet<FaceReaderToCr> FaceReaderToCrs { get; set; }

    public virtual DbSet<FaceUpdateResult> FaceUpdateResults { get; set; }

    public virtual DbSet<Fdtype> Fdtypes { get; set; }

    public virtual DbSet<Finger> Fingers { get; set; }

    public virtual DbSet<FingerAuthType> FingerAuthTypes { get; set; }

    public virtual DbSet<FingerToCr> FingerToCrs { get; set; }

    public virtual DbSet<FireInterlock> FireInterlocks { get; set; }

    public virtual DbSet<Firmware> Firmwares { get; set; }

    public virtual DbSet<GateInout> GateInouts { get; set; }

    public virtual DbSet<GateViewerEqMaster> GateViewerEqMasters { get; set; }

    public virtual DbSet<Gismap> Gismaps { get; set; }

    public virtual DbSet<GismapObject> GismapObjects { get; set; }

    public virtual DbSet<Gistype> Gistypes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<HrdoorAuthorityDatum> HrdoorAuthorityData { get; set; }

    public virtual DbSet<HrorgDatum> HrorgData { get; set; }

    public virtual DbSet<HrpersonDatum> HrpersonData { get; set; }

    public virtual DbSet<HrpersonTable1> HrpersonTable1s { get; set; }

    public virtual DbSet<HrpersonTable2> HrpersonTable2s { get; set; }

    public virtual DbSet<HrpersonTable3> HrpersonTable3s { get; set; }

    public virtual DbSet<InstallLog> InstallLogs { get; set; }

    public virtual DbSet<InterLock> InterLocks { get; set; }

    public virtual DbSet<InterLockCmdInterval> InterLockCmdIntervals { get; set; }

    public virtual DbSet<InterLockObject> InterLockObjects { get; set; }

    public virtual DbSet<IpcameraRtspaddress> IpcameraRtspaddresses { get; set; }

    public virtual DbSet<IpcameraType> IpcameraTypes { get; set; }

    public virtual DbSet<JoyStick> JoySticks { get; set; }

    public virtual DbSet<JoyStickButtonCmd> JoyStickButtonCmds { get; set; }

    public virtual DbSet<LightModeName> LightModeNames { get; set; }

    public virtual DbSet<LightSchedule> LightSchedules { get; set; }

    public virtual DbSet<Localization> Localizations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationLevel> LocationLevels { get; set; }

    public virtual DbSet<ManagementSetting> ManagementSettings { get; set; }

    public virtual DbSet<ModeChange> ModeChanges { get; set; }

    public virtual DbSet<Org> Orgs { get; set; }

    public virtual DbSet<PanelContent> PanelContents { get; set; }

    public virtual DbSet<PanelInfo> PanelInfos { get; set; }

    public virtual DbSet<PanelTypeId> PanelTypeIds { get; set; }

    public virtual DbSet<PatialAccessLocation> PatialAccessLocations { get; set; }

    public virtual DbSet<PatialAccessUpload> PatialAccessUploads { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonBlackList> PersonBlackLists { get; set; }

    public virtual DbSet<PersonGroup> PersonGroups { get; set; }

    public virtual DbSet<PersonGroupLink> PersonGroupLinks { get; set; }

    public virtual DbSet<PersonGroupToEqMaster> PersonGroupToEqMasters { get; set; }

    public virtual DbSet<PersonStatus> PersonStatuses { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<PublicLocationSchedule> PublicLocationSchedules { get; set; }

    public virtual DbSet<PwupdateCycle> PwupdateCycles { get; set; }

    public virtual DbSet<PxmalarmList> PxmalarmLists { get; set; }

    public virtual DbSet<PxmcontrolList> PxmcontrolLists { get; set; }

    public virtual DbSet<PxminitDescribe> PxminitDescribes { get; set; }

    public virtual DbSet<Pxmproperty> Pxmproperties { get; set; }

    public virtual DbSet<PxmproxyDescribe> PxmproxyDescribes { get; set; }

    public virtual DbSet<ReaderUpdateHistory> ReaderUpdateHistories { get; set; }

    public virtual DbSet<ReaderUpdateState> ReaderUpdateStates { get; set; }

    public virtual DbSet<SecurityMode> SecurityModes { get; set; }

    public virtual DbSet<ServerAuthResult> ServerAuthResults { get; set; }

    public virtual DbSet<ServiceLog> ServiceLogs { get; set; }

    public virtual DbSet<ServiceVersionList> ServiceVersionLists { get; set; }

    public virtual DbSet<SetupDatum> SetupData { get; set; }

    public virtual DbSet<SystemLevel> SystemLevels { get; set; }

    public virtual DbSet<SystemSetup> SystemSetups { get; set; }

    public virtual DbSet<TableHeader> TableHeaders { get; set; }

    public virtual DbSet<UpgradeEquipment> UpgradeEquipments { get; set; }

    public virtual DbSet<UpgradeFileList> UpgradeFileLists { get; set; }

    public virtual DbSet<UpgradeInfo> UpgradeInfos { get; set; }

    public virtual DbSet<UpgradeLog> UpgradeLogs { get; set; }

    public virtual DbSet<UpgradeSchedule> UpgradeSchedules { get; set; }

    public virtual DbSet<UploadControllerInfo> UploadControllerInfos { get; set; }

    public virtual DbSet<UploadProgressInfo> UploadProgressInfos { get; set; }

    public virtual DbSet<UserDefine> UserDefines { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserView> UserViews { get; set; }

    public virtual DbSet<Vacamera> Vacameras { get; set; }

    public virtual DbSet<Vatype> Vatypes { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    public virtual DbSet<VideoLayout> VideoLayouts { get; set; }

    public virtual DbSet<VideoLayoutLink> VideoLayoutLinks { get; set; }

    public virtual DbSet<ViewAccess> ViewAccesses { get; set; }

    public virtual DbSet<ViewAccessright> ViewAccessrights { get; set; }

    public virtual DbSet<ViewAttendance> ViewAttendances { get; set; }

    public virtual DbSet<ViewCardPerson> ViewCardPeople { get; set; }

    public virtual DbSet<ViewDevice> ViewDevices { get; set; }

    public virtual DbSet<ViewDoorStatus> ViewDoorStatuses { get; set; }

    public virtual DbSet<VisitNotice> VisitNotices { get; set; }

    public virtual DbSet<VisitReason> VisitReasons { get; set; }

    public virtual DbSet<VisitReserve> VisitReserves { get; set; }

    public virtual DbSet<VisitReserveVisitant> VisitReserveVisitants { get; set; }

    public virtual DbSet<VisitSetup> VisitSetups { get; set; }

    public virtual DbSet<VisitStatus> VisitStatuses { get; set; }

    public virtual DbSet<VisitToLocation> VisitToLocations { get; set; }

    public virtual DbSet<Visitant> Visitants { get; set; }

    public virtual DbSet<WavFile> WavFiles { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<WorkResultId> WorkResultIds { get; set; }

    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DBS1ACCESSDevContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessCardToEqMaster>(entity =>
        {
            entity.ToTable("AccessCardToEqMaster", tb => tb.HasTrigger("TRG_AccessCardToEqMasterHistory"));

            entity.Property(e => e.CardId).HasComment("카드의 ID");
            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessCardToEqMasterHistory>(entity =>
        {
            entity.Property(e => e.CardId).HasComment("카드의 ID");
            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessCommonToEqMaster>(entity =>
        {
            entity.ToTable("AccessCommonToEqMaster", tb => tb.HasTrigger("TRG_AccessCommonToEqMasterHistory"));

            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessCommonToEqMasterHistory>(entity =>
        {
            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessExcelToEqMaster>(entity =>
        {
            entity.ToTable("AccessExcelToEqMaster", tb => tb.HasTrigger("TRG_AccessExcelToEqMasterHistory"));
        });

        modelBuilder.Entity<AccessExcelToEqMasterHistory>(entity =>
        {
            entity.Property(e => e.CardId).HasComment("카드의 ID");
            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessGroupToEqMaster>(entity =>
        {
            entity.ToTable("AccessGroupToEqMaster", tb => tb.HasTrigger("TRG_AccessGroupToEqMasterHistory"));

            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.GroupId).HasComment("그룹의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessGroupToEqMasterHistory>(entity =>
        {
            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.GroupId).HasComment("그룹의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessLocation>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AccessOrgToEqMaster>(entity =>
        {
            entity.ToTable("AccessOrgToEqMaster", tb => tb.HasTrigger("TRG_AccessOrgToEqMasterHistory"));

            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.OrgId).HasComment("조직의 ID");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessOrgToEqMasterHistory>(entity =>
        {
            entity.Property(e => e.EqMasterId).HasComment("출입문, 방범의 ID");
            entity.Property(e => e.HasAccess).HasComment("0 권한없음, 1 권한있음. ");
            entity.Property(e => e.IsMaster).HasComment("방범 : 0 / 출입: 일반 0 , 마스터 1");
            entity.Property(e => e.ModeNum).HasComment("0~7번 출입모드");
            entity.Property(e => e.OrgId).HasComment("조직의 ID");
            entity.Property(e => e.Uiid).HasComment("권한설정 화면번호");
        });

        modelBuilder.Entity<AccessPattern>(entity =>
        {
            entity.Property(e => e.EnableFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AccessPatterns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessPattern_UpdateID");
        });

        modelBuilder.Entity<AccessPatternEqStatus>(entity =>
        {
            entity.HasOne(d => d.EqCode).WithMany(p => p.AccessPatternEqStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessPatternEqStatus_EqCodeList");
        });

        modelBuilder.Entity<AccessPatternObject>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AccessPatternObjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessPatternObject_UpdateID");
        });

        modelBuilder.Entity<AccessPatternPerson>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AccessPatternPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessPatternPerson_UpdateID");
        });

        modelBuilder.Entity<AccessPatternPersonLog>(entity =>
        {
            entity.HasKey(e => new { e.AccessPatternId, e.CardId, e.EqMasterId }).HasName("PK_AccessPatternPersonLog_1");
        });

        modelBuilder.Entity<AccessRightUiinfo>(entity =>
        {
            entity.Property(e => e.Uiid).ValueGeneratedNever();
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AccessUpdate>(entity =>
        {
            entity.ToTable("AccessUpdate", tb => tb.HasTrigger("TRG_AccessUpdateHistory"));
        });

        modelBuilder.Entity<AccessUpload>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AgentConf>(entity =>
        {
            entity.HasKey(e => e.AgentConfName).HasName("PK_EtcConf");
        });

        modelBuilder.Entity<Alarm>(entity =>
        {
            entity.ToTable("Alarm", tb => tb.HasTrigger("GATE_INOUT_TRG"));

            entity.Property(e => e.AlarmId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarms).HasConstraintName("FK_Alarm_EqUser");
        });

        modelBuilder.Entity<Alarm202211>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202211s).HasConstraintName("FK_Alarm_202211_EqUserID");
        });

        modelBuilder.Entity<Alarm202212>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202212s).HasConstraintName("FK_Alarm_202212_EqUserID");
        });

        modelBuilder.Entity<Alarm202301>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202301s).HasConstraintName("FK_Alarm_202301_EqUserID");
        });

        modelBuilder.Entity<Alarm202302>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202302s).HasConstraintName("FK_Alarm_202302_EqUserID");
        });

        modelBuilder.Entity<Alarm202303>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202303s).HasConstraintName("FK_Alarm_202303_EqUserID");
        });

        modelBuilder.Entity<Alarm202304>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202304s).HasConstraintName("FK_Alarm_202304_EqUserID");
        });

        modelBuilder.Entity<Alarm202305>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202305s).HasConstraintName("FK_Alarm_202305_EqUserID");
        });

        modelBuilder.Entity<Alarm202306>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.Alarm202306s).HasConstraintName("FK_Alarm_202306_EqUserID");
        });

        modelBuilder.Entity<AlarmBak>(entity =>
        {
            entity.Property(e => e.AlarmId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmBaks).HasConstraintName("FK_Alarm_Bak_EqUserID");
        });

        modelBuilder.Entity<AlarmPriority>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmPriorities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmPriority_EqUser");
        });

        modelBuilder.Entity<AlarmProcess>(entity =>
        {
            entity.Property(e => e.AlarmProcessId).ValueGeneratedOnAdd();
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcessEqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcessUpdates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202211>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202211s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202211_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202211EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202211_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202211Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202211_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202212>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202212s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202212_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202212EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202212_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202212Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202212_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202301>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202301s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202301_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202301EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202301_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202301Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202301_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202302>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202302s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202302_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202302EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202302_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202302Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202302_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202303>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202303s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202303_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202303EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202303_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202303Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202303_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202304>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202304s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202304_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202304EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202304_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202304Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202304_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202305>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202305s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202305_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202305EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202305_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202305Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202305_EqUser2");
        });

        modelBuilder.Entity<AlarmProcess202306>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcess202306s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202306_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcess202306EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202306_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcess202306Updates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_202306_EqUser2");
        });

        modelBuilder.Entity<AlarmProcessBak>(entity =>
        {
            entity.Property(e => e.EqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.AlarmReasonType).WithMany(p => p.AlarmProcessBaks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_Bak_AlarmReasonType");

            entity.HasOne(d => d.EqUser).WithMany(p => p.AlarmProcessBakEqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_Bak_EqUser");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcessBakUpdates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcess_Bak_EqUser2");
        });

        modelBuilder.Entity<AlarmProcessPlan>(entity =>
        {
            entity.HasKey(e => new { e.AlarmProcessPlanId, e.EqUserId }).HasName("PK_AlramProcessPlan");

            entity.Property(e => e.AlarmProcessPlanId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcessPlans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcessPlan_UpdateID");
        });

        modelBuilder.Entity<AlarmProcessType>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AlarmProcessTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlarmProcessType_EqUser");
        });

        modelBuilder.Entity<Announce>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Announces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Announce_EqUser");
        });

        modelBuilder.Entity<AnnounceToCr>(entity =>
        {
            entity.HasKey(e => new { e.EqMasterId, e.IsAnnounce, e.Num }).HasName("PK_AnnounceToCR_1");

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.AnnounceToCrs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnnounceToCR_EqUser");
        });

        modelBuilder.Entity<AuthServer>(entity =>
        {
            entity.HasKey(e => e.ContractNo).HasName("PK_AuthServer_1");

            entity.ToTable("AuthServer", tb => tb.HasTrigger("TRG_AuthToElevatorIF"));
        });

        modelBuilder.Entity<Cad2D>(entity =>
        {
            entity.Property(e => e.FixPosition).HasDefaultValueSql("((0))");
            entity.Property(e => e.Transparency).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Location).WithMany(p => p.Cad2Ds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cad2D_Location");

            entity.HasOne(d => d.Update).WithMany(p => p.Cad2Ds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cad2D_EqUser");
        });

        modelBuilder.Entity<Cad3D>(entity =>
        {
            entity.HasKey(e => e.EqMasterId).HasName("PK_CAD3D");

            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Cad3Ds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cad3D_EqUser");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardNo).HasName("PK_Card_1");

            entity.ToTable("Card", tb => tb.HasTrigger("TRG_CardToElevatorIF"));

            entity.Property(e => e.CardNo).HasComment("전체 Full ");
            entity.Property(e => e.AccessTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CardId)
                .ValueGeneratedOnAdd()
                .HasComment("초기값 : 1 , 증가값 : 1");
            entity.Property(e => e.Pid).HasComment("사원ID");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.ValidDate).HasDefaultValueSql("(((2099)-(12))-(31))");

            entity.HasOne(d => d.CardStatus).WithMany(p => p.Cards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_CardStatus");

            entity.HasOne(d => d.FaceAuthType).WithMany(p => p.Cards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_FaceAuthType");

            entity.HasOne(d => d.FingerAuthType).WithMany(p => p.Cards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_FingerAuthType");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.Cards).HasConstraintName("FK_Card_Person");

            entity.HasOne(d => d.Update).WithMany(p => p.Cards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_EqUser");
        });

        modelBuilder.Entity<CardKind>(entity =>
        {
            entity.Property(e => e.CardKindId).IsFixedLength();
            entity.Property(e => e.ViewCombo).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<CardStatus>(entity =>
        {
            entity.Property(e => e.ReservedWord)
                .HasDefaultValueSql("((0))")
                .IsFixedLength();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.ViewCombo).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Update).WithMany(p => p.CardStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardStatus_EqUser");
        });

        modelBuilder.Entity<CardToEqMaster>(entity =>
        {
            entity.ToTable("CardToEqMaster", tb => tb.HasTrigger("TRG_CardToEqmasterHistory"));

            entity.Property(e => e.AccessPattern).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.DownFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAccess).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.IsMaster).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.IsSecurity).HasDefaultValueSql("((0))");
            entity.Property(e => e.Lcindex).HasDefaultValueSql("((0))");
            entity.Property(e => e.Trace).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
        });

        modelBuilder.Entity<CardToEqMasterInactive>(entity =>
        {
            entity.Property(e => e.AccessPattern).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.DownFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAccess).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.IsMaster).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.IsSecurity).HasDefaultValueSql("((0))");
            entity.Property(e => e.Lcindex).HasDefaultValueSql("((0))");
            entity.Property(e => e.Trace).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.CardToEqMasterInactives)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardToEqMasterInactive_EqUser");
        });

        modelBuilder.Entity<CardToEqmasterHistory>(entity =>
        {
            entity.Property(e => e.ActionType).IsFixedLength();
        });

        modelBuilder.Entity<CardToFloor>(entity =>
        {
            entity.Property(e => e.Idx).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.Property(e => e.CardTypeId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.CardTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardType_EqUser");
        });

        modelBuilder.Entity<Carlendar>(entity =>
        {
            entity.ToTable("Carlendar", tb => tb.HasComment("년도 월의 특정일 설정 테이블"));

            entity.Property(e => e.M1).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M10).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M11).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M12).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M2).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M3).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M4).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M5).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M6).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M7).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M8).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M9).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Carlendars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carlendar_EqUser");
        });

        modelBuilder.Entity<CarlendarHoliday>(entity =>
        {
            entity.ToTable("CARLENDAR_HOLIDAYS", tb => tb.HasComment("법정 공휴일 데이터"));

            entity.Property(e => e.HSeq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ConsoleObject>(entity =>
        {
            entity.ToTable("ConsoleObject", tb => tb.HasComment("콘솔의 각 영역별 설정 값을 정의하는 테이블"));

            entity.Property(e => e.ConsoleId).ValueGeneratedNever();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.ConsoleObjects).HasConstraintName("FK_ConsoleObject_UpdateID");
        });

        modelBuilder.Entity<DashBoard>(entity =>
        {
            entity.ToTable("DashBoard", tb => tb.HasComment("데시보드를 위한 테이블정보"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.DashBoards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DashBoard_UpdateID");
        });

        modelBuilder.Entity<DashBoardObject>(entity =>
        {
            entity.ToTable("DashBoardObject", tb => tb.HasComment("데시보드 오브젝트"));

            entity.Property(e => e.DashBoardObjectId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.DashBoardObjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DashBoardObject_UpdateID");
        });

        modelBuilder.Entity<DayOfWeekString>(entity =>
        {
            entity.ToTable("DayOfWeekString", tb => tb.HasComment("스케쥴의 월~일, 특정일 정보 테이블"));
        });

        modelBuilder.Entity<DoorAccessModeName>(entity =>
        {
            entity.HasKey(e => new { e.DoorAccessModeId, e.ModeNameId }).HasName("PK_DoorAccessMode");

            entity.ToTable("DoorAccessModeName", tb => tb.HasComment("출입모드별 타임 테이블"));

            entity.Property(e => e.DoorAccessModeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<DoorAccessModeSchedule>(entity =>
        {
            entity.HasKey(e => new { e.EqMasterId, e.Year }).HasName("PK_DoorAccessModeSchedule_1");

            entity.ToTable("DoorAccessModeSchedule", tb => tb.HasComment("출입모드 스케쥴 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.DoorAccessModeSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoorAccessModeSchedule_EqUser");
        });

        modelBuilder.Entity<DoorAgtAlarmState>(entity =>
        {
            entity.HasKey(e => e.EqMasterId).HasName("PK_EqDoorState");

            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NotifyDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<DoorModeProperty>(entity =>
        {
            entity.HasKey(e => new { e.EqMasterId, e.EqCodeId }).HasName("PK_DoorMode");

            entity.ToTable("DoorModeProperty", tb => tb.HasComment("출입문 운영 모드 및 등록 정보"));

            entity.Property(e => e.Ann).HasDefaultValueSql("((0))");
            entity.Property(e => e.Annno).HasDefaultValueSql("((4))");
            entity.Property(e => e.AnnwdNo).HasDefaultValueSql("((1))");
            entity.Property(e => e.AntiPassBackUse).HasDefaultValueSql("((0))");
            entity.Property(e => e.BufferFullActionMode).HasDefaultValueSql("((0))");
            entity.Property(e => e.CardInOutLatency).HasDefaultValueSql("((3))");
            entity.Property(e => e.CardReaderCnt).HasDefaultValueSql("((0))");
            entity.Property(e => e.CardValidDateBeep).HasDefaultValueSql("((30))");
            entity.Property(e => e.CommErrDoorAction).HasDefaultValueSql("((0))");
            entity.Property(e => e.DataSaveAlert).HasDefaultValueSql("((0))");
            entity.Property(e => e.DoorCancelTime).HasDefaultValueSql("((3))");
            entity.Property(e => e.DoorRunId).HasDefaultValueSql("((0))");
            entity.Property(e => e.DownFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.FireDoorAction).HasDefaultValueSql("((1))");
            entity.Property(e => e.InOutDoorInfoBeep).HasDefaultValueSql("((0))");
            entity.Property(e => e.KindOfDoor).HasDefaultValueSql("((0))");
            entity.Property(e => e.KindOfSignal).HasDefaultValueSql("((0))");
            entity.Property(e => e.Lpc).HasDefaultValueSql("((0))");
            entity.Property(e => e.OpenLookoutAlert).HasDefaultValueSql("((0))");
            entity.Property(e => e.OpenLookoutTime).HasDefaultValueSql("((0))");
            entity.Property(e => e.Property1).IsFixedLength();
            entity.Property(e => e.Property2).IsFixedLength();
            entity.Property(e => e.Property3).IsFixedLength();
            entity.Property(e => e.Property4).IsFixedLength();
            entity.Property(e => e.Property5).IsFixedLength();
            entity.Property(e => e.ScheduleUse).HasDefaultValueSql("((0))");
            entity.Property(e => e.Slcno).HasDefaultValueSql("((0))");
            entity.Property(e => e.TemporaryInoutFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.TrackingUse).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<DoorModePropertyDataId>(entity =>
        {
            entity.ToTable("DoorModePropertyDataID", tb => tb.HasComment("출입문 등록정보의 각 필드값의 세부 다국어 테이블 정의"));
        });

        modelBuilder.Entity<DoorRunModeName>(entity =>
        {
            entity.ToTable("DoorRunModeName", tb => tb.HasComment("기기 운영 모드 스트링 테이블"));

            entity.Property(e => e.DoorRunId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<DoorRunSchedule>(entity =>
        {
            entity.HasKey(e => new { e.EqMasterId, e.Year }).HasName("PK_DoorRunSchedule_1");

            entity.ToTable("DoorRunSchedule", tb => tb.HasComment("기기 운영 모드 스케쥴 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.DoorRunSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoorRunSchedule_EqUser");
        });

        modelBuilder.Entity<Dvrtype>(entity =>
        {
            entity.ToTable("DVRType", tb => tb.HasComment("광 감지기의 종류 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Dvrtypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DVRType_UpdateID");
        });

        modelBuilder.Entity<ElevatorIf>(entity =>
        {
            entity.Property(e => e.ChangedType).IsFixedLength();
        });

        modelBuilder.Entity<EqCodeList>(entity =>
        {
            entity.ToTable("EqCodeList", tb => tb.HasComment("기기 코드 리스트 테이블"));

            entity.Property(e => e.EqCodeId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.EqCodeLists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqCodeList_UpdateID");
        });

        modelBuilder.Entity<EqCommand>(entity =>
        {
            entity.ToTable("EqCommand", tb => tb.HasComment("각 EqCode, EqType에 해당 하는 기기의 전송 명령어의 정의"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.EqType).WithMany(p => p.EqCommands)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqCommand_EqType");
        });

        modelBuilder.Entity<EqMaster>(entity =>
        {
            entity.HasKey(e => new { e.EqMasterId, e.EqCodeId, e.Master, e.Local, e.Point, e.Loop }).HasName("PK_EqMaster_1");

            entity.ToTable("EqMaster", tb => tb.HasComment("기기 정보 테이블"));

            entity.Property(e => e.EqMasterId).ValueGeneratedOnAdd();
            entity.Property(e => e.FwversionUpdate).HasDefaultValueSql("((0))");
            entity.Property(e => e.ManufacturerId).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.UseDdnsSip).HasDefaultValueSql("((0))");
            entity.Property(e => e.UsePeerIp).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.EqCode).WithMany(p => p.EqMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqMaster_EqCodeList");

            entity.HasOne(d => d.EqType).WithMany(p => p.EqMasters).HasConstraintName("FK_EqMaster_EqType");

            entity.HasOne(d => d.Location).WithMany(p => p.EqMasters).HasConstraintName("FK_EqMaster_Location");

            entity.HasOne(d => d.Update).WithMany(p => p.EqMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqMaster_EqUser");
        });

        modelBuilder.Entity<EqMasterGroup>(entity =>
        {
            entity.ToTable("EqMasterGroup", tb => tb.HasComment("공용부와 기기 그룹을 사용하기 위한 테이블"));

            entity.Property(e => e.EqGroupId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.EqMasterGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqMasterGroup_EqUser");
        });

        modelBuilder.Entity<EqMasterGroupLink>(entity =>
        {
            entity.ToTable("EqMasterGroupLink", tb => tb.HasComment("기기 그룹 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.EqGroup).WithMany(p => p.EqMasterGroupLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqMasterGroupLink_EqMasterGroup");

            entity.HasOne(d => d.Update).WithMany(p => p.EqMasterGroupLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqMasterGroupLink_EqUser");
        });

        modelBuilder.Entity<EqMasterLoopSecurityLink>(entity =>
        {
            entity.Property(e => e.LoopEqMasterId).ValueGeneratedNever();
            entity.Property(e => e.EqMasterLinkUid).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<EqStatus>(entity =>
        {
            entity.ToTable("EqStatus", tb => tb.HasComment("기기 상태 정보 테이블"));

            entity.Property(e => e.EqCodeId).HasDefaultValueSql("((450))");
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("('2012-01-01 01:01:01')");
            entity.Property(e => e.IsShow).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsSms).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.WavFileId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.EqCode).WithMany(p => p.EqStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqStatus_EqCodeList");

            entity.HasOne(d => d.Update).WithMany(p => p.EqStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqStatus_EqUser");
        });

        modelBuilder.Entity<EqType>(entity =>
        {
            entity.ToTable("EqType", tb => tb.HasComment("기기 상태 연동 정보 및 기기 구분 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.EqTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqType_UpdateID");
        });

        modelBuilder.Entity<EqUser>(entity =>
        {
            entity.ToTable("EqUser", tb => tb.HasComment("관리자, 사용자 테이블"));

            entity.Property(e => e.ParentEqUserId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.EqUserLevel).WithMany(p => p.EqUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqUser_EqUserLevel");
        });

        modelBuilder.Entity<EqUserLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK_EqUserLevel_1");

            entity.ToTable("EqUserLevel", tb => tb.HasComment("관리자 / 사용자 레벨 테이블"));

            entity.Property(e => e.AccessReleaseManage).HasDefaultValueSql("((2))");
            entity.Property(e => e.CadEditorFunc).HasDefaultValueSql("((1))");
            entity.Property(e => e.CameraFunc).HasDefaultValueSql("((1))");
            entity.Property(e => e.CardToDoorFunc).HasDefaultValueSql("((1))");
            entity.Property(e => e.ElevatorControlServer).HasDefaultValueSql("((2))");
            entity.Property(e => e.ExcelExportFunc).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.WorkScheduleFunc).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<EqUserRecentMenu>(entity =>
        {
            entity.ToTable("EqUserRecentMenu", tb => tb.HasComment("사용자의 최근 사용메뉴를 기록한다."));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.EqUser).WithMany(p => p.EqUserRecentMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EqUserRecentMenu_EqUser");
        });

        modelBuilder.Entity<ExcelCardToEqMaster>(entity =>
        {
            entity.ToTable("ExcelCardToEqMaster", tb => tb.HasComment("엑셀을 통한 출입권한 등록 테이블"));
        });

        modelBuilder.Entity<ExcelEqMaster>(entity =>
        {
            entity.ToTable("ExcelEqMaster", tb => tb.HasComment("엑셀을 통한 기기 등록"));
        });

        modelBuilder.Entity<ExcelLocation>(entity =>
        {
            entity.ToTable("ExcelLocation", tb => tb.HasComment("엑셀을 통한 위치 등록"));
        });

        modelBuilder.Entity<ExcelOrg>(entity =>
        {
            entity.ToTable("ExcelOrg", tb => tb.HasComment("엑셀을 통한 조직 등록"));
        });

        modelBuilder.Entity<ExcelPersonCard>(entity =>
        {
            entity.ToTable("ExcelPersonCard", tb => tb.HasComment("엑셀을 통한 사원 및 카드 등록"));
        });

        modelBuilder.Entity<ExcelPersonGroup>(entity =>
        {
            entity.ToTable("ExcelPersonGroup", tb => tb.HasComment("엑셀을 통한 출입그룹 등록"));
        });

        modelBuilder.Entity<ExcelPersonGroupToEqMaster>(entity =>
        {
            entity.ToTable("ExcelPersonGroupToEqMaster", tb => tb.HasComment("엑셀을 통한 출입그룹 권한 등록"));
        });

        modelBuilder.Entity<FaceAuthType>(entity =>
        {
            entity.ToTable("FaceAuthType", tb => tb.HasComment("얼굴 인증 방식 테이블"));
        });

        modelBuilder.Entity<FaceDetectKeyDatum>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.PictureId }).HasName("PK_FaceDetectKeyData_1");

            entity.ToTable(tb => tb.HasComment("사원의 사진정보에서 특징점을 추출하여 저장하도록 한다."));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.FaceDetectKeyData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FaceDetectKeyData_Person");
        });

        modelBuilder.Entity<FaceDetectParam>(entity =>
        {
            entity.ToTable("FaceDetectParam", tb => tb.HasComment("얼굴 인증을 위한 파라미터 저장"));

            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.FaceDetectParams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FaceDetectParam_UpdateID");
        });

        modelBuilder.Entity<FaceDetectPersonPicture>(entity =>
        {
            entity.ToTable("FaceDetectPersonPicture", tb => tb.HasComment("얼굴 인증을 위한 사원별 특징점 데이터"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
        });

        modelBuilder.Entity<FaceEqMaster>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<FaceReaderActionSetting>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
            entity.Property(e => e.MaskAuthMode).HasDefaultValueSql("((1))");
            entity.Property(e => e.TemperatureDeny).HasDefaultValueSql("((37.5))");
            entity.Property(e => e.TemperatureWarning).HasDefaultValueSql("((37.0))");
        });

        modelBuilder.Entity<FaceReaderEtcFunction>(entity =>
        {
            entity.Property(e => e.DownFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.TemperatureDeny)
                .HasDefaultValueSql("((37.5))")
                .HasComment("체온 측정 출입 불가 온도");
            entity.Property(e => e.TemperatureWarning)
                .HasDefaultValueSql("((37.0))")
                .HasComment("체온 측정 출입 경고 온도");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.UseMaskMode)
                .HasDefaultValueSql("((1))")
                .HasComment("마스크 모드 사용/미사용");
            entity.Property(e => e.UseThermometer)
                .HasDefaultValueSql("((0))")
                .HasComment("체온 측정 사용/미사용");
        });

        modelBuilder.Entity<FaceReaderServerSetting>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<FaceReaderStatus>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<FaceReaderToCr>(entity =>
        {
            entity.Property(e => e.DownFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAccess).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
        });

        modelBuilder.Entity<Fdtype>(entity =>
        {
            entity.ToTable("FDType", tb => tb.HasComment("광 감지기의 종류 테이블"));
        });

        modelBuilder.Entity<Finger>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.FingerType }).HasName("PK_FingerType");

            entity.ToTable("Finger", tb => tb.HasComment("지문 정보 테이블"));

            entity.Property(e => e.FingerEx).HasDefaultValueSql("((0))");
            entity.Property(e => e.FingerPrint1).IsFixedLength();
            entity.Property(e => e.FingerPrint2).IsFixedLength();
            entity.Property(e => e.FingerPrint3).IsFixedLength();
            entity.Property(e => e.FingerPrint4).IsFixedLength();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Fingers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finger_EqUser");
        });

        modelBuilder.Entity<FingerAuthType>(entity =>
        {
            entity.ToTable("FingerAuthType", tb => tb.HasComment("지문 인증 방식 테이블"));
        });

        modelBuilder.Entity<FingerToCr>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.EqMasterId }).HasName("PK_FingerToCR_1");

            entity.ToTable("FingerToCR", tb => tb.HasComment("지문정보의 카드리더 전송 테이블"));

            entity.Property(e => e.DownFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAccess).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.FingerToCrs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FingerToCR_UpdateID");
        });

        modelBuilder.Entity<FireInterlock>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Firmware>(entity =>
        {
            entity.ToTable("Firmware", tb => tb.HasComment("하드웨어 펌웨어에 대한 정보 테이블"));

            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Firmware_UpdateID");
        });

        modelBuilder.Entity<GateInout>(entity =>
        {
            entity.Property(e => e.SendYn).HasDefaultValueSql("('N')");
            entity.Property(e => e.Sync).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<GateViewerEqMaster>(entity =>
        {
            entity.ToTable("GateViewerEqMaster", tb => tb.HasComment("출입자 모니터링을 위한 출입문의 카드리더 정보"));

            entity.Property(e => e.IsPersonInfoView).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.GateViewerEqMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GateViewerEqMaster_UpdateID");
        });

        modelBuilder.Entity<Gismap>(entity =>
        {
            entity.ToTable("GISMap", tb => tb.HasComment("GIS 위에 작도한 도면 Object를 저장하는 테이블"));

            entity.Property(e => e.GismapId).ValueGeneratedNever();
            entity.Property(e => e.MapViewTypeId).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.Zoom).HasDefaultValueSql("((16))");

            entity.HasOne(d => d.Update).WithMany(p => p.Gismaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GISMap_UpdateID");
        });

        modelBuilder.Entity<GismapObject>(entity =>
        {
            entity.ToTable("GISMapObject", tb => tb.HasComment("GIS 위에 작도한 도면 Object를 저장하는 테이블"));

            entity.Property(e => e.ObjectId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.GismapObjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GISMapObject_UpdateID");
        });

        modelBuilder.Entity<Gistype>(entity =>
        {
            entity.ToTable("GISType", tb => tb.HasComment("GIS 맵 종류를 정의 한다."));
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.ToTable("Grade", tb => tb.HasComment("직급 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Grades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grade_EqUser");
        });

        modelBuilder.Entity<HrdoorAuthorityDatum>(entity =>
        {
            entity.Property(e => e.ApplyFlag).HasComment("0: 적용전(기본값), 1:출입권한적용, 2:출입권한제거");
            entity.Property(e => e.DeviceId).HasComment("출입문ID");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidDateE).HasComment("출입종료일");
            entity.Property(e => e.ValidDateS).HasComment("출입시작일");
        });

        modelBuilder.Entity<HrorgDatum>(entity =>
        {
            entity.ToTable("HROrgData", tb => tb.HasComment("부서 연동 테이블(인사연동)"));

            entity.Property(e => e.UpdateOption).HasComment("0:삭제, 1:등록, 2:수정");
        });

        modelBuilder.Entity<HrpersonDatum>(entity =>
        {
            entity.ToTable("HRPersonData", tb => tb.HasComment("사원의 연동 테이블(인사연동)"));

            entity.Property(e => e.CardStatus).HasDefaultValueSql("((0))");
            entity.Property(e => e.PersonStatus).HasDefaultValueSql("((0))");
            entity.Property(e => e.PersonType).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<HrpersonTable1>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<HrpersonTable2>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<HrpersonTable3>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<InterLock>(entity =>
        {
            entity.ToTable("InterLock", tb => tb.HasComment("연동설정 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.InterLocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InterLock_EqUser");
        });

        modelBuilder.Entity<InterLockCmdInterval>(entity =>
        {
            entity.ToTable("InterLockCmdInterval", tb => tb.HasComment("조건에 해당하는 기기의 동작이 바로 이루어지 않고 사용자가 지정한 Interval 뒤에 동작하도록 한다"));
        });

        modelBuilder.Entity<InterLockObject>(entity =>
        {
            entity.ToTable("InterLockObject", tb => tb.HasComment("연동설정 오브젝트 테이블"));

            entity.Property(e => e.ObjectId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.InterLockObjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InterLockObject_UpdateID");
        });

        modelBuilder.Entity<IpcameraRtspaddress>(entity =>
        {
            entity.ToTable("IPCameraRTSPAddress", tb => tb.HasComment("IP카메라의 해상도별 RTSP 주소"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.IpcameraRtspaddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IPCameraRTSPAddress_UpdateID");
        });

        modelBuilder.Entity<IpcameraType>(entity =>
        {
            entity.ToTable("IPCameraType", tb => tb.HasComment("광 감지기의 종류 테이블"));
        });

        modelBuilder.Entity<JoyStick>(entity =>
        {
            entity.ToTable("JoyStick", tb => tb.HasComment("조이스틱을 사용하는 환경을 저장한다."));

            entity.Property(e => e.EqUserId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.JoySticks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JoyStick_UpdateID");
        });

        modelBuilder.Entity<JoyStickButtonCmd>(entity =>
        {
            entity.ToTable("JoyStickButtonCmd", tb => tb.HasComment("조이스틱에 맵핑되는 기능을 정의 한다."));
        });

        modelBuilder.Entity<LightModeName>(entity =>
        {
            entity.ToTable("LightModeName", tb => tb.HasComment("조명 제어명 테이블"));
        });

        modelBuilder.Entity<LightSchedule>(entity =>
        {
            entity.HasKey(e => new { e.EqMasterId, e.Year }).HasName("PK_LightSchedule_1");

            entity.ToTable("LightSchedule", tb => tb.HasComment("조명 스케쥴 테이블"));

            entity.Property(e => e.DownFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.LightSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LightSchedule_UpdateID");
        });

        modelBuilder.Entity<Localization>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK_Localization$");

            entity.ToTable("Localization", tb => tb.HasComment("각 스트링에 대한 다국어 지원을 위한 테이블"));

            entity.Property(e => e.RecordId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Localizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Localization_UpdateID");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location", tb =>
                {
                    tb.HasComment("위치 정보 테이블");
                    tb.HasTrigger("TRG_LocationToElevatorIF");
                });

            entity.Property(e => e.DefaultMapType).HasDefaultValueSql("('0')");
            entity.Property(e => e.SecurityLocation).HasDefaultValueSql("('0')");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.LocationLevel).WithMany(p => p.Locations).HasConstraintName("FK_Location_LocationLevel");

            entity.HasOne(d => d.Update).WithMany(p => p.Locations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Location_UpdateID");
        });

        modelBuilder.Entity<LocationLevel>(entity =>
        {
            entity.Property(e => e.LocationLevelId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ManagementSetting>(entity =>
        {
            entity.HasKey(e => new { e.ServiceName, e.ConfKey }).HasName("ManagementSettings_PK");

            entity.Property(e => e.ServiceName).HasComment("사용하는 서비스(프로그램) 이름");
            entity.Property(e => e.ConfKey).HasComment("설정 구분자");
            entity.Property(e => e.ConfValue).HasComment("설정값");
            entity.Property(e => e.Description).HasComment("설정에 대한 설명");
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateDate).HasComment("업데이트 시간");
            entity.Property(e => e.ValueType).HasComment("설정 Value Type - int / string / bool / date ");
        });

        modelBuilder.Entity<ModeChange>(entity =>
        {
            entity.ToTable("ModeChange", tb => tb.HasComment("기기 제어자 항목"));
        });

        modelBuilder.Entity<Org>(entity =>
        {
            entity.ToTable("Org", tb =>
                {
                    tb.HasComment("조직 정보 테이블");
                    tb.HasTrigger("TRG_ORGToElevatorIF");
                });

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Orgs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Org_EqUser");
        });

        modelBuilder.Entity<PanelContent>(entity =>
        {
            entity.ToTable("PanelContent", tb => tb.HasComment(""));
        });

        modelBuilder.Entity<PanelInfo>(entity =>
        {
            entity.ToTable("PanelInfo", tb => tb.HasComment(""));
        });

        modelBuilder.Entity<PanelTypeId>(entity =>
        {
            entity.ToTable("PanelTypeID", tb => tb.HasComment("통제서버에서 사용하는 패널 TypeID 정의"));

            entity.Property(e => e.PanelTypeId1).ValueGeneratedNever();
        });

        modelBuilder.Entity<PatialAccessLocation>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PatialAccessUpload>(entity =>
        {
            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person", tb =>
                {
                    tb.HasComment("사원 테이블");
                    tb.HasTrigger("TRG_PersonToElevatorIF");
                });

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.ValidDate).HasDefaultValueSql("('2099-12-31 01:01:01:001')");

            entity.HasOne(d => d.Grade).WithMany(p => p.People).HasConstraintName("FK_Person_Grade");

            entity.HasOne(d => d.Org).WithMany(p => p.People).HasConstraintName("FK_Person_Org");

            entity.HasOne(d => d.PersonStatus).WithMany(p => p.People)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_PersonStatus");

            entity.HasOne(d => d.PersonType).WithMany(p => p.People)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_PersonType");

            entity.HasOne(d => d.Update).WithMany(p => p.People)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_UpdateID");
        });

        modelBuilder.Entity<PersonBlackList>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__PersonBl__C57755208376AE45");

            entity.Property(e => e.Pid).ValueGeneratedNever();
        });

        modelBuilder.Entity<PersonGroup>(entity =>
        {
            entity.ToTable("PersonGroup", tb => tb.HasComment("출입 권한 그룹 정보 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.PersonGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonGroup_UpdateID");
        });

        modelBuilder.Entity<PersonGroupLink>(entity =>
        {
            entity.ToTable("PersonGroupLink", tb => tb.HasComment("출입 권한 그룹ID와 사원ID를 저장 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.PersonGroupLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonGroupLink_UpdateID");
        });

        modelBuilder.Entity<PersonGroupToEqMaster>(entity =>
        {
            entity.ToTable("PersonGroupToEqMaster", tb => tb.HasComment("사원그룹의 기기 별 출입권한 테이블"));

            entity.Property(e => e.IsSecurity).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.PersonGroupToEqMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonGroupToEqMaster_UpdateID");
        });

        modelBuilder.Entity<PersonStatus>(entity =>
        {
            entity.ToTable("PersonStatus", tb => tb.HasComment("사원의 근무 재직 상태 구분 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.PersonStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonStatus_EqUser");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.ToTable("PersonType", tb => tb.HasComment("사원 구분 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.PersonTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonType_EqUser");
        });

        modelBuilder.Entity<PublicLocationSchedule>(entity =>
        {
            entity.ToTable("PublicLocationSchedule", tb => tb.HasComment("공용부 출입권한 자동등록 버퍼테이블"));

            entity.Property(e => e.Seq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<PwupdateCycle>(entity =>
        {
            entity.ToTable("PWUpdateCycle", tb => tb.HasComment("패스워드 업데이트 주기 테이블"));
        });

        modelBuilder.Entity<PxminitDescribe>(entity =>
        {
            entity.Property(e => e.PxminitUid).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Pxmproperty>(entity =>
        {
            entity.HasKey(e => e.PxmpropertiesUid).HasName("PK_PMSProperties");
        });

        modelBuilder.Entity<PxmproxyDescribe>(entity =>
        {
            entity.Property(e => e.ParentTypeId).HasDefaultValueSql("((-1))");
            entity.Property(e => e.PxmproxyUid).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ReaderUpdateHistory>(entity =>
        {
            entity.HasKey(e => new { e.EventDateTime, e.EqMasterId }).HasName("PK_ReaderUpdateHistory_1");

            entity.Property(e => e.Idx).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ReaderUpdateState>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SecurityMode>(entity =>
        {
            entity.ToTable("SecurityMode", tb => tb.HasComment("방범 모드 정의"));

            entity.Property(e => e.SecurityMode1).IsFixedLength();
        });

        modelBuilder.Entity<ServiceLog>(entity =>
        {
            entity.ToTable("ServiceLog", tb => tb.HasComment("웹에서 처리시, 각 로그를 시간별로 저장한다."));
        });

        modelBuilder.Entity<ServiceVersionList>(entity =>
        {
            entity.Property(e => e.ServiceId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<SetupDatum>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("운영 등록정보"));

            entity.Property(e => e.AutoPaging).HasDefaultValueSql("((1))");
            entity.Property(e => e.ConnectActionFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.DbpollingTime).HasDefaultValueSql("((1))");
            entity.Property(e => e.DdnsreadPort).HasDefaultValueSql("((9001))");
            entity.Property(e => e.DdnsserverIp).HasDefaultValueSql("('203.254.192.21')");
            entity.Property(e => e.DdnswritePort).HasDefaultValueSql("((9000))");
            entity.Property(e => e.EventServerPort).HasDefaultValueSql("((8170))");
            entity.Property(e => e.EventUpdateTime).HasDefaultValueSql("((10))");
            entity.Property(e => e.FdconnectInterval).HasDefaultValueSql("((1))");
            entity.Property(e => e.HideCardNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.ImageUploadUrl).HasDefaultValueSql("('ImageUploadForm.aspx')");
            entity.Property(e => e.MobileCardServer).HasDefaultValueSql("((0))");
            entity.Property(e => e.PersonDeleteOption).HasDefaultValueSql("((0))");
            entity.Property(e => e.PersonInfoKeep).HasDefaultValueSql("((90))");
            entity.Property(e => e.ScreenLockTime).HasDefaultValueSql("((10))");
            entity.Property(e => e.SystemVersion).HasDefaultValueSql("('2.1.0.0')");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.X2CommConnectInterval).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Update).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetupData_UpdateID");
        });

        modelBuilder.Entity<SystemSetup>(entity =>
        {
            entity.ToTable("SystemSetup", tb => tb.HasComment("시스템 환경설정에 대한 설정값 테이블"));

            entity.Property(e => e.AccessAuthority).HasDefaultValueSql("((0))");
            entity.Property(e => e.AccessFireInterlock).HasDefaultValueSql("((0))");
            entity.Property(e => e.AccessRelease).HasDefaultValueSql("((0))");
            entity.Property(e => e.AccessReleasePeriod).HasDefaultValueSql("((0))");
            entity.Property(e => e.AccessReleaseType).HasDefaultValueSql("((0))");
            entity.Property(e => e.AlarmMap).HasDefaultValueSql("((1))");
            entity.Property(e => e.AlarmProcess).HasDefaultValueSql("((0))");
            entity.Property(e => e.AlarmVideoViewer).HasDefaultValueSql("((0))");
            entity.Property(e => e.Attendance).HasDefaultValueSql("((0))");
            entity.Property(e => e.AutoConnectAction).HasDefaultValueSql("((0))");
            entity.Property(e => e.AutoConnectActionIntChecked).IsFixedLength();
            entity.Property(e => e.AutoRegSubEqMaster).HasDefaultValueSql("((0))");
            entity.Property(e => e.CardReaderFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.Cdr0310).HasDefaultValueSql("((0))");
            entity.Property(e => e.DeviceName).HasDefaultValueSql("((0))");
            entity.Property(e => e.EvserverFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.FaceReaderAuthTypeIdfunc).HasDefaultValueSql("((-1))");
            entity.Property(e => e.InDoors).HasDefaultValueSql("((0))");
            entity.Property(e => e.LightSchedule).HasDefaultValueSql("((0))");
            entity.Property(e => e.LockEqUserFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.LockEqUserPeriod).HasDefaultValueSql("((0))");
            entity.Property(e => e.Map3D).HasDefaultValueSql("((0))");
            entity.Property(e => e.PasswordCycle).HasDefaultValueSql("((0))");
            entity.Property(e => e.PatternAnalysis).HasDefaultValueSql("((0))");
            entity.Property(e => e.PersonalNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.PointFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.PublicHolidayFunc)
                .HasDefaultValueSql("((0))")
                .HasComment("법정 공휴일 설정 사용/미사용");
            entity.Property(e => e.SecurityFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.SecurityLocation).HasDefaultValueSql("((0))");
            entity.Property(e => e.SystemSetupId).ValueGeneratedOnAdd();
            entity.Property(e => e.TemperatureDeny)
                .HasDefaultValueSql("((37.5))")
                .HasComment("BIO리더 설정 - 체온 측정 - 출입 불가 온도");
            entity.Property(e => e.TemperatureWarning)
                .HasDefaultValueSql("((37.0))")
                .HasComment("BIO리더 설정 - 체온 측정 - 경고 알림 온도");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.UseMaskMode)
                .HasDefaultValueSql("((1))")
                .HasComment("BIO리더 부가 기능 - 마스크 모드 사용/미사용");
            entity.Property(e => e.UseThermometer)
                .HasDefaultValueSql("((0))")
                .HasComment("BIO리더 부가 기능 - 체온 측정 사용/미사용");
            entity.Property(e => e.Visit).HasDefaultValueSql("((0))");
            entity.Property(e => e.VisitSabunPw).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SystemSetup_UpdateID");
        });

        modelBuilder.Entity<TableHeader>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.TableHeaders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableHeader_UpdateID");
        });

        modelBuilder.Entity<UpgradeEquipment>(entity =>
        {
            entity.ToTable("UpgradeEquipment", tb => tb.HasComment(""));
        });

        modelBuilder.Entity<UpgradeFileList>(entity =>
        {
            entity.ToTable("UpgradeFileList", tb => tb.HasComment(""));
        });

        modelBuilder.Entity<UpgradeInfo>(entity =>
        {
            entity.ToTable("UpgradeInfo", tb => tb.HasComment(""));

            entity.Property(e => e.AutoUpgrade).HasDefaultValueSql("((0))");
            entity.Property(e => e.RebootRestore).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<UpgradeLog>(entity =>
        {
            entity.ToTable("UpgradeLog", tb => tb.HasComment(""));
        });

        modelBuilder.Entity<UpgradeSchedule>(entity =>
        {
            entity.ToTable("UpgradeSchedule", tb => tb.HasComment(""));
        });

        modelBuilder.Entity<UploadControllerInfo>(entity =>
        {
            entity.Property(e => e.UploadId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UploadProgressInfo>(entity =>
        {
            entity.Property(e => e.EqMasterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<UserDefine>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDefine_UpdateID");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.Property(e => e.UseSeq).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UserView>(entity =>
        {
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.UserViews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserView_UpdateID");
        });

        modelBuilder.Entity<Vacamera>(entity =>
        {
            entity.ToTable("VACamera", tb => tb.HasComment("지능형 영상 감시 서버 하단의 카메라 목록"));

            entity.Property(e => e.CameraId).ValueGeneratedNever();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Vacameras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VACamera_UpdateID");
        });

        modelBuilder.Entity<Vatype>(entity =>
        {
            entity.ToTable("VAType", tb => tb.HasComment("지능형 영상 판단 TYPE 정의"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Vatypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VAType_UpdateID");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.ToTable("VehicleType", tb => tb.HasComment("차량 종류 구분(내방객)"));

            entity.Property(e => e.VehicleTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VehicleTypes).HasConstraintName("FK_VehicleType_EqUser");
        });

        modelBuilder.Entity<VideoLayout>(entity =>
        {
            entity.ToTable("VideoLayout", tb => tb.HasComment("비디오 레이아웃 테이블"));

            entity.Property(e => e.LayoutId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VideoLayouts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoLayout_UpdateID");
        });

        modelBuilder.Entity<VideoLayoutLink>(entity =>
        {
            entity.HasKey(e => new { e.LayoutId, e.EqMasterId }).HasName("PK__VideoLay__CCA88347049C3A70");

            entity.ToTable("VideoLayoutLink", tb => tb.HasComment("사용자 비디오 레이아웃에 대한 카메라 저장"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VideoLayoutLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoLayoutLink_UpdateID");
        });

        modelBuilder.Entity<ViewAccess>(entity =>
        {
            entity.ToView("VIEW_ACCESS");
        });

        modelBuilder.Entity<ViewAccessright>(entity =>
        {
            entity.ToView("VIEW_ACCESSRIGHT");
        });

        modelBuilder.Entity<ViewAttendance>(entity =>
        {
            entity.ToView("VIEW_ATTENDANCE");
        });

        modelBuilder.Entity<ViewCardPerson>(entity =>
        {
            entity.ToView("VIEW_CARD_PERSON");
        });

        modelBuilder.Entity<ViewDevice>(entity =>
        {
            entity.ToView("VIEW_DEVICE");
        });

        modelBuilder.Entity<ViewDoorStatus>(entity =>
        {
            entity.ToView("VIEW_DOOR_STATUS");
        });

        modelBuilder.Entity<VisitNotice>(entity =>
        {
            entity.ToTable("VisitNotice", tb => tb.HasComment("내방객 시스템에서 공지사항 기능 테이블(내방객)"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VisitNotices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitNotice_UpdateID");
        });

        modelBuilder.Entity<VisitReason>(entity =>
        {
            entity.ToTable("VisitReason", tb => tb.HasComment("내방목적 테이블(내방객)"));

            entity.Property(e => e.ReservedWord)
                .HasDefaultValueSql("((0))")
                .IsFixedLength();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VisitReasons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitReason_UpdateID");
        });

        modelBuilder.Entity<VisitReserve>(entity =>
        {
            entity.HasKey(e => new { e.VisitId, e.Pid }).HasName("PK_VisitReserve_1");

            entity.ToTable("VisitReserve", tb => tb.HasComment("내방객 예약 및 현황 테이블(내방객)"));

            entity.Property(e => e.VisitId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.VisitObject).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Update).WithMany(p => p.VisitReserves).HasConstraintName("FK_VisitReserve_EqUser");
        });

        modelBuilder.Entity<VisitReserveVisitant>(entity =>
        {
            entity.ToTable("VisitReserveVisitant", tb => tb.HasComment("내방예약 내방객 목록(내방객)"));

            entity.Property(e => e.VisitReserveVisitantId).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VisitReserveVisitants).HasConstraintName("FK_VisitReserveVisitant_EqUser");
        });

        modelBuilder.Entity<VisitSetup>(entity =>
        {
            entity.ToTable("VisitSetup", tb => tb.HasComment("내방객 시스템 환경설정(내방객)"));

            entity.Property(e => e.AssignFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.IdcardScanner).HasDefaultValueSql("((0))");
            entity.Property(e => e.NoticeFunc).HasDefaultValueSql("((0))");
            entity.Property(e => e.SignPadFunc).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<VisitStatus>(entity =>
        {
            entity.HasKey(e => e.VisitStatusId).HasName("PK_VisitCode");

            entity.ToTable("VisitStatus", tb => tb.HasComment("내방객 현 상태 테이블(내방객)"));

            entity.Property(e => e.ReservedWord)
                .HasDefaultValueSql("((0))")
                .IsFixedLength();
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VisitStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitCode_EqUser");
        });

        modelBuilder.Entity<VisitToLocation>(entity =>
        {
            entity.ToTable("VisitToLocation", tb => tb.HasComment("내방객 별 출입위치 테이블(내방객)"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.VisitToLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitToLocation_UpdateID");
        });

        modelBuilder.Entity<Visitant>(entity =>
        {
            entity.ToTable("Visitant", tb => tb.HasComment("내방객 정보(내방객)"));

            entity.Property(e => e.UpdateId).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Visitants).HasConstraintName("FK_Visitant_UpdateID");
        });

        modelBuilder.Entity<WavFile>(entity =>
        {
            entity.ToTable("WavFile", tb => tb.HasComment("웨이브 파일 테이블"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.WavFileNameK).HasDefaultValueSql("(N'Alarm.wav')");

            entity.HasOne(d => d.Update).WithMany(p => p.WavFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WavFile_UpdateID");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.ToTable("Work", tb => tb.HasComment("근태 처리 결과 리포트"));

            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.Works)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Work_EqUser");

            entity.HasOne(d => d.WorkResult).WithMany(p => p.Works).HasConstraintName("FK_Work_WorkResultID");
        });

        modelBuilder.Entity<WorkResultId>(entity =>
        {
            entity.ToTable("WorkResultID", tb => tb.HasComment("근태 결과 스트링 ID 테이블"));

            entity.Property(e => e.ReservedWord).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");

            entity.HasOne(d => d.Update).WithMany(p => p.WorkResultIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkResultID_UpdateID");
        });

        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => new { e.WorkScheduleId, e.Year }).HasName("PK_WordSchedule_1");

            entity.ToTable("WorkSchedule", tb => tb.HasComment("근태 스케쥴 테이블"));

            entity.Property(e => e.WorkScheduleId).ValueGeneratedOnAdd();
            entity.Property(e => e.Fri).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.H1).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.H2).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.H3).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M1).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M10).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M11).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M12).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M2).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M3).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M4).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M5).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M6).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M7).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M8).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.M9).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.Mon).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.Sat).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.Sun).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.Thu).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.Tue).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.UpdateIp).HasDefaultValueSql("('127.0.0.1')");
            entity.Property(e => e.Wed).HasDefaultValueSql("((0.0))");

            entity.HasOne(d => d.Update).WithMany(p => p.WorkSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WordSchedule_EqUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 관리자, 사용자 테이블
/// </summary>
[Table("EqUser")]
public partial class EqUser
{
    [Key]
    [Column("EqUserID")]
    public int EqUserId { get; set; }

    [Column("ID")]
    [StringLength(12)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string? EqUserName { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Tel { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [Column("EqUserLevelID")]
    public int EqUserLevelId { get; set; }

    [Column("ParentEqUserID")]
    public int ParentEqUserId { get; set; }

    [Column("LoginIP")]
    [StringLength(20)]
    [Unicode(false)]
    public string LoginIp { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    public int DeleteFlag { get; set; }

    public int ReservedWord { get; set; }

    [StringLength(500)]
    public string? ExternalFunc1Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc2Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc3Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc4Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc5Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc6Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc7Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc8Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc9Path { get; set; }

    [StringLength(500)]
    public string? ExternalFunc10Path { get; set; }

    [Column("UpdatePWDate", TypeName = "datetime")]
    public DateTime? UpdatePwdate { get; set; }

    [Column("VideoLayoutID")]
    public int? VideoLayoutId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LastLoginDate { get; set; }

    [InverseProperty("Update")]
    public virtual ICollection<AccessPatternObject> AccessPatternObjects { get; set; } = new List<AccessPatternObject>();

    [InverseProperty("Update")]
    public virtual ICollection<AccessPatternPerson> AccessPatternPeople { get; set; } = new List<AccessPatternPerson>();

    [InverseProperty("Update")]
    public virtual ICollection<AccessPattern> AccessPatterns { get; set; } = new List<AccessPattern>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202211> Alarm202211s { get; set; } = new List<Alarm202211>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202212> Alarm202212s { get; set; } = new List<Alarm202212>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202301> Alarm202301s { get; set; } = new List<Alarm202301>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202302> Alarm202302s { get; set; } = new List<Alarm202302>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202303> Alarm202303s { get; set; } = new List<Alarm202303>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202304> Alarm202304s { get; set; } = new List<Alarm202304>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202305> Alarm202305s { get; set; } = new List<Alarm202305>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm202306> Alarm202306s { get; set; } = new List<Alarm202306>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmBak> AlarmBaks { get; set; } = new List<AlarmBak>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmPriority> AlarmPriorities { get; set; } = new List<AlarmPriority>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202211> AlarmProcess202211EqUsers { get; set; } = new List<AlarmProcess202211>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202211> AlarmProcess202211Updates { get; set; } = new List<AlarmProcess202211>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202212> AlarmProcess202212EqUsers { get; set; } = new List<AlarmProcess202212>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202212> AlarmProcess202212Updates { get; set; } = new List<AlarmProcess202212>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202301> AlarmProcess202301EqUsers { get; set; } = new List<AlarmProcess202301>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202301> AlarmProcess202301Updates { get; set; } = new List<AlarmProcess202301>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202302> AlarmProcess202302EqUsers { get; set; } = new List<AlarmProcess202302>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202302> AlarmProcess202302Updates { get; set; } = new List<AlarmProcess202302>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202303> AlarmProcess202303EqUsers { get; set; } = new List<AlarmProcess202303>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202303> AlarmProcess202303Updates { get; set; } = new List<AlarmProcess202303>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202304> AlarmProcess202304EqUsers { get; set; } = new List<AlarmProcess202304>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202304> AlarmProcess202304Updates { get; set; } = new List<AlarmProcess202304>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202305> AlarmProcess202305EqUsers { get; set; } = new List<AlarmProcess202305>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202305> AlarmProcess202305Updates { get; set; } = new List<AlarmProcess202305>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess202306> AlarmProcess202306EqUsers { get; set; } = new List<AlarmProcess202306>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess202306> AlarmProcess202306Updates { get; set; } = new List<AlarmProcess202306>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcessBak> AlarmProcessBakEqUsers { get; set; } = new List<AlarmProcessBak>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcessBak> AlarmProcessBakUpdates { get; set; } = new List<AlarmProcessBak>();

    [InverseProperty("EqUser")]
    public virtual ICollection<AlarmProcess> AlarmProcessEqUsers { get; set; } = new List<AlarmProcess>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcessPlan> AlarmProcessPlans { get; set; } = new List<AlarmProcessPlan>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcessType> AlarmProcessTypes { get; set; } = new List<AlarmProcessType>();

    [InverseProperty("Update")]
    public virtual ICollection<AlarmProcess> AlarmProcessUpdates { get; set; } = new List<AlarmProcess>();

    [InverseProperty("Update")]
    public virtual ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();

    [InverseProperty("Update")]
    public virtual ICollection<AnnounceToCr> AnnounceToCrs { get; set; } = new List<AnnounceToCr>();

    [InverseProperty("Update")]
    public virtual ICollection<Announce> Announces { get; set; } = new List<Announce>();

    [InverseProperty("Update")]
    public virtual ICollection<Cad2D> Cad2Ds { get; set; } = new List<Cad2D>();

    [InverseProperty("Update")]
    public virtual ICollection<Cad3D> Cad3Ds { get; set; } = new List<Cad3D>();

    [InverseProperty("Update")]
    public virtual ICollection<CardStatus> CardStatuses { get; set; } = new List<CardStatus>();

    [InverseProperty("Update")]
    public virtual ICollection<CardToEqMasterInactive> CardToEqMasterInactives { get; set; } = new List<CardToEqMasterInactive>();

    [InverseProperty("Update")]
    public virtual ICollection<CardType> CardTypes { get; set; } = new List<CardType>();

    [InverseProperty("Update")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    [InverseProperty("Update")]
    public virtual ICollection<Carlendar> Carlendars { get; set; } = new List<Carlendar>();

    [InverseProperty("Update")]
    public virtual ICollection<ConsoleObject> ConsoleObjects { get; set; } = new List<ConsoleObject>();

    [InverseProperty("Update")]
    public virtual ICollection<DashBoardObject> DashBoardObjects { get; set; } = new List<DashBoardObject>();

    [InverseProperty("Update")]
    public virtual ICollection<DashBoard> DashBoards { get; set; } = new List<DashBoard>();

    [InverseProperty("Update")]
    public virtual ICollection<DoorAccessModeSchedule> DoorAccessModeSchedules { get; set; } = new List<DoorAccessModeSchedule>();

    [InverseProperty("Update")]
    public virtual ICollection<DoorRunSchedule> DoorRunSchedules { get; set; } = new List<DoorRunSchedule>();

    [InverseProperty("Update")]
    public virtual ICollection<Dvrtype> Dvrtypes { get; set; } = new List<Dvrtype>();

    [InverseProperty("Update")]
    public virtual ICollection<EqCodeList> EqCodeLists { get; set; } = new List<EqCodeList>();

    [InverseProperty("Update")]
    public virtual ICollection<EqMasterGroupLink> EqMasterGroupLinks { get; set; } = new List<EqMasterGroupLink>();

    [InverseProperty("Update")]
    public virtual ICollection<EqMasterGroup> EqMasterGroups { get; set; } = new List<EqMasterGroup>();

    [InverseProperty("Update")]
    public virtual ICollection<EqMaster> EqMasters { get; set; } = new List<EqMaster>();

    [InverseProperty("Update")]
    public virtual ICollection<EqStatus> EqStatuses { get; set; } = new List<EqStatus>();

    [InverseProperty("Update")]
    public virtual ICollection<EqType> EqTypes { get; set; } = new List<EqType>();

    [ForeignKey("EqUserLevelId")]
    [InverseProperty("EqUsers")]
    public virtual EqUserLevel EqUserLevel { get; set; } = null!;

    [InverseProperty("EqUser")]
    public virtual ICollection<EqUserRecentMenu> EqUserRecentMenus { get; set; } = new List<EqUserRecentMenu>();

    [InverseProperty("Update")]
    public virtual ICollection<FaceDetectParam> FaceDetectParams { get; set; } = new List<FaceDetectParam>();

    [InverseProperty("Update")]
    public virtual ICollection<FingerToCr> FingerToCrs { get; set; } = new List<FingerToCr>();

    [InverseProperty("Update")]
    public virtual ICollection<Finger> Fingers { get; set; } = new List<Finger>();

    [InverseProperty("Update")]
    public virtual ICollection<GateViewerEqMaster> GateViewerEqMasters { get; set; } = new List<GateViewerEqMaster>();

    [InverseProperty("Update")]
    public virtual ICollection<GismapObject> GismapObjects { get; set; } = new List<GismapObject>();

    [InverseProperty("Update")]
    public virtual ICollection<Gismap> Gismaps { get; set; } = new List<Gismap>();

    [InverseProperty("Update")]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    [InverseProperty("Update")]
    public virtual ICollection<InterLockObject> InterLockObjects { get; set; } = new List<InterLockObject>();

    [InverseProperty("Update")]
    public virtual ICollection<InterLock> InterLocks { get; set; } = new List<InterLock>();

    [InverseProperty("Update")]
    public virtual ICollection<IpcameraRtspaddress> IpcameraRtspaddresses { get; set; } = new List<IpcameraRtspaddress>();

    [InverseProperty("Update")]
    public virtual ICollection<JoyStick> JoySticks { get; set; } = new List<JoyStick>();

    [InverseProperty("Update")]
    public virtual ICollection<LightSchedule> LightSchedules { get; set; } = new List<LightSchedule>();

    [InverseProperty("Update")]
    public virtual ICollection<Localization> Localizations { get; set; } = new List<Localization>();

    [InverseProperty("Update")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [InverseProperty("Update")]
    public virtual ICollection<Org> Orgs { get; set; } = new List<Org>();

    [InverseProperty("Update")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [InverseProperty("Update")]
    public virtual ICollection<PersonGroupLink> PersonGroupLinks { get; set; } = new List<PersonGroupLink>();

    [InverseProperty("Update")]
    public virtual ICollection<PersonGroupToEqMaster> PersonGroupToEqMasters { get; set; } = new List<PersonGroupToEqMaster>();

    [InverseProperty("Update")]
    public virtual ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();

    [InverseProperty("Update")]
    public virtual ICollection<PersonStatus> PersonStatuses { get; set; } = new List<PersonStatus>();

    [InverseProperty("Update")]
    public virtual ICollection<PersonType> PersonTypes { get; set; } = new List<PersonType>();

    [InverseProperty("Update")]
    public virtual ICollection<TableHeader> TableHeaders { get; set; } = new List<TableHeader>();

    [InverseProperty("Update")]
    public virtual ICollection<UserView> UserViews { get; set; } = new List<UserView>();

    [InverseProperty("Update")]
    public virtual ICollection<Vacamera> Vacameras { get; set; } = new List<Vacamera>();

    [InverseProperty("Update")]
    public virtual ICollection<Vatype> Vatypes { get; set; } = new List<Vatype>();

    [InverseProperty("Update")]
    public virtual ICollection<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();

    [InverseProperty("Update")]
    public virtual ICollection<VideoLayoutLink> VideoLayoutLinks { get; set; } = new List<VideoLayoutLink>();

    [InverseProperty("Update")]
    public virtual ICollection<VideoLayout> VideoLayouts { get; set; } = new List<VideoLayout>();

    [InverseProperty("Update")]
    public virtual ICollection<VisitNotice> VisitNotices { get; set; } = new List<VisitNotice>();

    [InverseProperty("Update")]
    public virtual ICollection<VisitReason> VisitReasons { get; set; } = new List<VisitReason>();

    [InverseProperty("Update")]
    public virtual ICollection<VisitReserveVisitant> VisitReserveVisitants { get; set; } = new List<VisitReserveVisitant>();

    [InverseProperty("Update")]
    public virtual ICollection<VisitReserve> VisitReserves { get; set; } = new List<VisitReserve>();

    [InverseProperty("Update")]
    public virtual ICollection<VisitStatus> VisitStatuses { get; set; } = new List<VisitStatus>();

    [InverseProperty("Update")]
    public virtual ICollection<VisitToLocation> VisitToLocations { get; set; } = new List<VisitToLocation>();

    [InverseProperty("Update")]
    public virtual ICollection<Visitant> Visitants { get; set; } = new List<Visitant>();

    [InverseProperty("Update")]
    public virtual ICollection<WavFile> WavFiles { get; set; } = new List<WavFile>();

    [InverseProperty("Update")]
    public virtual ICollection<WorkResultId> WorkResultIds { get; set; } = new List<WorkResultId>();

    [InverseProperty("Update")]
    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();

    [InverseProperty("Update")]
    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}

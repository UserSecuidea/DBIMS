using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 관리자 / 사용자 레벨 테이블
/// </summary>
[Table("EqUserLevel")]
public partial class EqUserLevel
{
    [Key]
    [Column("LevelID")]
    public int LevelId { get; set; }

    [Column("LevelStringID")]
    public int? LevelStringId { get; set; }

    [StringLength(50)]
    public string? LevelStringName { get; set; }

    public int CardToDoorFunc { get; set; }

    public int DeviceControlFunc { get; set; }

    public int CadEditorFunc { get; set; }

    public int CameraFunc { get; set; }

    public int VisitFunc { get; set; }

    public int WorkScheduleFunc { get; set; }

    public int ExcelExportFunc { get; set; }

    public int BasicFunc { get; set; }

    public int DeviceFunc { get; set; }

    public int OrgManage { get; set; }

    public int GradeManage { get; set; }

    public int PersonManage { get; set; }

    public int CardManage { get; set; }

    public int CardToDoorManage { get; set; }

    public int AccessGroup { get; set; }

    public int DeviceScheduleSet { get; set; }

    public int DoorPropertySet { get; set; }

    public int AccessPatternSet { get; set; }

    public int AttendanceSet { get; set; }

    [Column("FPReaderSet")]
    public int FpreaderSet { get; set; }

    public int ProxyDeviceSet { get; set; }

    public int CommandSet { get; set; }

    public int EventSet { get; set; }

    public int RuleDesignSet { get; set; }

    public int EventSearch { get; set; }

    public int ReservedWord { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    public int AccessReleaseManage { get; set; }

    public int ElevatorControlServer { get; set; }

    [InverseProperty("EqUserLevel")]
    public virtual ICollection<EqUser> EqUsers { get; set; } = new List<EqUser>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 근태 스케쥴 테이블
/// </summary>
[PrimaryKey("WorkScheduleId", "Year")]
[Table("WorkSchedule")]
public partial class WorkSchedule
{
    [Key]
    [Column("WorkScheduleID")]
    public int WorkScheduleId { get; set; }

    [StringLength(50)]
    public string WorkScheduleName { get; set; } = null!;

    [Key]
    [StringLength(4)]
    [Unicode(false)]
    public string Year { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string? M1 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M2 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M3 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M4 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M5 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M6 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M7 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M8 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M9 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M10 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M11 { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string? M12 { get; set; }

    [Column("SUN")]
    [StringLength(8)]
    [Unicode(false)]
    public string Sun { get; set; } = null!;

    [Column("MON")]
    [StringLength(8)]
    [Unicode(false)]
    public string Mon { get; set; } = null!;

    [Column("TUE")]
    [StringLength(8)]
    [Unicode(false)]
    public string Tue { get; set; } = null!;

    [Column("WED")]
    [StringLength(8)]
    [Unicode(false)]
    public string Wed { get; set; } = null!;

    [Column("THU")]
    [StringLength(8)]
    [Unicode(false)]
    public string Thu { get; set; } = null!;

    [Column("FRI")]
    [StringLength(8)]
    [Unicode(false)]
    public string Fri { get; set; } = null!;

    [Column("SAT")]
    [StringLength(8)]
    [Unicode(false)]
    public string Sat { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string H1 { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string H2 { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string H3 { get; set; } = null!;

    [StringLength(4)]
    [Unicode(false)]
    public string? BeforeTime { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? AfterTime { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? OverTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("WorkSchedules")]
    public virtual EqUser Update { get; set; } = null!;
}

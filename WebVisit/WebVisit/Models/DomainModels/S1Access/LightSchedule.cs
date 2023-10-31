using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 조명 스케쥴 테이블
/// </summary>
[PrimaryKey("EqMasterId", "Year")]
[Table("LightSchedule")]
public partial class LightSchedule
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [StringLength(4)]
    [Unicode(false)]
    public string Year { get; set; } = null!;

    [Column("SUN")]
    [StringLength(30)]
    [Unicode(false)]
    public string Sun { get; set; } = null!;

    [Column("MON")]
    [StringLength(30)]
    [Unicode(false)]
    public string Mon { get; set; } = null!;

    [Column("TUE")]
    [StringLength(30)]
    [Unicode(false)]
    public string Tue { get; set; } = null!;

    [Column("WED")]
    [StringLength(30)]
    [Unicode(false)]
    public string Wed { get; set; } = null!;

    [Column("THU")]
    [StringLength(30)]
    [Unicode(false)]
    public string Thu { get; set; } = null!;

    [Column("FRI")]
    [StringLength(30)]
    [Unicode(false)]
    public string Fri { get; set; } = null!;

    [Column("SAT")]
    [StringLength(30)]
    [Unicode(false)]
    public string Sat { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string H1 { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string H2 { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string H3 { get; set; } = null!;

    public int? DownFlag { get; set; }

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

    [ForeignKey("UpdateId")]
    [InverseProperty("LightSchedules")]
    public virtual EqUser Update { get; set; } = null!;
}

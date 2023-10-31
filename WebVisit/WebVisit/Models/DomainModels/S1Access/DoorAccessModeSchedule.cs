using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입모드 스케쥴 테이블
/// </summary>
[PrimaryKey("EqMasterId", "Year")]
[Table("DoorAccessModeSchedule")]
public partial class DoorAccessModeSchedule
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [StringLength(4)]
    [Unicode(false)]
    public string Year { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode1 { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode2 { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode3 { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode4 { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode5 { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode6 { get; set; } = null!;

    [StringLength(800)]
    [Unicode(false)]
    public string Mode7 { get; set; } = null!;

    public int DownFlag { get; set; }

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
    [InverseProperty("DoorAccessModeSchedules")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 근태 처리 결과 리포트
/// </summary>
[PrimaryKey("WorkResultDate", "WorkScheduleId", "Pid")]
[Table("Work")]
public partial class Work
{
    [Key]
    [Column(TypeName = "date")]
    public DateTime WorkResultDate { get; set; }

    [Key]
    [Column("WorkScheduleID")]
    public int WorkScheduleId { get; set; }

    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? InTime { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? OutTime { get; set; }

    [Column("WorkResultID")]
    public int? WorkResultId { get; set; }

    [StringLength(30)]
    public string? Etc { get; set; }

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
    [InverseProperty("Works")]
    public virtual EqUser Update { get; set; } = null!;

    [ForeignKey("WorkResultId")]
    [InverseProperty("Works")]
    public virtual WorkResultId? WorkResult { get; set; }
}

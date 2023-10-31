using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방객 예약 및 현황 테이블(내방객)
/// </summary>
[PrimaryKey("VisitId", "Pid")]
[Table("VisitReserve")]
public partial class VisitReserve
{
    [Key]
    [Column("VisitID")]
    public int VisitId { get; set; }

    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    public int? VisitObject { get; set; }

    [Column("RegisterPID")]
    public int? RegisterPid { get; set; }

    [Column("VisitSDate", TypeName = "datetime")]
    public DateTime VisitSdate { get; set; }

    [Column("VisitEDate", TypeName = "datetime")]
    public DateTime VisitEdate { get; set; }

    [Column("VisitStatusID")]
    public int VisitStatusId { get; set; }

    [Column("VisitReasonID")]
    public int VisitReasonId { get; set; }

    [StringLength(50)]
    public string? VisitReasonText { get; set; }

    public int? VisitProtocol { get; set; }

    public int VisitAssign { get; set; }

    [Column("VisitAssignPID")]
    public int? VisitAssignPid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("VisitReserves")]
    public virtual EqUser? Update { get; set; }
}

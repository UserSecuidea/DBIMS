using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방예약 내방객 목록(내방객)
/// </summary>
[PrimaryKey("VisitReserveVisitantId", "VisitId")]
[Table("VisitReserveVisitant")]
public partial class VisitReserveVisitant
{
    [Key]
    [Column("VisitReserveVisitantID")]
    public int VisitReserveVisitantId { get; set; }

    [Key]
    [Column("VisitID")]
    public int VisitId { get; set; }

    [Column("VisitantID")]
    public int? VisitantId { get; set; }

    [Column("PID")]
    public int? Pid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? VisitInTime { get; set; }

    [StringLength(30)]
    public string? LicensePlateNumber { get; set; }

    [Column("VehicleTypeID")]
    public int? VehicleTypeId { get; set; }

    [Column("VisitStatusID")]
    public int VisitStatusId { get; set; }

    [Column("CardID")]
    public int? CardId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("updateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("VisitReserveVisitants")]
    public virtual EqUser? Update { get; set; }
}

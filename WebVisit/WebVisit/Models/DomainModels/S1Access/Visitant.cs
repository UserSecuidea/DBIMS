using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방객 정보(내방객)
/// </summary>
[Table("Visitant")]
public partial class Visitant
{
    [Key]
    [Column("VisitantID")]
    public int VisitantId { get; set; }

    [StringLength(50)]
    public string VisitantName { get; set; } = null!;

    [Column("VisitantYMD")]
    [StringLength(6)]
    [Unicode(false)]
    public string VisitantYmd { get; set; } = null!;

    [StringLength(30)]
    public string? VisitantCompany { get; set; }

    [StringLength(100)]
    public string? Address { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? OfficeTel { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    public int Agreement { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AgreementDate { get; set; }

    public byte[] SignImage { get; set; } = null!;

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
    [InverseProperty("Visitants")]
    public virtual EqUser? Update { get; set; }
}

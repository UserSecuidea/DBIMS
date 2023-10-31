using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방목적 테이블(내방객)
/// </summary>
[Table("VisitReason")]
public partial class VisitReason
{
    [Key]
    [Column("VisitReasonID")]
    public int VisitReasonId { get; set; }

    [Column("VisitReasonNameID")]
    public int VisitReasonNameId { get; set; }

    [StringLength(1)]
    public string? ReservedWord { get; set; }

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
    [InverseProperty("VisitReasons")]
    public virtual EqUser Update { get; set; } = null!;
}

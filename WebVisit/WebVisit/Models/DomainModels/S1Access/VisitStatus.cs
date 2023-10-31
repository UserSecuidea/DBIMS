using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방객 현 상태 테이블(내방객)
/// </summary>
[Table("VisitStatus")]
public partial class VisitStatus
{
    [Key]
    [Column("VisitStatusID")]
    public int VisitStatusId { get; set; }

    [Column("VisitStatusNameID")]
    public int VisitStatusNameId { get; set; }

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
    [InverseProperty("VisitStatuses")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방객 별 출입위치 테이블(내방객)
/// </summary>
[PrimaryKey("VisitId", "LocationId")]
[Table("VisitToLocation")]
public partial class VisitToLocation
{
    [Key]
    [Column("VisitID")]
    public int VisitId { get; set; }

    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

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
    [InverseProperty("VisitToLocations")]
    public virtual EqUser Update { get; set; } = null!;
}

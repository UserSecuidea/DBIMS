using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 위치 정보 테이블
/// </summary>
[Table("Location")]
[Index("LocationId", Name = "IX_Location")]
public partial class Location
{
    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

    [StringLength(50)]
    public string LocationName { get; set; } = null!;

    public int? ParentLocationCode { get; set; }

    [Column("LocationLevelID")]
    public int? LocationLevelId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BackGroundImage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BackGroundImageHash { get; set; }

    public int SecurityLocation { get; set; }

    public int DefaultMapType { get; set; }

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

    [InverseProperty("Location")]
    public virtual ICollection<Cad2D> Cad2Ds { get; set; } = new List<Cad2D>();

    [InverseProperty("Location")]
    public virtual ICollection<EqMaster> EqMasters { get; set; } = new List<EqMaster>();

    [ForeignKey("LocationLevelId")]
    [InverseProperty("Locations")]
    public virtual LocationLevel? LocationLevel { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("Locations")]
    public virtual EqUser Update { get; set; } = null!;
}

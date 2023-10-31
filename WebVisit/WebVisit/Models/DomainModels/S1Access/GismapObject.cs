using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// GIS 위에 작도한 도면 Object를 저장하는 테이블
/// </summary>
[PrimaryKey("GismapId", "ObjectId", "EqMasterId", "ObjectType")]
[Table("GISMapObject")]
public partial class GismapObject
{
    [Key]
    [Column("GISMapID")]
    public int GismapId { get; set; }

    [Key]
    [Column("ObjectID")]
    public int ObjectId { get; set; }

    [Key]
    public int ObjectType { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Lat { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Lng { get; set; } = null!;

    [Column("IConFile")]
    [StringLength(100)]
    public string? IconFile { get; set; }

    [Column("MoveLocationID")]
    public int? MoveLocationId { get; set; }

    [Column("InterLockID")]
    public int? InterLockId { get; set; }

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
    [InverseProperty("GismapObjects")]
    public virtual EqUser Update { get; set; } = null!;
}

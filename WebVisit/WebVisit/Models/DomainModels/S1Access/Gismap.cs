using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// GIS 위에 작도한 도면 Object를 저장하는 테이블
/// </summary>
[Table("GISMap")]
public partial class Gismap
{
    [Key]
    [Column("GISMapID")]
    public int GismapId { get; set; }

    [Column("GISTypeID")]
    public int GistypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Lat { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Lng { get; set; } = null!;

    public int Zoom { get; set; }

    [Column("MapViewTypeID")]
    public int MapViewTypeId { get; set; }

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
    [InverseProperty("Gismaps")]
    public virtual EqUser Update { get; set; } = null!;
}

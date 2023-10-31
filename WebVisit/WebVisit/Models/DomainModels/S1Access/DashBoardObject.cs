using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 데시보드 오브젝트
/// </summary>
[Table("DashBoardObject")]
public partial class DashBoardObject
{
    [Key]
    [Column("DashBoardObjectID")]
    public int DashBoardObjectId { get; set; }

    [StringLength(50)]
    public string? DashBoardObjectName { get; set; }

    public int? Grid1PanelType { get; set; }

    public double? Grid1X { get; set; }

    public double? Grid1Y { get; set; }

    public int? Grid2PanelType { get; set; }

    public double? Grid2X { get; set; }

    public double? Grid2Y { get; set; }

    public int? Grid3PanelType { get; set; }

    public double? Grid3X { get; set; }

    public double? Grid3Y { get; set; }

    public int? Grid4PanelType { get; set; }

    public double? Grid4X { get; set; }

    public double? Grid4Y { get; set; }

    public int? Grid5PanelType { get; set; }

    public double? Grid5X { get; set; }

    public double? Grid5Y { get; set; }

    public int? Grid6PanelType { get; set; }

    public double? Grid6X { get; set; }

    public double? Grid6Y { get; set; }

    public int? Grid7PanelType { get; set; }

    public double? Grid7X { get; set; }

    public double? Grid7Y { get; set; }

    public int? Grid8PanelType { get; set; }

    public double? Grid8X { get; set; }

    public double? Grid8Y { get; set; }

    public int? Grid9PanelType { get; set; }

    public double? Grid9X { get; set; }

    public double? Grid9Y { get; set; }

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
    [InverseProperty("DashBoardObjects")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("Cad2D")]
public partial class Cad2D
{
    [Key]
    [Column("ObjectID")]
    public int ObjectId { get; set; }

    [Column("LocationID")]
    public int LocationId { get; set; }

    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column("ObjectTypeID")]
    public int ObjectTypeId { get; set; }

    [Column("PID")]
    public int? Pid { get; set; }

    public double? X1 { get; set; }

    public double? Y1 { get; set; }

    public double? X2 { get; set; }

    public double? Y2 { get; set; }

    public int? Division { get; set; }

    [Column("XYRatio")]
    public double? Xyratio { get; set; }

    public double? Rotate { get; set; }

    [Column("ZOrder")]
    public int? Zorder { get; set; }

    [StringLength(50)]
    public string? ImageFileName { get; set; }

    public int? BrushStyle { get; set; }

    public int? BrushColor1 { get; set; }

    public int? BrushColor2 { get; set; }

    public int? PenStyle { get; set; }

    public int? PenColor { get; set; }

    public int? PenWidth { get; set; }

    public int? TextColor { get; set; }

    [StringLength(20)]
    public string? FontName { get; set; }

    public int? FontType { get; set; }

    public int? FontSize { get; set; }

    [Column("MoveLocationID")]
    public int? MoveLocationId { get; set; }

    [Column("InterLockID")]
    public int? InterLockId { get; set; }

    [Column("VAlign")]
    public int? Valign { get; set; }

    [Column("HAlign")]
    public int? Halign { get; set; }

    public int? Transparency { get; set; }

    public int? FixPosition { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? EqCommand { get; set; }

    public int EqMasterGroupUse { get; set; }

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

    [ForeignKey("LocationId")]
    [InverseProperty("Cad2Ds")]
    public virtual Location Location { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("Cad2Ds")]
    public virtual EqUser Update { get; set; } = null!;
}

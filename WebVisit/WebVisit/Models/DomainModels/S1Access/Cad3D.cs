using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("Cad3D")]
public partial class Cad3D
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Column("EqTypeID")]
    public int EqTypeId { get; set; }

    [Column("LA")]
    [StringLength(50)]
    [Unicode(false)]
    public string La { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Building { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Floor { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ObjectModel { get; set; } = null!;

    public double Matrix11 { get; set; }

    public double Matrix12 { get; set; }

    public double Matrix13 { get; set; }

    public double Matrix14 { get; set; }

    public double Matrix21 { get; set; }

    public double Matrix22 { get; set; }

    public double Matrix23 { get; set; }

    public double Matrix24 { get; set; }

    public double Matrix31 { get; set; }

    public double Matrix32 { get; set; }

    public double Matrix33 { get; set; }

    public double Matrix34 { get; set; }

    public double Matrix41 { get; set; }

    public double Matrix42 { get; set; }

    public double Matrix43 { get; set; }

    public double Matrix44 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Tooltip { get; set; } = null!;

    [Column("Scale_x")]
    public double ScaleX { get; set; }

    [Column("Scale_y")]
    public double ScaleY { get; set; }

    [Column("Scale_z")]
    public double ScaleZ { get; set; }

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
    [InverseProperty("Cad3Ds")]
    public virtual EqUser Update { get; set; } = null!;
}

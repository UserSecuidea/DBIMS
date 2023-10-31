using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("BatchList")]
public partial class BatchList
{
    [Key]
    public int BatId { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string BatUserName { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string BatType { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string BatDes { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string BatName { get; set; } = null!;

    [Column("batLocation")]
    [StringLength(128)]
    [Unicode(false)]
    public string? BatLocation { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string Period { get; set; } = null!;

    public int? ProcessN { get; set; }

    [Column("ProcessHM")]
    public int? ProcessHm { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? SunDay { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? MonDay { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? TueDay { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? WedDay { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ThursDay { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? FriDay { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? SatDay { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomKey1 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomValue1 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomKey2 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomValue2 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomKey3 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomValue3 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomKey4 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomValue4 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomKey5 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CustomValue5 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActivateDate { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? Result { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ActionFlag { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string IsUsing { get; set; } = null!;

    [Column("UpdateID")]
    public int? UpdateId { get; set; }
}

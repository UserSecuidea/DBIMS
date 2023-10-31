using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 지능형 영상 판단 TYPE 정의
/// </summary>
[Table("VAType")]
public partial class Vatype
{
    [Key]
    [Column("VATypeID")]
    public int VatypeId { get; set; }

    [Column("VATypeNameID")]
    public int VatypeNameId { get; set; }

    [Column("OV_FMS")]
    public int? OvFms { get; set; }

    [Column("SVMS")]
    public int? Svms { get; set; }

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
    [InverseProperty("Vatypes")]
    public virtual EqUser Update { get; set; } = null!;
}

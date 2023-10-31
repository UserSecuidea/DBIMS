using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("EqMasterLoopSecurityLink")]
[Index("SecurityEqMasterId", Name = "NonClusteredIndex-SecurityEqMasterID")]
public partial class EqMasterLoopSecurityLink
{
    [Column("EqMasterLinkUID")]
    public int EqMasterLinkUid { get; set; }

    [Key]
    [Column("LoopEqMasterID")]
    public int LoopEqMasterId { get; set; }

    [Column("SecurityEqMasterID")]
    public int SecurityEqMasterId { get; set; }

    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("CardId", "EqMasterId")]
[Table("CardToEqMaster")]
[Index("DownFlag", Name = "IX_CardToEqMaster")]
public partial class CardToEqMaster
{
    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string IsSecurity { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string IsRelease { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string IsAccess { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string IsMaster { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string AccessPattern { get; set; } = null!;

    [StringLength(32)]
    [Unicode(false)]
    public string Trace { get; set; } = null!;

    public int DownFlag { get; set; }

    [Column("UpdateEqMasterID")]
    public int? UpdateEqMasterId { get; set; }

    [Column("IsSecurity_EqMasterID")]
    [StringLength(500)]
    [Unicode(false)]
    public string? IsSecurityEqMasterId { get; set; }

    [Column("IsAccess_EqMasterID")]
    [StringLength(1024)]
    [Unicode(false)]
    public string? IsAccessEqMasterId { get; set; }

    [Column("IsMaster_EqMasterID")]
    [StringLength(1024)]
    [Unicode(false)]
    public string? IsMasterEqMasterId { get; set; }

    [Column("AccessPattern_EqMasterID")]
    [StringLength(1024)]
    [Unicode(false)]
    public string? AccessPatternEqMasterId { get; set; }

    [Column("Trace_EqMasterID")]
    [StringLength(500)]
    [Unicode(false)]
    public string? TraceEqMasterId { get; set; }

    [Column("LCIndex")]
    public int? Lcindex { get; set; }

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

    public int DelayCount { get; set; }
}

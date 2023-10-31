using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("UpgradeInfo")]
public partial class UpgradeInfo
{
    [Key]
    [Column("UpgradeInfoID")]
    public int UpgradeInfoId { get; set; }

    [Column("SWType")]
    [StringLength(24)]
    public string Swtype { get; set; } = null!;

    [StringLength(1000)]
    public string ContractNo { get; set; } = null!;

    [StringLength(16)]
    public string TargetVersion { get; set; } = null!;

    [StringLength(16)]
    public string UpgradeVersion { get; set; } = null!;

    [StringLength(64)]
    public string VersionFileName { get; set; } = null!;

    [StringLength(512)]
    public string DownloadUrl { get; set; } = null!;

    public int? AutoUpgrade { get; set; }

    public int? RebootRestore { get; set; }

    public string UgradeFileListXml { get; set; } = null!;

    public int IsUpgrade { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

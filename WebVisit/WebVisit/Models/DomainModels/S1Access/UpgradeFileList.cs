using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("UpgradeFileList")]
public partial class UpgradeFileList
{
    [Key]
    [Column("UpgradeFileListID")]
    public int UpgradeFileListId { get; set; }

    [Column("UpgradeInfoID")]
    public int UpgradeInfoId { get; set; }

    public int ProgramType { get; set; }

    [StringLength(64)]
    public string UpgradeFileName { get; set; } = null!;

    [StringLength(64)]
    public string UpgradeCheckSum { get; set; } = null!;

    public int UpgradeFileSize { get; set; }

    [StringLength(512)]
    public string UpgradeFileUrl { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("UpgradeLog")]
public partial class UpgradeLog
{
    [Key]
    [Column("UpgradeLogID")]
    public int UpgradeLogId { get; set; }

    [Column("UpgradeEquipmentID")]
    public int UpgradeEquipmentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("UpgradeLog")]
    [StringLength(1000)]
    public string? UpgradeLog1 { get; set; }
}

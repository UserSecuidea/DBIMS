using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("UpgradeEquipment")]
public partial class UpgradeEquipment
{
    [Key]
    [Column("UpgradeEquipmentID")]
    public int UpgradeEquipmentId { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string MacAddress { get; set; } = null!;

    [Column("EqIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string EqIp { get; set; } = null!;

    public int UpgradeFlag { get; set; }

    [StringLength(512)]
    public string? Properties { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

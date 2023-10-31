using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("UpgradeSchedule")]
public partial class UpgradeSchedule
{
    [Key]
    [Column("UpgradeScheduleID")]
    public int UpgradeScheduleId { get; set; }

    [Column("UpgradeEquipmentID")]
    public int UpgradeEquipmentId { get; set; }

    public int IsMasterSchedule { get; set; }

    public int UseScheduleUpgrade { get; set; }

    public int ScheduleInstallType { get; set; }

    public int ScheduleConfirmType { get; set; }

    public int SchedulePeriodDayOfWeek { get; set; }

    public int SchedulePeriodTime { get; set; }

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

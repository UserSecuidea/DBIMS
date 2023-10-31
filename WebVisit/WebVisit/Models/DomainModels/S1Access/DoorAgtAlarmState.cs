using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("DoorAgtAlarmState")]
public partial class DoorAgtAlarmState
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int DoorMode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? DoorState { get; set; }

    [Column("SWDoorState")]
    [StringLength(30)]
    [Unicode(false)]
    public string? SwdoorState { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NotifyDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(20)]
    public string? UpdateIp { get; set; }
}

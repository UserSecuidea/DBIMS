﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("AlarmProcessId", "AlarmId")]
[Table("AlarmProcess_202211")]
public partial class AlarmProcess202211
{
    [Key]
    [Column("AlarmProcessID")]
    public int AlarmProcessId { get; set; }

    [Key]
    [Column("AlarmID")]
    public int AlarmId { get; set; }

    [Column("EqUserID")]
    public int EqUserId { get; set; }

    [Column("AlarmProcessPlanID")]
    public int AlarmProcessPlanId { get; set; }

    [Column("AlarmPriorityID")]
    public int AlarmPriorityId { get; set; }

    [Column("AlarmProcessTypeID")]
    public int AlarmProcessTypeId { get; set; }

    [Column("AlarmReasonTypeID")]
    public int AlarmReasonTypeId { get; set; }

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

    [ForeignKey("AlarmReasonTypeId")]
    [InverseProperty("AlarmProcess202211s")]
    public virtual AlarmReasonType AlarmReasonType { get; set; } = null!;

    [ForeignKey("EqUserId")]
    [InverseProperty("AlarmProcess202211EqUsers")]
    public virtual EqUser EqUser { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("AlarmProcess202211Updates")]
    public virtual EqUser Update { get; set; } = null!;
}

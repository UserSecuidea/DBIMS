using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("AlarmProcessPlanId", "EqUserId")]
[Table("AlarmProcessPlan")]
public partial class AlarmProcessPlan
{
    [Key]
    [Column("AlarmProcessPlanID")]
    public int AlarmProcessPlanId { get; set; }

    [Key]
    [Column("EqUserID")]
    public int EqUserId { get; set; }

    [StringLength(100)]
    public string PlanName { get; set; } = null!;

    public string PlanData { get; set; } = null!;

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
    [InverseProperty("AlarmProcessPlans")]
    public virtual EqUser Update { get; set; } = null!;
}

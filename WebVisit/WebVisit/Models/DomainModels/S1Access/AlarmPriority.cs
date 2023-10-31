using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AlarmPriority")]
public partial class AlarmPriority
{
    [Key]
    [Column("AlarmPriorityID")]
    public int AlarmPriorityId { get; set; }

    [Column("AlarmPriorityNameID")]
    public int AlarmPriorityNameId { get; set; }

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

    [ForeignKey("UpdateId")]
    [InverseProperty("AlarmPriorities")]
    public virtual EqUser Update { get; set; } = null!;
}

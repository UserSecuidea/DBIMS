using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("AccessPatternId", "Pid")]
[Table("AccessPatternPerson")]
public partial class AccessPatternPerson
{
    [Key]
    [Column("AccessPatternID")]
    public int AccessPatternId { get; set; }

    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [Column("CheckEqMasterID")]
    public int? CheckEqMasterId { get; set; }

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
    [InverseProperty("AccessPatternPeople")]
    public virtual EqUser Update { get; set; } = null!;
}

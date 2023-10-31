using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("AccessPatternId", "PatternOrder", "EqMasterId")]
[Table("AccessPatternObject")]
public partial class AccessPatternObject
{
    [Key]
    [Column("AccessPatternID")]
    public int AccessPatternId { get; set; }

    [Key]
    public int PatternOrder { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckInTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOutTime { get; set; }

    [Column("CheckCardOK")]
    public int? CheckCardOk { get; set; }

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
    [InverseProperty("AccessPatternObjects")]
    public virtual EqUser Update { get; set; } = null!;
}

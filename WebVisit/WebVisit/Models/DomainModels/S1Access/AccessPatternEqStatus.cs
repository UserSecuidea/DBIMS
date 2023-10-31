using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("EqCodeId", "Status")]
[Table("AccessPatternEqStatus")]
public partial class AccessPatternEqStatus
{
    [Key]
    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Key]
    [StringLength(30)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [ForeignKey("EqCodeId")]
    [InverseProperty("AccessPatternEqStatuses")]
    public virtual EqCodeList EqCode { get; set; } = null!;
}

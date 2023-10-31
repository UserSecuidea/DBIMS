using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AccessPattern")]
public partial class AccessPattern
{
    [Key]
    [Column("AccessPatternID")]
    public int AccessPatternId { get; set; }

    [StringLength(50)]
    public string AccessPatternName { get; set; } = null!;

    public int IsAccessTimePattern { get; set; }

    public int EnableFlag { get; set; }

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
    [InverseProperty("AccessPatterns")]
    public virtual EqUser Update { get; set; } = null!;
}

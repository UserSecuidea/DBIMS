using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 제어자 항목
/// </summary>
[Table("ModeChange")]
public partial class ModeChange
{
    [Key]
    [Column("ModeID")]
    public int ModeId { get; set; }

    [Column("ModeChange")]
    public int ModeChange1 { get; set; }

    [Column("ModeChangeNameID")]
    public int ModeChangeNameId { get; set; }
}

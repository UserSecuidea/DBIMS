using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 방범 모드 정의
/// </summary>
[Table("SecurityMode")]
public partial class SecurityMode
{
    [Key]
    [Column("SecurityModeID")]
    public int SecurityModeId { get; set; }

    [Column("SecurityMode")]
    [StringLength(1)]
    [Unicode(false)]
    public string SecurityMode1 { get; set; } = null!;

    [Column("SecurityModeStringID")]
    public int SecurityModeStringId { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 조명 제어명 테이블
/// </summary>
[Table("LightModeName")]
public partial class LightModeName
{
    [Key]
    [Column("LightModeID")]
    public int LightModeId { get; set; }

    [Column("ModeNameID")]
    public int ModeNameId { get; set; }
}

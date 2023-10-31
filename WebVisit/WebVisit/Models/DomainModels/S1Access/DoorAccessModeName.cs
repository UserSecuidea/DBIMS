using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입모드별 타임 테이블
/// </summary>
[PrimaryKey("DoorAccessModeId", "ModeNameId")]
[Table("DoorAccessModeName")]
public partial class DoorAccessModeName
{
    [Key]
    [Column("DoorAccessModeID")]
    public int DoorAccessModeId { get; set; }

    [Key]
    [Column("ModeNameID")]
    public int ModeNameId { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 패스워드 업데이트 주기 테이블
/// </summary>
[Table("PWUpdateCycle")]
public partial class PwupdateCycle
{
    [Key]
    [Column("CycleID")]
    public int CycleId { get; set; }

    [Column("CycleStringID")]
    public int CycleStringId { get; set; }

    public int? DefaultView { get; set; }
}

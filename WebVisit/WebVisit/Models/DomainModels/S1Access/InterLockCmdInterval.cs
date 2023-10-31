using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 조건에 해당하는 기기의 동작이 바로 이루어지 않고 사용자가 지정한 Interval 뒤에 동작하도록 한다
/// </summary>
[Table("InterLockCmdInterval")]
public partial class InterLockCmdInterval
{
    [Key]
    [Column("InterLockCmdIntervalID")]
    public int InterLockCmdIntervalId { get; set; }

    [Column("InterLockCmdInterval")]
    public int InterLockCmdInterval1 { get; set; }

    public int InterLockCmdIntervalName { get; set; }
}

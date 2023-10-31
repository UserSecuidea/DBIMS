using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 법정 공휴일 데이터
/// </summary>
[Keyless]
[Table("CARLENDAR_HOLIDAYS")]
public partial class CarlendarHoliday
{
    [Column("H_SEQ")]
    public int HSeq { get; set; }

    [Column("YEAR")]
    [StringLength(4)]
    [Unicode(false)]
    public string? Year { get; set; }

    [Column("MONTH")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Month { get; set; }

    [Column("DAY")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Day { get; set; }

    [Column("CONTENT")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Content { get; set; }
}

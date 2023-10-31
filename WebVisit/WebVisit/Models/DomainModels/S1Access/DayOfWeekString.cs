using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 스케쥴의 월~일, 특정일 정보 테이블
/// </summary>
[Table("DayOfWeekString")]
public partial class DayOfWeekString
{
    [Key]
    [Column("DayOfWeekID")]
    public int DayOfWeekId { get; set; }

    [Column("DayWeekNameID")]
    public int DayWeekNameId { get; set; }
}

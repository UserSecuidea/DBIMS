using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 연동설정 테이블
/// </summary>
[Table("InterLock")]
public partial class InterLock
{
    [Key]
    [Column("InterLockID")]
    public int InterLockId { get; set; }

    [StringLength(50)]
    public string InterLockName { get; set; } = null!;

    public int Using { get; set; }

    public int CalType { get; set; }

    public int ScheduleType { get; set; }

    public TimeSpan? ScheduleTime { get; set; }

    public int ScheduleWeekDay { get; set; }

    public int ScheduleDay { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("InterLocks")]
    public virtual EqUser Update { get; set; } = null!;
}

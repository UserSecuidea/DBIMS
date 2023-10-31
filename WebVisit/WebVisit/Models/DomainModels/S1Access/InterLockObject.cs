using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 연동설정 오브젝트 테이블
/// </summary>
[PrimaryKey("ObjectId", "InterLockId")]
[Table("InterLockObject")]
public partial class InterLockObject
{
    [Key]
    [Column("InterLockID")]
    public int InterLockId { get; set; }

    [Key]
    [Column("ObjectID")]
    public int ObjectId { get; set; }

    public int Type { get; set; }

    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column("CommServerID")]
    public int CommServerId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Code { get; set; }

    public int IsActive { get; set; }

    [Column("PID")]
    public int? Pid { get; set; }

    [Column("InterLockCmdIntervalID")]
    public int InterLockCmdIntervalId { get; set; }

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
    [InverseProperty("InterLockObjects")]
    public virtual EqUser Update { get; set; } = null!;
}

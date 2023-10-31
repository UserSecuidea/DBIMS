using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 상태 정보 테이블
/// </summary>
[PrimaryKey("EqCodeId", "Status", "EqStatusNameId", "Seq")]
[Table("EqStatus")]
public partial class EqStatus
{
    [Key]
    public int Seq { get; set; }

    [Key]
    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Key]
    [StringLength(30)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Key]
    [Column("EqStatusNameID")]
    public int EqStatusNameId { get; set; }

    public int Color { get; set; }

    [Column("Color_Original")]
    public int ColorOriginal { get; set; }

    [Column("isAlarm")]
    public int IsAlarm { get; set; }

    [Column("WavFileID")]
    public int? WavFileId { get; set; }

    [Column("AlarmProcessPlanID")]
    public int AlarmProcessPlanId { get; set; }

    [Column("isShow")]
    public int? IsShow { get; set; }

    [Column("isSMS")]
    public int? IsSms { get; set; }

    public int ExternalStatus { get; set; }

    public int Reserved { get; set; }

    public int EnableFlag { get; set; }

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

    [ForeignKey("EqCodeId")]
    [InverseProperty("EqStatuses")]
    public virtual EqCodeList EqCode { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("EqStatuses")]
    public virtual EqUser Update { get; set; } = null!;
}

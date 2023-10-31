using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 지문정보의 카드리더 전송 테이블
/// </summary>
[PrimaryKey("CardId", "EqMasterId")]
[Table("FingerToCR")]
[Index("DownFlag", Name = "IX_FingerToCR")]
public partial class FingerToCr
{
    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int IsAccess { get; set; }

    public int DownFlag { get; set; }

    [Column("UpdateEqMasterID")]
    public int? UpdateEqMasterId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ResultCode { get; set; }

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
    [InverseProperty("FingerToCrs")]
    public virtual EqUser Update { get; set; } = null!;
}

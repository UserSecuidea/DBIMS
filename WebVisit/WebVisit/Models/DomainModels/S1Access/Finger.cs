using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 지문 정보 테이블
/// </summary>
[PrimaryKey("CardId", "FingerType")]
[Table("Finger")]
public partial class Finger
{
    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    public int FingerType { get; set; }

    public int? FingerEx { get; set; }

    [MaxLength(400)]
    public byte[]? FingerPrint1 { get; set; }

    [MaxLength(400)]
    public byte[]? FingerPrint2 { get; set; }

    [MaxLength(400)]
    public byte[]? FingerPrint3 { get; set; }

    [MaxLength(400)]
    public byte[]? FingerPrint4 { get; set; }

    [Column("FPassword")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Fpassword { get; set; }

    [Column("FPID")]
    [StringLength(8)]
    [Unicode(false)]
    public string? Fpid { get; set; }

    [Column("UpdateEqMasterID")]
    public int? UpdateEqMasterId { get; set; }

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
    [InverseProperty("Fingers")]
    public virtual EqUser Update { get; set; } = null!;
}

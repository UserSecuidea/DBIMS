using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 각 스트링에 대한 다국어 지원을 위한 테이블
/// </summary>
[Table("Localization")]
public partial class Localization
{
    [Key]
    [Column("RecordID")]
    public int RecordId { get; set; }

    [Column("isEqStatus")]
    public int IsEqStatus { get; set; }

    [Column("KOR")]
    [StringLength(200)]
    public string? Kor { get; set; }

    [Column("ENG")]
    [StringLength(200)]
    public string? Eng { get; set; }

    [Column("CHI")]
    [StringLength(200)]
    public string? Chi { get; set; }

    [StringLength(200)]
    public string? UserDefine { get; set; }

    [Column("Update_KOR")]
    [StringLength(200)]
    public string? UpdateKor { get; set; }

    [Column("Update_ENG")]
    [StringLength(200)]
    public string? UpdateEng { get; set; }

    [Column("Update_CHI")]
    [StringLength(200)]
    public string? UpdateChi { get; set; }

    [Column("Update_UserDefine")]
    [StringLength(200)]
    public string? UpdateUserDefine { get; set; }

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
    [InverseProperty("Localizations")]
    public virtual EqUser Update { get; set; } = null!;
}

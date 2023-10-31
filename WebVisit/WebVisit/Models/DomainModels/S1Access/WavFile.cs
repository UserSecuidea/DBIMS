using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 웨이브 파일 테이블
/// </summary>
[Table("WavFile")]
public partial class WavFile
{
    [Key]
    [Column("WavID")]
    public int WavId { get; set; }

    [Column("WavFileName_K")]
    [StringLength(50)]
    public string? WavFileNameK { get; set; }

    [Column("WavFileName_E")]
    [StringLength(50)]
    public string? WavFileNameE { get; set; }

    [Column("WavFileName_C")]
    [StringLength(50)]
    public string? WavFileNameC { get; set; }

    [Column("WavFileName_U")]
    [StringLength(50)]
    public string? WavFileNameU { get; set; }

    [StringLength(50)]
    public string? WavName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("WavFiles")]
    public virtual EqUser Update { get; set; } = null!;
}

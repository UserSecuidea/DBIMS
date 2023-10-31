using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("EqMasterId", "EtcType")]
[Table("FaceReaderEtcFunction")]
public partial class FaceReaderEtcFunction
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    public int EtcType { get; set; }

    public int DownFlag { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDateTime { get; set; }

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

    /// <summary>
    /// 체온 측정 사용/미사용
    /// </summary>
    public int? UseThermometer { get; set; }

    /// <summary>
    /// 체온 측정 출입 경고 온도
    /// </summary>
    public double? TemperatureWarning { get; set; }

    /// <summary>
    /// 체온 측정 출입 불가 온도
    /// </summary>
    public double? TemperatureDeny { get; set; }

    /// <summary>
    /// 마스크 모드 사용/미사용
    /// </summary>
    public int? UseMaskMode { get; set; }
}

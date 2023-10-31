using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 얼굴 인증을 위한 파라미터 저장
/// </summary>
[Table("FaceDetectParam")]
public partial class FaceDetectParam
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int? MaxFaceNum { get; set; }

    [Column("FCThresholdLevel")]
    public int? FcthresholdLevel { get; set; }

    public int? RegLowLevel { get; set; }

    public int? RegHighLevel { get; set; }

    public int? WatchLowLevel { get; set; }

    public int? WatchHighLevel { get; set; }

    public int? CardRegLevel { get; set; }

    public int? EyeGapMin { get; set; }

    public int? EyeGapMax { get; set; }

    public int? RectX { get; set; }

    public int? RectY { get; set; }

    public int? RectWidth { get; set; }

    public int? RectHeight { get; set; }

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
    [InverseProperty("FaceDetectParams")]
    public virtual EqUser Update { get; set; } = null!;
}

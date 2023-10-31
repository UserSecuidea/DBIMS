using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 얼굴 인증을 위한 사원별 특징점 데이터
/// </summary>
[PrimaryKey("Pid", "PictureId")]
[Table("FaceDetectPersonPicture")]
public partial class FaceDetectPersonPicture
{
    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [Key]
    [Column("PictureID")]
    public int PictureId { get; set; }

    public byte[] Picture { get; set; } = null!;

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
}

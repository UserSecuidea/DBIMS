using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사원의 사진정보에서 특징점을 추출하여 저장하도록 한다.
/// </summary>
[PrimaryKey("Pid", "PictureId")]
public partial class FaceDetectKeyDatum
{
    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [Key]
    [Column("PictureID")]
    public int PictureId { get; set; }

    public string FaceDetectKeyData { get; set; } = null!;

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

    [ForeignKey("Pid")]
    [InverseProperty("FaceDetectKeyData")]
    public virtual Person PidNavigation { get; set; } = null!;
}

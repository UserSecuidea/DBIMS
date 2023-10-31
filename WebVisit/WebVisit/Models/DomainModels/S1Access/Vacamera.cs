using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 지능형 영상 감시 서버 하단의 카메라 목록
/// </summary>
[Table("VACamera")]
public partial class Vacamera
{
    [Key]
    [Column("CameraID")]
    public int CameraId { get; set; }

    [Column("ParentEqMasterID")]
    public int ParentEqMasterId { get; set; }

    [StringLength(50)]
    public string? LocationString { get; set; }

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
    [InverseProperty("Vacameras")]
    public virtual EqUser Update { get; set; } = null!;
}

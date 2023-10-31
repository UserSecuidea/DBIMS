using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// IP카메라의 해상도별 RTSP 주소
/// </summary>
[PrimaryKey("EqMasterId", "ResolutionId")]
[Table("IPCameraRTSPAddress")]
public partial class IpcameraRtspaddress
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [Column("ResolutionID")]
    public int ResolutionId { get; set; }

    [Column("GUID")]
    [StringLength(30)]
    public string? Guid { get; set; }

    [Column("RTSPAddress")]
    [StringLength(100)]
    public string? Rtspaddress { get; set; }

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
    [InverseProperty("IpcameraRtspaddresses")]
    public virtual EqUser Update { get; set; } = null!;
}

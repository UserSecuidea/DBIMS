using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사용자 비디오 레이아웃에 대한 카메라 저장
/// </summary>
[PrimaryKey("LayoutId", "EqMasterId")]
[Table("VideoLayoutLink")]
public partial class VideoLayoutLink
{
    [Key]
    [Column("LayoutID")]
    public int LayoutId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int? PosX1 { get; set; }

    public int? PosY1 { get; set; }

    public int? PosX2 { get; set; }

    public int? PosY2 { get; set; }

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
    [InverseProperty("VideoLayoutLinks")]
    public virtual EqUser Update { get; set; } = null!;
}

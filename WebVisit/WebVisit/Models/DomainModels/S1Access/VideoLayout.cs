using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 비디오 레이아웃 테이블
/// </summary>
[PrimaryKey("EqUserId", "LayoutId")]
[Table("VideoLayout")]
public partial class VideoLayout
{
    [Key]
    [Column("EqUserID")]
    public int EqUserId { get; set; }

    [Key]
    [Column("LayoutID")]
    public int LayoutId { get; set; }

    [StringLength(100)]
    public string LayoutName { get; set; } = null!;

    public int? PosX { get; set; }

    public int? PosY { get; set; }

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
    [InverseProperty("VideoLayouts")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사용자의 최근 사용메뉴를 기록한다.
/// </summary>
[PrimaryKey("EqUserId", "WebMenuId")]
[Table("EqUserRecentMenu")]
public partial class EqUserRecentMenu
{
    [Key]
    [Column("EqUserID")]
    public int EqUserId { get; set; }

    [Key]
    [Column("WebMenuID")]
    public int WebMenuId { get; set; }

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

    [ForeignKey("EqUserId")]
    [InverseProperty("EqUserRecentMenus")]
    public virtual EqUser EqUser { get; set; } = null!;
}

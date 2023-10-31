using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 데시보드를 위한 테이블정보
/// </summary>
[Table("DashBoard")]
public partial class DashBoard
{
    [Key]
    [Column("DashBoardID")]
    public int DashBoardId { get; set; }

    public int DashBoardType { get; set; }

    [StringLength(50)]
    public string DashBoardName { get; set; } = null!;

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
    [InverseProperty("DashBoards")]
    public virtual EqUser Update { get; set; } = null!;
}

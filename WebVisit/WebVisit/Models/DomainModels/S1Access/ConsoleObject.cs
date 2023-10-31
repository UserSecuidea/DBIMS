using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 콘솔의 각 영역별 설정 값을 정의하는 테이블
/// </summary>
[Table("ConsoleObject")]
public partial class ConsoleObject
{
    [Key]
    [Column("ConsoleID")]
    public int ConsoleId { get; set; }

    public int? Grid1PanelType { get; set; }

    public int? Grid1EqMaster { get; set; }

    public int? Grid2PanelType { get; set; }

    public int? Grid2EqMaster { get; set; }

    public int? Grid3PanelType { get; set; }

    public int? Grid3EqMaster { get; set; }

    public int? Grid4PanelType { get; set; }

    public int? Grid4EqMaster { get; set; }

    public int? Grid5PanelType { get; set; }

    public int? Grid5EqMaster { get; set; }

    public int? Grid6PanelType { get; set; }

    public int? Grid6EqMaster { get; set; }

    public int? Grid7PanelType { get; set; }

    public int? Grid7EqMaster { get; set; }

    public int? Grid8PanelType { get; set; }

    public int? Grid8EqMaster { get; set; }

    public int? Grid9PanelType { get; set; }

    public int? Grid9EqMaster { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("ConsoleObjects")]
    public virtual EqUser? Update { get; set; }
}

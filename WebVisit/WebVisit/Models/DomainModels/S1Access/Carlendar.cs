using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 년도 월의 특정일 설정 테이블
/// </summary>
[PrimaryKey("EqMasterId", "Year")]
[Table("Carlendar")]
public partial class Carlendar
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [StringLength(4)]
    [Unicode(false)]
    public string Year { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M1 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M2 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M3 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M4 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M5 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M6 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M7 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M8 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M9 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M10 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M11 { get; set; } = null!;

    [StringLength(31)]
    [Unicode(false)]
    public string M12 { get; set; } = null!;

    public int DownFlag { get; set; }

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
    [InverseProperty("Carlendars")]
    public virtual EqUser Update { get; set; } = null!;
}

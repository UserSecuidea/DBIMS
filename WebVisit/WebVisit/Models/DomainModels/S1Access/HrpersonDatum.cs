using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사원의 연동 테이블(인사연동)
/// </summary>
[Table("HRPersonData")]
public partial class HrpersonDatum
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Sabun { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(36)]
    public string? CardNo { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Tel { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? OrgCode { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    public int? PersonType { get; set; }

    public int? PersonStatus { get; set; }

    public int? CardStatus { get; set; }

    public int? IssuCnt { get; set; }

    public byte[]? Picture { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ValidDate { get; set; }

    [StringLength(30)]
    public string? PersonUser1 { get; set; }

    [StringLength(30)]
    public string? PersonUser2 { get; set; }

    [StringLength(30)]
    public string? PersonUser3 { get; set; }

    [StringLength(30)]
    public string? PersonUser4 { get; set; }

    [StringLength(30)]
    public string? PersonUser5 { get; set; }

    [StringLength(50)]
    public string? CardUser1 { get; set; }

    [StringLength(50)]
    public string? CardUser2 { get; set; }

    [StringLength(50)]
    public string? CardUser3 { get; set; }

    [StringLength(50)]
    public string? CardUser4 { get; set; }

    [StringLength(50)]
    public string? CardUser5 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }
}

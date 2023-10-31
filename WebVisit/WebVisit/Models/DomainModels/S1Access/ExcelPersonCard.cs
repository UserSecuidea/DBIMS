using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 엑셀을 통한 사원 및 카드 등록
/// </summary>
[Table("ExcelPersonCard")]
public partial class ExcelPersonCard
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int UpdateType { get; set; }

    [StringLength(200)]
    public string? Sabun { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? OrgName { get; set; }

    [StringLength(200)]
    public string? GradeName { get; set; }

    [StringLength(200)]
    public string? PersonTypeName { get; set; }

    [StringLength(200)]
    public string? Tel { get; set; }

    [StringLength(200)]
    public string? PersonStatusName { get; set; }

    [StringLength(200)]
    public string? Mobile { get; set; }

    [StringLength(200)]
    public string? PersonUser1 { get; set; }

    [StringLength(200)]
    public string? PersonUser2 { get; set; }

    [StringLength(200)]
    public string? PersonUser3 { get; set; }

    [StringLength(200)]
    public string? PersonUser4 { get; set; }

    [StringLength(200)]
    public string? PersonUser5 { get; set; }

    [Column("EMail")]
    [StringLength(200)]
    public string? Email { get; set; }

    [StringLength(300)]
    public string? ImageData { get; set; }

    [StringLength(200)]
    public string? CardNo { get; set; }

    [StringLength(200)]
    public string? CardStatusName { get; set; }

    [StringLength(200)]
    public string? CardTypeName { get; set; }

    [StringLength(200)]
    public string? CardKindName { get; set; }

    [StringLength(200)]
    public string? IssuCnt { get; set; }

    [StringLength(200)]
    public string? Validate { get; set; }

    [StringLength(200)]
    public string? CardUser1 { get; set; }

    [StringLength(200)]
    public string? CardUser2 { get; set; }

    [StringLength(200)]
    public string? CardUser3 { get; set; }

    [StringLength(200)]
    public string? CardUser4 { get; set; }

    [StringLength(200)]
    public string? CardUser5 { get; set; }

    public int UpdateFlag { get; set; }

    public int ResultFlag { get; set; }

    public int? ExcelColumn { get; set; }

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

    public int MasterCard { get; set; }

    public int AccessPattern { get; set; }
}

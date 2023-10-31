using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 엑셀을 통한 출입그룹 등록
/// </summary>
[Table("ExcelPersonGroup")]
public partial class ExcelPersonGroup
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int UpdateType { get; set; }

    [StringLength(200)]
    public string? PersonGroupName { get; set; }

    [StringLength(200)]
    public string? PersonGroupComment { get; set; }

    [StringLength(200)]
    public string? Sabun { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? OrgName { get; set; }

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
}

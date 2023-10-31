using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 엑셀을 통한 조직 등록
/// </summary>
[Table("ExcelOrg")]
public partial class ExcelOrg
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int UpdateType { get; set; }

    [StringLength(200)]
    public string? Org1 { get; set; }

    [StringLength(200)]
    public string? Org2 { get; set; }

    [StringLength(200)]
    public string? Org3 { get; set; }

    [StringLength(200)]
    public string? Org4 { get; set; }

    [StringLength(200)]
    public string? Org5 { get; set; }

    [StringLength(200)]
    public string? Org6 { get; set; }

    [StringLength(200)]
    public string? Org7 { get; set; }

    [StringLength(200)]
    public string? Org8 { get; set; }

    [StringLength(200)]
    public string? Org9 { get; set; }

    [StringLength(200)]
    public string? Org10 { get; set; }

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

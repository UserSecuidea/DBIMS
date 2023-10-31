using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 엑셀을 통한 위치 등록
/// </summary>
[Table("ExcelLocation")]
public partial class ExcelLocation
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int UpdateType { get; set; }

    [StringLength(200)]
    public string? Location1 { get; set; }

    [StringLength(200)]
    public string? Location2 { get; set; }

    [StringLength(200)]
    public string? Location3 { get; set; }

    [StringLength(200)]
    public string? Location4 { get; set; }

    [StringLength(200)]
    public string? Location5 { get; set; }

    [StringLength(200)]
    public string? Location6 { get; set; }

    [StringLength(200)]
    public string? Location7 { get; set; }

    [StringLength(200)]
    public string? Location8 { get; set; }

    [StringLength(200)]
    public string? Location9 { get; set; }

    [StringLength(200)]
    public string? Location10 { get; set; }

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

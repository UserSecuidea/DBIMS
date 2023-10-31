using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 엑셀을 통한 기기 등록
/// </summary>
[Table("ExcelEqMaster")]
public partial class ExcelEqMaster
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int UpdateType { get; set; }

    [StringLength(200)]
    public string? EqCodeName { get; set; }

    [StringLength(200)]
    public string? EqMasterTypeName { get; set; }

    [StringLength(200)]
    public string? CardReaderName { get; set; }

    [StringLength(200)]
    public string? EqMasterNo { get; set; }

    [StringLength(200)]
    public string? EqName { get; set; }

    [StringLength(200)]
    public string? LocationName { get; set; }

    [Column("MAC")]
    [StringLength(200)]
    public string? Mac { get; set; }

    [StringLength(200)]
    public string? CommServerNo { get; set; }

    [Column("CommIP")]
    [StringLength(200)]
    public string? CommIp { get; set; }

    [StringLength(200)]
    public string? CommPort { get; set; }

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

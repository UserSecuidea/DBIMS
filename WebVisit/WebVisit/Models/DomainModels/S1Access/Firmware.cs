using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 하드웨어 펌웨어에 대한 정보 테이블
/// </summary>
[Keyless]
[Table("Firmware")]
public partial class Firmware
{
    public int Seq { get; set; }

    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    public int Active { get; set; }

    [StringLength(50)]
    public string FileNameString { get; set; } = null!;

    [Column("Firmware")]
    public int? Firmware1 { get; set; }

    [Column("FWVersion")]
    [StringLength(10)]
    public string? Fwversion { get; set; }

    [StringLength(20)]
    public string? Description { get; set; }

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
    public virtual EqUser Update { get; set; } = null!;
}

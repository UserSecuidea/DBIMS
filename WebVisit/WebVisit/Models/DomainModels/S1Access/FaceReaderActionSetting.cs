using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

public partial class FaceReaderActionSetting
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int UseTemperatureNotify { get; set; }

    public double? TemperatureWarning { get; set; }

    public double? TemperatureDeny { get; set; }

    public int MaskAuthMode { get; set; }

    public int DownFlag { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? ErrorCode { get; set; }

    [StringLength(100)]
    public string? ErrorMessage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

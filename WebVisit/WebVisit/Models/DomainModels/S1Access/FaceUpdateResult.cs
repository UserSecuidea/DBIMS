using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("Type", "Pid", "EtcInfo1")]
[Table("FaceUpdateResult")]
public partial class FaceUpdateResult
{
    [Key]
    public int Type { get; set; }

    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [Key]
    public int EtcInfo1 { get; set; }

    [StringLength(64)]
    public string? EtcInfo2 { get; set; }

    [StringLength(64)]
    public string? EtcInfo3 { get; set; }

    [StringLength(64)]
    public string? EtcInfo4 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [StringLength(64)]
    public string? EtcInfo5 { get; set; }
}

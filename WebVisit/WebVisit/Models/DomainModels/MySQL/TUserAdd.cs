using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.MySQL;

[Table("T_UserAdd")]
public partial class TUserAdd
{
    [Key]
    [Column(TypeName = "bigint(20)")]
    public long Seq { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReqTime { get; set; }

    [Column("UserID")]
    [StringLength(100)]
    public string UserId { get; set; } = null!;

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(100)]
    public string UserPassword { get; set; } = null!;

    [Column(TypeName = "int(10) unsigned")]
    public uint? ExpireDate { get; set; }

    [Column("Position_2")]
    [StringLength(50)]
    public string Position2 { get; set; } = null!;

    [StringLength(50)]
    public string? Rank { get; set; }

    [StringLength(50)]
    public string? TelNum { get; set; }

    [StringLength(13)]
    public string? CellPhone { get; set; }

    [Column(TypeName = "tinyint(4)")]
    public int? ErrorFlag { get; set; } = 0;
    // public sbyte? ErrorFlag { get; set; }
}

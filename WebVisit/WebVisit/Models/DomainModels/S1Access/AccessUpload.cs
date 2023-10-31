using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("AccessUpload")]
public partial class AccessUpload
{
    public int Seq { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserCardNo { get; set; }

    public int? DownFlag { get; set; }

    public int? IsSelected { get; set; }

    [Column("EqUserID")]
    public int? EqUserId { get; set; }

    public int? ExistFlag { get; set; }
}

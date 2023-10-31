using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("ServiceVersionList")]
public partial class ServiceVersionList
{
    public int ServiceId { get; set; }

    [StringLength(128)]
    public string ServiceName { get; set; } = null!;

    [StringLength(512)]
    public string? ServiceDes { get; set; }

    [StringLength(128)]
    public string Version { get; set; } = null!;

    [Column("builddate", TypeName = "date")]
    public DateTime Builddate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Insertdate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }
}

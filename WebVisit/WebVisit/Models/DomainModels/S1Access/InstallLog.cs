using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("InstallLog")]
public partial class InstallLog
{
    [Column(TypeName = "datetime")]
    public DateTime DateTime { get; set; }

    public string Query { get; set; } = null!;

    [Column("InstallLog")]
    public string? InstallLog1 { get; set; }
}

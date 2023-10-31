using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
public partial class ViewAccess
{
    [Column(TypeName = "datetime")]
    public DateTime AccessTime { get; set; }

    [StringLength(50)]
    public string? LocationName { get; set; }

    [StringLength(50)]
    public string? DeviceName { get; set; }

    [StringLength(50)]
    public string? Sabun { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string? StatusName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string State { get; set; } = null!;
}

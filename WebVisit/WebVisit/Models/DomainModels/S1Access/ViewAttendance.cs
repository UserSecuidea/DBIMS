using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
public partial class ViewAttendance
{
    [Column(TypeName = "date")]
    public DateTime AttendanceDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? InTime { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? OutTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewPersonInout
{
    [Column(TypeName = "date")]
    public DateTime? EventDate { get; set; }

    [Column("DIR")]
    [StringLength(4)]
    [Unicode(false)]
    public string Dir { get; set; } = null!;

    [Column("LOCATION_NAME")]
    [StringLength(100)]
    public string? LocationName { get; set; }

    public int? Cnt { get; set; }
}

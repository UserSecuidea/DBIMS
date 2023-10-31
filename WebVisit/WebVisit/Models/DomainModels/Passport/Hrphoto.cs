using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("HRPhoto")]
public partial class Hrphoto
{
    public int Seq { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Sabun { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Photo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }
}

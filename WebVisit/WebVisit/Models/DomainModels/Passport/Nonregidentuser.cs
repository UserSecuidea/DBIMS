using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("NONREGIDENTUSER")]
public partial class Nonregidentuser
{
    [Key]
    public int Idx { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Sabun { get; set; } = null!;

    [StringLength(50)]
    public string? CompanyName { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

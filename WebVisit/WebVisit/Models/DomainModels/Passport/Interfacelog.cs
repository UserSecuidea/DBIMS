using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("INTERFACELOG")]
public partial class Interfacelog
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    public string? Log { get; set; }
}

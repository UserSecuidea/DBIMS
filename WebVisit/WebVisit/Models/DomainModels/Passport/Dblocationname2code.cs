using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("DBLOCATIONNAME2CODE")]
public partial class Dblocationname2code
{
    [Key]
    [Column("LOCATION_NAME")]
    [StringLength(50)]
    public string LocationName { get; set; } = null!;

    [Column("LOCATION_CODE")]
    public int LocationCode { get; set; }
}

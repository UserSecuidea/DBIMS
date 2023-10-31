using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("APPROVERPOSITION")]
public partial class Approverposition
{
    [Key]
    [Column("SAP_POSITION_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string SapPositionCode { get; set; } = null!;

    [Column("SAP_POSITION_NAME")]
    [StringLength(100)]
    public string? SapPositionName { get; set; }
}

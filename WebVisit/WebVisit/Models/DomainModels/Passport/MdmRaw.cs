using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("MDM_RAW")]
public partial class MdmRaw
{
    [Key]
    [Column("ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Id { get; set; } = null!;
}

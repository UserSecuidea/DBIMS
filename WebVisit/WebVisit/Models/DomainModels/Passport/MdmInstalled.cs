using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("MDM_INSTALLED")]
public partial class MdmInstalled
{
    [Key]
    [Column("ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    public int IsInstalled { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }
}

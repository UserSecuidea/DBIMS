using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("VI_ZTAD001")]
public partial class ViZtad001
{
    [Key]
    [Column("PERNR")]
    [StringLength(16)]
    public string Pernr { get; set; } = null!;

    [Column("OBJID")]
    [StringLength(16)]
    public string? Objid { get; set; }

    [Column("BEGDA")]
    [StringLength(20)]
    public string? Begda { get; set; }

    [Column("ENDDA")]
    [StringLength(20)]
    public string? Endda { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }

    [Column("INSERTDATE", TypeName = "datetime")]
    public DateTime? Insertdate { get; set; }
}

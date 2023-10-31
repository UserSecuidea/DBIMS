using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("V_GATE_BIRTHDAY")]
public partial class VGateBirthday
{
    [Key]
    [Column("USER_ID")]
    [StringLength(8)]
    public string UserId { get; set; } = null!;

    [Column("BIRTHDAY")]
    [StringLength(14)]
    public string? Birthday { get; set; }

    [Column("SEX_CODE")]
    [StringLength(2)]
    public string? SexCode { get; set; }

    [Column("SEX")]
    [StringLength(2)]
    public string? Sex { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }

    [Column("INSERTDATE", TypeName = "datetime")]
    public DateTime? Insertdate { get; set; }
}

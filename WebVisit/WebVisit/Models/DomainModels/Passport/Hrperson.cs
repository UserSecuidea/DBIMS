using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("HRPerson")]
public partial class Hrperson
{
    public int Seq { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Sabun { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserCardNo { get; set; }

    public int? IssuCnt { get; set; }

    public int? CardStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ValidDate { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }

    [StringLength(50)]
    public string? OrgCode { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    public int? PersonType { get; set; }

    public int? PersonStatus { get; set; }

    public int? CampusCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }
}

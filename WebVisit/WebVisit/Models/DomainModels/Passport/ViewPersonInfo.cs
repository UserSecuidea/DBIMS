using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewPersonInfo
{
    [StringLength(20)]
    public string? Sabun { get; set; }

    [Column("Name_Kor")]
    [StringLength(64)]
    public string? NameKor { get; set; }

    [Column("Name_Eng")]
    [StringLength(64)]
    public string? NameEng { get; set; }

    [StringLength(256)]
    public string? Company { get; set; }

    [StringLength(32)]
    public string? OrgCode { get; set; }

    [StringLength(256)]
    public string? OrgName { get; set; }

    [StringLength(64)]
    public string? GradeName { get; set; }

    [Column("Employee_Type")]
    [StringLength(1)]
    public string? EmployeeType { get; set; }

    public int? PersonType { get; set; }

    [Column(TypeName = "numeric(1, 0)")]
    public decimal? PersonStatus { get; set; }

    [StringLength(150)]
    public string? Tel { get; set; }

    [StringLength(20)]
    public string? Mobile { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [Column("LOCATION_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LocationCode { get; set; } = null!;

    [Column("Campus_Name")]
    [StringLength(100)]
    public string? CampusName { get; set; }
}

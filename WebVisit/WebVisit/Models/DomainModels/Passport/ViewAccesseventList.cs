using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewAccesseventList
{
    [Column("AlarmID")]
    public int AlarmId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EventDateTime { get; set; }

    [StringLength(100)]
    public string? StateName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    public int? IssueCnt { get; set; }

    [StringLength(50)]
    public string? EqTypeName { get; set; }

    [StringLength(50)]
    public string? EqName { get; set; }

    [StringLength(50)]
    public string? LocationName { get; set; }

    [StringLength(50)]
    public string? PersonName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    public int Success { get; set; }

    [Column("COMP_NAME")]
    [StringLength(256)]
    public string? CompName { get; set; }

    [Column("COMP_ID")]
    [StringLength(32)]
    public string? CompId { get; set; }

    [Column("LOCATION_CODE")]
    public int? LocationCode { get; set; }

    [Column("LOCATION_NAME")]
    [StringLength(100)]
    public string? LocationName1 { get; set; }
}

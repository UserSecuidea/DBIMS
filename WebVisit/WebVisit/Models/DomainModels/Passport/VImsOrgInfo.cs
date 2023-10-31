using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("V_IMS_ORG_INFO")]
public partial class VImsOrgInfo
{
    [Column("SAP_ORG_CODE")]
    [StringLength(32)]
    public string SapOrgCode { get; set; } = null!;

    [Column("ORG_NAME")]
    [StringLength(256)]
    public string? OrgName { get; set; }

    [Column("SAP_ORG_PARENT_CODE")]
    [StringLength(32)]
    public string? SapOrgParentCode { get; set; }

    [Column("COMP_ID")]
    [StringLength(32)]
    public string? CompId { get; set; }

    [Column("IS_DELETED")]
    [StringLength(1)]
    public string? IsDeleted { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("V_IMS_USER_INFO")]
public partial class VImsUserInfo
{
    [Column("EMPLOYEE_ID")]
    [StringLength(20)]
    public string? EmployeeId { get; set; }

    [Column("USER_ID")]
    [StringLength(32)]
    public string UserId { get; set; } = null!;

    [Column("PASSWORD")]
    [StringLength(100)]
    public string? Password { get; set; }

    [Column("USER_NAME")]
    [StringLength(64)]
    public string? UserName { get; set; }

    [Column("USER_OTHER_NAME")]
    [StringLength(64)]
    public string? UserOtherName { get; set; }

    [Column("PORTE_ID")]
    [StringLength(32)]
    public string? PorteId { get; set; }

    [Column("COMP_ID")]
    [StringLength(32)]
    public string? CompId { get; set; }

    [Column("COMP_NAME")]
    [StringLength(256)]
    public string? CompName { get; set; }

    [Column("LOCATION")]
    [StringLength(4)]
    public string? Location { get; set; }

    [Column("SAP_DEPT_CODE")]
    [StringLength(32)]
    public string? SapDeptCode { get; set; }

    [Column("DEPT_NAME")]
    [StringLength(256)]
    public string? DeptName { get; set; }

    [Column("SAP_TITLE_CODE")]
    [StringLength(32)]
    public string? SapTitleCode { get; set; }

    [Column("SAP_TITLE_NAME")]
    [StringLength(64)]
    public string? SapTitleName { get; set; }

    [Column("SAP_POSITION_NAME")]
    [StringLength(64)]
    public string? SapPositionName { get; set; }

    [Column("SAP_POSITION_CODE")]
    [StringLength(32)]
    public string? SapPositionCode { get; set; }

    [Column("EMPLOYEE_TYPE")]
    [StringLength(1)]
    public string? EmployeeType { get; set; }

    [Column("OFFICE_TEL")]
    [StringLength(150)]
    public string? OfficeTel { get; set; }

    [Column("MOBILE")]
    [StringLength(20)]
    public string? Mobile { get; set; }

    [Column("CAR_NO")]
    [StringLength(40)]
    public string? CarNo { get; set; }

    [Column("EMAIL")]
    [StringLength(150)]
    public string? Email { get; set; }

    [Column("IS_DELETED", TypeName = "numeric(1, 0)")]
    public decimal? IsDeleted { get; set; }

    [Column("RETIRE_DATE")]
    [StringLength(8)]
    public string? RetireDate { get; set; }

    [Column("SYNC_DATE")]
    public DateTime? SyncDate { get; set; }
}

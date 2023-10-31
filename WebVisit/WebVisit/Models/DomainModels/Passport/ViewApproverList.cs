using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewApproverList
{
    [Column("SAP_DEPT_CODE")]
    [StringLength(32)]
    public string? SapDeptCode { get; set; }

    [Column("USER_NAME")]
    [StringLength(64)]
    public string? UserName { get; set; }

    [Column("EMPLOYEE_ID")]
    [StringLength(20)]
    public string? EmployeeId { get; set; }

    [Column("DEPT_NAME")]
    [StringLength(256)]
    public string? DeptName { get; set; }
}

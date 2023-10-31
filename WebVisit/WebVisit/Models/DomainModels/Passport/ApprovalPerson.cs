using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

/// <summary>
/// PROC_FIND_APPROVER 의 return
/// </summary>
public partial class ApprovalPerson
{
    public string? DeptCode { get; set; } = string.Empty;
    
    [Column("IS_INFORMAL_PART_MANAGER")]
    public string? IsInformalPartManager { get; set; } = string.Empty;

    [Column("MEMBER_ID")]
    public string? MemberID { get; set; } = string.Empty;

    [Column("CONFIRM_STAFF_NO")]
    public string? ConfirmStaffNo { get; set; } = string.Empty;

    [Column("MEMBER_NAME")]
    public string? MemberName { get; set; } = string.Empty;

    [Column("COMPANY_NAME")]
    public string? CompanyName { get; set; } = string.Empty;

    [Column("POSITION")]
    public string? Position { get; set; } = string.Empty; // 직급명 한글

    [Column("EMAIL")]
    public string? Email { get; set; } = string.Empty;

    [Column("IS_PERSONNEL")]
    public string? IsPersonal { get; set; } = string.Empty;

    [Column("MOBILE_NO")]
    public string? MobileNo { get; set; } = string.Empty;

    [Column("TELEPHONE_NO")]
    public string? TelNo { get; set; } = string.Empty;

    [Column("BIRTH_DATE")]
    public string? BirthDate { get; set; } = string.Empty; // 빈값

    [Column("RESIGNATION_DATE")]
    public string? ResignationDate { get; set; } = string.Empty; // 사직일

    [Column("COMPANY_REGISTRATION_NO")]
    public string? CompanyRegistrationNo { get; set; } = string.Empty;

    [Column("MEMBER_TYPE")]
    public string? MemberType { get; set; } = string.Empty; // ex: S

    [Column("MEMBER_TYPE_NAME")]
    public string? MemberTypeName { get; set; } = string.Empty; // ex: 직원

    [Column("DEPARTMENT_NAME")]
    public string? DeptName { get; set; } = string.Empty;
}

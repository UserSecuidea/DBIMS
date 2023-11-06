using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

/// <summary>
/// PROC_FIND_SENIOR 의 return
/// </summary>
public partial class SeniorPerson
{
    public string? DeptCode { get; set; } = string.Empty;

    [Column("SAP_POSITION_NAME")]
    public string? SapPositionName { get; set; } = string.Empty;  //실장

    [Column("SAP_POSITION_CODE")]
    public string? SapPositionCode { get; set; } = string.Empty;   //14

    [Column("SAP_DEPT_CODE")]
    public string? SapDeptCode { get; set; } = string.Empty;  //51000705

    [Column("OBJID")]
    public string? Objid { get; set; } = string.Empty;  //NULL

    [Column("MEMBER_ID")]
    public string? MemberId { get; set; } = string.Empty;   //10004621

    [Column("MEMBER_NAME")]
    public string? MemberName { get; set; } = string.Empty; //양승주

    [Column("COMPANY_NAME")]
    public string? CompanyName { get; set; } = string.Empty;  //DB HiTek

    [Column("POSITION")]
    public string? Position { get; set; } = string.Empty;   //실장

    [Column("EMAIL")]
    public string? Email { get; set; } = string.Empty;  //seungjoo.yang@dbhitekdev.com

    [Column("IS_PERSONNEL")]
    public string? IsPersonnel { get; set; } = string.Empty; //010-8726-6156

    [Column("MOBILE_NO")]
    public string? MobileNo { get; set; } = string.Empty; //010-8726-6156

    [Column("TELEPHONE_NO")]
    public string? TelephoneNo { get; set; } = string.Empty; //032-680-4300

    [Column("BIRTH_DATE")]
    public string? BirthDate { get; set; } = string.Empty;   //NULL

    [Column("RESIGNATION_DATE")]
    public string? ResignationDate { get; set; } = string.Empty; //00000000

    [Column("COMPANY_REGISTRATION_NO")]
    public string? CompanyRegistrationNo { get; set; } = string.Empty; //NULL

    [Column("MEMBER_TYPE")]
    public string? MemberType { get; set; } = string.Empty; //S

    [Column("MEMBER_TYPE_NAME")]
    public string? MemberTypeName { get; set; } = string.Empty;  //직원

    [Column("DEPT_NAME")]
    public string? DeptName { get; set; } = string.Empty;   //경영지원실
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewCardPerson
{
    [Column("PID")]
    public int? Pid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Tel { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }

    [StringLength(30)]
    public string? OrgCode { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    public int? PersonType { get; set; }

    public int? PersonStatus { get; set; }

    [Column("personValid", TypeName = "datetime")]
    public DateTime? PersonValid { get; set; }

    [StringLength(30)]
    public string? PersonUser1 { get; set; }

    [StringLength(30)]
    public string? PersonUser2 { get; set; }

    [StringLength(30)]
    public string? PersonUser3 { get; set; }

    [StringLength(30)]
    public string? PersonUser4 { get; set; }

    [StringLength(30)]
    public string? PersonUser5 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? ReIssueCnt { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CardFullNo { get; set; } = null!;

    public int CardType { get; set; }

    [Column("CardStatusID")]
    public int CardStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ValidDate { get; set; }

    [StringLength(50)]
    public string? CardUser1 { get; set; }

    [StringLength(50)]
    public string? CardUser2 { get; set; }

    [StringLength(50)]
    public string? CardUser3 { get; set; }

    [StringLength(50)]
    public string? CardUser4 { get; set; }

    [StringLength(50)]
    public string? CardUser5 { get; set; }

    [StringLength(30)]
    public string? Company { get; set; }

    [StringLength(30)]
    public string? Campus { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CampusCode { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }
}

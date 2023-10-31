using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사원 테이블
/// </summary>
[Table("Person")]
public partial class Person
{
    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(68)]
    [Unicode(false)]
    public string? Tel { get; set; }

    [StringLength(68)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [Column("OrgID")]
    public int? OrgId { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }

    [Column("GradeID")]
    public int? GradeId { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    [Column("PersonTypeID")]
    public int PersonTypeId { get; set; }

    [StringLength(50)]
    public string? PersonTypeName { get; set; }

    [Column("PersonStatusID")]
    public int PersonStatusId { get; set; }

    [StringLength(50)]
    public string? PersonStatusName { get; set; }

    [Column("WorkScheduleID")]
    public int? WorkScheduleId { get; set; }

    [StringLength(300)]
    public string? ImageData { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ImageDataHash { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ValidDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Password { get; set; }

    public int AccessAuthority { get; set; }

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

    [Column("UpdatePWDate", TypeName = "datetime")]
    public DateTime? UpdatePwdate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [InverseProperty("PidNavigation")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    [InverseProperty("PidNavigation")]
    public virtual ICollection<FaceDetectKeyDatum> FaceDetectKeyData { get; set; } = new List<FaceDetectKeyDatum>();

    [ForeignKey("GradeId")]
    [InverseProperty("People")]
    public virtual Grade? Grade { get; set; }

    [ForeignKey("OrgId")]
    [InverseProperty("People")]
    public virtual Org? Org { get; set; }

    [ForeignKey("PersonStatusId")]
    [InverseProperty("People")]
    public virtual PersonStatus PersonStatus { get; set; } = null!;

    [ForeignKey("PersonTypeId")]
    [InverseProperty("People")]
    public virtual PersonType PersonType { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("People")]
    public virtual EqUser Update { get; set; } = null!;
}

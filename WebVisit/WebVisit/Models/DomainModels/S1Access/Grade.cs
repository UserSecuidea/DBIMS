using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 직급 테이블
/// </summary>
[Table("Grade")]
public partial class Grade
{
    [Key]
    [Column("GradeID")]
    public int GradeId { get; set; }

    [StringLength(50)]
    public string GradeName { get; set; } = null!;

    public int GradeOrder { get; set; }

    [StringLength(30)]
    public string? GradeCode { get; set; }

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

    [InverseProperty("Grade")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [ForeignKey("UpdateId")]
    [InverseProperty("Grades")]
    public virtual EqUser Update { get; set; } = null!;
}

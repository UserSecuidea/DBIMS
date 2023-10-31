using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 조직 정보 테이블
/// </summary>
[Table("Org")]
public partial class Org
{
    [Key]
    [Column("OrgID")]
    public int OrgId { get; set; }

    [StringLength(50)]
    public string OrgName { get; set; } = null!;

    [Column("ParentOrgID")]
    public int ParentOrgId { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    public int ReservedWord { get; set; }

    [StringLength(30)]
    public string? OrgCode { get; set; }

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

    [InverseProperty("Org")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [ForeignKey("UpdateId")]
    [InverseProperty("Orgs")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 공용부와 기기 그룹을 사용하기 위한 테이블
/// </summary>
[Table("EqMasterGroup")]
public partial class EqMasterGroup
{
    [Key]
    [Column("EqGroupID")]
    public int EqGroupId { get; set; }

    [StringLength(50)]
    public string? EqGroupName { get; set; }

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

    [InverseProperty("EqGroup")]
    public virtual ICollection<EqMasterGroupLink> EqMasterGroupLinks { get; set; } = new List<EqMasterGroupLink>();

    [ForeignKey("UpdateId")]
    [InverseProperty("EqMasterGroups")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입 권한 그룹 정보 테이블
/// </summary>
[Table("PersonGroup")]
public partial class PersonGroup
{
    [Key]
    [Column("PersonGroupID")]
    public int PersonGroupId { get; set; }

    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    [StringLength(100)]
    public string? GroupComment { get; set; }

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

    [ForeignKey("UpdateId")]
    [InverseProperty("PersonGroups")]
    public virtual EqUser Update { get; set; } = null!;
}

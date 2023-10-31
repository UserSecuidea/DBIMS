using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사원그룹의 기기 별 출입권한 테이블
/// </summary>
[PrimaryKey("PersonGroupId", "EqMasterId")]
[Table("PersonGroupToEqMaster")]
public partial class PersonGroupToEqMaster
{
    [Key]
    [Column("PersonGroupID")]
    public int PersonGroupId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string IsSecurity { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string IsAccess { get; set; } = null!;

    [Column("IsSecurity_EqMasterID")]
    [StringLength(1024)]
    [Unicode(false)]
    public string? IsSecurityEqMasterId { get; set; }

    [Column("IsAccess_EqMasterID")]
    [StringLength(1024)]
    [Unicode(false)]
    public string? IsAccessEqMasterId { get; set; }

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
    [InverseProperty("PersonGroupToEqMasters")]
    public virtual EqUser Update { get; set; } = null!;
}

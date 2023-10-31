using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 그룹 테이블
/// </summary>
[PrimaryKey("EqGroupId", "LocationId")]
[Table("EqMasterGroupLink")]
public partial class EqMasterGroupLink
{
    [Key]
    [Column("EqGroupID")]
    public int EqGroupId { get; set; }

    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

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

    [ForeignKey("EqGroupId")]
    [InverseProperty("EqMasterGroupLinks")]
    public virtual EqMasterGroup EqGroup { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("EqMasterGroupLinks")]
    public virtual EqUser Update { get; set; } = null!;
}

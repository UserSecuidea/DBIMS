using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입 권한 그룹ID와 사원ID를 저장 테이블
/// </summary>
[PrimaryKey("PersonGroupId", "Pid")]
[Table("PersonGroupLink")]
public partial class PersonGroupLink
{
    [Key]
    [Column("PersonGroupID")]
    public int PersonGroupId { get; set; }

    [Key]
    [Column("PID")]
    public int Pid { get; set; }

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
    [InverseProperty("PersonGroupLinks")]
    public virtual EqUser Update { get; set; } = null!;
}

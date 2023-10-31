using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 근태 결과 스트링 ID 테이블
/// </summary>
[Table("WorkResultID")]
public partial class WorkResultId
{
    [Key]
    [Column("WorkResultID")]
    public int WorkResultId1 { get; set; }

    [Column("WorkResultNameID")]
    public int WorkResultNameId { get; set; }

    public int? ReservedWord { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("WorkResultIds")]
    public virtual EqUser Update { get; set; } = null!;

    [InverseProperty("WorkResult")]
    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사원의 근무 재직 상태 구분 테이블
/// </summary>
[Table("PersonStatus")]
public partial class PersonStatus
{
    [Key]
    [Column("PersonStatueID")]
    public int PersonStatueId { get; set; }

    [Column("PersonStatusNameID")]
    public int PersonStatusNameId { get; set; }

    public int ViewCombo { get; set; }

    public int ReservedWord { get; set; }

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

    [InverseProperty("PersonStatus")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [ForeignKey("UpdateId")]
    [InverseProperty("PersonStatuses")]
    public virtual EqUser Update { get; set; } = null!;
}

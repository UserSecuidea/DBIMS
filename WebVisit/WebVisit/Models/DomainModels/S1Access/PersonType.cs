using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 사원 구분 테이블
/// </summary>
[Table("PersonType")]
public partial class PersonType
{
    [Key]
    [Column("PersonTypeID")]
    public int PersonTypeId { get; set; }

    [Column("PersonTypeNameID")]
    public int PersonTypeNameId { get; set; }

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

    [InverseProperty("PersonType")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    [ForeignKey("UpdateId")]
    [InverseProperty("PersonTypes")]
    public virtual EqUser Update { get; set; } = null!;
}

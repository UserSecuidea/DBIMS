using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 광 감지기의 종류 테이블
/// </summary>
[Table("DVRType")]
public partial class Dvrtype
{
    [Key]
    [Column("DVRTypeID")]
    public int DvrtypeId { get; set; }

    [Column("EqTypeNameID")]
    public int EqTypeNameId { get; set; }

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
    [InverseProperty("Dvrtypes")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 조이스틱을 사용하는 환경을 저장한다.
/// </summary>
[Table("JoyStick")]
public partial class JoyStick
{
    [Key]
    [Column("EqUserID")]
    public int EqUserId { get; set; }

    public int? Button1 { get; set; }

    public int? Button2 { get; set; }

    public int? Button3 { get; set; }

    public int? Button4 { get; set; }

    public int? Button5 { get; set; }

    public int? Button6 { get; set; }

    public int? Button7 { get; set; }

    public int? Button8 { get; set; }

    public int? Button9 { get; set; }

    public int? Button10 { get; set; }

    public int? Button11 { get; set; }

    public int? Button12 { get; set; }

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
    [InverseProperty("JoySticks")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("UserDefine")]
public partial class UserDefine
{
    public int PersonUser1 { get; set; }

    public int PersonUser2 { get; set; }

    public int PersonUser3 { get; set; }

    public int PersonUser4 { get; set; }

    public int PersonUser5 { get; set; }

    public int CardUser1 { get; set; }

    public int CardUser2 { get; set; }

    public int CardUser3 { get; set; }

    public int CardUser4 { get; set; }

    public int CardUser5 { get; set; }

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
    public virtual EqUser Update { get; set; } = null!;
}

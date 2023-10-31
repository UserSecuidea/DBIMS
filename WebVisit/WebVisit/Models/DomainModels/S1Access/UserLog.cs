using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("UserLog")]
public partial class UserLog
{
    public int UseSeq { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UseDate { get; set; }

    [Column("UseIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UseIp { get; set; }

    [Column("EqUserID")]
    public int EqUserId { get; set; }

    [Column("ID")]
    [StringLength(12)]
    [Unicode(false)]
    public string? Id { get; set; }

    public int? SystemType { get; set; }

    [StringLength(200)]
    public string? PageName { get; set; }

    public int? UseType { get; set; }

    public int? SubType { get; set; }

    [Column("TargetID")]
    public int? TargetId { get; set; }

    [Column("ETC")]
    public string? Etc { get; set; }
}

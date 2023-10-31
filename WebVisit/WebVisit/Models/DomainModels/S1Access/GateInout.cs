using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("GATE_INOUT")]
public partial class GateInout
{
    public long Sync { get; set; }

    [Column("IO")]
    [StringLength(1)]
    [Unicode(false)]
    public string Io { get; set; } = null!;

    [Column("ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string EventOfDate { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string EventOfTime { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? GateCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("SendYN")]
    [StringLength(1)]
    [Unicode(false)]
    public string SendYn { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? SendDate { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("ElevatorIF")]
public partial class ElevatorIf
{
    [Key]
    [Column("SEQ")]
    public int Seq { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TableName { get; set; } = null!;

    [Column("ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    public int DownFlag { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string ChangedType { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ChangedDate { get; set; }
}

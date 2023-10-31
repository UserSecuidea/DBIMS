using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("BatchLog")]
public partial class BatchLog
{
    [Column(TypeName = "datetime")]
    public DateTime DateTime { get; set; }

    [Column("BatID")]
    public int BatId { get; set; }

    public string? BatLog { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? Result { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string? UpdateUser { get; set; }
}

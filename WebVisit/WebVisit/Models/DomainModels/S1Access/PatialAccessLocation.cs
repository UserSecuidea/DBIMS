using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("PatialAccessLocation")]
public partial class PatialAccessLocation
{
    public int Seq { get; set; }

    [Column("CardID")]
    public int? CardId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    public int? DownFlag { get; set; }

    public int? IsSelected { get; set; }

    [Column("EqUserID")]
    public int? EqUserId { get; set; }
}

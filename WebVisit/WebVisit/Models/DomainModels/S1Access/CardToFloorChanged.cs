using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("CardToFloorChanged")]
public partial class CardToFloorChanged
{
    [Column("CardID")]
    public int CardId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ChangedDate { get; set; }
}

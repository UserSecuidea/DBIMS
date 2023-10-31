using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("CardKind")]
public partial class CardKind
{
    [Key]
    [Column("CardKindID")]
    [StringLength(2)]
    public string CardKindId { get; set; } = null!;

    [Column("CardKindNameID")]
    public int CardKindNameId { get; set; }

    public int ViewCombo { get; set; }
}

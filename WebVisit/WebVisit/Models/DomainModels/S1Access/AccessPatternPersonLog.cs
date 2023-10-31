using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("AccessPatternId", "CardId", "EqMasterId")]
[Table("AccessPatternPersonLog")]
public partial class AccessPatternPersonLog
{
    [Key]
    [Column("AccessPatternID")]
    public int AccessPatternId { get; set; }

    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

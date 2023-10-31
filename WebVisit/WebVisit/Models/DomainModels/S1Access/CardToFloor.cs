using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("CardId", "LocationId")]
[Table("CardToFloor")]
public partial class CardToFloor
{
    [Column("IDX")]
    public int Idx { get; set; }

    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

    public int IsAccess { get; set; }

    public int DownFlag { get; set; }

    [Column("isChanged")]
    public int? IsChanged { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

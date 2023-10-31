using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("EventDateTime", "EqMasterId")]
[Table("ReaderUpdateHistory")]
public partial class ReaderUpdateHistory
{
    public int Idx { get; set; }

    [Key]
    [Column(TypeName = "datetime")]
    public DateTime EventDateTime { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("FWType")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Fwtype { get; set; }

    [Column("FWIndex")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Fwindex { get; set; }

    [Column("FWVersion")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Fwversion { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("VI_ZTHR0100")]
public partial class ViZthr0100
{
    [Column("MANDT", TypeName = "numeric(3, 0)")]
    public decimal Mandt { get; set; }

    [Column("PERNR1")]
    [StringLength(8)]
    public string Pernr1 { get; set; } = null!;

    [Column("FDATE")]
    [StringLength(10)]
    public string Fdate { get; set; } = null!;

    [Column("TDATE")]
    [StringLength(10)]
    public string Tdate { get; set; } = null!;

    [Column("PERNR2")]
    [StringLength(8)]
    public string Pernr2 { get; set; } = null!;

    [Column("REMARK")]
    [StringLength(100)]
    public string? Remark { get; set; }

    [Column("STYPE")]
    [StringLength(1)]
    public string? Stype { get; set; }

    [Column("SEQNR", TypeName = "numeric(10, 0)")]
    public decimal Seqnr { get; set; }

    [Column("INUID")]
    [StringLength(8)]
    public string Inuid { get; set; } = null!;

    [Column("INDAT")]
    [StringLength(10)]
    public string Indat { get; set; } = null!;

    [Column("UDUID")]
    [StringLength(8)]
    public string? Uduid { get; set; }

    [Column("UDDAT")]
    [StringLength(10)]
    public string? Uddat { get; set; }

    [Column("UFLAG")]
    [StringLength(1)]
    public string Uflag { get; set; } = null!;
}

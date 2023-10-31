using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("PanelInfo")]
public partial class PanelInfo
{
    [Key]
    [Column("PanelID")]
    [StringLength(50)]
    [Unicode(false)]
    public string PanelId { get; set; } = null!;

    [StringLength(70)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("PanelTypeID")]
    public int PanelTypeId { get; set; }

    public int Position { get; set; }

    public float Width { get; set; }

    public float Height { get; set; }

    public float TranslateX { get; set; }

    public float TranslateY { get; set; }

    public int Zindex { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SearchStartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SearchEndTime { get; set; }

    [Column("EqUserID")]
    public int? EqUserId { get; set; }

    [Column("IsLPR")]
    public bool? IsLpr { get; set; }

    public int ViewFlag { get; set; }
}

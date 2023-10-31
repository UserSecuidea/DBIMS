using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("PanelContent")]
public partial class PanelContent
{
    [Key]
    [Column("PK")]
    public int Pk { get; set; }

    [Column("PanelID")]
    [StringLength(50)]
    [Unicode(false)]
    public string PanelId { get; set; } = null!;

    [Column("ContentID")]
    [StringLength(10)]
    public string? ContentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecvDateTime { get; set; }

    [Column("EqMasterID")]
    public int? EqMasterId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("MonitoringCardID")]
    [StringLength(46)]
    [Unicode(false)]
    public string? MonitoringCardId { get; set; }

    [Column("MonitoringEqID")]
    public int? MonitoringEqId { get; set; }
}

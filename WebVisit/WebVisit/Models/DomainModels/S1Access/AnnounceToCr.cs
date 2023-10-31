using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("EqMasterId", "IsAnnounce", "Num")]
[Table("AnnounceToCR")]
public partial class AnnounceToCr
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [Column("isAnnounce")]
    public int IsAnnounce { get; set; }

    [Key]
    public int Num { get; set; }

    [Column("AnnounceID")]
    public int AnnounceId { get; set; }

    public int DownFlag { get; set; }

    public int DeleteFlag { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("AnnounceToCrs")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AccessUpdate")]
[Index("DownFlag", Name = "IX_AccessUpdate_DownFlag", AllDescending = true)]
[Index("IsUrgency", Name = "IX_AccessUpdate_Urgency", AllDescending = true)]
public partial class AccessUpdate
{
    [Key]
    public int Idx { get; set; }

    [Column("CardID")]
    public int CardId { get; set; }

    public int DownFlag { get; set; }

    public int IsUrgency { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UPdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;
}

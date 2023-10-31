using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("CardStatus")]
public partial class CardStatus
{
    [Key]
    [Column("CardStatusID")]
    public int CardStatusId { get; set; }

    [Column("CardStatusNameID")]
    public int CardStatusNameId { get; set; }

    public int ViewCombo { get; set; }

    [StringLength(1)]
    public string ReservedWord { get; set; } = null!;

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

    [InverseProperty("CardStatus")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    [ForeignKey("UpdateId")]
    [InverseProperty("CardStatuses")]
    public virtual EqUser Update { get; set; } = null!;
}

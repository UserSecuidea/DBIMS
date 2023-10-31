using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("Announce")]
public partial class Announce
{
    [Key]
    [Column("AnnounceID")]
    public int AnnounceId { get; set; }

    [Column("isAnnounce")]
    public int IsAnnounce { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDateTime { get; set; }

    [Column("Announce")]
    [StringLength(100)]
    public string? Announce1 { get; set; }

    [Column("Image_1")]
    public byte[]? Image1 { get; set; }

    [Column("Image_2")]
    public byte[]? Image2 { get; set; }

    [Column("Image_3")]
    public byte[]? Image3 { get; set; }

    [Column("Image_4")]
    public byte[]? Image4 { get; set; }

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
    [InverseProperty("Announces")]
    public virtual EqUser Update { get; set; } = null!;
}

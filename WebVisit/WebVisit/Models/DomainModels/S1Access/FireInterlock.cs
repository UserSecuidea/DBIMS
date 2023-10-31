using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("FireInterlock")]
public partial class FireInterlock
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int FireEquipType { get; set; }

    [Column("HostEqMasterID")]
    public int? HostEqMasterId { get; set; }

    [Column("FireIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? FireIp { get; set; }

    public int DownFlag { get; set; }

    public int? EqState { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

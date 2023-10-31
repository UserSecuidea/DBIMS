using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("CardId", "EqMasterId")]
[Table("FaceReaderToCR")]
public partial class FaceReaderToCr
{
    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int IsAccess { get; set; }

    public int DownFlag { get; set; }

    [Column("UpdateEqMasterID")]
    public int? UpdateEqMasterId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ResultCode { get; set; }

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
}

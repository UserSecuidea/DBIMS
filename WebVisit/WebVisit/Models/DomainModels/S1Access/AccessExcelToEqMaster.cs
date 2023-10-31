using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AccessExcelToEqMaster")]
public partial class AccessExcelToEqMaster
{
    [Key]
    public int Idx { get; set; }

    [Column("CardID")]
    public int CardId { get; set; }

    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int IsMaster { get; set; }

    public int ModeNum { get; set; }

    [Column("UIID")]
    public int Uiid { get; set; }

    public int HasAccess { get; set; }

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

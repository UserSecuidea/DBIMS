using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("ServerAuthResult")]
public partial class ServerAuthResult
{
    [Key]
    [Column("idx")]
    public int Idx { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EventDateTime { get; set; }

    public int? GateNo { get; set; }

    public int? CardReaderNo { get; set; }

    public int? InOutMode { get; set; }

    [Column("ReqQR")]
    [StringLength(128)]
    [Unicode(false)]
    public string? ReqQr { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ServerSuccess { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ServerMessageCode { get; set; }

    [StringLength(128)]
    public string? ServerMessage { get; set; }

    [StringLength(128)]
    public string? ServerIdno { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string? ResCardNo { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ResErrorCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }
}

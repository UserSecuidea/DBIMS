using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("MTS_SMS_MSG")]
[Index("TranPhone", Name = "MTS_SMS_MSG_IDX1")]
[Index("TranDate", Name = "MTS_SMS_MSG_IDX2")]
[Index("TranStatus", Name = "MTS_SMS_MSG_IDX3")]
[Index("TranRefkey", Name = "MTS_SMS_MSG_IDX4")]
public partial class MtsSmsMsg
{
    [Key]
    [Column("TRAN_PR")]
    public int TranPr { get; set; }

    [Column("TRAN_REFKEY")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TranRefkey { get; set; }

    [Column("TRAN_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TranId { get; set; }

    [Column("TRAN_PHONE")]
    [StringLength(15)]
    [Unicode(false)]
    public string TranPhone { get; set; } = null!;

    [Column("TRAN_CALLBACK")]
    [StringLength(15)]
    [Unicode(false)]
    public string TranCallback { get; set; } = null!;

    [Column("TRAN_MSG")]
    [StringLength(150)]
    [Unicode(false)]
    public string TranMsg { get; set; } = null!;

    [Column("TRAN_DATE", TypeName = "datetime")]
    public DateTime TranDate { get; set; }

    [Column("TRAN_TYPE")]
    public int TranType { get; set; }

    [Column("TRAN_STATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string TranStatus { get; set; } = null!;

    [Column("TRAN_SENDDATE", TypeName = "datetime")]
    public DateTime? TranSenddate { get; set; }

    [Column("TRAN_REPORTDATE", TypeName = "datetime")]
    public DateTime? TranReportdate { get; set; }

    [Column("TRAN_RSLTDATE", TypeName = "datetime")]
    public DateTime? TranRsltdate { get; set; }

    [Column("TRAN_RSLT")]
    [StringLength(2)]
    [Unicode(false)]
    public string? TranRslt { get; set; }

    [Column("TRAN_ETC1")]
    [StringLength(160)]
    [Unicode(false)]
    public string? TranEtc1 { get; set; }

    [Column("TRAN_ETC2")]
    [StringLength(160)]
    [Unicode(false)]
    public string? TranEtc2 { get; set; }

    [Column("TRAN_ETC3")]
    [StringLength(160)]
    [Unicode(false)]
    public string? TranEtc3 { get; set; }

    [Column("TRAN_ETC4")]
    [StringLength(160)]
    [Unicode(false)]
    public string? TranEtc4 { get; set; }

    [Column("TRAN_END_TELCO")]
    [StringLength(8)]
    [Unicode(false)]
    public string? TranEndTelco { get; set; }

    [Column("TRAN_LOG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? TranLog { get; set; }

    [Column("TRAN_GRPSEQ")]
    public int? TranGrpseq { get; set; }
}

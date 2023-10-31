using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("MTS_ATALK_MSG_LOG")]
[Index("TranPr", Name = "idx1_MTS_ATALK_MSG_LOG")]
[Index("TranPhone", Name = "idx2_MTS_ATALK_MSG_LOG")]
[Index("TranDate", Name = "idx3_MTS_ATALK_MSG_LOG")]
[Index("TranStatus", Name = "idx4_MTS_ATALK_MSG_LOG")]
public partial class MtsAtalkMsgLog
{
    [Column("TRAN_PR")]
    public int? TranPr { get; set; }

    [Column("TRAN_REFKEY")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TranRefkey { get; set; }

    [Column("TRAN_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TranId { get; set; }

    [Column("TRAN_SENDER_KEY")]
    [StringLength(40)]
    [Unicode(false)]
    public string? TranSenderKey { get; set; }

    [Column("TRAN_TMPL_CD")]
    [StringLength(30)]
    [Unicode(false)]
    public string TranTmplCd { get; set; } = null!;

    [Column("TRAN_BUTTON")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? TranButton { get; set; }

    [Column("TRAN_PHONE")]
    [StringLength(15)]
    [Unicode(false)]
    public string? TranPhone { get; set; }

    [Column("TRAN_MSG")]
    [StringLength(2000)]
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
    [StringLength(5)]
    [Unicode(false)]
    public string? TranRslt { get; set; }

    [Column("TRAN_REPLACE_MSG")]
    [StringLength(2000)]
    [Unicode(false)]
    public string TranReplaceMsg { get; set; } = null!;

    [Column("TRAN_SUBJECT")]
    [StringLength(100)]
    [Unicode(false)]
    public string? TranSubject { get; set; }

    [Column("TRAN_CALLBACK")]
    [StringLength(15)]
    [Unicode(false)]
    public string TranCallback { get; set; } = null!;

    [Column("TRAN_REPLACE_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? TranReplaceType { get; set; }

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

    [Column("TRAN_TITLE")]
    [StringLength(500)]
    [Unicode(false)]
    public string? TranTitle { get; set; }

    [Column("TRAN_NATION_PHONE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? TranNationPhone { get; set; }

    [Column("TRAN_SUPPLEMENT")]
    [StringLength(4000)]
    [Unicode(false)]
    public string? TranSupplement { get; set; }

    [Column("TRAN_PRICE")]
    public int? TranPrice { get; set; }

    [Column("TRAN_CURRENCY")]
    [StringLength(3)]
    [Unicode(false)]
    public string? TranCurrency { get; set; }

    [Column("TRAN_HEADER")]
    [StringLength(32)]
    [Unicode(false)]
    public string? TranHeader { get; set; }

    [Column("TRAN_ATTACHMENT")]
    [StringLength(4000)]
    [Unicode(false)]
    public string? TranAttachment { get; set; }

    [Column("TRAN_APPUSERID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TranAppuserid { get; set; }
}

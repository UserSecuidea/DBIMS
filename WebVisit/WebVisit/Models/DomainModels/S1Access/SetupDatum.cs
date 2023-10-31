using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 운영 등록정보
/// </summary>
[Keyless]
public partial class SetupDatum
{
    public int SetUpManagerTitle { get; set; }

    public int VisitTitle { get; set; }

    public int MobileTitle { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string ScreenLockTime { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string WebDomainName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string VisitDomainName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string MobileDomainName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ToolDomainName { get; set; }

    [Column("DDNSServerIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? DdnsserverIp { get; set; }

    [Column("DDNSWritePort")]
    [StringLength(5)]
    [Unicode(false)]
    public string? DdnswritePort { get; set; }

    [Column("DDNSReadPort")]
    [StringLength(5)]
    [Unicode(false)]
    public string? DdnsreadPort { get; set; }

    [StringLength(50)]
    public string? SetUpManagerTitleImage { get; set; }

    [StringLength(50)]
    public string? SetUpManagerMainImage { get; set; }

    [Column("SMSSentenceID")]
    public int? SmssentenceId { get; set; }

    [Column("ImageUploadURL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ImageUploadUrl { get; set; }

    public int? PersonDeleteOption { get; set; }

    public int? PersonInfoKeep { get; set; }

    [StringLength(50)]
    public string? FooterImage { get; set; }

    [StringLength(50)]
    public string? VisitorTitleImage { get; set; }

    [StringLength(50)]
    public string? MobileWebTitleImage { get; set; }

    [Column("SMSID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Smsid { get; set; }

    [Column("SMSPW")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Smspw { get; set; }

    [Column("SMSTel")]
    [StringLength(68)]
    [Unicode(false)]
    public string? Smstel { get; set; }

    [Column("SMTPAddress")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Smtpaddress { get; set; }

    [Column("SMTPType")]
    public int? Smtptype { get; set; }

    [Column("SMTPPort")]
    public int? Smtpport { get; set; }

    [Column("SMTPID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Smtpid { get; set; }

    [Column("SMTPPW")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Smtppw { get; set; }

    [Column("DBPollingTime")]
    public int? DbpollingTime { get; set; }

    public int? EventUpdateTime { get; set; }

    [Column("x2CommConnectInterval")]
    public int? X2CommConnectInterval { get; set; }

    [Column("FDConnectInterval")]
    public int? FdconnectInterval { get; set; }

    public int? HideCardNumber { get; set; }

    public int? ExternalPort { get; set; }

    public int? AutoPaging { get; set; }

    public int? VideoLayoutX { get; set; }

    public int? VideoLayoutY { get; set; }

    [Column("PWUpdateCycle")]
    public int? PwupdateCycle { get; set; }

    [Column("PWUpdateNotify")]
    public int? PwupdateNotify { get; set; }

    public int? GroupDelOption { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? SystemVersion { get; set; }

    public int? EventServerPort { get; set; }

    public int ConnectActionFlag { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string MobileCardServer { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [ForeignKey("UpdateId")]
    public virtual EqUser Update { get; set; } = null!;
}

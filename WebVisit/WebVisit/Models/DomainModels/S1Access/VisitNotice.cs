using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방객 시스템에서 공지사항 기능 테이블(내방객)
/// </summary>
[Table("VisitNotice")]
public partial class VisitNotice
{
    [Key]
    [Column("VisitNoticeID")]
    public int VisitNoticeId { get; set; }

    [StringLength(50)]
    public string NoticeTitle { get; set; } = null!;

    public string? Content { get; set; }

    [Column("NoticeSDate", TypeName = "datetime")]
    public DateTime? NoticeSdate { get; set; }

    [Column("NoticeEDate", TypeName = "datetime")]
    public DateTime? NoticeEdate { get; set; }

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
    [InverseProperty("VisitNotices")]
    public virtual EqUser Update { get; set; } = null!;
}

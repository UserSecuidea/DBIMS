using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("SMS_MESSAGE")]
public partial class SmsMessage
{
    [Column("IDX")]
    public int Idx { get; set; }

    [Column("TO")]
    [StringLength(32)]
    public string? To { get; set; }

    [Column("MESSAGE")]
    [StringLength(4000)]
    public string? Message { get; set; }

    [Column("FROM")]
    [StringLength(32)]
    public string? From { get; set; }

    [Column("URL")]
    [StringLength(200)]
    public string? Url { get; set; }

    [Column("INSERTED", TypeName = "datetime")]
    public DateTime? Inserted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Updated { get; set; }

    [Column("STATUS")]
    public int Status { get; set; }

    public int? MsgType { get; set; }

    [Column("TITLE")]
    [StringLength(100)]
    public string? Title { get; set; }

    [Column("TMPL_CD")]
    [StringLength(40)]
    [Unicode(false)]
    public string? TmplCd { get; set; }
}

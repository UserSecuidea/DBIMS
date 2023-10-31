using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("MAIL_MESSAGE")]
public partial class MailMessage
{
    [Column("IDX")]
    public int Idx { get; set; }

    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? SenderName { get; set; }

    [StringLength(100)]
    public string? SenderMail { get; set; }

    [StringLength(100)]
    public string? ReceiverName { get; set; }

    [StringLength(100)]
    public string? ReceiverMail { get; set; }

    [StringLength(4000)]
    public string? Contents { get; set; }

    [Column("INSERTED", TypeName = "datetime")]
    public DateTime? Inserted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Updated { get; set; }

    [Column("STATUS")]
    public int Status { get; set; }

    public int? MsgType { get; set; }
}

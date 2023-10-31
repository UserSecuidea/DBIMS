using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("TEMPLATE_MAIL")]
public partial class TemplateMail
{
    [Key]
    [Column("MAIL_TYPE")]
    [StringLength(20)]
    [Unicode(false)]
    public string MailType { get; set; } = null!;

    [Column("MAIL_SENDER")]
    [StringLength(50)]
    public string? MailSender { get; set; }

    [Column("MAIL_SENDER_ADDRESS")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MailSenderAddress { get; set; }

    [Column("MAIL_TITLE")]
    [StringLength(200)]
    public string? MailTitle { get; set; }

    [Column("MAIL_CONTENTS")]
    public string? MailContents { get; set; }

    [Column("INSERTDATE", TypeName = "datetime")]
    public DateTime? Insertdate { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }
}

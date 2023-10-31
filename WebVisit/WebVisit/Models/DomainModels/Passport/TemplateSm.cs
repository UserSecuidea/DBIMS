using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("TEMPLATE_SMS")]
public partial class TemplateSm
{
    [Key]
    [Column("MSG_TYPE")]
    public int MsgType { get; set; }

    [Column("MSG_SUBJECT")]
    [StringLength(200)]
    public string? MsgSubject { get; set; }

    [Column("SENDER_KEY")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SenderKey { get; set; }

    [Column("TMPL_CD")]
    [StringLength(100)]
    [Unicode(false)]
    public string? TmplCd { get; set; }

    [Column("NUMFROM")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Numfrom { get; set; }

    [Column("TITLE")]
    [StringLength(200)]
    public string? Title { get; set; }

    [Column("MSG")]
    public string? Msg { get; set; }

    [Column("INSERTDATE", TypeName = "datetime")]
    public DateTime? Insertdate { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }
}

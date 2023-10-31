using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[PrimaryKey("TranPr", "ContentSeq")]
[Table("MTS_MMS_CONTENTS")]
public partial class MtsMmsContent
{
    [Key]
    [Column("TRAN_PR")]
    [StringLength(16)]
    [Unicode(false)]
    public string TranPr { get; set; } = null!;

    [Key]
    [Column("CONTENT_SEQ")]
    public int ContentSeq { get; set; }

    [Column("CONTENT_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string ContentType { get; set; } = null!;

    [Column("CONTENT_NAME")]
    [StringLength(255)]
    [Unicode(false)]
    public string ContentName { get; set; } = null!;

    [Column("CONTENT_SVC")]
    [StringLength(3)]
    [Unicode(false)]
    public string? ContentSvc { get; set; }

    [Column("TRAN_LOG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? TranLog { get; set; }
}

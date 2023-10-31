using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewTempCard
{
    [StringLength(50)]
    [Unicode(false)]
    public string UserCardNo { get; set; } = null!;

    public int? IssuCnt { get; set; }

    [Column("TCardName")]
    [StringLength(50)]
    public string? TcardName { get; set; }

    [Column("TCardNo")]
    [StringLength(3)]
    public string? TcardNo { get; set; }

    [StringLength(50)]
    public string? Area { get; set; }

    [StringLength(50)]
    public string? UserInfo { get; set; }

    [Column("RStatus")]
    [StringLength(50)]
    public string? Rstatus { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ValidDate { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewVisitorCard
{
    [StringLength(50)]
    [Unicode(false)]
    public string UserCardNo { get; set; } = null!;

    public int? IssuCnt { get; set; }

    [Column("VCardName")]
    [StringLength(50)]
    public string? VcardName { get; set; }

    [Column("VCardNo")]
    [StringLength(4)]
    public string? VcardNo { get; set; }

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

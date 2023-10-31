using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AccessRightUIInfo")]
public partial class AccessRightUiinfo
{
    public int Seq { get; set; }

    [Key]
    [Column("UIID")]
    public int Uiid { get; set; }

    [Column("UICode")]
    [StringLength(200)]
    public string Uicode { get; set; } = null!;

    [Column("UIName")]
    [StringLength(200)]
    public string Uiname { get; set; } = null!;
}

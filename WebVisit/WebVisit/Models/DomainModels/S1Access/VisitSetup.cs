using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 내방객 시스템 환경설정(내방객)
/// </summary>
[Keyless]
[Table("VisitSetup")]
public partial class VisitSetup
{
    public int? AssignFunc { get; set; }

    public int? NoticeFunc { get; set; }

    public int? SignPadFunc { get; set; }

    [Column("IDCardScanner")]
    public int? IdcardScanner { get; set; }

    [Column("PInformation")]
    public string? Pinformation { get; set; }
}

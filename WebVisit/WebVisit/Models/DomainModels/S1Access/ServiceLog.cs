using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 웹에서 처리시, 각 로그를 시간별로 저장한다.
/// </summary>
[Keyless]
[Table("ServiceLog")]
[Index("DateTime", Name = "IX_SERVICELOG", AllDescending = true)]
public partial class ServiceLog
{
    [Column(TypeName = "datetime")]
    public DateTime DateTime { get; set; }

    [Column("ServiceLog")]
    public string? ServiceLog1 { get; set; }

    [Column("isPatch")]
    public bool? IsPatch { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 공용부 출입권한 자동등록 버퍼테이블
/// </summary>
[Keyless]
[Table("PublicLocationSchedule")]
public partial class PublicLocationSchedule
{
    public int Seq { get; set; }

    [Column("CardID")]
    public int CardId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CardNo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? DoneTime { get; set; }
}

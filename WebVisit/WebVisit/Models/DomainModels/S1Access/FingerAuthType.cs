using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 지문 인증 방식 테이블
/// </summary>
[Table("FingerAuthType")]
public partial class FingerAuthType
{
    [Key]
    [Column("FingeAuthTypeID")]
    public int FingeAuthTypeId { get; set; }

    [Column("FingerAuthTypeNameID")]
    public int FingerAuthTypeNameId { get; set; }

    [InverseProperty("FingerAuthType")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}

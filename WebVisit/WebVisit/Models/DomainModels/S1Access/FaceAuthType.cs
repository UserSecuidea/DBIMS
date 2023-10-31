using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 얼굴 인증 방식 테이블
/// </summary>
[Table("FaceAuthType")]
public partial class FaceAuthType
{
    [Key]
    [Column("FaceAuthTypeID")]
    public int FaceAuthTypeId { get; set; }

    [Column("FaceAuthTypeNameID")]
    public int FaceAuthTypeNameId { get; set; }

    [InverseProperty("FaceAuthType")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 광 감지기의 종류 테이블
/// </summary>
[Table("FDType")]
public partial class Fdtype
{
    [Key]
    [Column("FDTypeID")]
    public int FdtypeId { get; set; }

    [Column("EqTypeNameID")]
    public int EqTypeNameId { get; set; }
}

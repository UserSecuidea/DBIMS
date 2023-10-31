using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 부서 연동 테이블(인사연동)
/// </summary>
[Keyless]
[Table("HROrgData")]
public partial class HrorgDatum
{
    [StringLength(50)]
    public string OrgName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? OrgCode { get; set; }

    [StringLength(50)]
    public string? ParentOrgName { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ParentOrgCode { get; set; }

    /// <summary>
    /// 0:삭제, 1:등록, 2:수정
    /// </summary>
    public int UpdateOption { get; set; }
}

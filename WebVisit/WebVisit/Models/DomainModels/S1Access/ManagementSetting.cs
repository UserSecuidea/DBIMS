using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("ServiceName", "ConfKey")]
public partial class ManagementSetting
{
    [Column("SEQ")]
    public int Seq { get; set; }

    /// <summary>
    /// 사용하는 서비스(프로그램) 이름
    /// </summary>
    [Key]
    [StringLength(128)]
    public string ServiceName { get; set; } = null!;

    /// <summary>
    /// 설정 구분자
    /// </summary>
    [Key]
    [StringLength(128)]
    public string ConfKey { get; set; } = null!;

    /// <summary>
    /// 설정 Value Type - int / string / bool / date 
    /// </summary>
    [StringLength(64)]
    public string? ValueType { get; set; }

    /// <summary>
    /// 설정값
    /// </summary>
    [StringLength(512)]
    public string? ConfValue { get; set; }

    [StringLength(512)]
    public string? DefaultValue { get; set; }

    public int? MinValue { get; set; }

    public int? MaxValue { get; set; }

    [StringLength(1000)]
    public string? RegExpression { get; set; }

    /// <summary>
    /// 설정에 대한 설명
    /// </summary>
    [StringLength(512)]
    public string? Description { get; set; }

    /// <summary>
    /// 업데이트 시간
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

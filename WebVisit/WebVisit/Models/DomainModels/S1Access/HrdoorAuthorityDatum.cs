using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("HRDoorAuthorityData")]
[Index("ValidDateS", Name = "NonClusteredIndex-HRDoorAuthorityData-VaildDate_S", AllDescending = true)]
[Index("ValidDateE", Name = "NonClusteredIndex-HRDoorAuthorityData-ValidDate_E", AllDescending = true)]
public partial class HrdoorAuthorityDatum
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(36)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    /// <summary>
    /// 출입문ID
    /// </summary>
    [Column("DeviceID")]
    public int DeviceId { get; set; }

    /// <summary>
    /// 출입시작일
    /// </summary>
    [Column("ValidDate_S", TypeName = "datetime")]
    public DateTime ValidDateS { get; set; }

    /// <summary>
    /// 출입종료일
    /// </summary>
    [Column("ValidDate_E", TypeName = "datetime")]
    public DateTime ValidDateE { get; set; }

    /// <summary>
    /// 0: 적용전(기본값), 1:출입권한적용, 2:출입권한제거
    /// </summary>
    public int ApplyFlag { get; set; }
}

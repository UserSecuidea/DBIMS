using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입문 등록정보의 각 필드값의 세부 다국어 테이블 정의
/// </summary>
[PrimaryKey("DoorModePropertyId", "DoorModePropertyDataId1")]
[Table("DoorModePropertyDataID")]
public partial class DoorModePropertyDataId
{
    [Key]
    [Column("DoorModePropertyID")]
    public int DoorModePropertyId { get; set; }

    [Key]
    [Column("DoorModePropertyDataID")]
    public int DoorModePropertyDataId1 { get; set; }

    [Column("DoorModePropertyNameID")]
    public int DoorModePropertyNameId { get; set; }
}

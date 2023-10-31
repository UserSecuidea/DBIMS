using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 운영 모드 스트링 테이블
/// </summary>
[PrimaryKey("DoorRunId", "DoorRunNameId")]
[Table("DoorRunModeName")]
public partial class DoorRunModeName
{
    [Key]
    [Column("DoorRunID")]
    public int DoorRunId { get; set; }

    [Key]
    [Column("DoorRunNameID")]
    public int DoorRunNameId { get; set; }
}

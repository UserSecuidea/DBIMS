using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 광 감지기의 종류 테이블
/// </summary>
[Table("IPCameraType")]
public partial class IpcameraType
{
    [Key]
    [Column("IPCameraTypeID")]
    public int IpcameraTypeId { get; set; }

    [Column("IPCameraTypeNameID")]
    public int IpcameraTypeNameId { get; set; }
}

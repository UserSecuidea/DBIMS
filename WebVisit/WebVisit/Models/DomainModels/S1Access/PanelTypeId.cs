using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 통제서버에서 사용하는 패널 TypeID 정의
/// </summary>
[Table("PanelTypeID")]
public partial class PanelTypeId
{
    [Key]
    [Column("PanelTypeID")]
    public int PanelTypeId1 { get; set; }

    [Column("PanelTypeNameID")]
    public int PanelTypeNameId { get; set; }

    public int? ConsoleFlag { get; set; }

    public int? DashBoardFlag { get; set; }
}

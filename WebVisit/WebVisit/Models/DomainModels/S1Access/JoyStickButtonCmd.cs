using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 조이스틱에 맵핑되는 기능을 정의 한다.
/// </summary>
[Table("JoyStickButtonCmd")]
public partial class JoyStickButtonCmd
{
    [Key]
    [Column("JoyStickButtonCmdID")]
    public int JoyStickButtonCmdId { get; set; }

    [Column("JoyStickButtonCmd")]
    [StringLength(10)]
    [Unicode(false)]
    public string JoyStickButtonCmd1 { get; set; } = null!;

    [Column("JoyStickButtonCmdNameID")]
    public int JoyStickButtonCmdNameId { get; set; }
}

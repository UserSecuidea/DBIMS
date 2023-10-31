using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 각 EqCode, EqType에 해당 하는 기기의 전송 명령어의 정의
/// </summary>
[PrimaryKey("EqTypeId", "Command")]
[Table("EqCommand")]
public partial class EqCommand
{
    [Key]
    [Column("EqTypeID")]
    public int EqTypeId { get; set; }

    [Key]
    [StringLength(2)]
    [Unicode(false)]
    public string Command { get; set; } = null!;

    [Column("CommandNameID")]
    public int CommandNameId { get; set; }

    public int Reserved { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [ForeignKey("EqTypeId")]
    [InverseProperty("EqCommands")]
    public virtual EqType EqType { get; set; } = null!;
}

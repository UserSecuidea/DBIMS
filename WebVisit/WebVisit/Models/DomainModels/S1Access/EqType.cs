using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 상태 연동 정보 및 기기 구분 테이블
/// </summary>
[Table("EqType")]
public partial class EqType
{
    [Key]
    [Column("EqTypeID")]
    public int EqTypeId { get; set; }

    public int AlertParent { get; set; }

    public int AlertChild { get; set; }

    [Column("EqTypeNameID")]
    public int EqTypeNameId { get; set; }

    public int DoorAccessMode { get; set; }

    public int SecurityMode { get; set; }

    public int DoorRunMode { get; set; }

    public int EquipLightMode { get; set; }

    public int CardReader { get; set; }

    [Column("TTS")]
    public int Tts { get; set; }

    public int Reserved { get; set; }

    public int EnableFlag { get; set; }

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

    [InverseProperty("EqType")]
    public virtual ICollection<EqCommand> EqCommands { get; set; } = new List<EqCommand>();

    [InverseProperty("EqType")]
    public virtual ICollection<EqMaster> EqMasters { get; set; } = new List<EqMaster>();

    [ForeignKey("UpdateId")]
    [InverseProperty("EqTypes")]
    public virtual EqUser Update { get; set; } = null!;
}

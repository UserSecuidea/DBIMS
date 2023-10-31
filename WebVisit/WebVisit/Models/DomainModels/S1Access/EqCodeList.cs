using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 코드 리스트 테이블
/// </summary>
[Table("EqCodeList")]
public partial class EqCodeList
{
    [Key]
    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Column("EqNameID")]
    public int EqNameId { get; set; }

    public int? Port { get; set; }

    public int AccessController { get; set; }

    public int VideoController { get; set; }

    public int CommManage { get; set; }

    public int IsPointView { get; set; }

    public int IsConditionView { get; set; }

    public int IsControlView { get; set; }

    public int DoorRunScMax { get; set; }

    public int DoorAcsScMax { get; set; }

    [Column("IsFDSpider")]
    public int IsFdspider { get; set; }

    public int EnableFlag { get; set; }

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

    [InverseProperty("EqCode")]
    public virtual ICollection<AccessPatternEqStatus> AccessPatternEqStatuses { get; set; } = new List<AccessPatternEqStatus>();

    [InverseProperty("EqCode")]
    public virtual ICollection<EqMaster> EqMasters { get; set; } = new List<EqMaster>();

    [InverseProperty("EqCode")]
    public virtual ICollection<EqStatus> EqStatuses { get; set; } = new List<EqStatus>();

    [ForeignKey("UpdateId")]
    [InverseProperty("EqCodeLists")]
    public virtual EqUser Update { get; set; } = null!;
}

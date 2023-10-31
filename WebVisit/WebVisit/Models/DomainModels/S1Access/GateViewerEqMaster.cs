using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입자 모니터링을 위한 출입문의 카드리더 정보
/// </summary>
[PrimaryKey("EqMasterId", "SeqIndex", "CdreqMasterId")]
[Table("GateViewerEqMaster")]
public partial class GateViewerEqMaster
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    public int SeqIndex { get; set; }

    [Key]
    [Column("CDREqMasterID")]
    public int CdreqMasterId { get; set; }

    public int IsFacePicView { get; set; }

    [Column("isPersonInfoView")]
    public int IsPersonInfoView { get; set; }

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

    [ForeignKey("UpdateId")]
    [InverseProperty("GateViewerEqMasters")]
    public virtual EqUser Update { get; set; } = null!;
}

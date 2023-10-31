using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 출입문 운영 모드 및 등록 정보
/// </summary>
[PrimaryKey("EqMasterId", "EqCodeId")]
[Table("DoorModeProperty")]
public partial class DoorModeProperty
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Column("DoorRunID")]
    public int? DoorRunId { get; set; }

    public int? CardValidDateBeep { get; set; }

    public int? DoorCancelTime { get; set; }

    public int? OpenLookoutTime { get; set; }

    public int? KindOfSignal { get; set; }

    public int? FireDoorAction { get; set; }

    public int? KindOfDoor { get; set; }

    public int? CardInOutLatency { get; set; }

    public int? TemporaryInoutFunc { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TemporaryInoutTime { get; set; }

    public int? BufferFullActionMode { get; set; }

    public int? OpenLookoutAlert { get; set; }

    public int? DataSaveAlert { get; set; }

    public int? InOutDoorInfoBeep { get; set; }

    public int? ScheduleUse { get; set; }

    public int? AntiPassBackUse { get; set; }

    [Column("LPC")]
    public int? Lpc { get; set; }

    [Column("SLCNo")]
    public int? Slcno { get; set; }

    public int? CardReaderCnt { get; set; }

    [Column("ANN")]
    public int? Ann { get; set; }

    [Column("ANNNo")]
    public int? Annno { get; set; }

    [Column("ANNWdNo")]
    public int? AnnwdNo { get; set; }

    public int? CommErrDoorAction { get; set; }

    public int? TrackingUse { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Property1 { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Property2 { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Property3 { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Property4 { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Property5 { get; set; }

    public int? DownFlag { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public int? CancelInOutMode { get; set; }

    public int? DoorFaceAuthType { get; set; }
}

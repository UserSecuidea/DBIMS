using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 기기 정보 테이블
/// </summary>
[PrimaryKey("EqMasterId", "EqCodeId", "Master", "Local", "Point", "Loop")]
[Table("EqMaster")]
[Index("Mac", "EqCodeId", Name = "IX_EqMasterMAC")]
[Index("CommServerId", Name = "IX_EqMaster_CSID")]
[Index("IsConnect", Name = "IX_EqMaster_IsCON")]
[Index("EqCodeId", "Master", "Local", "Point", "Loop", "EqTypeId", Name = "UX_Eqmaster", IsUnique = true)]
public partial class EqMaster
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Key]
    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Key]
    public int Master { get; set; }

    [Key]
    public int Local { get; set; }

    [Key]
    public int Point { get; set; }

    [Key]
    public int Loop { get; set; }

    public int? EqMasterNo { get; set; }

    [Column("EqTypeID")]
    public int? EqTypeId { get; set; }

    [Column("ParentEqMasterID")]
    public int? ParentEqMasterId { get; set; }

    [Column("CommServerID")]
    public int? CommServerId { get; set; }

    [StringLength(50)]
    public string? EqName { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [StringLength(30)]
    public string? ContractNo { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? Mac { get; set; }

    [Column("UseDDNS_SIP")]
    public int? UseDdnsSip { get; set; }

    [Column("RTSPAddress")]
    [StringLength(512)]
    [Unicode(false)]
    public string? Rtspaddress { get; set; }

    [Column("RTSPID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Rtspid { get; set; }

    [Column("RTSPPW")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Rtsppw { get; set; }

    [Column("AStatusCode")]
    [StringLength(30)]
    [Unicode(false)]
    public string? AstatusCode { get; set; }

    [Column("EStatusCode")]
    [StringLength(30)]
    [Unicode(false)]
    public string? EstatusCode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? PreStatusCode { get; set; }

    [Column("ManufacturerID")]
    public int? ManufacturerId { get; set; }

    [Column("CommIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CommIp { get; set; }

    public int? CommPort { get; set; }

    public int ConnectAction { get; set; }

    public int IsConnect { get; set; }

    [Column("FWVersion")]
    [StringLength(10)]
    public string? Fwversion { get; set; }

    [Column("FWVersionUpdate")]
    public int? FwversionUpdate { get; set; }

    public int CardReaderType { get; set; }

    public int? SetActive { get; set; }

    [Column("UsePeerIP")]
    public int? UsePeerIp { get; set; }

    public int EnableFlag { get; set; }

    public int AttendanceUse { get; set; }

    public int IsFeverDevice { get; set; }

    [Column("FaceReaderAuthModeID")]
    public int FaceReaderAuthModeId { get; set; }

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

    [ForeignKey("EqCodeId")]
    [InverseProperty("EqMasters")]
    public virtual EqCodeList EqCode { get; set; } = null!;

    [ForeignKey("EqTypeId")]
    [InverseProperty("EqMasters")]
    public virtual EqType? EqType { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("EqMasters")]
    public virtual Location? Location { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("EqMasters")]
    public virtual EqUser Update { get; set; } = null!;
}

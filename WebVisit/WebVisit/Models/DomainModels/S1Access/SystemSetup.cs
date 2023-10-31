using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 시스템 환경설정에 대한 설정값 테이블
/// </summary>
[Keyless]
[Table("SystemSetup")]
public partial class SystemSetup
{
    [Column("SystemSetupID")]
    public int SystemSetupId { get; set; }

    public int? InDoors { get; set; }

    public int? LightSchedule { get; set; }

    public int? CardReaderFunc { get; set; }

    public int? Attendance { get; set; }

    public int? SecurityLocation { get; set; }

    public int? Visit { get; set; }

    public int? AutoRegSubEqMaster { get; set; }

    [Column("CDR0310")]
    public int? Cdr0310 { get; set; }

    public int? PasswordCycle { get; set; }

    [Column("VisitSabunPW")]
    public int? VisitSabunPw { get; set; }

    public int? DeviceName { get; set; }

    public int? SecurityFunc { get; set; }

    public int? PersonalNumber { get; set; }

    public int? Map3D { get; set; }

    public int? PatternAnalysis { get; set; }

    public int? AccessAuthority { get; set; }

    public int? AlarmProcess { get; set; }

    public int? AlarmMap { get; set; }

    public int? AlarmVideoViewer { get; set; }

    public int? AccessRelease { get; set; }

    public int? AccessReleasePeriod { get; set; }

    public int? AccessReleaseType { get; set; }

    public int? AutoConnectAction { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateIp { get; set; }

    [Column("AutoConnectAction	int	Checked")]
    [StringLength(10)]
    public string? AutoConnectActionIntChecked { get; set; }

    public int? HuenServer { get; set; }

    public int? PointFunc { get; set; }

    [Column("EVServerFunc")]
    public int? EvserverFunc { get; set; }

    public int? MasterPatternFunc { get; set; }

    [Column("FaceReaderAuthTypeIDFunc")]
    public int FaceReaderAuthTypeIdfunc { get; set; }

    public int EffectiveLifePolicyFunc { get; set; }

    public int? LockEqUserFunc { get; set; }

    public int? LockEqUserPeriod { get; set; }

    public int GetFaceEventPicture { get; set; }

    public int? AccessFireInterlock { get; set; }

    /// <summary>
    /// 법정 공휴일 설정 사용/미사용
    /// </summary>
    public int? PublicHolidayFunc { get; set; }

    /// <summary>
    /// BIO리더 부가 기능 - 체온 측정 사용/미사용
    /// </summary>
    public int? UseThermometer { get; set; }

    /// <summary>
    /// BIO리더 부가 기능 - 마스크 모드 사용/미사용
    /// </summary>
    public int? UseMaskMode { get; set; }

    /// <summary>
    /// BIO리더 설정 - 체온 측정 - 경고 알림 온도
    /// </summary>
    public double? TemperatureWarning { get; set; }

    /// <summary>
    /// BIO리더 설정 - 체온 측정 - 출입 불가 온도
    /// </summary>
    public double? TemperatureDeny { get; set; }

    [ForeignKey("UpdateId")]
    public virtual EqUser Update { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("ACCESSEVENT")]
public partial class Accessevent
{
    [Key]
    [Column("AlarmID")]
    public int AlarmId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EventDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RecvDateTime { get; set; }

    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string State { get; set; } = null!;

    [StringLength(4)]
    public string? StateMode { get; set; }

    [StringLength(12)]
    public string? StateCode { get; set; }

    [StringLength(100)]
    public string? StateName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PreState { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    [Column("EqTypeID")]
    public int? EqTypeId { get; set; }

    [StringLength(50)]
    public string? EqTypeName { get; set; }

    [StringLength(50)]
    public string? EqName { get; set; }

    [Column("LocationID")]
    public int? LocationId { get; set; }

    [StringLength(50)]
    public string? LocationName { get; set; }

    [Column("PID")]
    public int? Pid { get; set; }

    [StringLength(50)]
    public string? PersonName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [Column("OrgID")]
    public int? OrgId { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }

    [Column("GradeID")]
    public int? GradeId { get; set; }

    [StringLength(50)]
    public string? GradeName { get; set; }

    public int? EventOperator { get; set; }

    [StringLength(50)]
    public string? Content1 { get; set; }

    [StringLength(50)]
    public string? Content2 { get; set; }

    [StringLength(50)]
    public string? Content3 { get; set; }

    [StringLength(50)]
    public string? Content4 { get; set; }

    [StringLength(100)]
    public string? Reason { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    public double? BodyTemp { get; set; }

    public int? BodyTempState { get; set; }

    [StringLength(30)]
    public string? BodyTempStateName { get; set; }

    public int? MaskState { get; set; }

    [StringLength(30)]
    public string? MaskStateName { get; set; }

    [StringLength(100)]
    public string? ImageInfo { get; set; }

    [StringLength(50)]
    public string? BaseLocationName { get; set; }
}

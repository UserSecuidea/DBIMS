using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("PXMAlarmList")]
public partial class PxmalarmList
{
    [Key]
    [Column("PXMAlarmUID")]
    public int PxmalarmUid { get; set; }

    [StringLength(64)]
    public string Guid { get; set; } = null!;

    [Column("TypeID")]
    public int TypeId { get; set; }

    public int ItemIndex { get; set; }

    [StringLength(64)]
    public string? Name { get; set; }

    [StringLength(64)]
    public string? DisplayName { get; set; }

    [StringLength(64)]
    public string? FieldType { get; set; }

    [Column("AlarmID")]
    public int? AlarmId { get; set; }

    [StringLength(1024)]
    public string? Describe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    public string? UpdateIp { get; set; }
}

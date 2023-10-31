using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("Guid", "TypeId")]
[Table("PXMProxyDescribe")]
public partial class PxmproxyDescribe
{
    [Column("PXMProxyUID")]
    public int PxmproxyUid { get; set; }

    [Key]
    [StringLength(64)]
    public string Guid { get; set; } = null!;

    [Key]
    [Column("TypeID")]
    public int TypeId { get; set; }

    [Column("ParentTypeID")]
    public int ParentTypeId { get; set; }

    public int? ItemIndex { get; set; }

    public int? Depth { get; set; }

    [StringLength(32)]
    public string? DeviceType { get; set; }

    public int? MaxDevice { get; set; }

    [StringLength(64)]
    public string? Name { get; set; }

    [StringLength(64)]
    public string? Version { get; set; }

    [Column("MakeUI")]
    [StringLength(10)]
    public string? MakeUi { get; set; }

    [StringLength(64)]
    public string? DescCompany { get; set; }

    [StringLength(64)]
    public string? DescModel { get; set; }

    [StringLength(64)]
    public string? DescName { get; set; }

    [StringLength(64)]
    public string? DescVersion { get; set; }

    [StringLength(10)]
    public string? DescGroup { get; set; }

    public string? Describe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    public string? UpdateIp { get; set; }
}

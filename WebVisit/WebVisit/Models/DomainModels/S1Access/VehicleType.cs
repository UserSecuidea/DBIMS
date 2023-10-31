using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// 차량 종류 구분(내방객)
/// </summary>
[PrimaryKey("VehicleTypeId", "VehicleTypeNameId")]
[Table("VehicleType")]
public partial class VehicleType
{
    [Key]
    [Column("VehicleTypeID")]
    public int VehicleTypeId { get; set; }

    [Key]
    [Column("VehicleTypeNameID")]
    public int VehicleTypeNameId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int? UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    [ForeignKey("UpdateId")]
    [InverseProperty("VehicleTypes")]
    public virtual EqUser? Update { get; set; }
}

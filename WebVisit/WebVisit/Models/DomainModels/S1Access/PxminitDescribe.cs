using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("PXMInitDescribe")]
public partial class PxminitDescribe
{
    [Column("PXMInitUID")]
    public int PxminitUid { get; set; }

    [Key]
    [StringLength(64)]
    public string Guid { get; set; } = null!;

    public string? Describe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    public string? UpdateIp { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("DBLOCATION")]
public partial class Dblocation
{
    [Key]
    [Column("LOCATION_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LocationCode { get; set; } = null!;

    [Column("LOCATION_NAME")]
    [StringLength(100)]
    public string? LocationName { get; set; }

    [Column("LONG_NAME_KOR")]
    [StringLength(100)]
    public string? LongNameKor { get; set; }

    [Column("LONG_NAME_ENG")]
    [StringLength(100)]
    public string? LongNameEng { get; set; }

    [Column("ASSET_MANAGER")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AssetManager { get; set; }

    [Column("HR_MANAGER")]
    [StringLength(50)]
    [Unicode(false)]
    public string? HrManager { get; set; }

    [Column("PASS_MANAGER")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PassManager { get; set; }

    [Column("SUPPLY_CHAIN_EMAIL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SupplyChainEmail { get; set; }
}

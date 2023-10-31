using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
public partial class ViewDuty
{
    [Column("LOCATION_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string LocationCode { get; set; } = null!;

    [Column("LOCATION_NAME")]
    [StringLength(100)]
    public string? LocationName { get; set; }

    [StringLength(100)]
    public string? Campus { get; set; }

    [Column("SUPPLY_CHAIN_EMAIL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SupplyChainEmail { get; set; }

    [Column("ASSETSabun")]
    [StringLength(20)]
    public string? Assetsabun { get; set; }

    [Column("ASSETName")]
    [StringLength(64)]
    public string? Assetname { get; set; }

    [Column("ASSETDeptName")]
    [StringLength(256)]
    public string? AssetdeptName { get; set; }

    [Column("HRSabun")]
    [StringLength(20)]
    public string? Hrsabun { get; set; }

    [Column("HRName")]
    [StringLength(64)]
    public string? Hrname { get; set; }

    [Column("HRDeptName")]
    [StringLength(256)]
    public string? HrdeptName { get; set; }

    [Column("PASSSabun")]
    [StringLength(20)]
    public string? Passsabun { get; set; }

    [Column("PASSName")]
    [StringLength(64)]
    public string? Passname { get; set; }

    [Column("PASSDeptName")]
    [StringLength(256)]
    public string? PassdeptName { get; set; }
}

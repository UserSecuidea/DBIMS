using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("G_VW_TSR_VENDOR")]
public partial class GVwTsrVendor
{
    [Key]
    [Column("VENDOR_ID", TypeName = "numeric(10, 0)")]
    public decimal VendorId { get; set; }

    [Column("VENDOR_NO")]
    [StringLength(12)]
    public string? VendorNo { get; set; }

    [Column("VENDOR_NAME")]
    [StringLength(80)]
    public string? VendorName { get; set; }

    [Column("BUSINESS_NO")]
    [StringLength(20)]
    public string? BusinessNo { get; set; }

    [Column("CORPORATION_NO")]
    [StringLength(20)]
    public string? CorporationNo { get; set; }

    [Column("REPRESENT_NAME")]
    [StringLength(80)]
    public string? RepresentName { get; set; }

    [Column("BUSINESS_TYPE")]
    [StringLength(60)]
    public string? BusinessType { get; set; }

    [Column("BUSINESS_CLASS")]
    [StringLength(100)]
    public string? BusinessClass { get; set; }

    [Column("ENTRY_EMAIL")]
    [StringLength(50)]
    public string? EntryEmail { get; set; }

    [Column("ENTRY_NAME")]
    [StringLength(80)]
    public string? EntryName { get; set; }

    [Column("ENTRY_TEL")]
    [StringLength(100)]
    public string? EntryTel { get; set; }

    [Column("COUNTRY")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("CHARGER_NAME")]
    [StringLength(80)]
    public string? ChargerName { get; set; }

    [Column("VENDOR_BLOCK")]
    [StringLength(1)]
    public string? VendorBlock { get; set; }

    [Column("REASON_DELETE")]
    [StringLength(400)]
    public string? ReasonDelete { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }

    [Column("INSERTDATE", TypeName = "datetime")]
    public DateTime? Insertdate { get; set; }
}

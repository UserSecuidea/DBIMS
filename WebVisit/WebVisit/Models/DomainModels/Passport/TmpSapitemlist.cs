using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Keyless]
[Table("tmpSAPITEMLIST")]
public partial class TmpSapitemlist
{
    [StringLength(100)]
    public string? Field1 { get; set; }

    [StringLength(100)]
    public string? Field2 { get; set; }

    [StringLength(100)]
    public string? Field3 { get; set; }

    [StringLength(100)]
    public string? Field4 { get; set; }

    [StringLength(100)]
    public string? Field5 { get; set; }

    [StringLength(100)]
    public string? Field6 { get; set; }

    [StringLength(100)]
    public string? Field7 { get; set; }

    [StringLength(100)]
    public string? Field8 { get; set; }

    [StringLength(100)]
    public string? Field9 { get; set; }

    [StringLength(100)]
    public string? Field10 { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR;

[Keyless]
public partial class VMemberIntegration
{
    public long SeqRegistedVehicle { get; set; }

    [Column("VehicleID")]
    [StringLength(20)]
    [Unicode(false)]
    public string VehicleId { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? VehicleType { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CellPhoneNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string MemberName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Memo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? EmployeeNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? MemberGroupName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DepartmentName { get; set; }

    public bool IsFiveCamException { get; set; }

    public bool IsTwoCamException { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
public partial class ViewDevice
{
    [Column("DeviceID")]
    public int DeviceId { get; set; }

    [StringLength(50)]
    public string? DeviceName { get; set; }

    [StringLength(50)]
    public string? LocationName { get; set; }

    [Column("EqTypeID")]
    public int? EqTypeId { get; set; }
}

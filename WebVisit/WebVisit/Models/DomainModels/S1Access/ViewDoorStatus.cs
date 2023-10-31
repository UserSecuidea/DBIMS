using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
public partial class ViewDoorStatus
{
    [StringLength(50)]
    public string? DeviceName { get; set; }

    [StringLength(50)]
    public string? LocationName { get; set; }

    public int OpenStatus { get; set; }
}

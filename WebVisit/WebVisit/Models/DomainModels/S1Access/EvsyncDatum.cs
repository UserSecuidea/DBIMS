using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("EVSyncData")]
public partial class EvsyncDatum
{
    [Column(TypeName = "datetime")]
    public DateTime CardToEqMasterSync { get; set; }

    public int EventSync { get; set; }
}

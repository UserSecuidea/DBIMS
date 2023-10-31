using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
public partial class ViewAccessright
{
    [Column("EqMasterID")]
    public int? EqMasterId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CardNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sabun { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? OrgName { get; set; }
}

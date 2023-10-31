using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("LocationLevel")]
public partial class LocationLevel
{
    [Key]
    [Column("LocationLevelID")]
    public int LocationLevelId { get; set; }

    public int LocationLevelName { get; set; }

    [InverseProperty("LocationLevel")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}

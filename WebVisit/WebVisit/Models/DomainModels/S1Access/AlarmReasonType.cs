using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AlarmReasonType")]
public partial class AlarmReasonType
{
    [Key]
    [Column("AlarmReasonTypeID")]
    public int AlarmReasonTypeId { get; set; }

    [Column("AlarmReasonTypeNameID")]
    public int AlarmReasonTypeNameId { get; set; }

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202211> AlarmProcess202211s { get; set; } = new List<AlarmProcess202211>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202212> AlarmProcess202212s { get; set; } = new List<AlarmProcess202212>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202301> AlarmProcess202301s { get; set; } = new List<AlarmProcess202301>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202302> AlarmProcess202302s { get; set; } = new List<AlarmProcess202302>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202303> AlarmProcess202303s { get; set; } = new List<AlarmProcess202303>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202304> AlarmProcess202304s { get; set; } = new List<AlarmProcess202304>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202305> AlarmProcess202305s { get; set; } = new List<AlarmProcess202305>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess202306> AlarmProcess202306s { get; set; } = new List<AlarmProcess202306>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcessBak> AlarmProcessBaks { get; set; } = new List<AlarmProcessBak>();

    [InverseProperty("AlarmReasonType")]
    public virtual ICollection<AlarmProcess> AlarmProcesses { get; set; } = new List<AlarmProcess>();
}

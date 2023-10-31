using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AgentConf")]
public partial class AgentConf
{
    [Key]
    [StringLength(50)]
    public string AgentConfName { get; set; } = null!;

    [StringLength(50)]
    public string? AgentConfValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AgentConfDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

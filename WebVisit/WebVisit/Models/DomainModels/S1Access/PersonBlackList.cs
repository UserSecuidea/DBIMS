using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("PersonBlackList")]
public partial class PersonBlackList
{
    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    public int BlackListFlag { get; set; }
}

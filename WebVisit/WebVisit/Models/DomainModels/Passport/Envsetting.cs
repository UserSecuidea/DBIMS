using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport;

[Table("ENVSETTINGS")]
public partial class Envsetting
{
    [Key]
    [Column("SETTING_NAME")]
    [StringLength(50)]
    public string SettingName { get; set; } = null!;

    [Column("SETTING_VALUE")]
    [StringLength(100)]
    public string? SettingValue { get; set; }
}

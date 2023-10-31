using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AuthServer")]
public partial class AuthServer
{
    [Key]
    [StringLength(30)]
    public string ContractNo { get; set; } = null!;

    [StringLength(32)]
    [Unicode(false)]
    public string? LocalKey { get; set; }

    [Column("SWType")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Swtype { get; set; }

    public int IsAuth { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? MobileKey { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? MifareKey { get; set; }

    [StringLength(30)]
    public string? MobileContractNo { get; set; }
}

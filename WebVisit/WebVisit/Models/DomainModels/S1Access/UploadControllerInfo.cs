using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("EqMasterId", "CardNo")]
[Table("UploadControllerInfo")]
public partial class UploadControllerInfo
{
    [Column("UploadID")]
    public int UploadId { get; set; }

    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Column("EqTypeID")]
    public int EqTypeId { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string CardNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string UserCardNo { get; set; } = null!;

    [Column("CardTypeID")]
    public int CardTypeId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string IsSecurity { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string IsRelease { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string IsAccess { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string IsMaster { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string AccessPattern { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ValidDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }
}

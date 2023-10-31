using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("UploadProgressInfo")]
public partial class UploadProgressInfo
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    [Column("EqCodeID")]
    public int EqCodeId { get; set; }

    [Column("EqTypeID")]
    public int EqTypeId { get; set; }

    public int UploadState { get; set; }

    public int TotalCount { get; set; }

    public int CurrentCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public int? UploadStateTotal { get; set; }

    public int? UploadStateIndex { get; set; }

    [StringLength(64)]
    public string? ErrorMessage { get; set; }
}

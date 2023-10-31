using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[PrimaryKey("CardId", "FaceReaderTypeId")]
[Table("FaceReaderTemplate")]
public partial class FaceReaderTemplate
{
    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Key]
    [Column("FaceReaderTypeID")]
    public int FaceReaderTypeId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ImageFormat { get; set; }

    public byte[]? ImageData { get; set; }

    public int? FaceTemplateCount { get; set; }

    public int IsImage { get; set; }

    public byte[]? FaceTemplate1 { get; set; }

    public byte[]? FaceTemplate2 { get; set; }

    public byte[]? FaceTemplate3 { get; set; }

    public byte[]? FaceTemplate4 { get; set; }

    public byte[]? FaceTemplate5 { get; set; }

    public byte[]? FaceTemplate6 { get; set; }

    public byte[]? FaceTemplate7 { get; set; }

    [Column("UpdateEqMasterID")]
    public int? UpdateEqMasterId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;
}

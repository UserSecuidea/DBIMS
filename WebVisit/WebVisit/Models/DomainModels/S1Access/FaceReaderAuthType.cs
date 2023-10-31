using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("FaceReaderAuthType")]
public partial class FaceReaderAuthType
{
    [Key]
    [Column("FaceReaderAuthTypeID")]
    public int FaceReaderAuthTypeId { get; set; }

    [Column("FaceReaderAuthTypeNameID")]
    public int FaceReaderAuthTypeNameId { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("FaceReaderAuthMode")]
public partial class FaceReaderAuthMode
{
    [Column("FaceReaderAuthModeID")]
    public int FaceReaderAuthModeId { get; set; }

    [Column("FaceReaderAuthModeNameID")]
    public int FaceReaderAuthModeNameId { get; set; }
}

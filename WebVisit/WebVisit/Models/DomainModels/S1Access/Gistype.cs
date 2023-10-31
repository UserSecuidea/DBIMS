using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

/// <summary>
/// GIS 맵 종류를 정의 한다.
/// </summary>
[Table("GISType")]
public partial class Gistype
{
    [Key]
    [Column("GISTypeID")]
    public int GistypeId { get; set; }

    [Column("GISTypeNameID")]
    public int GistypeNameId { get; set; }

    public int EnableFlag { get; set; }
}

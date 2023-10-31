using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("FaceEqMaster")]
public partial class FaceEqMaster
{
    [Key]
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    public int EnableFlag { get; set; }

    public int UseServer { get; set; }

    public int CommErrAction { get; set; }

    public int AuthType { get; set; }

    public int AuthTimeout { get; set; }

    public int InOutAuthReader { get; set; }

    [Column("IP")]
    [StringLength(15)]
    [Unicode(false)]
    public string Ip { get; set; } = null!;

    public int DownFlag { get; set; }

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

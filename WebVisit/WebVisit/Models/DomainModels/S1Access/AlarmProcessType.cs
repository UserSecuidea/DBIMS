using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AlarmProcessType")]
public partial class AlarmProcessType
{
    [Key]
    [Column("AlarmProcessTypeID")]
    public int AlarmProcessTypeId { get; set; }

    [Column("AlarmProcessTypeNameID")]
    public int AlarmProcessTypeNameId { get; set; }

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

    [ForeignKey("UpdateId")]
    [InverseProperty("AlarmProcessTypes")]
    public virtual EqUser Update { get; set; } = null!;
}

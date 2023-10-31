using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Keyless]
[Table("SystemLevel")]
public partial class SystemLevel
{
    public int VisitorSystem { get; set; }

    [Column("LPRSystem")]
    public int Lprsystem { get; set; }

    public int ImageAnalysisServer { get; set; }

    public int ImageController { get; set; }

    public int OpticalSensor { get; set; }

    public int ContactPointController { get; set; }

    [Column("MAP3D")]
    public int Map3d { get; set; }

    public int Elevator { get; set; }

    [Column("GIS")]
    public int Gis { get; set; }

    public int? AutoConnectAction { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR;

public class PassedVehicleLog
{
    public string PassedTime { get; set; } = string.Empty;
    public string VehiclePurpose { get; set; } = string.Empty;

    public int CountVisitorEntry { get; set; } = 0; // 방문객 입구
    public int CountVisitorExit { get; set; } = 0; // 방문객 출구
    

    public int Count1Entry { get; set; } = 0; // 부천 정문 입구
    public int Count1Exit { get; set; } = 0; // 부천 정문 출구

    public int Count2Entry { get; set; } = 0; // 상우 정문 입구
    public int Count2Exit { get; set; } = 0; // 상우 정문 출구
}

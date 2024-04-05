using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class AverageInspectionPerDayV
{
    public int LocationIdent { get; set; }

    public string LocationName { get; set; } = null!;

    public int? TotalInspections { get; set; }

    public int? TotalDays { get; set; }

    public double? AverageInspectionProDay { get; set; }
}

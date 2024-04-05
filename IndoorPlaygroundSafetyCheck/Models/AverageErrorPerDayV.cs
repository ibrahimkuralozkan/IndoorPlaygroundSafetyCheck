using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class AverageErrorPerDayV
{
    public string StationName { get; set; } = null!;

    public int StationIdent { get; set; }

    public int? TotalDays { get; set; }

    public int? Error1Count { get; set; }

    public int? Error2Count { get; set; }

    public int? Error3Count { get; set; }

    public decimal? Error3PerDay { get; set; }

    public decimal? AllErrorsPerDay { get; set; }
}

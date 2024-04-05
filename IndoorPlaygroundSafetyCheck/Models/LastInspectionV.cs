using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class LastInspectionV
{
    public string StationName { get; set; } = null!;

    public int StationIdent { get; set; }

    public string LocationName { get; set; } = null!;

    public string LastInspection { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class AverageInspectionTimeV
{
    public string Station { get; set; } = null!;

    public int? AverageInspectionTime { get; set; }
}

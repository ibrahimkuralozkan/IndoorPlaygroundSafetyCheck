using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class TodaysAllInspectionsV
{
    public int InspectionIdent { get; set; }

    public int LocationIdent { get; set; }

    public string LocationName { get; set; } = null!;

    public int StationIdent { get; set; }

    public string StationName { get; set; } = null!;

    public DateTime? CheckStart { get; set; }

    public DateTime? CheckDone { get; set; }

    public int? CardIdent { get; set; }

    public int? ErrorType { get; set; }
}

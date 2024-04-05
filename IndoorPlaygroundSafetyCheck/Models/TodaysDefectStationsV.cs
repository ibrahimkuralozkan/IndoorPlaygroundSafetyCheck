using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class TodaysDefectStationsV
{
    public int LocationIdent { get; set; }

    public string LocationName { get; set; } = null!;

    public int StationIdent { get; set; }

    public string StationName { get; set; } = null!;

    public DateTime? CheckStart { get; set; }

    public DateTime? CheckDone { get; set; }

    public int? CardIdent { get; set; }

    public int QuestionIdent { get; set; }

    public string QuestionText { get; set; } = null!;

    public int? ErrorType { get; set; }

    public string? Notes { get; set; }
}

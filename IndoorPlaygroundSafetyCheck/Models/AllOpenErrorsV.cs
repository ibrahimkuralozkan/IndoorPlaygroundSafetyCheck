using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class AllOpenErrorsV
{
    public int? InspectionIdent { get; set; }

    public int? LocationIdent { get; set; }

    public int? CardIdent { get; set; }

    public string? StationName { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? AnswerNotes { get; set; }

    public DateTime? CheckStart { get; set; }

    public DateTime? CheckDone { get; set; }
}

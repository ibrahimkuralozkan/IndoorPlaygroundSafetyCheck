﻿using System;
using System.Collections.Generic;


namespace IndoorPlaygroundSafetyCheck.Models
{
    public partial class InspectionQuestionResult
    {
        public int Ident { get; set; }
        public int InspectionIdent { get; set; }
        public int StationIdent { get; set; }
        public int StationQuestionIdent { get; set; }
        public string QuestionText { get; set; } = null!;
        public int? ErrorType { get; set; }
        public string? Notes { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; } = null!;
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdateTimeStamp { get; set; }

        // Add the new properties here
        public DateTime? RepairPlan { get; set; } // Nullable, assuming the repair plan date can be null
        public DateTime? RepairedTime { get; set; } // Nullable, assuming the repaired time can be null
        public virtual Station Station { get; set; } = null!;
        public virtual Inspection InspectionIdentNavigation { get; set; } = null!;
        public virtual StationQuestion StationQuestionIdentNavigation { get; set; } = null!;
    }
}

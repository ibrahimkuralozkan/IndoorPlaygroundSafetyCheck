using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // Add this using directive
using IndoorPlaygroundSafetyCheck.Enums;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public partial class Inspection
    {
        public int Ident { get; set; }
        public int EmployeeIdent { get; set; }
        public DateTime? CheckStart { get; set; }
        public DateTime? CheckDone { get; set; }
        public int? Type { get; set; }
        public byte[]? Signature { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; } = null!;
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdateTimeStamp { get; set; }
        public string? RfidUid { get; set; }
        public int IsSent { get; set; }

        [NotMapped] // This tells EF to ignore the property
        public InspectionType InspectionType
        {
            get => (InspectionType)(Type ?? 1); // Default to DailyInspection if null
            set => Type = (int)value;
        }

        public virtual ICollection<InspectionQuestionResult> InspectionQuestionResults { get; set; } = new List<InspectionQuestionResult>();
    }
}

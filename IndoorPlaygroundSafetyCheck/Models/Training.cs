using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;



namespace IndoorPlaygroundSafetyCheck.Models
{
    public class Training
    {
        public int Ident { get; set; }
        public int StationIdent { get; set; }
        public string? CorrectSetupImagePath { get; set; }
        public string? CommonErrorsImagePath { get; set; }
        public string? Description { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateTimeStamp { get; set; }
        public string? VideoPath { get; set; }
        public virtual Station StationIdentNavigation { get; set; }
    }

}

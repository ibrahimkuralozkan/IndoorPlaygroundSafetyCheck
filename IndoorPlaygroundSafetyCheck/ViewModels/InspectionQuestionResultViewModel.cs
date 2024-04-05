using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class InspectionQuestionResultViewModel
    {
        
            public int Ident { get; set; }
            public int InspectionIdent { get; set; }
            public int StationQuestionIdent { get; set; }
            public string QuestionText { get; set; }
            public int? ErrorType { get; set; }
            public string Notes { get; set; }
            public DateTime InsertTimeStamp { get; set; }
            public string InsertedBy { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdateTimeStamp { get; set; }
            public int StationIdent { get; set; }
        public string StationName { get; set; }
    }
    
}

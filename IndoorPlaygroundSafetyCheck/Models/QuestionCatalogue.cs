using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class QuestionCatalogue
{
    public int Ident { get; set; }
    public string Description { get; set; }
    public DateTime InsertTimeStamp { get; set; }
    public string InsertedBy { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdateTimeStamp { get; set; }
    public virtual ICollection<StationQuestion> StationQuestions { get; set; } = new List<StationQuestion>();
}

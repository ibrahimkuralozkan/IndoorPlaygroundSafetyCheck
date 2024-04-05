using System;
using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models;

public partial class Station
{
    public Station()
    {
        StationQuestions = new HashSet<StationQuestion>();
        Training = new HashSet<Training>();
        Repairs = new HashSet<Repair>(); // Initialize the Repairs collection
    }

    public int Ident { get; set; }
    public string Name { get; set; } = null!;
    // Remove the int Repairs property since it's incorrect
    public DateTime InsertTimeStamp { get; set; }
    public string InsertedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdateTimeStamp { get; set; }
   

    // Navigation properties
    public virtual ICollection<StationQuestion> StationQuestions { get; set; }
    public virtual ICollection<Training> Training { get; set; }
    public virtual ICollection<Repair> Repairs { get; set; } // This correctly defines the relationship
}

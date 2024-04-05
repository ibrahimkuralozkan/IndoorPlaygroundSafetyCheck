using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndoorPlaygroundSafetyCheck.Models;


namespace IndoorPlaygroundSafetyCheck.Models
{
    public class Repair
    {
        public int Ident { get; set; }
        public int StationIdent { get; set; }
        public int InspectionIdent { get; set; }
        public string? Notes { get; set; }
        public DateTime? RepairDatePlan { get; set; }
        public int? IsRepaired { get; set; } // Consider using bool for clarity
        public DateTime? RepairedDate { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; } = string.Empty;
        public DateTime UpdateTimeStamp { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;

        // Navigation property for related Station
        public virtual Station? Station { get; set; }
    }

    

}

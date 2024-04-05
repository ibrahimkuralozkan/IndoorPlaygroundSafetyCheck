using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndoorPlaygroundSafetyCheck.Enums
{
    public enum InspectionType
    {
        DailyInspection = 1, // Corresponds to "1 Daily Inspection"
        PopUpInspection,     // Automatically gets the value 2
        AfterRepairInspection // Automatically gets the value 3
    }
}

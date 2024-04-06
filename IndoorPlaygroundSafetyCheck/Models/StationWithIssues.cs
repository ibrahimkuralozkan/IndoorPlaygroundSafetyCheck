using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public class StationWithIssues
    {
        public string Name { get; set; }
        public List<Question> StationQuestions { get; set; }

        // Add other properties that represent the issues or data related to the station.
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public class Position
    {
        public int Ident { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdateTimeStamp { get; set; }
        public string UpdatedBy { get; set; }
        public string AuthenticationCode { get; set; }
    }
}

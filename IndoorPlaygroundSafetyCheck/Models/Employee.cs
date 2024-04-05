using System;
using System.Collections.Generic;



namespace IndoorPlaygroundSafetyCheck.Models
{
    public partial class Employee
    {
        public int Ident { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Position { get; set; }
        public string RfidUid { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdateTimeStamp { get; set; }
        public string UpdatedBy { get; set; }
       
       
            public string FullName => $"{FirstName}{LastName}";
        

    }
}

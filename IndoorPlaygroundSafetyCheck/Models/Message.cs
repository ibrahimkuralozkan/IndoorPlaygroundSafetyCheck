using System;

namespace IndoorPlaygroundSafetyCheck.Models
{
    public partial class Message
    {
        public int Ident { get; set; }
        public int InspectionIdent { get; set; }
        public int InspectionQuestionResultIdent { get; set; }
        public int SenderIdent { get; set; }
        public int ReceiverIdent { get; set; }
        public byte[] PictureData { get; set; }
        public string Notes { get; set; }
        public DateTime InsertTimeStamp { get; set; }
        public string InsertedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateTimeStamp { get; set; }
        public bool IsRead { get; set; }
    }
}

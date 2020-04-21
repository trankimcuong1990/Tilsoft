using System;

namespace Module.ClientComplaint.DTO
{
    public class ClientComplaintCommunication
    {
        public int ClientComplaintCommunicationID { get; set; }
        public int ClientComplaintID { get; set; }
        public string MessageText { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
     
        public string CreatorFullName { get; set; }
        public int? CreatorID { get; set; }
        
    }
}

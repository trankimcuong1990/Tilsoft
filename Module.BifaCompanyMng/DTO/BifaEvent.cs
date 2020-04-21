namespace Module.BifaCompanyMng.DTO
{
    public class BifaEvent
    {
        public int BifaEventID { get; set; }
        public string BifaEventUD { get; set; }
        public string BifaEventNM { get; set; }
        public int? BifaCompanyID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }

        public System.Collections.Generic.List<BifaEventFile> BifaEventFiles { get; set; }
        public System.Collections.Generic.List<BifaEventParticipant> BifaEventParticipants { get; set; }

        public BifaEvent()
        {
            BifaEventFiles = new System.Collections.Generic.List<BifaEventFile>();
            BifaEventParticipants = new System.Collections.Generic.List<BifaEventParticipant>();
        }
    }
}

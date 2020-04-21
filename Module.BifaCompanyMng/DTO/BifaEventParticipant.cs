namespace Module.BifaCompanyMng.DTO
{
    public class BifaEventParticipant
    {
        public int BifaEventParticipantID { get; set; }
        public int? BifaCompanyID { get; set; }
        public int? BifaEventID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
    }
}

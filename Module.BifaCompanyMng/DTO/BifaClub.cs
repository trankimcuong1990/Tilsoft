namespace Module.BifaCompanyMng.DTO
{
    public class BifaClub
    {
        public int BifaClubID { get; set; }

        public string BifaClubUD { get; set; }
        public string BifaClubNM { get; set; }
        public string Description { get; set; }
        public string EmployeeNM { get; set; }

        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}

namespace Module.BifaCompanyMng.DTO
{
    public class BifaClubMember
    {
        public int BifaClubMemberID { get; set; }

        public int? BifaClubID { get; set; }
        public int? BifaCompanyID { get; set; }
        public int? UpdatedBy { get; set; }

        public string BifaClubUD { get; set; }
        public string BifaClubNM { get; set; }
        public string JoinedDate { get; set; }
        public string Remark { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
    }
}

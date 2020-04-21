namespace Module.BifaCompanyMng.DTO
{
    public class BifaEmailAddress
    {
        public int BifaEmailAddressID { get; set; }

        public int? BifaCompanyID { get; set; }
        public int? BifaPersonID { get; set; }
        public int? UpdatedBy { get; set; }

        public string EmailAddress { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
    }
}

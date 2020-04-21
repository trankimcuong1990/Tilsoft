namespace Module.BifaCompanyMng.DTO
{
    public class BifaAddress
    {
        public int BifaAddressID { get; set; }
        public int? BifaCompanyID { get; set; }
        public int? BifaCityID { get; set; }
        public int? UpdatedBy { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Remark { get; set; }
        public string BifaCityNM { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
    }
}

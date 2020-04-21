namespace Module.BifaCompanyMng.DTO
{
    public class BifaTelephone
    {
        public int BifaTelephoneID { get; set; }
        public int? BifaPersonID { get; set; }
        public int? BifaCompanyID { get; set; }
        public int? PhoneTypeID { get; set; }
        public string PhoneTypeNM { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExtension { get; set; }
        public string Remark { get; set; }
        public bool? IsPrimary { get; set; }
        public int? UpdatedBy { get; set; }
        public string EmployeeNM { get; set; }
        public string UpdatedDate { get; set; }

        public BifaTelephone()
        {
            IsPrimary = false;
        }
    }
}

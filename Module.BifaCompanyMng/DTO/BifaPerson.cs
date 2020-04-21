namespace Module.BifaCompanyMng.DTO
{
    public class BifaPerson
    {
        public int BifaPersonID { get; set; }
        public string FullName { get; set; }
        public string CallingName { get; set; }
        public string DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int? BifaCompanyID { get; set; }
        public int? PositionGroupID { get; set; }
        public string PositionGroupNM { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string EmployeeNM { get; set; }
        public string UpdatedDate { get; set; }

        public System.Collections.Generic.List<BifaEmailAddress> BifaEmailAddresses { get; set; }
        public System.Collections.Generic.List<BifaTelephone> BifaTelephones { get; set; }

        public BifaPerson()
        {
            BifaEmailAddresses = new System.Collections.Generic.List<BifaEmailAddress>();
            BifaTelephones = new System.Collections.Generic.List<BifaTelephone>();
        }
    }
}

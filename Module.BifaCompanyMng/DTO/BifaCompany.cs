namespace Module.BifaCompanyMng.DTO
{
    public class BifaCompany
    {
        public int BifaCompanyID { get; set; }
        public int? BifaIndustryID { get; set; }
        public int? BifaCityID { get; set; }
        public int? NumberOfEmployee { get; set; }
        public int? UpdatedBy { get; set; }

        public string BifaCompanyUD { get; set; }
        public string BifaCompanyNM { get; set; }
        public string Logo { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string Logo_DisplayUrl { get; set; }
        public string Logo_NewFile { get; set; }
        public string TaxCode { get; set; }
        public string FoundingDate { get; set; }
        public string BifaIndustryNM { get; set; }
        public string BifaCityNM { get; set; }
        public string JoinedDate { get; set; }
        public string Remark { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }

        public bool Logo_HasChange { get; set; }

        public decimal? Acreage { get; set; }
        public bool? IsActive { get; set; }

        public System.Collections.Generic.List<BifaAddress> BifaAddresses { get; set; }
        public System.Collections.Generic.List<BifaClubMember> BifaClubMembers { get; set; }
        public System.Collections.Generic.List<BifaEmailAddress> BifaEmailAddresses { get; set; }
        public System.Collections.Generic.List<BifaPerson> BifaPersons { get; set; }
        public System.Collections.Generic.List<BifaTelephone> BifaTelephones { get; set; }
        public System.Collections.Generic.List<BifaEvent> BifaEvents { get; set; }
        public System.Collections.Generic.List<BifaEventParticipant> BifaEventParticipants { get; set; }

        public BifaCompany()
        {
            IsActive = true;

            BifaAddresses = new System.Collections.Generic.List<BifaAddress>();
            BifaClubMembers = new System.Collections.Generic.List<BifaClubMember>();
            BifaEmailAddresses = new System.Collections.Generic.List<BifaEmailAddress>();
            BifaPersons = new System.Collections.Generic.List<BifaPerson>();
            BifaTelephones = new System.Collections.Generic.List<BifaTelephone>();
            BifaEventParticipants = new System.Collections.Generic.List<BifaEventParticipant>();
            BifaEvents = new System.Collections.Generic.List<BifaEvent>();
        }
    }
}

namespace Module.BifaCompanyMng.DTO
{
    public class InitFormData
    {
        public System.Collections.Generic.List<BifaCity> BifaCities { get; set; }
        public System.Collections.Generic.List<BifaIndustry> BifaIndustries { get; set; }
        public System.Collections.Generic.List<BifaClub> BifaClubs { get; set; }
        public System.Collections.Generic.List<BifaPhoneType> BifaPhoneTypes { get; set; }
        public System.Collections.Generic.List<BifaPositionGroup> BifaPositionGroups { get; set; }

        public InitFormData()
        {
            BifaCities = new System.Collections.Generic.List<BifaCity>();
            BifaIndustries = new System.Collections.Generic.List<BifaIndustry>();
            BifaClubs = new System.Collections.Generic.List<BifaClub>();
            BifaPhoneTypes = new System.Collections.Generic.List<BifaPhoneType>();
            BifaPositionGroups = new System.Collections.Generic.List<BifaPositionGroup>();
        }
    }
}

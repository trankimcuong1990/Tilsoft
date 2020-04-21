namespace Module.FactoryQuotation2Mng.DTO
{
    public class FactoryDTO
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string Email { get; set; }
        public int TotalPendingItem { get; set; }
        public int TotalItem
        {
            get
            {
                return this.OverDue1Day + this.OverDue2Day + this.OverDue3Day + this.OverDue4DayOrMore;
            }
        }
        public int OverDue1Day { get; set; }
        public int OverDue2Day { get; set; }
        public int OverDue3Day { get; set; }
        public int OverDue4DayOrMore { get; set; }
        public int? PricingTeamMemberID { get; set; }
        public string PricingTeamMemberNM { get; set; }


        public int OverLDS { get; set; }
        public int LDS { get; set; }
        public int OneToTwoDays { get; set; }
        public int ThreeToFourDays { get; set; }
        public int FiveToSixDays { get; set; }
        public int MoreThanSixDays { get; set; }
    }
}

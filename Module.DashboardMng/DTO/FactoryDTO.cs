namespace Module.DashboardMng.DTO
{
    public class FactoryDTO
    {
        public string Season { get; set; }
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string Email { get; set; }
        public int TotalItem
        {
            get
            {
                return this.LessThan1Week + this.From1To2Week + this.From2To3Week + this.Over3Week;
            }
        }
        public int LessThan1Week { get; set; }
        public int From1To2Week { get; set; }
        public int From2To3Week { get; set; }
        public int Over3Week { get; set; }
    }
}

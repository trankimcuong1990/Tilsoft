using System;

namespace Module.FactoryQuotation2Mng.DTO
{
    public class WaitForFactoryConclusionDTO
    {
        public Nullable<long> RowNumber { get; set; }
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public int TotalWaitedWeeks { get; set; }
        public int TotalItem { get; set; }
        public int TotalPendingItem { get; set; }
        public int? PricingTeamMemberID { get; set; }
        public string PricingTeamMemberNM { get; set; }
    }
}

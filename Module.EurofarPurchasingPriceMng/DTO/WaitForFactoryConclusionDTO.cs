using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class WaitForFactoryConclusionDTO
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public int TotalWaitedWeeks { get; set; }
        public int TotalItem { get; set; }
        public int TotalPendingItem { get; set; }
        public int? PricingTeamMemberID { get; set; }
        public string PricingTeamMemberNM { get; set; }
    }
}

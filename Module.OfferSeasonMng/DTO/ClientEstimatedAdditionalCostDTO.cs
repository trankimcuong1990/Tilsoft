using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class ClientEstimatedAdditionalCostDTO
    {
        public int ClientEstimatedAdditionalCostID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Season { get; set; }
        public Nullable<decimal> InterestCostPercent { get; set; }
        public Nullable<decimal> LCCostPercent { get; set; }
        public Nullable<decimal> BonusPercent { get; set; }
        public Nullable<decimal> InspectionCostUSD { get; set; }
        public Nullable<decimal> SampleCostUSD { get; set; }
        public Nullable<decimal> SparepartCostUSD { get; set; }
    }
}

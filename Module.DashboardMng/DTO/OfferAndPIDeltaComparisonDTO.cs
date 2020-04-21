using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class OfferAndPIDeltaComparisonDTO
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }

        public decimal? TotalPurchaseLastYear { get; set; }
        public decimal? TotalMarginLastYear { get; set; }
        public decimal? Delta7Percent { get; set; }

        public decimal? OfferTotalPurchaseLastYear { get; set; }
        public decimal? OfferTotalMarginLastYear { get; set; }
        public decimal? OfferDelta7Percent { get; set; }
    }
}

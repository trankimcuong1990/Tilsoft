using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Offer
{
    public class OfferMarginDetailDTO
    {
        public long PrimaryID { get; set; }
        public int? OfferID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Qnt40HC { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? AVTPurchasingPrice { get; set; }
        public decimal? PercentMargin { get; set; }
        public decimal? SalePrice { get; set; }
        public int? SaleUnit { get; set; }
        public decimal? SaleAmount { get; set; }
        public decimal? PercentSaleMargin { get; set; }
        public int? ModelID { get; set; }
        public string ProductFileLocation { get; set; }
        public string ProductThumbnailLocation { get; set; }
        public decimal? LastAVTPurchasingPrice { get; set; }
        public decimal? LastUnitPrice { get; set; }
        public int? AVTPurchasingPriceStatusID { get; set; }

        public decimal? LCCostAmountUSD { get; set; }
        public decimal? InterestAmountUSD { get; set; }
        public decimal? TransportationAmountUSD { get; set; }
        public decimal? DamageAmountUSD { get; set; }
        public decimal? CommissionAmountUSD { get; set; }
        public decimal? TotalMargin { get; set; }

        public decimal? OfferAmount { get; set; }
        public decimal? FactoryAmount { get; set; }

        public string Currency { get; set; }
        public string ExRate { get; set; }
        public decimal? UnitPriceToUSD { get; set; }
        public int? OfferLineID { get; set; }

        public decimal? PercentSingleMargin { get; set; }
        public decimal? SingleMargin { get; set; }
    }
}

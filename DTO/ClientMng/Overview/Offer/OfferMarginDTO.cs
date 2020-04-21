using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Offer
{
    public class OfferMarginDTO
    {
        //public long PrimaryID { get; set; }
        public int OfferID { get; set; }
        public int? TotalQnt { get; set; }
        public int? ClientID { get; set; }
        public decimal? PercentMargin { get; set; }
        public int? TotalSaleUnit { get; set; }
        public decimal? TotalSaleAmount { get; set; }
        public decimal? PercentSaleMargin { get; set; }
        public string OfferUD { get; set; }
        public string Season { get; set; }
        public string CreatedDate { get; set; }
        public decimal? TotalFactorySaleAmount { get; set; }
        public decimal? TotalCont { get; set; }
        public int? Status { get; set; }
        public int? TotalItem { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalFactoryAmount { get; set; }
        public bool Isloaded { get; set; }
        public decimal? TotalMargin { get; set; }

        public decimal? TransportationAmountUSD { get; set; }
        public decimal? LCCostAmountUSD { get; set; }
        public decimal? InterestAmountUSD { get; set; }
        public decimal? CommissionAmountUSD { get; set; }
        public decimal? DamageAmountUSD { get; set; }
        public bool? IsPotential { get; set; }
        public bool IsSelected { get; set; }

        public decimal? SingleMargin { get; set; }
        public decimal? PercentSingleMargin { get; set; }
        public string Currency { get; set; }

        public List<OfferMarginDetailDTO> offerMarginDetailDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonQuotationRequestMng.DTO
{
    public class OfferSeasonQuotationRequestSearchResultDTO
    {
        public int OfferSeasonQuotationRequestID { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string OfferSeasonUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int? TotalFactoryQuotation { get; set; }

        public Nullable<decimal> PurchasingPrice { get; set; }
        public string QuotationStatusNM { get; set; }
        public Nullable<decimal> SalePriceInUSD { get; set; }
        public Nullable<decimal> CommissionInUSD { get; set; }
        public Nullable<decimal> LCCostInUSD { get; set; }
        public Nullable<decimal> InterestInUSD { get; set; }
        public Nullable<decimal> DamagesCostInUSD { get; set; }
        public Nullable<decimal> TransportationInUSD { get; set; }
        public decimal? Delta5 { get; set; }
        public decimal? Delta5Percent { get; set; }
        //public Nullable<decimal> Delta5
        //{
        //    get {
        //        return SalePriceInUSD - PurchasingPrice - CommissionInUSD - LCCostInUSD - InterestInUSD - DamagesCostInUSD - TransportationInUSD;
        //    }
        //}
        //public Nullable<decimal> Delta5Percent
        //{
        //    get {
        //        if (PurchasingPrice != 0)
        //        {
        //            return Delta5 * 100 / PurchasingPrice;
        //        }
        //        else {
        //            return 0;
        //        }
        //    }
        //}
    }
}

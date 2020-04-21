using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class ItemDeltaNeedAttentionDTO
    {
        public int? FactoryOrderDetailID { get; set; }
        public int? SaleOrderDetailID { get; set; }
        public int? OfferSeasonDetailID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string OfferUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public int? PIStatus { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public decimal? SalePrice { get; set; }
        public string Currency { get; set; }
        public string FactoryUD { get; set; }
        public decimal? PurchasingPriceUSD { get; set; }
        public decimal? LCCostUSD { get; set; }
        public decimal? InterestUSD { get; set; }
        public decimal? CommissionUSD { get; set; }
        public decimal? DamagesCost { get; set; }
        public decimal? TransportationUSD { get; set; }
        public decimal? Delta5 { get; set; }
        public decimal? Delta5Percent { get; set; }
        public string Season { get; set; }
        public int OfferID { get; set; }
        public int OfferSeasonID { get; set; }
        public int ClientID { get; set; }
    }
}

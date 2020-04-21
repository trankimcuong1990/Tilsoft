using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Delta
{
    public class EurofarPriceOverviewDTO
    {
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? OrderQnt { get; set; }
        public string OrderTypeNM { get; set; }
        public int? ShippedQnt { get; set; }
        public int? LoadedQnt { get; set; }
        public string MaterialNM { get; set; }
        public string ProductionStatus { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string QuotationUD { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string LocationNM { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public string QuotationStatusNM { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string Remark { get; set; }
        public string Currency { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Transportation { get; set; }
        public decimal? Commission { get; set; }
        public int? ClientID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? OfferID { get; set; }
        public string Season { get; set; }
        public decimal? LCCostAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? DamagesCost { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? GrossMarginAfterInUSD { get; set; }
    }
}

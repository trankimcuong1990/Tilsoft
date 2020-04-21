using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingPriceOverview2Rpt.DTO
{
    public class PurchasingPriceOverview
    {
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string VNMonitorName { get; set; }
        public string NLMonitorName { get; set; }
        public string AgentNM { get; set; }
        public string SaleNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? Qnt40HC { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public int? LoadedQnt { get; set; }
        public decimal? LoadedQnt40HC { get; set; }
        public int? ShippedQnt { get; set; }
        public decimal? ShippedQnt40HC { get; set; }
        public int? ToBeShippedQnt { get; set; }
        public decimal? ToBeShippedQnt40HC { get; set; }
        public string MaterialNM { get; set; }
        public string ProductionStatus { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string SC { get; set; }
        public string QuotationUD { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string City { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? MinSalePriceInThePast2Season { get; set; }
        public decimal? MaxSalePriceInThePast2Season { get; set; }
        public decimal? MinSalePriceInTheLastSeason { get; set; }
        
        public decimal? MaxSalePriceInTheLastSeason { get; set; }
        public decimal? PurchasingInvoicePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public decimal? BreakDownPrice { get; set; }
        public string QuotationStatusNM { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string FactoryProformaInvoiceNo { get; set; }
        public string Remark { get; set; }
        public string Season { get; set; }
        public int? FactoryID { get; set; }
        public int? ClientID { get; set; }
        public int? ItemType { get; set; }
    }
}

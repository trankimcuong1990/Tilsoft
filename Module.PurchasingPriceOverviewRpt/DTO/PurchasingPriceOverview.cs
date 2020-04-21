using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingPriceOverviewRpt.DTO
{
    public class PurchasingPriceOverview
    {
        public int? QuotationDetailID { get; set; }
        public int? QuotationID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PriceDifferenceRate { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? BreakDownPrice { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public string Remark { get; set; }
        public int? StatusID { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string Season { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string QuotationStatusNM { get; set; }
        public string UpdatorName { get; set; }
        public int UpdatorID { get; set; }
        public int? OrderQnt { get; set; }
        public string LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}

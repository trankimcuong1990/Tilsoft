using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductOverviewRpt.DTO
{
    public class PriceOverviewDTO
    {
        public int FactoryOrderDetailID { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ShowImage { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? OrderQnt { get; set; }
        public string QuotationStatusNM { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public string PackagingMethodNM { get; set; }
        public int? FactoryID { get; set; }
    }
}

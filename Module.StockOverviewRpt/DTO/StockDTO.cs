using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockOverviewRpt.DTO
{
    public class StockDTO
    {
        public long PrimaryID { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ItemSource { get; set; }
        public int? CataloguePageNo { get; set; }
        public string ProductTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string SubEANCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? TotalStockQnt { get; set; }
        public int? ReservedQnt { get; set; }
        public int? StockQnt { get; set; }
        public decimal UnitPrice { get; set; }
        public int? ProductTypeID { get; set; }
    }
}

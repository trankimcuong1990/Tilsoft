using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StandardCostPriceOverviewRpt.DTO
{
   public class ProductSearchResultDto
    {
        public long PrimaryID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string QuotationStatusNM { get; set; }
        public string StatusUpdatedDate { get; set; }
        public int? Qnt40HC { get; set; }
        public decimal? SalePrice { get; set; }
        public int? FactoryID { get; set; }
        public int StatusID { get; set; }
        public int ItemType { get; set; }
        public decimal? ExRate { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public decimal? CostPrice { get; set; }
        public int? ProductID { get; set; }
        public bool IsCostPriceChanged { get; set; }
    }
}

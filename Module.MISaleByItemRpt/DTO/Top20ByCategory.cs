using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DTO
{
    public class Top20ByCategory
    {
        public int PrimaryID { get; set; }
        public string Season { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? CataloguePageNo { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductCategoryNM { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderQntIn40HC { get; set; }
        public decimal? AveragePrice { get; set; }
        public decimal? EURAmount { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int? ProductCategoryDisplayOrder { get; set; }
    }
}

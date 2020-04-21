using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DTO
{
    public class Top30
    {
        public int PrimaryID { get; set; }
        public string Season { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? CataloguePageNo { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderQntIn40HC { get; set; }
        public decimal? AveragePrice { get; set; }
        public decimal? EURAmount { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public decimal? AvgFactoryPrice { get; set; }
        public decimal? DeltaPrice { get; set; }
        public decimal? DeltaPercent { get; set; }
    }
}

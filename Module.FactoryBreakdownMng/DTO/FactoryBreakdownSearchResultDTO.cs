using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng.DTO
{
    public class FactoryBreakdownSearchResultDTO
    {
        public int FactoryBreakdownID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public bool? IsConfirmed { get; set; }
        public string ClientUD { get; set; }
        public string SampleOrderUD { get; set; }
        public string SampleProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public int? SampleProductID { get; set; }
        public decimal? IndicatedPrice { get; set; }
        public string FactoryUD { get; set; }
        public string Season { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? ItemTypeID { get; set; }
        public int? FactoryID { get; set; }
    }
}

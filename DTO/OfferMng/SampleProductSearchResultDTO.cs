using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class SampleProductSearchResultDTO
    {
        public int SampleProductID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string SampleProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public string SampleOrderUD { get; set; }
        public decimal? LoadAbility40HC { get; set; }
        public decimal? IndicatedPrice { get; set; }
        public string StatusUpdatorName { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public int? ClientID { get; set; }
        public int? FactoryBreakdownID { get; set; }
        public int? SampleOrderID { get; set; }
        public bool IsSelected { get; set; }
    }
}

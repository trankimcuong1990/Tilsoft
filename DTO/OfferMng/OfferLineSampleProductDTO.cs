using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class OfferLineSampleProductDTO
    {
        public int OfferLineSampleProductID { get; set; }
        public int? OfferID { get; set; }
        public int? SampleProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Remark { get; set; }
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
        public int RowIndex { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleOrderOverview
{
    public class ProductDTO
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleProductStatusNM { get; set; }
        public string ArticleDescription { get; set; }
        public int Quantity { get; set; }
        public bool IsTDNeeded { get; set; }
        public string FactoryDeadline { get; set; }
        public int QARemarkUpdatedBy { get; set; }
        public string QARemarkUpdatorName { get; set; }
        public string QARemarkUpdatedDate { get; set; }
        public string VNSuggestedFactoryUD { get; set; }
        public decimal? IndicatedPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }

        public List<ProductLocationDTO> ProductLocationDTOs { get; set; }
        public List<SubFactoryDTO> SubFactoryDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.QARemark
{
    public class ProductDTO
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleProductStatusNM { get; set; }
        public string ArticleDescription { get; set; }
        public int Quantity { get; set; }
        public string SampleOrderUD { get; set; }
        public string ClientUD { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryDeadline { get; set; }

        public List<RemarkDTO> RemarkDTOs { get; set; }
    }
}

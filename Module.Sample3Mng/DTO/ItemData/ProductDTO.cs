using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.ItemData
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


        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public int? LoadAbility20DC { get; set; }
        public int? LoadAbility40DC { get; set; }
        public int? LoadAbility40HC { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string QntInBox { get; set; }
        public decimal? IndicatedPrice { get; set; }
    }
}

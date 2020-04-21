using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class ProductionProcessSearchDTO
    {
        public int ProductionProcessID { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleArticleDescription { get; set; }
        public string OPSequenceNM { get; set; }
        public int? OPSequenceID { get; set; }
        public int? BOMStandardID { get; set; }
        public bool? HasBOM { get; set; }
        public bool? IsDefaultOfModel { get; set; }
        public int? FrameMaterialID { get; set; }
        public string FrameMaterialNM { get; set; }

    }
}

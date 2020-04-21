using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class SupportProductBreakDownPALSampleProductData
    {
        public int ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }

        public int? Qnt40HC { get; set; }
    }
}

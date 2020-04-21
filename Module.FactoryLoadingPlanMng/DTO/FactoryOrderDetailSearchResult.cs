using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class FactoryOrderDetailSearchResult
    {
        public int FactoryOrderDetailID { get; set; }
        public string FactoryUD { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public int? TotalLoaded { get; set; }
        public int? Qnt40HC { get; set; }
        public string ItemDescription { get; set; }
        public int? FactoryID { get; set; }
    }
}

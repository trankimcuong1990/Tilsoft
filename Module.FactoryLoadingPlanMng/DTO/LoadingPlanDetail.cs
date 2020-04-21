using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class LoadingPlanDetail
    {
        public int LoadingPlanDetailID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? Quantity { get; set; }
        public string Remark { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? OrderQnt { get; set; }
        public int? TotalLoaded { get; set; }
    }
}

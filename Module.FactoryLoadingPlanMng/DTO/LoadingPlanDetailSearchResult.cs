using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class LoadingPlanDetailSearchResult
    {
        public string FactoryOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
    }
}

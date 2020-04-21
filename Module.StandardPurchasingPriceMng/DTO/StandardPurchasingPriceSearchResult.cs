using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StandardPurchasingPriceMng.DTO
{
    public class StandardPurchasingPriceSearchResult
    {
        public int ProductID { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public decimal? PurchasingPrice { get; set; }
    }
}

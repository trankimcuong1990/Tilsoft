using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StandardCostPriceOverviewRpt.DTO
{
    public class ProductItem
    {
        public int ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public int? DefaultFactoryID { get; set; }
    }
}

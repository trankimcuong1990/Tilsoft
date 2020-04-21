using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StandardCostPriceOverviewRpt.DTO
{
    public class StandardCostPriceDetail
    {
        public int KeyId { get; set; }
        public string Season { get; set; }
        public int? ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? FactoryID { get; set; }
        public decimal? SalePrice { get; set; }
        public int? StatusID { get; set; }
        public string StatusUpdatedDate { get; set; }
        public int? ItemType { get; set; }
        public string FactoryUD { get; set; }
        public int? DefaultFactoryID { get; set; }
    }
}

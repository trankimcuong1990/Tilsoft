using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockReceipt.DTO
{
    public class StockRemains
    {
        public List<StockRemain> Data { get; set; }
        public int TotalRows { get; set; }
    }
    public class StockRemain
    {
        public object KeyID { get; set; }
        public int? ProductID { get; set; }
        public int? FactoryID { get; set; }
        public int? TotalStockQnt { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}

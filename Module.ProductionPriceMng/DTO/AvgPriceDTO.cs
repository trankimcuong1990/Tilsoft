using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng.DTO
{
    public class AvgPriceDTO
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? StockQnt { get; set; }
        public decimal? Price { get; set; }
        public int? ProductionPriceDetailID { get; set; }        
    }
}

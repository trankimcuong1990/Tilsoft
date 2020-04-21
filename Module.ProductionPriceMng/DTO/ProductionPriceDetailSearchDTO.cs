using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng.DTO
{
    public class ProductionPriceDetailSearchDTO
    {
        public int ProductionPriceDetailID { get; set; }
        public int? ProductionPriceID { get; set; }
        public decimal? Price { get; set; }
        public decimal? StockQnt { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
    }
}

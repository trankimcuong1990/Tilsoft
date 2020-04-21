using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceList.DTO
{
    public class PriceListDTO
    {
        public int PriceListID { get; set; }
        public int? ProductID { get; set; }
        public string DateValid { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? StockPrice { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}

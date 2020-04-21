using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SaleOrderMng.InnerDTO
{
    public class MagentoOrderDetail
    {
        public string ArticleCode { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}

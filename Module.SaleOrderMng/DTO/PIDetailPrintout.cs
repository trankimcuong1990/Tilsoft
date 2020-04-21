using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class PIDetailPrintout
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SaleAmount { get; set; }
    }
}

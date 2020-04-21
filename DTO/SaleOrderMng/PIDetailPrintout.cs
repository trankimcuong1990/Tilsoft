using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class PIDetailPrintout
    {
        public int? RowIndex { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SaleAmount { get; set; }
    }
}

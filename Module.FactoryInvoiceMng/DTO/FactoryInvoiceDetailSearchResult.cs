using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DTO
{
    public class FactoryInvoiceDetailSearchResult
    {
        public int? FactoryInvoiceID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ClientUD { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public string Remark { get; set; }
    }
}

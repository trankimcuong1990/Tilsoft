using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DTO
{
    public class FactoryInvoiceSearchResult
    {
        public int FactoryInvoiceID { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? SupplierID { get; set; }
    }
}

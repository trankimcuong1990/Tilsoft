using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng.DTO
{
    public class PurchasingInvoiceSearchResult
    {
        public int PurchasingInvoiceID { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? TotalAMount { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? RemainingAmount { get; set; }
        public int? SupplierID { get; set; }
    }
}

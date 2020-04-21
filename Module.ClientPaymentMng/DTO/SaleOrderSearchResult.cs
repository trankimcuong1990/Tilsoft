using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class SaleOrderSearchResult
    {
        public int SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? DPAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public int? ClientID { get; set; }
        public string Currency { get; set; }
    }
}

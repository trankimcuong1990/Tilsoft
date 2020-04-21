using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class SaleOrderForDeductionSearchResult
    {
        public int ECommercialInvoiceID { get; set; }
        public int SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DeductedAmount { get; set; }
        public decimal? ToBePaid { get; set; }
    }
}

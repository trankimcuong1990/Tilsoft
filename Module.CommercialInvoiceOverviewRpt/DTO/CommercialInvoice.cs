using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CommercialInvoiceOverviewRpt.DTO
{
    public class CommercialInvoice
    {
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ETA { get; set; }
        public string PaymentTerm { get; set; }
    }
}

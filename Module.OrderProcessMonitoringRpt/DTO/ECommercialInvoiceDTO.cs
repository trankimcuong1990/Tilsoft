using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrderProcessMonitoringRpt.DTO
{
    public class ECommercialInvoiceDTO
    {
        public int ECommercialInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Currency { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? SaleOrderID { get; set; }
    }
}

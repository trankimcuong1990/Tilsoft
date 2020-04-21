using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class PurchasingInvoice
    {
        public int? PurchasingInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public string BLNo { get; set; }
        public List<PurchasingInvoiceDetail> PurchasingInvoiceDetails { get; set; }
        public List<PurchasingInvoiceSparepartDetail> PurchasingInvoiceSparepartDetails { get; set; }
        public List<PurchasingInvoiceSampleDetail> PurchasingInvoiceSampleDetails { get; set; }
    }
}

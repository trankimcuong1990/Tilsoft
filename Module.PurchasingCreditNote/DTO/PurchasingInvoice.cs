using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote.DTO
{
    public class PurchasingInvoice
    {
        public int? PurchasingInvoiceID { get; set; }
        public int? SupplierID { get; set; }
        public string InvoiceNo { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public string BLNo { get; set; }
        public string ShipedDate { get; set; }
        public string SupplierNM { get; set; }
        public string ForwarderNM { get; set; }
        public string Feeder { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public string Season { get; set; }
        public List<PurchasingInvoiceDetail> PurchasingInvoiceDetails { get; set; }
        public List<PurchasingInvoiceSparepartDetail> PurchasingInvoiceSparepartDetails { get; set; }
    }
}

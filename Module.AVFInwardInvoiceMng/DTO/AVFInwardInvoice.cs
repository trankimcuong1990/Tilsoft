using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFInwardInvoiceMng.DTO
{
    public class AVFInwardInvoice
    {
        public int? AVFPurchasingInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string AVFSupplierNM { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public string TaxRate { get; set; }
        public decimal? VAT { get; set; }
        public string DebitAccountNo { get; set; }
        public string CreditAccountNo { get; set; }
        public decimal? Total { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Module.LedgerAccountMng.DTO
{
    public class LedgerAccountDetailOverview
    {
        public int? AVFPurchasingInvoiceID { get; set; }
        public int? AVFPurchasingInvoiceDetailID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string AVFSupplierNM { get; set; }
        public string Description { get; set; }
        public string DebitAccountNo { get; set; }
        public string CreditAccountNo { get; set; }
    }
}

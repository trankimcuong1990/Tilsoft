using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.AVFPurchasingInvoiceMng.DTO
{
    public class AVFPurchasingInvoiceDetail
    {
        public int? AVFPurchasingInvoiceDetailID { get; set; }
        public int? AVFPurchasingInvoiceID { get; set; }
        public int? DetailIndex { get; set; }
        public int? DebitAccountID { get; set; }
        public int? CreditAccountID { get; set; }
        public string Description { get; set; }
        public decimal? USD { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? Amount { get; set; }
    }
}

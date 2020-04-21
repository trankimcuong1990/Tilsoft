using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.AVFSupplierMng
{
    public class AVFSupplierPurchasingInvoiceDetail
    {
        public int? AVFPurchasingInvoiceDetailID { get; set; }
        public int? AVFPurchasingInvoiceID { get; set; }
        public int? DetailIndex { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal? Amount { get; set; }
        public string DebitAccNo { get; set; }
        public string CreditAccNo { get; set; }
    }
}

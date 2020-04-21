using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.AVFOutwardInvoiceMng.DTO
{
    public class AVFOutwardInvoiceDetail
    {
        public int? AVFOutwardInvoiceDetailID { get; set; }
        public int? AVFOutwardInvoiceID { get; set; }
        public int? DetailIndex { get; set; }
        public string Description { get; set; }
        public decimal? USD { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? Amount { get; set; }
    }
}

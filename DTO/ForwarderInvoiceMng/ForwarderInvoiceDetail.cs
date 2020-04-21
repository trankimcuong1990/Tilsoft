using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ForwarderInvoiceMng
{
    public class ForwarderInvoiceDetail
    {
        public int? ForwarderInvoiceDetailID { get; set; }
        public int? ForwarderInvoiceID { get; set; }
        public int? FeeTypeID { get; set; }
        public string FeeName { get; set; }
        public string Currency { get; set; }
        public decimal? Amount { get; set; }
        public string FeeTypeNM { get; set; }
    }
}
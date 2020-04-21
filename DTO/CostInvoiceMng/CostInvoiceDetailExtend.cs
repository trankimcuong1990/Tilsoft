using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CostInvoiceMng
{
    public class CostInvoiceDetailExtend
    {
        public int? CostInvoiceDetailExtendID { get; set; }

        public int? CostInvoiceDetailID { get; set; }

        public string Description { get; set; }

        public decimal? Amount { get; set; }

        public int? CostInvoiceID { get; set; }

    }
}
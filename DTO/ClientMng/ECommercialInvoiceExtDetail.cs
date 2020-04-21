using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ECommercialInvoiceExtDetail
    {
        public int? ECommercialInvoiceExtDetailID { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Currency { get; set; }
        public int? TypeOfInvoice { get; set; }
    }
}

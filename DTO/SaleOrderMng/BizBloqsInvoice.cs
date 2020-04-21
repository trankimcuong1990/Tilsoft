using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class BizBloqsInvoice
    {
        public bool? IsSelected { get; set; }
        public bool? IsError { get; set; }
        public List<string> ErrorList { get; set; }
        public string Currency { get; set; }
        public string ClientUD { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public decimal? VATPercentage { get; set; }
        public string Remark { get; set; }
        public List<BizBloqsInvoiceLine> InvoiceLines { get; set; }
        public BizBloqsInvoice() {
            InvoiceLines = new List<BizBloqsInvoiceLine>();
        }
    }

    public class BizBloqsInvoiceLine
    {
        public string ArticleCode { get; set; }
        public decimal? NetPrice { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }          
    }
}

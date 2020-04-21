using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class BizBloqsInvoice
    {
        public bool? IsSelected { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string OrderNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string AccountNo { get; set; }
        public decimal? VATRate { get; set; }
        public string Currency { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public string Remark { get; set; }        
        public List<BizBloqsInvoiceLine> InvoiceLines { get; set; }
        public BizBloqsInvoice() {
            InvoiceLines = new List<BizBloqsInvoiceLine>();
        }
    }

    public class BizBloqsInvoiceLine
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Currency { get; set; }
        public decimal? VATPercent { get; set; }
        public string AccountNo { get; set; }
        public decimal? DiscountPercentage { get; set; }        
    }
}

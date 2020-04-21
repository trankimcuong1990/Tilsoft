using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceSparepartDetailOverview
    {
        public int? ECommercialInvoiceSparepartDetailID { get; set; }

        public int? ECommercialInvoiceDetailID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public string Remark { get; set; }

        public int? PurchasingInvoiceSparepartDetailID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ContainerNo { get; set; }

        public string SealNo { get; set; }

        public string ContainerType { get; set; }

        public string Currency { get; set; }

        public decimal? VAT { get; set; }

        public string ClientArticleCode { get; set; }

        public string ClientArticleName { get; set; }

        public string ClientOrderNumber { get; set; }

        public string ClientEANCode { get; set; }

        public string Conditions { get; set; }

        public int? PaymentTermID { get; set; }

        public string PaymentTermNM { get; set; }

        public int? DeliveryTermID { get; set; }

        public string DeliveryTermNM { get; set; }
    }
}

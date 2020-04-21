using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceDetail
    {
        public int? ECommercialInvoiceDetailID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public string Remark { get; set; }

        public int? PurchasingInvoiceDetailID { get; set; }

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
        public int? LoadingPlanDetailID { get; set; }

        public string HSCode { get; set; }

        public int? SaleOrderID { get; set; }
        public int? OfferID { get; set; }
        public int? LoadingPlanID { get; set; }
        public int? BookingID { get; set; }
        public int? PurchasingInvoiceID { get; set; }


        public List<ECommercialInvoiceDetailDescription> ECommercialInvoiceDetailDescriptions { get; set; }

    }
}
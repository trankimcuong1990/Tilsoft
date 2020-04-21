using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceSampleDetail
    {
        public int ECommercialInvoiceSampleDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> PurchasingInvoiceSampleDetailID { get; set; }
        public Nullable<int> LoadingPlanSampleDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerType { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public string Conditions { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public string PaymentTermNM { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public string DeliveryTermNM { get; set; }
        public string HSCode { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> BookingID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }

        public List<ECommercialInvoiceSampleDetailDescription> ECommercialInvoiceSampleDetailDescriptions { get; set; }
    }
}

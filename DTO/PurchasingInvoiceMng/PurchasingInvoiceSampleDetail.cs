using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PurchasingInvoiceMng
{
    public class PurchasingInvoiceSampleDetail
    {
        public int PurchasingInvoiceSampleDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> LoadingPlanSampleDetailID { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> FactoryAmount { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PriceStatus { get; set; }
        public Nullable<decimal> QuotationSalePrice { get; set; }
        public Nullable<decimal> QuotationPurchasingPrice { get; set; }
        public string LoadingPlanUD { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<decimal> FactoryProformaPrice { get; set; }
        public string FactoryProformaInvoiceNo { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> QuotationID { get; set; }
    }
}

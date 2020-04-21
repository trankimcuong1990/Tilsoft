using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PurchasingInvoiceMng
{
    public class PurchasingInvoiceDetail
    {
        public int? PurchasingInvoiceDetailID { get; set; }

        public int? PurchasingInvoiceID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? LoadingPlanDetailID { get; set; }

        public decimal? FactoryPrice { get; set; }

        public decimal? Amount { get; set; }

        public decimal? FactoryAmount { get; set; }

        public string ClientUD { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string ContainerNo { get; set; }

        public string SealNo { get; set; }

        public string ContainerTypeNM { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string PriceStatus { get; set; }

        public decimal? QuotationSalePrice { get; set; }

        public decimal? QuotationPurchasingPrice { get; set; }

        public string LoadingPlanUD { get; set; }

        public int? SupplierID { get; set; }

        public decimal? FactoryProformaPrice { get; set; }

        public string FactoryProformaInvoiceNo { get; set; }

        public int? OfferID { get; set; }
        public int? ClientID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? LoadingPlanID { get; set; }
        public int? QuotationID { get; set; }

    }
}
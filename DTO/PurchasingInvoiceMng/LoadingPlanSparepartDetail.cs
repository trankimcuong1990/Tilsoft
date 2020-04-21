using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PurchasingInvoiceMng
{
    public class LoadingPlanSparepartDetail
    {
        public int? LoadingPlanSparepartDetailID { get; set; }

        public int? BookingID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Amount { get; set; }

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

    }
}
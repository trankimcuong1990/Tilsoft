using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class WarehouseInvoiceProductDetail
    {
        public int? WarehouseInvoiceProductDetailID { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? ProductID { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? WarehousePickingListProductDetailID { get; set; }

        public decimal? PurchasingPrice { get; set; }

        public int? FactoryID { get; set; }

        public string FactoryUD { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string CMRNo { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public string VAT { get; set; }

        public string Conditions { get; set; }

        public int? PaymentTermID { get; set; }

        public string PaymentTermNM { get; set; }

        public int? DeliveryTermID { get; set; }

        public string DeliveryTermNM { get; set; }

    }
}
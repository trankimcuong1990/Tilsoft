using System;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoicePurchasing
    {

        public int ECommercialInvoiceDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> ECommercialQnt { get; set; }
        public Nullable<int> PurchasingQnt { get; set; }
        public Nullable<decimal> ECommercialPrice { get; set; }
        public Nullable<decimal> SaleOrderPrice { get; set; }
    }
}
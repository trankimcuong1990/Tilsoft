//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.CreditNoteMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View
    {
        public int ECommercialInvoiceSparepartDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> PurchasingInvoiceSparepartDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerType { get; set; }
    
        public virtual ECommercialInvoiceMng_ECommercialInvoice_View ECommercialInvoiceMng_ECommercialInvoice_View { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PurchasingInvoiceMng2
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingInvoiceMng_PurchasingInvoiceExtraDetail_View
    {
        public int PurchasingInvoiceExtraDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public Nullable<int> PurchasingInvoiceDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceSparepartDetailID { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string LoadingPlanUD { get; set; }
        public string GoodsType { get; set; }
    
        public virtual PurchasingInvoiceMng_PurchasingInvoice_View PurchasingInvoiceMng_PurchasingInvoice_View { get; set; }
    }
}
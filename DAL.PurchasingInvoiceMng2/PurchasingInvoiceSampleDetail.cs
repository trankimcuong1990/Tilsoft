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
    
    public partial class PurchasingInvoiceSampleDetail
    {
        public int PurchasingInvoiceSampleDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> LoadingPlanSampleDetailID { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
    
        public virtual PurchasingInvoice PurchasingInvoice { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DashboardMng.DAL
{
    using System;
    
    public partial class DashboardMng_function_GetPurchasingInvoice_Result
    {
        public int PurchasingInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Season { get; set; }
        public string AVTStatus { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<bool> HasURLLink { get; set; }
    }
}

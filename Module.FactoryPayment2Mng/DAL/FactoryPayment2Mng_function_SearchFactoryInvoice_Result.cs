//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryPayment2Mng.DAL
{
    using System;
    
    public partial class FactoryPayment2Mng_function_SearchFactoryInvoice_Result
    {
        public int FactoryInvoiceID { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalPaid { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<int> SupplierID { get; set; }
    }
}

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
    
    public partial class DashboardMng_function_FinanceOverview_Result
    {
        public int FactoryPayment2DetailID { get; set; }
        public Nullable<int> FactoryPayment2ID { get; set; }
        public string ReceiptNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public Nullable<decimal> DPDeductedAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
    }
}

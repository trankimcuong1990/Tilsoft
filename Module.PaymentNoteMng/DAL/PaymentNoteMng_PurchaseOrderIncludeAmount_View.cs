//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PaymentNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentNoteMng_PurchaseOrderIncludeAmount_View
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> PurchaseOrderStatusID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string RequiredDocuments { get; set; }
        public string PaymentDocuments { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string ETA { get; set; }
        public Nullable<decimal> PaymentByPO { get; set; }
        public Nullable<decimal> TotalPurchaseOrderAmount { get; set; }
        public Nullable<decimal> RemainPurchaseOrderAmount { get; set; }
        public Nullable<decimal> TotalDepositAmount { get; set; }
        public Nullable<decimal> TotalRealDeposit { get; set; }
        public Nullable<decimal> TotalPayDepositAmount { get; set; }
        public Nullable<decimal> RemainDepositAmount { get; set; }
        public string SupplierPaymentTermNM { get; set; }
    }
}
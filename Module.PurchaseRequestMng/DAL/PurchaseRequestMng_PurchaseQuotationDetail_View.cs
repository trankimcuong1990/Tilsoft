//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseRequestMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseRequestMng_PurchaseQuotationDetail_View
    {
        public int PurchaseQuotationDetailID { get; set; }
        public Nullable<int> PurchaseQuotationID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public bool IsDefault { get; set; }
        public long PrimaryID { get; set; }
    }
}
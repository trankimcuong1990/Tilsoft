//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseQuotationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseQuotationMng_DefaultPrice_Suppliers_View
    {
        public long PrimaryID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        public virtual PurchaseQuotationMng_DefaultPrice_View PurchaseQuotationMng_DefaultPrice_View { get; set; }
    }
}

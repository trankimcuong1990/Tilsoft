//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySaleInvoice.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactorySaleInvoice_FactorySaleOrderDetail_View
    {
        public long KeyID { get; set; }
        public string FactorySaleOrderUD { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public decimal InvoiceQnt { get; set; }
        public Nullable<decimal> FSOPrice { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string ProductionItemUD { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public int FactorySaleOrderDetailID { get; set; }
        public Nullable<int> FactorySaleOrderID { get; set; }
        public Nullable<decimal> RemainQnt { get; set; }
    }
}

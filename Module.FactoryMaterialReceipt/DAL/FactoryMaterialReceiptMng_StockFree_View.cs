//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMaterialReceipt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMaterialReceiptMng_StockFree_View
    {
        public long KeyID { get; set; }
        public Nullable<int> FactoryMaterialID { get; set; }
        public Nullable<int> FactoryAreaID { get; set; }
        public Nullable<decimal> TotalStockQnt { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string UnitNM { get; set; }
        public string FactoryAreaNM { get; set; }
        public Nullable<decimal> TotalImportedQnt { get; set; }
        public Nullable<decimal> TotalExportedQnt { get; set; }
    }
}

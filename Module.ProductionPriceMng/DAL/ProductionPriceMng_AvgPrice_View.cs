//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionPriceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionPriceMng_AvgPrice_View
    {
        public long KeyID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> StockQnt { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> ProductionPriceDetailID { get; set; }
    }
}

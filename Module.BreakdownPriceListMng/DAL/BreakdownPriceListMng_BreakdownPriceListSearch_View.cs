//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BreakdownPriceListMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BreakdownPriceListMng_BreakdownPriceListSearch_View
    {
        public long PrimaryID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemVNNM { get; set; }
        public string UnitNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<decimal> AVFPrice { get; set; }
        public Nullable<decimal> AVTPrice { get; set; }
    }
}

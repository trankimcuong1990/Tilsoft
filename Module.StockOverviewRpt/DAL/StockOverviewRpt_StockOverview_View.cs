//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.StockOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockOverviewRpt_StockOverview_View
    {
        public long PrimaryID { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ItemSource { get; set; }
        public Nullable<int> CataloguePageNo { get; set; }
        public string ProductTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string SubEANCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> TotalStockQnt { get; set; }
        public Nullable<int> ReservedQnt { get; set; }
        public Nullable<int> StockQnt { get; set; }
        public decimal UnitPrice { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
    }
}
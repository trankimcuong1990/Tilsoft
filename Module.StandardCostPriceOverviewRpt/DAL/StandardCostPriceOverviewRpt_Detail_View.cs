//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.StandardCostPriceOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StandardCostPriceOverviewRpt_Detail_View
    {
        public string Season { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public int StatusID { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public int ItemType { get; set; }
        public long KeyId { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> DefaultFactoryID { get; set; }
    }
}
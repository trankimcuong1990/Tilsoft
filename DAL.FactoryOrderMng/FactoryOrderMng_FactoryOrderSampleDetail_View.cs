//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryOrderMng_FactoryOrderSampleDetail_View
    {
        public int FactoryOrderSampleDetailID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> SaleOrderDetailSampleID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public string Remark { get; set; }
        public Nullable<int> TotalQnt { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> SaleOrderQntSample { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public string PackagingMethodText { get; set; }
    
        public virtual FactoryOrderMng_FactoryOrder_View FactoryOrderMng_FactoryOrder_View { get; set; }
    }
}

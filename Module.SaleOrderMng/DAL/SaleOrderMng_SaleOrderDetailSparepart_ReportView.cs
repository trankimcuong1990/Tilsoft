//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SaleOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderMng_SaleOrderDetailSparepart_ReportView
    {
        public int SaleOrderDetailSparepartID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> SaleAmount { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientEANCode { get; set; }
    
        public virtual SaleOrderMng_SaleOrder_ReportView SaleOrderMng_SaleOrder_ReportView { get; set; }
    }
}

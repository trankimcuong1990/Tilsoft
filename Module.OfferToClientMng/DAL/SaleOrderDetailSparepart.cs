//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferToClientMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderDetailSparepart
    {
        public int SaleOrderDetailSparepartID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> OfferLineSparePartID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> V4ID { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientEANCode { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> CommissionPercentOTC { get; set; }
    
        public virtual OfferLineSparepart OfferLineSparepart { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
    }
}

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
    
    public partial class SaleOrderMng_SaleOrderSparepartReturn_View
    {
        public int SaleOrderSparepartReturnID { get; set; }
        public Nullable<int> LoadingPlanSparepartDetailID { get; set; }
        public Nullable<int> SaleOrderDetailSparepartID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> ReturnIndex { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string BLNo { get; set; }
        public string ContainerNo { get; set; }
        public string FactoryOrderUD { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string NewPINo { get; set; }
        public Nullable<int> NewSaleOrderID { get; set; }
        public Nullable<int> NewOfferID { get; set; }
        public Nullable<int> RemaingReturnQnt { get; set; }
    
        public virtual SaleOrderMng_SaleOrder_View SaleOrderMng_SaleOrder_View { get; set; }
    }
}

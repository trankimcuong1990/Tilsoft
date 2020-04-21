//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SaleOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderMng_SaleOrderDetail_View
    {
        public SaleOrderMng_SaleOrderDetail_View()
        {
            this.SaleOrderMng_SaleOrderDetailExtend_View = new HashSet<SaleOrderMng_SaleOrderDetailExtend_View>();
        }
    
        public int SaleOrderDetailID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public string Reference { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> PhysicalQnt { get; set; }
        public Nullable<int> OnSeaQnt { get; set; }
        public Nullable<int> TobeShippedQnt { get; set; }
        public Nullable<int> ReservationQnt { get; set; }
        public Nullable<int> FTSQnt { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> V4ID { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> OfferItemTypeID { get; set; }
        public string OfferItemTypeNM { get; set; }
        public string ApproverName { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public Nullable<int> PlaningPurchasingPriceFactoryID { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<bool> IsPlaningPurchasingPriceSelected { get; set; }
        public Nullable<int> PlaningPurchasingPriceSelectedBy { get; set; }
        public Nullable<System.DateTime> PlaningPurchasingPriceSelectedDate { get; set; }
        public string PlaningPurchasingPriceSourceNM { get; set; }
        public string PlanningPriceSelectorName { get; set; }
        public Nullable<decimal> OfferSalePrice { get; set; }
        public int IsFactoryOrderCreated { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> CommissionPercentOTC { get; set; }
    
        public virtual ICollection<SaleOrderMng_SaleOrderDetailExtend_View> SaleOrderMng_SaleOrderDetailExtend_View { get; set; }
        public virtual SaleOrderMng_SaleOrder_View SaleOrderMng_SaleOrder_View { get; set; }
    }
}

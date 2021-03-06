//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackSaleOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferLine
    {
        public OfferLine()
        {
            this.SaleOrderDetail = new HashSet<SaleOrderDetail>();
        }
    
        public int OfferLineID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public Nullable<int> POLID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> IncreasePercent { get; set; }
        public Nullable<decimal> TransportationFee { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> FinalPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<bool> IsClientSelected { get; set; }
        public Nullable<int> ClientQuantity { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> V4ID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> CushionID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public string CushionRemark { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<bool> UseFSCLabel { get; set; }
    
        public virtual Offer Offer { get; set; }
        public virtual ICollection<SaleOrderDetail> SaleOrderDetail { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.OfferMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferMng_OfferLine_View
    {
        public OfferMng_OfferLine_View()
        {
            this.OfferMng_OfferLinePriceOption_View = new HashSet<OfferMng_OfferLinePriceOption_View>();
        }
    
        public int OfferLineID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> CushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> POLID { get; set; }
        public Nullable<decimal> IncreasePercent { get; set; }
        public Nullable<decimal> TransportationFee { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> FinalPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsClientSelected { get; set; }
        public Nullable<int> ClientQuantity { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public Nullable<decimal> CommissionAmount { get; set; }
        public Nullable<decimal> SurchargeAmount { get; set; }
        public Nullable<decimal> SubTotalAmount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductTypeNM { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public string ProductFileLocation { get; set; }
        public string ProductThumbnailLocation { get; set; }
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
        public Nullable<bool> IsCreatedOrder { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> SalePriceCalculated { get; set; }
        public Nullable<int> ClientSpecialPackagingMethodID { get; set; }
        public string SpecialRequirement { get; set; }
        public Nullable<decimal> EstimatedPurchasingPrice { get; set; }
        public Nullable<int> EstimatedPurchasingPriceFromFactoryID { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> EstimatedPurchasingPriceUpdatedByID { get; set; }
        public string EstimatedPurchasingPriceUpdatedBy { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> PlaningPurchasingPriceFactoryID { get; set; }
        public Nullable<int> QuotationDetailID { get; set; }
        public Nullable<bool> IsPlaningPurchasingPriceSelected { get; set; }
        public Nullable<int> PlaningPurchasingPriceSelectedBy { get; set; }
        public Nullable<System.DateTime> PlaningPurchasingPriceSelectedDate { get; set; }
        public string PlaningPurchasingPriceSourceNM { get; set; }
        public string PlaningPurchasingPriceFactoryUD { get; set; }
        public string PlaningPurchasingPriceUpdatorName { get; set; }
        public Nullable<decimal> AutoPlaningPurchasingPrice { get; set; }
        public Nullable<int> AutoPlaningPurchasingPriceSourceID { get; set; }
        public string AutoPlaningPurchasingPriceSourceNM { get; set; }
        public string AutoPlaningPurchasingPriceFactoryNM { get; set; }
        public string PackagingMethodText { get; set; }
        public Nullable<int> CurrentSupplierID { get; set; }
        public string CurrentSupplierUD { get; set; }
        public Nullable<decimal> CurrentSupplierPrice { get; set; }
        public Nullable<int> OfferItemTypeID { get; set; }
        public string OfferItemTypeNM { get; set; }
        public string ApproverName { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string EstimatedPurchasingPriceRemark { get; set; }
        public Nullable<int> TotalItemSalePriceHistory { get; set; }
        public string LocationNM { get; set; }
        public Nullable<decimal> Delta5PercentLastYear { get; set; }
    
        public virtual OfferMng_Offer_View OfferMng_Offer_View { get; set; }
        public virtual ICollection<OfferMng_OfferLinePriceOption_View> OfferMng_OfferLinePriceOption_View { get; set; }
    }
}

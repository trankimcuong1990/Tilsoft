//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVTPurchasingPriceMng.DAL
{
    using System;
    
    public partial class AVTPurchasingPriceMng_function_SearchFactoryQuotation_Result
    {
        public int QuotationDetailID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PriceDifferenceCode { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<decimal> PurchasingPrice3rdPreviousSeason { get; set; }
        public Nullable<decimal> PurchasingPrice2ndPreviousSeason { get; set; }
        public Nullable<decimal> PurchasingPricePreviousSeason { get; set; }
        public Nullable<decimal> PurchasingPrice { get; set; }
        public Nullable<decimal> PriceIncludeDiff { get; set; }
        public Nullable<decimal> TargetPrice { get; set; }
        public string QuotationStatusNM { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public string StatusUpdatorNM { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> Qnt40HC { get; set; }
        public Nullable<decimal> OrderQnt40HC { get; set; }
        public string PackagingMethodNM { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> PriceUpdatedDate { get; set; }
        public Nullable<decimal> PriceDifferenceRate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> StatusUpdatedBy { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ItemTypeID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public Nullable<int> QuotationOfferDirectionID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> BOMStandardID { get; set; }
        public Nullable<int> BOMID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<int> ClientSpecialPackagingMethodID { get; set; }
        public string SpecialRequirement { get; set; }
        public string DeliveryTerm { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public decimal ExportCost40HC { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> AVFBreakdownAmount { get; set; }
        public Nullable<int> AVTBreakdownAmount { get; set; }
        public Nullable<int> AVFBreakdownLoadability { get; set; }
        public Nullable<int> AVTBreakdownLoadability { get; set; }
        public int TotalShippedQnt { get; set; }
        public string PackagingType { get; set; }
    }
}

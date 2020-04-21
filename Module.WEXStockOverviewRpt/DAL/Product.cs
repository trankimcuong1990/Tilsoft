//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WEXStockOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> CataloguePageNumber { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> CushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<bool> IsActiveFreeToSale { get; set; }
        public Nullable<int> FreeToSaleCategoryID { get; set; }
        public Nullable<bool> IsMatchedImage { get; set; }
        public Nullable<decimal> MaterialWeight { get; set; }
        public Nullable<int> MaterialWeightUpdatedBy { get; set; }
        public Nullable<System.DateTime> MaterialWeightUpdatedDate { get; set; }
        public Nullable<decimal> FabricWeight { get; set; }
        public Nullable<int> FabricWeightUpdatedBy { get; set; }
        public Nullable<System.DateTime> FabricWeightUpdatedDate { get; set; }
        public string EANCode { get; set; }
        public Nullable<int> EANCodeIndex { get; set; }
        public Nullable<int> DefaultFactoryID { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public string HSCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}

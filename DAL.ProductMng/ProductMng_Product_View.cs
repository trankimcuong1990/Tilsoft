//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ProductMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMng_Product_View
    {
        public ProductMng_Product_View()
        {
            this.ProductMng_ProductPiece_View = new HashSet<ProductMng_ProductPiece_View>();
            this.ProductMng_ProductSetEANCode_View = new HashSet<ProductMng_ProductSetEANCode_View>();
            this.ProductMng_ProductECommerceSpec_View = new HashSet<ProductMng_ProductECommerceSpec_View>();
        }
    
        public int ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> CushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> CataloguePageNumber { get; set; }
        public string ImageFile { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<bool> IsMatchedImage { get; set; }
        public Nullable<bool> IsActiveFreeToSale { get; set; }
        public Nullable<int> FreeToSaleCategoryID { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public Nullable<decimal> MaterialWeight { get; set; }
        public string MaterialWeightUpdatorName { get; set; }
        public Nullable<System.DateTime> MaterialWeightUpdatedDate { get; set; }
        public Nullable<decimal> FabricWeight { get; set; }
        public string FabricWeightUpdatorName { get; set; }
        public Nullable<System.DateTime> FabricWeightUpdatedDate { get; set; }
        public Nullable<int> Qnt20DC { get; set; }
        public Nullable<int> Qnt40DC { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string RangeName { get; set; }
        public string Collection { get; set; }
        public string ProductTypeNM { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string SeatDimL { get; set; }
        public string SeatDimW { get; set; }
        public string SeatDimH { get; set; }
        public string ArmDimL { get; set; }
        public string ArmDimW { get; set; }
        public string ArmDimH { get; set; }
        public string OtherDimL { get; set; }
        public string OtherDimW { get; set; }
        public string OtherDimH { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<bool> HasCushion { get; set; }
        public Nullable<bool> HasFrameMaterial { get; set; }
        public Nullable<bool> HasSubMaterial { get; set; }
        public string ProductGroupNM { get; set; }
        public string MaterialText { get; set; }
        public string FrameText { get; set; }
        public string SubMaterialText { get; set; }
        public string CushionText { get; set; }
        public string FSCText { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string EANCode { get; set; }
        public string UpdatorName2 { get; set; }
        public string ConfirmerName2 { get; set; }
        public string HSCode { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public Nullable<decimal> CartonBoxDimL { get; set; }
        public Nullable<decimal> CartonBoxDimW { get; set; }
        public Nullable<decimal> CartonBoxDimH { get; set; }
        public Nullable<decimal> NetWeight2 { get; set; }
        public Nullable<decimal> GrossWeight2 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatorName { get; set; }
    
        public virtual ICollection<ProductMng_ProductPiece_View> ProductMng_ProductPiece_View { get; set; }
        public virtual ICollection<ProductMng_ProductSetEANCode_View> ProductMng_ProductSetEANCode_View { get; set; }
        public virtual ICollection<ProductMng_ProductECommerceSpec_View> ProductMng_ProductECommerceSpec_View { get; set; }
    }
}

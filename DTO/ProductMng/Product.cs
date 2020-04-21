using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ProductMng
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public int ModelID { get; set; }
        public int MaterialID { get; set; }
        public int MaterialTypeID { get; set; }
        public int MaterialColorID { get; set; }
        public int FrameMaterialID { get; set; }
        public int FrameMaterialColorID { get; set; }
        public int SubMaterialID { get; set; }
        public int SubMaterialColorID { get; set; }
        public int CushionID { get; set; }
        public int CushionColorID { get; set; }
        public int BackCushionID { get; set; }
        public int SeatCushionID { get; set; }
        public int FSCTypeID { get; set; }
        public int FSCPercentID { get; set; }
        public int ManufacturerCountryID { get; set; }
        public int CataloguePageNumber { get; set; }
        public string ImageFile { get; set; }
        public string Remark { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public int ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public bool? IsMatchedImage { get; set; }
        public bool? IsActiveFreeToSale { get; set; }
        public int? FreeToSaleCategoryID { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public decimal? MaterialWeight { get; set; }
        public string MaterialWeightUpdatorName { get; set; }
        public string MaterialWeightUpdatedDate { get; set; }
        public decimal? FabricWeight { get; set; }
        public string FabricWeightUpdatorName { get; set; }
        public string FabricWeightUpdatedDate { get; set; }
        public int ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
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
        public bool HasCushion { get; set; }
        public bool HasFrameMaterial { get; set; }
        public bool HasSubMaterial { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public int? Qnt20DC { get; set; }
        public int? Qnt40DC { get; set; }
        public int? Qnt40HC { get; set; }
        public string MaterialText { get; set; }
        public string FrameText { get; set; }
        public string SubMaterialText { get; set; }
        public string CushionText { get; set; }
        public string FSCText { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string ConfirmedStatus 
        {
            get 
            {
                if (this.IsConfirmed)
                {
                    return "CONFIRMED";
                }
                else
                {
                    return "PENDING";
                }
            }
        }

        public List<LoadAbility> LoadAbilities { get; set; }
        public List<ProductPiece> ProductPieces { get; set; }
        public string EANCode { get; set; }
        public List<ProductSetEANCode> ProductSetEANCodes { get; set; }
        public string UpdatorName2 { get; set; }
        public string ConfirmerName2 { get; set; }
        public string HSCode { get; set; }
        public List<ProductECommerceSpec> ProductECommerceSpecs { get; set; }

        public int? PackagingMethodID { get; set; }
        public string PackagingMethodUD { get; set; }
        public string PackagingMethodNM { get; set; }
        public decimal? CartonBoxDimL { get; set; }
        public decimal? CartonBoxDimW { get; set; }
        public decimal? CartonBoxDimH { get; set; }
        public decimal? NetWeight2 { get; set; }
        public decimal? GrossWeight2 { get; set; } 

        public bool IsOffer { get; set; }
    }
}
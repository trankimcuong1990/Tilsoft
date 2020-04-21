using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class Model
    {
        public Model()
        {
            ModelPurchasingPriceConfigurationDTOs = new List<ModelPurchasingPriceConfigurationDTO>();
        }
        public int ModelID { get; set; }

        [Display(Name = "ModelUD")]
        public string ModelUD { get; set; }

        [Display(Name = "ModelNM")]
        public string ModelNM { get; set; }

        [Display(Name = "Season")]
        public string Season { get; set; }

        [Display(Name = "ProductTypeID")]
        public int? ProductTypeID { get; set; }

        [Display(Name = "RangeName")]
        public string RangeName { get; set; }

        [Display(Name = "Collection")]
        public string Collection { get; set; }

        [Display(Name = "OverallDimL")]
        public string OverallDimL { get; set; }

        [Display(Name = "OverallDimW")]
        public string OverallDimW { get; set; }

        [Display(Name = "OverallDimH")]
        public string OverallDimH { get; set; }

        [Display(Name = "SeatDimL")]
        public string SeatDimL { get; set; }

        [Display(Name = "SeatDimW")]
        public string SeatDimW { get; set; }

        [Display(Name = "SeatDimH")]
        public string SeatDimH { get; set; }

        [Display(Name = "ArmDimL")]
        public string ArmDimL { get; set; }

        [Display(Name = "ArmDimW")]
        public string ArmDimW { get; set; }

        [Display(Name = "ArmDimH")]
        public string ArmDimH { get; set; }

        [Display(Name = "OtherDimL")]
        public string OtherDimL { get; set; }

        [Display(Name = "OtherDimW")]
        public string OtherDimW { get; set; }

        [Display(Name = "OtherDimH")]
        public string OtherDimH { get; set; }


        public string MaterialWeight { get; set; }
        public string FabricWeight { get; set; }
        public string NetWeight { get; set; }
        public string BackCushionDimL { get; set; }
        public string BackCushionDimW { get; set; }
        public string BackCushionDimH { get; set; }
        public string BackCushionStuffing { get; set; }
        public string BackCushionParts { get; set; }
        public string BackCushionWeight { get; set; }
        public string SeatCushionDimL { get; set; }
        public string BackCushionFoam { get; set; }
        public string BackCushionFabric { get; set; }
        public string SeatCushionDimW { get; set; }
        public string SeatCushionDimH { get; set; }
        public string SeatCushionStuffing { get; set; }
        public string SeatCushionParts { get; set; }
        public string SeatCushionWeight { get; set; }
        public string SeatCushionFoam { get; set; }
        public string SeatCushionFabric { get; set; }
        public bool IsExcludedInNotification { get; set; }
        public string EANCode { get; set; }
        public string ProductTypeNM { get; set; }
        public string ProductGroupNM { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string ModelStatusNM { get; set; }
        public string ProductCategoryNM { get; set; }
        public string FactoryUD { get; set; }
        public string HSCode { get; set; }



        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "CreatedDate")]
        public string CreatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }
        public bool IsStandard { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "CreatorName2")]
        public string CreatorName2 { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }
        public string ConcurrencyFlag_String { get; set; }

        public string UpdatorName2 { get; set; }

        public int? ProductGroupID { get; set; }
        public bool HasCushion { get; set; }
        public bool HasFrameMaterial { get; set; }
        public bool HasSubMaterial { get; set; }

        public string ImageFile { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }

        public int? CataloguePageNo { get; set; }
        
        [Required]
        public int ManufacturerCountryID { get; set; }
        public int? ProductCategoryID { get; set; }
        public int? FactoryID { get; set; }
        
        [Required]
        public int ModelStatusID { get; set; }

        public List<SubMaterialOption> SubMaterialOptions { get; set; }
        public List<CushionOption> CushionOptions { get; set; }
        public List<PackagingMethodOption> PackagingMethodOptions { get; set; }
        public List<ModelCheckListGroupDTO> ModelCheckListGroupDTO { get; set; }
        public List<Sparepart> Spareparts { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<ImageGallery> ImageGalleries { get; set; }
        public List<ImageGalleryVersion> ImageGalleryVersions { get; set; }
        public List<TestReport> TestReports { get; set; }
        public List<ModelIssue> ModelIssues { get; set; }
        public List<tdFile> Tdfiles { get; set; }
        public List<SampleProductView> sampleProductViews { get; set; }
        public List<productsView> productsViews { get; set; }
        public List<ProductBreakDown> productBreakDowns { get; set; }
        public List<AIFile> AIFiles { get; set; }       
        public ModelIssue ModelIssue { get; set; }
        public int ModelIssueID { get; set; }
        public string ModelIssueStrongPoint { get; set; }
        public string ModelIssueWeakPoint { get; set; }

        public ModelIssueTrack ModelIssueTrack { get; set; }
        public List<ModelIssueTrack> ModelIssueTracks { get; set; }

        public List<DTO.ModelMng.ModelFixPriceConfiguration> ModelFixPriceConfigurations { get; set; }
        public List<DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO> ModelPurchasingFixPriceConfigurationDTOs { get; set; }    
        public List<DTO.ModelMng.ModelPriceConfiguration> ModelPriceConfigurations { get; set; }       
        public List<DTO.ModelMng.ModelPurchasingPriceConfigurationDTO> ModelPurchasingPriceConfigurationDTOs { get; set; }
        public List<DTO.ModelMng.ModelPriceStatusDTO> ModelPriceStatusDTOs { get; set; }
        public List<DTO.ModelMng.ModelPurchasingPriceStatusDTO> ModelPurchasingPriceStatusDTOs { get; set; }

        public int? TotalFactoryOrderItem { get; set; }

        /*
         * default option         
         */
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public int? BackCushionID { get; set; }
        public int? SeatCushionID { get; set; }
        public int? CushionColorID { get; set; }
        public int? FSCTypeID { get; set; }
        public int? FSCPercentID { get; set; }

        public string MaterialText { get; set; }
        public string FrameText { get; set; }
        public string SubMaterialText { get; set; }
        public string CushionText { get; set; }
        public string FSCText { get; set; }
        public string MaterialThumbnailLocation { get; set; }
        public string FrameMaterialThumbnailLocation { get; set; }
        public string SubMaterialColorThumbnailLocation { get; set; }
        public string CushionColorThumbnailLocation { get; set; }
        public string BackCushionThumbnailLocation { get; set; }
        public string SeatCushionThumbnailLocation { get; set; }

        // Default Factory in Model.
        public int? DefaultFactoryID { get; set; }
        public string DefaultFactoryUD { get; set; }

        public List<ModelDefaultFactory> ModelDefaultFactories { get; set; }
        public List<ModelDefaultOptionDTO> ModelDefaultOptionDTOs { get; set; }
        public List<ConfigPriceDTO> ConfigPriceDTOs { get; set; }
        public string SeasonDefaultOption { get; set; }
    }
}
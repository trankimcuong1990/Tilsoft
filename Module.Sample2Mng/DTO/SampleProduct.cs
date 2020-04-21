using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleProduct
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public int? RequestTypeID { get; set; }
        public string ETADestination { get; set; }
        public string ArticleDescription { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public int? ModelID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public int? SeatCushionID { get; set; }
        public string SeatCushionThickness { get; set; }
        public string SeatCushionSpecification { get; set; }
        public int? BackCushionID { get; set; }
        public string BackCushionThickness { get; set; }
        public string BackCushionSpecification { get; set; }
        public int? CushionColorID { get; set; }
        public int? DisplayIndex { get; set; }
        public int? Quantity { get; set; }
        public string Remark { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? NLSuggestedFactoryID { get; set; }
        public string NLSuggestedFactoryRemark { get; set; }
        public int? VNSuggestedFactoryID { get; set; }
        public string VNSuggestedFactoryRemark { get; set; }
        public string FactoryDeadline { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public int? LoadAbility20DC { get; set; }
        public int? LoadAbility40DC { get; set; }
        public int? LoadAbility40HC { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string QntInBox { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? SampleProductStatusID { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialTypeDescription { get; set; }
        public string MaterialColorDescription { get; set; }
        public string SubMaterialDescription { get; set; }
        public string SubMaterialColorDescription { get; set; }
        public string FrameMaterialDescription { get; set; }
        public string FrameMaterialColorDescription { get; set; }
        public string BackCushionDescription { get; set; }
        public string SeatCushionDescription { get; set; }
        public string CushionColorDescription { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorUD { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string MaterialUD { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorUD { get; set; }
        public string MaterialColorNM { get; set; }
        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorUD { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string SeatCushionUD { get; set; }
        public string SeatCushionNM { get; set; }
        public string BackCushionUD { get; set; }
        public string BackCushionNM { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public string RequestTypeNM { get; set; }
        public string NLSuggestedFactoryUD { get; set; }
        public string VNSuggestedFactoryUD { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public string SampleProductStatusNM { get; set; }
        public string StatusUpdatorName { get; set; }
        public string ModelThumbnailLocation { get; set; }
        public string ModelFileLocation { get; set; }
        public decimal? IndicatedPrice { get; set; }
        public string ItemUpdatorName { get; set; }
        public int ItemInfoUpdatedBy { get; set; }
        public string ItemInfoUpdatedDate { get; set; }
        public string FinishedImage { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }

        public string ItemPerBox { get; set; }
        public string BoxPerSet { get; set; }

        public bool IsTDNeeded { get; set; }
        public string InternRemark { get; set; }


        public string Material2UD { get; set; }
        public string Material2NM { get; set; }
        public string MaterialType2UD { get; set; }
        public string MaterialType2NM { get; set; }
        public string MaterialColor2UD { get; set; }
        public string MaterialColor2NM { get; set; }
        public string Material3UD { get; set; }
        public string Material3NM { get; set; }
        public string MaterialType3UD { get; set; }
        public string MaterialType3NM { get; set; }
        public string MaterialColor3UD { get; set; }
        public string MaterialColor3NM { get; set; }
        public int? Material2ID { get; set; }
        public string Material2Description { get; set; }
        public int? Material2TypeID { get; set; }
        public string Material2TypeDescription { get; set; }
        public int? Material2ColorID { get; set; }
        public string Material2ColorDescription { get; set; }
        public int? Material3ID { get; set; }
        public string Material3Description { get; set; }
        public int? Material3TypeID { get; set; }
        public string Material3TypeDescription { get; set; }
        public int? Material3ColorID { get; set; }
        public string Material3ColorDescription { get; set; }
        public bool? IsDevelopment { get; set; }
        public int? ProductBreakDownID { get; set; }

        public string FrameWeight { get; set; }
        public string WickerWeight { get; set; }
        public string RemarkPaperWrap { get; set; }
        public string PackagingOption { get; set; }
        public bool? IsPaperWrap { get; set; }
        public bool? IsCartonBox { get; set; }
        public int? PALProductBreakDownID { get; set; }

        public decimal FactoryBreakdownPriceUSD { get; set; }
        public decimal AVTBreakdownPriceUSD { get; set; }
        public int FactoryBreakdownID { get; set; }
        public int? SampleProductBreakdownID { get; set; }

        public int? PackagingMethodID { get; set; }
        public string PackagingMethodNM { get; set; }

        public string Description { get; set; }

        public string ProgressImageThumbnailLocation { get; set; }
        public string ProgressImageFileLocation { get; set; }
        public string ProgressImage { get; set; }
        public int? SampleProductMinuteID { get; set; }
        public int? SampleRemarkID { get; set; }
        public bool? IsBuildFromExistModel { get; set; }
        public int? DevelopmentTypeID { get; set; }
        public string DevelopmentTypeNM { get; set; }
        public bool? IsEurofarSampleCollection { get; set; }

        public List<DTO.SampleProductLocationDTO> SampleProductLocationDTOs { get; set; }
        public List<DTO.SampleRemark> SampleRemarks { get; set; }
        public List<DTO.SampleQARemark> SampleQARemarks { get; set; }
        public List<DTO.SampleProductMinute> SampleProductMinutes { get; set; }
        public List<DTO.SampleReferenceImage> SampleReferenceImages { get; set; }
        public List<DTO.SampleProgress> SampleProgresses { get; set; }
        public List<DTO.SampleTechnicalDrawing> SampleTechnicalDrawings { get; set; }
        public List<DTO.SampleProductSubFactory> SampleProductSubFactories { get; set; }
        public List<DTO.SamplePackaging> samplePackagings { get; set; }

        public List<DTO.SampleProductDimensionDTO> SampleProductDimensionDTOs { get; set; }
        public List<DTO.SampleProductPackagingMaterialDTO> SampleProductPackagingMaterialDTOs { get; set; }
        public List<DTO.SampleProductCartonBoxDTO> SampleProductCartonBoxDTOs { get; set; }

        public SampleProduct()
        {
            SampleProductLocationDTOs = new List<SampleProductLocationDTO>();
            SampleRemarks = new List<SampleRemark>();
            SampleQARemarks = new List<SampleQARemark>();
            SampleProductMinutes = new List<SampleProductMinute>();
            SampleReferenceImages = new List<SampleReferenceImage>();
            SampleProgresses = new List<SampleProgress>();
            SampleProductSubFactories = new List<SampleProductSubFactory>();
            samplePackagings = new List<SamplePackaging>();

            SampleProductDimensionDTOs = new List<SampleProductDimensionDTO>();
            SampleProductPackagingMaterialDTOs = new List<SampleProductPackagingMaterialDTO>();
            SampleProductCartonBoxDTOs = new List<SampleProductCartonBoxDTO>();
        }
    }
}

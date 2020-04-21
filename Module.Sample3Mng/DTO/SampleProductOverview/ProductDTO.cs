using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleProductOverview
{
    public class ProductDTO
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleProductStatusNM { get; set; }
        public string ArticleDescription { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialTypeDescription { get; set; }
        public string MaterialColorDescription { get; set; }
        public string Material2Description { get; set; }
        public string Material2TypeDescription { get; set; }
        public string Material2ColorDescription { get; set; }
        public string Material3Description { get; set; }
        public string Material3TypeDescription { get; set; }
        public string Material3ColorDescription { get; set; }
        public string BackCushionDescription { get; set; }
        public string BackCushionThickness { get; set; }
        public string BackCushionSpecification { get; set; }
        public string SeatCushionDescription { get; set; }
        public string SeatCushionThickness { get; set; }
        public string SeatCushionSpecification { get; set; }
        public string CushionColorDescription { get; set; }
        public int? Quantity { get; set; }
        public string ItemPerBox { get; set; }
        public string BoxPerSet { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }
        public bool IsTDNeeded { get; set; }
        public string VNSuggestedFactoryUD { get; set; }
        public string FactoryDeadline { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public int? LoadAbility20DC { get; set; }
        public int? LoadAbility40DC { get; set; }
        public int? LoadAbility40HC { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string QntInBox { get; set; }
        public decimal? IndicatedPrice { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string Updator { get; set; }
        public string UpdatedDate { get; set; }
        public int SampleOrderID { get; set; }

        public List<InternalRemarkDTO> InternalRemarkDTOs { get; set; }
        public List<ItemLocationDTO> ItemLocationDTOs { get; set; }
        public List<ProgressDTO> ProgressDTOs { get; set; }
        public List<QARemarkDTO> QARemarkDTOs { get; set; }
        public List<ReferenceImageDTO> ReferenceImageDTOs { get; set; }
        public List<SubFactoryDTO> SubFactoryDTOs { get; set; }
        public List<TechnicalDrawingDTO> TechnicalDrawingDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SampleMng
{
    public class SampleProduct
    {
        public int SampleProductID { get; set; }

        public int? SampleOrderID { get; set; }

        public int? RequestTypeID { get; set; }

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
        public int? Quantity { get; set; }
        public int? DisplayIndex { get; set; }
        public string Remark { get; set; }

        public bool? IsConfirmed { get; set; }

        public string ModelNM { get; set; }

        public string FrameMaterialNM { get; set; }

        public string FrameMaterialColorNM { get; set; }

        public string MaterialNM { get; set; }

        public string MaterialTypeNM { get; set; }

        public string MaterialColorNM { get; set; }

        public string SubMaterialNM { get; set; }

        public string SubMaterialColorNM { get; set; }

        public string SeatCushionNM { get; set; }

        public string BackCushionNM { get; set; }

        public string CushionColorNM { get; set; }

        public string RequestTypeNM { get; set; }

        public int? NLSuggestedFactoryID { get; set; }
        public string NLSuggestedFactoryRemark { get; set; }
        public int? VNSuggestedFactoryID { get; set; }
        public string VNSuggestedFactoryRemark { get; set; }
        public string FactoryDeadline { get; set; }
        public string NLSuggestedFactoryUD { get; set; }
        public string VNSuggestedFactoryUD { get; set; }

        public List<SampleProductMinute> SampleProductMinutes { get; set; }
        public List<SampleReferenceImage> SampleReferenceImages { get; set; }
        public List<SampleProgress> SampleProgresses { get; set; }
        public List<SampleTechnicalDrawing> SampleTechnicalDrawings { get; set; }
        public List<SampleRemark> SampleRemarks { get; set; }

        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        public string ConcurrencyFlag { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public int? LoadAbility20DC { get; set; }
        public int? LoadAbility40DC { get; set; }
        public int? LoadAbility40HC { get; set; }

        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string QntInBox { get; set; }

        public int SampleProductStatusID { get; set; }
        public string SampleProductStatusNM { get; set; }
        public int StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string StatusUpdatorName { get; set; }
        public string ETADestination { get; set; }

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
    }
}
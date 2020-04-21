using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class SpecificationOfProductDTO
    {
        public int ProductSpecificationID { get; set; }
        public string ProductOverallDimL { get; set; }
        public string ProductOverallDimW { get; set; }
        public string ProductOverallDimH { get; set; }
        public string ProductOverallWeight { get; set; }
        public string FrameTDCode { get; set; }
        public string FrameDimensionL { get; set; }
        public string FrameDimensionW { get; set; }
        public string FrameDimensionH { get; set; }
        public string FrameMaterial { get; set; }
        public string FrameColor { get; set; }
        public string FrameColorCode { get; set; }
        public string FrameWelding { get; set; }
        public string FrameWeight { get; set; }
        public string WoodenPartTDCode { get; set; }
        public string WoodenPartDimensionL { get; set; }
        public string WoodenPartDimensionW { get; set; }
        public string WoodenPartDimensionH { get; set; }
        public string WoodenPartSpecies { get; set; }
        public string WoodenPartColor { get; set; }
        public string WoodenPartSnail { get; set; }
        public string WickerColorCode { get; set; }
        public string WickerType { get; set; }
        public string WickerColor { get; set; }
        public string WickerWeight { get; set; }
        public string WeavingMethod { get; set; }
        public string TextilenDimensionL { get; set; }
        public string TextilenDimensionW { get; set; }
        public string TextilenColor { get; set; }
        public string CushionColor { get; set; }
        public string CushionWeight { get; set; }
        public string CushionDimensionL { get; set; }
        public string CushionDimensionW { get; set; }
        public string CushionDimensionH { get; set; }
        public string CushionLine { get; set; }
        public string CushionWashingLabelDimL { get; set; }
        public string CushionWashingLabelDimW { get; set; }
        public string CartonBoxTDCode { get; set; }
        public string CartonBoxType { get; set; }
        public string CartonBoxDimensionL { get; set; }
        public string CartonBoxDimensionW { get; set; }
        public string CartonBoxDimensionH { get; set; }
        public string HardwareBoltQuantity { get; set; }
        public string HardwareBoltDimension { get; set; }
        public string HardwareKeyDimensionL { get; set; }
        public string HardwareKeyDimensionW { get; set; }
        public string HardwareKeyThickness { get; set; }
        public string HardwareRing { get; set; }
        public string HardwareSpring { get; set; }
        public string HardwareConnector { get; set; }
        public string HardwareLeveller { get; set; }
        public string AssemblyInstructionCode { get; set; }
        public string OtherDescription { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public int? FactoryID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? SampleProductID { get; set; }
        public string ProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string UpdatorName { get; set; }
        public string FrameCoatingColor { get; set; }
        public int? ProductID { get; set; }
        public int? ClientID { get; set; }
        public int? ModelID { get; set; }

        public List<DTO.SpecificationCushionImageDTO> specificationCushionImageDTOs { get; set; }
        public List<DTO.SpecificationImageDTO> specificationImageDTOs { get; set; }
        public List<DTO.SpecificationPackingDTO> specificationPackingDTOs { get; set; }
        public List<DTO.SpecificationWeavingFileDTO> specificationWeavingFileDTOs { get; set; }
        public List<DTO.SpecificationWoodenartDTO> specificationWoodenartDTOs { get; set; }
        public List<DTO.PackingSpecificationDTO> packingSpecificationDTOs { get; set; }
        public List<DTO.ClientOfProductDTO> clientOfProductDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class EditFormData
    {
        public DTO.SampleOrder Data { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.SamplePurpose> SamplePurposes { get; set; }
        public List<Support.DTO.SampleTransportType> SampleTransportTypes { get; set; }
        public List<Support.DTO.User> Users { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.SampleRequestType> SampleRequestTypes { get; set; }
        public List<Support.DTO.SampleProductStatus> SampleProductStatuses { get; set; }
        public List<Support.DTO.SampleOrderStatus> SampleOrderStatuses { get; set; }
        public List<DTO.PackagingMethodDTO> PackagingMethods { get; set; }
        public List<UnitDTO> UnitDTOs { get; set; }
        public List<SampleProductPackagingMaterialTypeDTO> SampleProductPackagingMaterialTypeDTOs { get; set; }
        public List<DTO.DevelopmentTypeDTO> developmentTypeDTOs { get; set; }

        //        data.Models = new List<DTO.Support.Model>();
        //data.Materials = new List<DTO.Support.Material>();
        //data.MaterialTypes = new List<DTO.Support.MaterialTypeOptionRaw>();
        //data.MaterialColors = new List<DTO.Support.MaterialColorOptionRaw>();
        //data.FrameMaterials = new List<DTO.Support.FrameMaterial>();
        //data.FrameMaterialColors = new List<DTO.Support.FrameMaterialColor>();
        //data.SubMaterials = new List<DTO.Support.SubMaterial>();
        //data.SubMaterialColors = new List<DTO.Support.SubMaterialColor>();
        //data.BackCushions = new List<DTO.Support.BackCushionOptionRaw>();
        //data.SeatCushions = new List<DTO.Support.SeatCushionOptionRaw>();
        //data.CushionColors = new List<DTO.Support.CushionColorOptionRaw>();
    }
}

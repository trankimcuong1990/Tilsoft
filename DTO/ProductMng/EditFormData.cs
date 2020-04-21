using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class EditFormData
    {
        public DTO.ProductMng.Product Data { get; set; }
        public List<ModelPackagingMethodOption2DTO> ModelPackagingMethodOption2DTOs { get; set; }
        public List<DTO.Support.ModelPackagingMethodOption> PackagingMethodOptions { get; set; }
        public List<Support.Season> Seasons { get; set; }
        public List<Package> Packages { get; set; }

        public List<DTO.Support.Material> Materials { get; set; }
        public List<DTO.Support.FrameMaterial> FrameMaterials { get; set; }
        public List<DTO.Support.MaterialType> MaterialTypes { get; set; }
        public List<DTO.Support.FSCType> FSCTypes { get; set; }

        public List<DTO.Support.MaterialColor> MaterialColors { get; set; }
        public List<DTO.Support.FrameMaterialColor> FrameMaterialColors { get; set; }
        public List<DTO.Support.CushionColor> CushionColors { get; set; }
        public List<DTO.Support.CushionType> CushionTypes { get; set; }
    }

}

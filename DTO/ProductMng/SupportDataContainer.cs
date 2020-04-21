using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class SupportDataContainer
    {
        public List<DTO.Support.ModelFrameMaterialOption> FrameOptions { get; set; }
        public List<DTO.Support.ModelMaterialOption> MaterialOptions { get; set; }
        public List<DTO.Support.CushionOption> CushionOptions { get; set; }
        public List<DTO.Support.ManufacturerCountry> ManufacturerCountries { get; set; }
        public List<Support.Season> Seasons { get; set; }
        public List<Support.ProductType> ProductTypes { get; set; }
        public List<Support.YesNo> ConfirmStatuses { get; set; }
    }
}

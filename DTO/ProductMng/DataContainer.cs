using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class DataContainer
    {
        public Product ProductData { get; set; }
        public List<DTO.Support.ModelFrameMaterialOption> FrameOptions { get; set; }
        public List<DTO.Support.ModelMaterialOption> MaterialOptions { get; set; }
        public List<DTO.Support.ModelCushionOption> CushionOptions { get; set; }
        public List<DTO.Support.ManufacturerCountry> ManufacturerCountries { get; set; }

        public List<DTO.Support.Season> Seasons { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration.DTO
{
    public class PriceConfigurationDto
    {
        public int PriceConfigurationID { get; set; }
        public int? ProductElementID { get; set; }
        public string DisplayText { get; set; }
        public string Season { get; set; }
        public int? UpdatedBy { get; set; }
        public string FullName { get; set; }
        public string EmployeeName { get; set; }
        public string UpdatedDate { get; set; }

        public List<PriceConfigurationDetailDto> PriceConfigurationDetailOfFCS { get; set; }
        public List<PriceConfigurationDetailDto> PriceConfigurationDetailOfMaterialColor { get; set; }
        public List<PriceConfigurationDetailDto> PriceConfigurationDetailOfFrameMaterial { get; set; }
        public List<PriceConfigurationDetailDto> PriceConfigurationDetailOfCushionColor { get; set; }
        public List<PriceConfigurationDetailDto> PriceConfigurationDetailOfPackingMethod { get; set; }
    }
}

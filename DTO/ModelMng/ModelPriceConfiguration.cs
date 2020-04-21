using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ModelPriceConfiguration
    {
        public int ModelPriceConfigurationID { get; set; }
        public string Season { get; set; }
        public int? ProductElementID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string ProductElementNM { get; set; }

        public List<DTO.ModelMng.ModelPriceConfigurationDetail> ModelPriceConfigurationDetails { get; set; }
    }
}

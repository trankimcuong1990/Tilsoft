using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class CreateModelEditData
    {
        public Model Data { get; set; }

        public List<ModelPriceConfiguration> ModelPriceConfigurationDefault { get; set; }

        public bool IsPriceEnabled { get; set; }

        public bool CanApprove { get; set; }
    }
}

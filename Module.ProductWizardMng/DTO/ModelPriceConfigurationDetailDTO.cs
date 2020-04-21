using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class ModelPriceConfigurationDetailDTO
    {
        public int ModelPriceConfigurationDetailID { get; set; }
        public Nullable<int> OptionID { get; set; }
        public Nullable<decimal> PercentValue { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
        public Nullable<decimal> EURAmount { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ProductElementID { get; set; }
    }
}

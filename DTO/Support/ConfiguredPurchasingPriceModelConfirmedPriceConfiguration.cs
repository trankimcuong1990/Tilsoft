using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ConfiguredPurchasingPriceModelConfirmedPriceConfiguration
    {
        public int ModelPurchasingPriceConfigurationDetailID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ProductElementID { get; set; }
        public Nullable<int> OptionID { get; set; }
        public Nullable<decimal> PercentValue { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
    }
}

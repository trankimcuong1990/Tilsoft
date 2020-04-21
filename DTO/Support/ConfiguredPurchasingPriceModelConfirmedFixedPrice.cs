using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ConfiguredPurchasingPriceModelConfirmedFixedPrice
    {
        public int ModelPurchasingFixPriceConfigurationID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
    }
}

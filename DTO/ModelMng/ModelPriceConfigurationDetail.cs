using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ModelPriceConfigurationDetail
    {
        public int ModelPriceConfigurationDetailID { get; set; }
        public int ModelPriceConfigurationID { get; set; }        
        public int? OptionID { get; set; }
        public decimal? PercentValue { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public string OptionNM { get; set; }

        public int ModelPurchasingPriceConfigurationDetailID { get; set; }
        public int? ModelPurchasingPriceConfigurationID { get; set; }     
        public decimal? PurchasingPercentValue { get; set; }
        public decimal? PurchasingUSDAmount { get; set; }       
    }
}

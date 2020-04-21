using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ModelPriceConfigurationDetail
    {
        public int ModelPriceConfigurationDetailID { get; set; }
        public int? OptionID { get; set; }
        public decimal? PercentValue { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public int? ModelID { get; set; }
        public string Season { get; set; }
        public int? ProductElementID { get; set; }
    }
}

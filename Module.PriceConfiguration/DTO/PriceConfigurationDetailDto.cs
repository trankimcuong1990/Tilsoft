using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration.DTO
{
    public class PriceConfigurationDetailDto
    {
        public int PriceConfigurationDetailID { get; set; }
        public int? PriceConfigurationID { get; set; }
        public int? OptionID { get; set; }
        public string OptionNM { get; set; }
        public decimal? PercentValue { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public string Season { get; set; }
    }
}

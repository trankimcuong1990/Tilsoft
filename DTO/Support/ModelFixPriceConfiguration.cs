using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ModelFixPriceConfiguration
    {
        public int ModelFixPriceConfigurationID { get; set; }
        public int? ModelID { get; set; }
        public string Season { get; set; }
        public int? MaterialTypeID { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
        public decimal? ExRate { get; set; }
    }
}

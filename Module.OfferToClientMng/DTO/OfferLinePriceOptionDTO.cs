using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferToClientMng.DTO
{
    public class OfferLinePriceOptionDTO
    {
        public int OfferLinePriceOptionID { get; set; }
        public int? OfferLineID { get; set; }
        public int? ProductElementID { get; set; }
        public int? ModelPriceConfigurationDetailID { get; set; }
        public decimal? IncreasePercent { get; set; }
        public decimal? IncreaseAmount { get; set; }
        public string ProductElementNM { get; set; }
        public int? OptionID { get; set; }
        public string OptionNM { get; set; }
        public decimal? PercentValue { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? EURAmount { get; set; }
    }
}

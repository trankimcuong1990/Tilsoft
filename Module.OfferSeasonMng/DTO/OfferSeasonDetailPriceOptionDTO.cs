using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferSeasonDetailPriceOptionDTO
    {
        public int OfferSeasonDetailPriceOptionID { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public Nullable<int> ProductElementID { get; set; }
        public Nullable<int> ModelPriceConfigurationDetailID { get; set; }
        public Nullable<decimal> IncreasePercent { get; set; }
        public Nullable<decimal> IncreaseAmount { get; set; }
        public string ProductElementNM { get; set; }
        public Nullable<int> OptionID { get; set; }
        public string OptionNM { get; set; }
        public Nullable<decimal> PercentValue { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
        public Nullable<decimal> EURAmount { get; set; }
    }
}

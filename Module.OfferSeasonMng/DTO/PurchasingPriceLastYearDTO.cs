using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class PurchasingPriceLastYearDTO
    {
        public int OfferSeasonDetailID { get; set; }
        public Nullable<decimal> PurchasingPriceIncludeDiff { get; set; }
        public string FactoryUD { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class PlaningPurchasingPriceSourceDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public string PlaningPurchasingPriceSourceNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}

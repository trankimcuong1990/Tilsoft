using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class PlanningPurchasingPriceDTO
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<decimal> PurchasingPrice { get; set; }
        public int? PlaningPurchasingPriceSourceID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class PlaningPurchasingPriceData
    {
        public List<DTO.PlanningPurchasingPriceDTO> PlanningPurchasingPriceDTOs { get; set; }
        public List<DTO.FactoryPendingPriceDTO> FactoryPendingPriceDTOs { get; set; }
    }
}

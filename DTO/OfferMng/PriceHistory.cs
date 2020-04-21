using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class PriceHistory
    {
        public List<OfferLineSalePriceHistoryDTO> offerLineSalePriceHistoryDTOs { get; set; }
        public List<OfferLineSalePriceHistoryLastSeasonDTO> offerLineSalePriceHistoryLastSeasonDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Offer
{
    public class DataContainer
    {
        //public List<OfferOverview> OfferDatas { get; set; }
        public List<OfferMarginDTO> offerMarginDTOs { get; set; }
        public string Season { get; set; }
        public decimal ExRate { get; set; }
        public List<PlaningPurchasingPriceSourceDTO> planingPurchasingPriceSourceDTOs { get; set; }
    }
}

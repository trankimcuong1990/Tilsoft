using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class EditFormData
    {
        public OfferSeasonDTO OfferSeasonDTO { get; set; }
        public List<OfferSeasonDetailDTO> OfferSeasonDetailDTOs { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<object> VAT { get; set; }
        public List<object> Currencies { get; set; }
        public List<DTO.PlaningPurchasingPriceSourceDTO> PlaningPurchasingPriceSourceDTOs { get; set; }
        public List<ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCostDTOs { get; set; }
        public List<FactoryDTO> FactoryDTOs { get; set; }
        public List<OfferSeasonItemStatusDTO> OfferSeasonItemStatusDTOs { get; set; }
        public List<ProductElementDTO> ProductElementDTOs { get; set; }
        public List<DTO.SalePriceTableLastSeason> SalePriceTableLastSeasons { get; set; }
        public List<DTO.ImageGalleryDTO> ImageGalleryDTOs { get; set; }
        public List<DTO.PurchasingPriceLastYearDTO> PurchasingPriceLastYearDTOs { get; set; }
        public List<DTO.RelatedFactoryOrderDetailDTO> RelatedFactoryOrderDetailDTOs { get; set; }
        public MasterSettingDTO MasterSettingDTO { get; set; }
        public EditFormData() {
            OfferSeasonDTO = new OfferSeasonDTO();
            OfferSeasonDetailDTOs = new List<OfferSeasonDetailDTO>();
            ClientEstimatedAdditionalCostDTOs = new List<ClientEstimatedAdditionalCostDTO>();
        }
    }
}

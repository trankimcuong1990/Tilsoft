using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class SupportDataContainer
    {
        public List<Support.Season> Seasons { get; set; }
        public List<Support.Saler> Sales { get; set; }
        public List<Support.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.POD> PODs { get; set; }
        public List<Support.Currency> Currency { get; set; }
        public List<Support.VATPercent> VATPercents { get; set; }
        public List<Support.LabelingType> LabelingTypes { get; set; }
        public List<Support.PackagingType> PackagingTypes { get; set; }
        public List<Support.Forwarder> Forwarders { get; set; }
        public List<Support.PutInProductionTerm> PutInProductionTerms { get; set; }
        public List<Support.Factory> factories { get; set; }

        //support list for offer line
        public List<OfferMng.RatePercent> CommissionPercents { get; set; }
        public List<Support.POL> POLs { get; set; }
        public List<Support.ModelStatus> ModelStatuses { get; set; }
        public List<Support.ProductElement> ProductElements { get; set; }
        public List<DTO.OfferMng.OfferStatusDTO> offerStatusDTOs { get; set; }
        public List<DTO.OfferMng.PlaningPurchasingPriceSourceDTO> PlaningPurchasingPriceSourceDTOs { get; set; }
        public List<DTO.OfferMng.ExchangeRateDTO> ExchangeRateDTOs { get; set; }

        public List<DTO.OfferMng.ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCostDTOs { get; set; }
    }
}

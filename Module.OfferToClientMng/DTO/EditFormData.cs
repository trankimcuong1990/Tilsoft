using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferToClientMng.DTO
{
    public class EditFormData
    {
        public OfferDTO OfferDTO { get; set; }
        public List<ExchangeRateDTO> ExchangeRateDTOs { get; set; }
        public List<PaymentTermDTO> PaymentTermDTOs { get; set; }
        public List<DeliveryTermDTO> DeliveryTermDTOs { get; set; }
        public List<CurrencyDTO> CurrencyDTOs { get; set; }
        public List<VATPercentDTO> VATPercentDTOs { get; set; }
        public List<LabelingTypeDTO> LabelingTypeDTOs { get; set; }
        public List<PackagingTypeDTO> PackagingTypeDTOs { get; set; }
        public List<ForwarderDTO> ForwarderDTOs { get; set; }
        public List<PutInProductionTermDTO> PutInProductionTermDTOs { get; set; }
        public List<ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCostDTOs { get; set; }
        public List<InterestPercentDTO> InterestPercentDTOs { get; set; }
    }
}

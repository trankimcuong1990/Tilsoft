using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class EditFormData
    {
        public ClientOffer Data { get; set; }
        public List<Module.Support.DTO.Client> Clients { get; set; }
        public List<Module.Support.DTO.Currency> Currencies { get; set; }
        public List<Module.Support.DTO.POL> POLs { get; set; }
        public List<Module.Support.DTO.POD> PODs { get; set; }
        public List<DTO.ClientCostItemDTO> ClientCostItemDTOs { get; set; }
        public List<DTO.ClientConditionItemDTO> ClientConditionItemDTOs { get; set; }
        public List<DTO.ClientAdditionalItemDTO> ClientAdditionalItemDTOs { get; set; }
        public List<DTO.ClientOfferAdditionalDetailDTO> ClientOfferAdditionalDetailDTOs { get; set; }
        public List<DTO.ClientOfferConditionDetailDTO> ClientOfferConditionDetailDTOs { get; set; }
        public List<Module.Support.DTO.TransportCostType> ClientCostTypes { get; set; }
        public List<Module.Support.DTO.TransportCostChargeType> ClientCostChargeTypes { get; set; }

        public List<Support.DTO.PaymentTerm> PaymentTerms { get; set; }
    }
}

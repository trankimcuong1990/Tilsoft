using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer.DTO
{
    public class EditFormData
    {
        public TransportOfferDTO Data { get; set; }
        public List<Module.Support.DTO.Forwarder> Forwarders { get; set; }
        public List<Module.Support.DTO.Currency> Currencies { get; set; }
        public List<Module.Support.DTO.POL> POLs { get; set; }
        public List<Module.Support.DTO.POD> PODs { get; set; }
        public List<DTO.TransportCostItemDTO> TransportCostItemDTOs { get; set; }
        public List<DTO.TransportConditionItemDTO> TransportConditionItemDTOs { get; set; }
        public List<Module.Support.DTO.TransportCostType> TransportCostTypes { get; set; }
        public List<Module.Support.DTO.TransportCostChargeType> TransportCostChargeTypes { get; set; }

    }
}

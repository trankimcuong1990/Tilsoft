using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class EditFormData
    {
        public TransportInvoiceDTO Data { get; set; }
        public List<LoadingPlanDTO> LoadingPlanDTOs { get; set; }
        public List<TransportCostItemDTO> TransportCostItemDTOs { get; set; }
        public List<Support.DTO.Currency> Currencies { get; set; }
        public List<Module.Support.DTO.TransportCostType> TransportCostTypes { get; set; }
        public List<Module.Support.DTO.TransportCostChargeType> TransportCostChargeTypes { get; set; }
        public List<InitCostItemDTO> InitCostItems { get; set; }
    }
}

using System.Collections.Generic;

namespace Module.PurchaseRequestMng.DTO
{
    public class InitFormDTO
    {
        public List<DTO.PurchaseRequestStatusDTO> PurchaseRequestStatuses { get; set; }
        public List<DTO.ProductionItemGroupDTO> ProductionItemGroups { get; set; }

        public InitFormDTO()
        {
            PurchaseRequestStatuses = new List<PurchaseRequestStatusDTO>();
            ProductionItemGroups = new List<ProductionItemGroupDTO>();
        }
    }
}

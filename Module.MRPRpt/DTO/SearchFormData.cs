using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MRPRpt.DTO
{
    public class SearchFormData
    {
        public List<DTO.MRPSearchFormData> Data { get; set; }
        public List<DTO.PurchaseOrderDetailMRP> purchaseOrderDetailMRPs { get; set; }
        public List<DTO.PurchaseRequestDetailMRP> purchaseRequestDetailMRPs { get; set; }
       // public List<DTO.SupportProductionItemGroupSearchView> supportProductionItemGroupSearchViews { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class SupportFormData
    {
        public List<QuotationStatusDTO> QuotationStatusDTOs { get; set; }
        public List<OrderTypeDTO> OrderTypeDTOs { get; set; }
        public List<EurofarBreakdownCategoryDTO> EurofarBreakdownCategoryDTOs { get; set; }
        public List<PricingTeamMemberInChargeDTO> PricingTeamMemberInChargeDTOs { get; set; }
        public decimal? Exchange { get; set; }
        public bool? isPermissionSeeAllPurchasingData { get; set; }

    }
}

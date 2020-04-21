using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Breakdown
{
    public class SupportDataDTO
    {
        public List<DTO.ClientMng.Overview.Breakdown.QuotationStatusDTO> quotationStatusDTOs { get; set; }
        public List<DTO.ClientMng.Overview.Breakdown.OrderTypeDTO> orderTypeDTOs { get; set; }
        public List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryDTO> breakdownCategoryDTOs { get; set; }

        public decimal? Exchange { get; set; }
    }
}

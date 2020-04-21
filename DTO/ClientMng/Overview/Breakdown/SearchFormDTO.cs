using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Breakdown
{
    public class SearchFormDTO
    {
        public DTO.ClientMng.Overview.Breakdown.SubTotalDTO SubTotalRow { get; set; }
        public DTO.ClientMng.Overview.Breakdown.SubTotalDTO TotalRow { get; set; }

        public int TotalItem { get; set; }
        public int TotalConfirmedItem { get; set; }
        public int TotalPendingItem
        {
            get
            {
                return TotalItem - TotalConfirmedItem;
            }
        }
        public int TotalWaitForEurofar { get; set; }
        public int TotalWaitForFactory
        {
            get
            {
                return TotalPendingItem - TotalWaitForEurofar;
            }
        }

        public List<DTO.ClientMng.Overview.Breakdown.FactoryQuotationSeachDTO> Data { get; set; }
        public List<DTO.ClientMng.Overview.Breakdown.BreakdownCategoryOptionDTO> breakdownCategoryOptionDTOs { get; set; }
        public List<DTO.ClientMng.Overview.Breakdown.BreakdownManagementFeeDTO> breakdownManagementFeeDTOs { get; set; }
        public List<DTO.ClientMng.Overview.Breakdown.BreakdownSeasonSpecPercentDTO> breakdownSeasonSpecPercentDTOs { get; set; }


    }
}

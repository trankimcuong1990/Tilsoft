using System.Collections.Generic;

namespace Module.BreakDownMng.DTO
{
    public class GetPriceDetailDTO
    {
        public List<DTO.BreakDownBreakdownCategoryDTO> BreakDownBreakdownCategoryDTOs { get; set; }    
        public List<DTO.BreakdownPriceHistoryDTO> BreakdownPriceHistoryDTOs { get; set; }    
    }
}

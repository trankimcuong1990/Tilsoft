using System.Collections.Generic;

namespace Module.BreakDownMng.DTO
{
    public class BreakDownBreakdownCategoryDTO
    {
        public int BreakdownCategoryID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public int BreakdownCategoryOptionID { get; set; }
        public string OptionName { get; set; }
        public List<BreakDownPriceDefaultDTO> BreakDownPriceDefaultDTOs { get; set; }
    }
}

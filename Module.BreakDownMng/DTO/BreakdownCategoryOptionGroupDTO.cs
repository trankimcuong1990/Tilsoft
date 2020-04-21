using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownCategoryOptionGroupDTO
    {
        public int BreakdownCategoryOptionGroupID { get; set; }
        public int BreakdownCategoryOptionID { get; set; }
        public string BreakdownCategoryOptionGroupNM { get; set; }
        public string BreakdownCategoryOptionGroupNMEN { get; set; }
        public int Quantity { get; set; }
    }
}

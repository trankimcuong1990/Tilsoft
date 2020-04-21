using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownCategoryDTO
    {
        public int BreakdownCategoryID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public string BreakdownCategoryNMEN { get; set; }
        public int? DisplayOrder { get; set; }
    }
}

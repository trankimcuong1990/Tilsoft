using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class BreakdownCategory
    {
        public int BreakdownCategoryID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public int DisplayOrder { get; set; }
        public int ProductElementID { get; set; }

    }
}

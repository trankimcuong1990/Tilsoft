using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class BreakdownCategoryDTO
    {
        public int BreakdownCategoryID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public int ProductElementID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class EurofarBreakdownCategoryDTO
    {
        public int BreakdownCategoryID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}

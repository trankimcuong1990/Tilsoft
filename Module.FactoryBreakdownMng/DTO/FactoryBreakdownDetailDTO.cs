using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng.DTO
{
    public class FactoryBreakdownDetailDTO
    {
        public int FactoryBreakdownDetailID { get; set; }

        public string UnitNM { get; set; }
        public decimal? Quantity { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public string Remark { get; set; }
    }
}

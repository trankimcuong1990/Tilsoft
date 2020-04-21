using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderNorm.DTO
{
    public class FactoryFinishedProductOrderNormFactoryGoodsProcedure
    {
        public int FactoryFinishedProductOrderNormFactoryGoodsProcedureID { get; set; }
        public int? FactoryFinishedProductOrderNormID { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public bool? IsDefault { get; set; }
    }
}

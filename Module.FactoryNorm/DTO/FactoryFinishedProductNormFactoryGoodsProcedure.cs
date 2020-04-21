using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm.DTO
{
    public class FactoryFinishedProductNormFactoryGoodsProcedure
    {
        public int FactoryFinishedProductNormFactoryGoodsProcedureID { get; set; }
        public int? FactoryFinishedProductNormID { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public bool? IsDefault { get; set; }
    }
}

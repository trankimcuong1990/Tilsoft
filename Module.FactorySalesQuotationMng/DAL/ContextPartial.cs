using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesQuotationMng.DAL
{
    public partial class FactorySalesQuotationMngEntities
    {
        public FactorySalesQuotationMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

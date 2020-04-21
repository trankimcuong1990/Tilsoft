using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng.DAL
{
    public partial class FactoryQuotationMngEntities
    {
        public FactoryQuotationMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

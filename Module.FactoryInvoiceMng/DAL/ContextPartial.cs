using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DAL
{
    public partial class FactoryInvoiceMngEntities
    {
        public FactoryInvoiceMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

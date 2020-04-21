using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DAL
{
    public partial class FactorySaleInvoiceEntities
    {
        public FactorySaleInvoiceEntities(string iConnectionString)
           : base(iConnectionString)
        { }
    }
}

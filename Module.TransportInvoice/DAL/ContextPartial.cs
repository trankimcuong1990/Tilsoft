using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DAL
{
    public partial class TransportInvoiceEntities
    {
        public TransportInvoiceEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

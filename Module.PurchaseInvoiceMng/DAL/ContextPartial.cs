using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DAL
{
    public partial class PurchaseInvoiceMngEntities
    {
        public PurchaseInvoiceMngEntities(string iConnectionString)
           : base(iConnectionString)
        { }
    }
}

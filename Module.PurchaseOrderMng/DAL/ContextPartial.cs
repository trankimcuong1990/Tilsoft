using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DAL
{
    public partial class PurchaseOrderMngEntities
    {
        public PurchaseOrderMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

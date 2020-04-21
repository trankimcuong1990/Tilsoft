using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DAL
{
    public partial class PurchaseRequestMngEntities
    {
        public PurchaseRequestMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierMng.DAL
{
    public partial class SupplierMngEntities
    {
        public SupplierMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

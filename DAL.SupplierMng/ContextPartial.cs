using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SupplierMng
{
    public partial class SupplierMngEntities
    {
        public SupplierMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

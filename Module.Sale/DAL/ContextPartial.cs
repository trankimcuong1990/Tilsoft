using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sale.DAL
{
    public partial class SaleEntities
    {
        public SaleEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

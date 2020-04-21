using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesOrderMng.DAL
{
    public partial class FactorySalesOrderMngEntities
    {
        public FactorySalesOrderMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

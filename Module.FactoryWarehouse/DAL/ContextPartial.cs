using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DAL
{
    public partial class FactoryWarehouseEntities
    {
        public FactoryWarehouseEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

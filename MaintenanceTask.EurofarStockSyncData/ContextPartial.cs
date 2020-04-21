using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData
{
    public partial class EurofarStockEntities
    {
        public EurofarStockEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

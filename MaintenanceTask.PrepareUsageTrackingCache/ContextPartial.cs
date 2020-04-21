using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.PrepareUsageTrackingCache
{
    public partial class UsageTrackingEntities
    {
        public UsageTrackingEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DAL
{
    public partial class UsageTrackingRptEntities
    {
        public UsageTrackingRptEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

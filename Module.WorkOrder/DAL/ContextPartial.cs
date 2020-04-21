using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DAL
{
    public partial class WorkOrderEntities
    {
        public WorkOrderEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

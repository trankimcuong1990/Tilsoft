using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DAL
{
    public partial class AgentsEntities
    {
        public AgentsEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

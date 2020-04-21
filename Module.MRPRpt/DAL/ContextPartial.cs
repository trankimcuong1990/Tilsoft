using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MRPRpt.DAL
{
    public partial class MRPEntities
    {
        public MRPEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

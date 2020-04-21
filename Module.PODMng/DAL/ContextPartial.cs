using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PODMng.DAL
{
    public partial class PODMngEntities
    {
        public PODMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

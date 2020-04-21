using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DAL
{
    public partial class FactoryPLCMngEntities
    {
        public FactoryPLCMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

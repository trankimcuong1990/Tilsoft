using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng.DAL
{
    public partial class FactoryBreakdownMngEntities
    {
        public FactoryBreakdownMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DAL
{
    public partial class FactoryLoadingPlanMngEntities
    {
        public FactoryLoadingPlanMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

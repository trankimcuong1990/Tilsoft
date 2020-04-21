using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DAL
{
    public partial class FactoryProductionStatusEntities
    {
        public FactoryProductionStatusEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

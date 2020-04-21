using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderNorm.DAL
{
    public partial class FactoryOrderNormEntities
    {
        public FactoryOrderNormEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm.DAL
{
    public partial class FactoryNormEntities
    {
        public FactoryNormEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

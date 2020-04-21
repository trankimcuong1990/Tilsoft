using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySpecificationMng.DAL
{
    public partial class FactorySpecificationMngEntities
    {
        public FactorySpecificationMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

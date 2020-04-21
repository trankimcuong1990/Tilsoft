using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterial.DAL
{
    public partial class FactoryMaterialEntities
    {
        public FactoryMaterialEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

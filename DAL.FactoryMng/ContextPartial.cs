using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FactoryMng
{
    public partial class FactoryMngEntities
    {
        public FactoryMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

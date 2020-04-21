using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryCreditNoteMng.DAL
{
    public partial class FactoryCreditNoteMngEntities
    {
        public FactoryCreditNoteMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

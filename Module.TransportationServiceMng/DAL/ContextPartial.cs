using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportationServiceMng.DAL
{
    public partial class TransportationServiceMngEntities
    {
        public TransportationServiceMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

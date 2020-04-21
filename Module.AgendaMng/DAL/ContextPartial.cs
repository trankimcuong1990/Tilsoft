using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AgendaMng.DAL
{
    public partial class AgendaMngEntities
    {
        public AgendaMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}

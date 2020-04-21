using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarServiceMng.DAL
{
    public partial class EurofarServiceEntities
    {
        public EurofarServiceEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ForwarderMng
{
    public partial class ForwarderMngEntities
    {
        public ForwarderMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DAL
{
    public partial class SupportEntities
    {
        public SupportEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

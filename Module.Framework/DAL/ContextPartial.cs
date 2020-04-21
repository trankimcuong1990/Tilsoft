using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Framework.DAL
{
    public partial class FrameworkEntities
    {
        public FrameworkEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

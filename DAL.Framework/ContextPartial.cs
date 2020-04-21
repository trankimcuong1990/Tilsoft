using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Framework
{
    public partial class FrameworkEntities
    {
        public FrameworkEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

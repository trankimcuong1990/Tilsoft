using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM.DAL
{
    public partial class BOMEntities
    {
        public BOMEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

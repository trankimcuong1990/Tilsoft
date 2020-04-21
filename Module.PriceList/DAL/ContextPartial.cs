using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceList.DAL
{
    public partial class PriceListEntities
    {
        public PriceListEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

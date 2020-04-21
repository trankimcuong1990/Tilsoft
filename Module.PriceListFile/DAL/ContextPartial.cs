using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceListFile.DAL
{
    public partial class PriceListFileEntities
    {
        public PriceListFileEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

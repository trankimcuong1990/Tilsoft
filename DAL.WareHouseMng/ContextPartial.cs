using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.WareHouseMng
{
    public partial class WareHouseMngEntities
    {
        public WareHouseMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

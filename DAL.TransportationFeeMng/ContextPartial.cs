using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TransportationFeeMng
{
    public partial class TransportationFeeEntities
    {
        public TransportationFeeEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

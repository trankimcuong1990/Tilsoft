using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FactoryPaymentMng
{
    public partial class FactoryPaymentMngEntities
    {
        public FactoryPaymentMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

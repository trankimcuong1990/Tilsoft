using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockReceipt.DAL
{
    public partial class FactoryStockReceiptEntities
    {
        public FactoryStockReceiptEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

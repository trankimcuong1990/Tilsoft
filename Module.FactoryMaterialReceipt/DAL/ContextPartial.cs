using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialReceipt.DAL
{
    public partial class FactoryMaterialReceiptEntities
    {
        public FactoryMaterialReceiptEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

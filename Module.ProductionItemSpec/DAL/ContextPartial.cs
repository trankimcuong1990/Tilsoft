using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemSpec.DAL
{
    public partial class ProductionItemSpecEntities
    {
        public ProductionItemSpecEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DAL
{
    public partial class ProductionItemEntities
    {
        public ProductionItemEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

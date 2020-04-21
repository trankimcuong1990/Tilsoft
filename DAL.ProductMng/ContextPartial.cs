using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ProductMng
{
    public partial class ProductMngEntities
    {
        public ProductMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

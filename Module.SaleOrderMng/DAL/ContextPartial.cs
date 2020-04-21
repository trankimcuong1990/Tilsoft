using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DAL
{
    public partial class SaleOrderMngEntities
    {
        public SaleOrderMngEntities(string iConnectionString) : base(iConnectionString) { }
    }

    public partial class SaleOrderReturnEntities
    {
        public SaleOrderReturnEntities(string iConnectionString) : base(iConnectionString) { }
    }

    public partial class ReturnGoodsFromSaleOrderEntities
    {
        public ReturnGoodsFromSaleOrderEntities(string iConnectionString) : base(iConnectionString) { }
    }
}

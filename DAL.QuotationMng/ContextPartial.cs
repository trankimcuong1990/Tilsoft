using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QuotationMng
{
    public partial class QuotationMngEntities
    {
        public QuotationMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

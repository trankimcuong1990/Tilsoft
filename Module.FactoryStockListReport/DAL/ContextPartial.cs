using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockListReport.DAL
{
    public partial class FactoryStockListReportEntities
    {
        public FactoryStockListReportEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}

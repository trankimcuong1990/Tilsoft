using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardFinanceData
    {
        public List<DashboardFinanceOverviewData> FinanceGridview { get; set; }
        public List<DashboardFinanceChart> FinanceChart { get; set; }
        public bool IsFactoryManufacturing { get; set; }
    }
}

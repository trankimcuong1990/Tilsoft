using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardDeltaChartDTO
    {
        public string Name { get; set; }
        public List<decimal> Data { get; set; }
    }
}

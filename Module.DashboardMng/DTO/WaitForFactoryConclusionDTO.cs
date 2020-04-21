using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class WaitForFactoryConclusionDTO
    {
        public string Season { get; set; }
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public int TotalWaitedWeeks { get; set; }
        public int TotalItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardDetalDTO
    {
        public decimal TotalPrice { get; set; }
        public int MonthData { get; set; }
        public int YearData { get; set; }
        public System.DateTime Date { get; set; }
        public long STT { get; set; }
        public int Type { get; set; }
    }
}

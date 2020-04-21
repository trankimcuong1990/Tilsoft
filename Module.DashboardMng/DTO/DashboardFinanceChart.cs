using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardFinanceChart
    {
        public int WeekIndex { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? TurnOver { get; set; }
        public decimal? TurnOverLastSeason { get; set; }
    }
}

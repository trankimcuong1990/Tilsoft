using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccManagerPerformanceRpt.DTO
{
    public class AccManagerPerformanceDeltaDTO
    {
        public int SaleID { get; set; }
        public string SaleUD { get; set; }
        public string Season { get; set; }
        public decimal? Delta6Percent { get; set; }
        public decimal? SaleAmountEUR { get; set; }
        public decimal? Delta6AmountEUR { get; set; }
    }
}

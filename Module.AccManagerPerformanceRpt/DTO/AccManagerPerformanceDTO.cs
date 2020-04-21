using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccManagerPerformanceRpt.DTO
{
    public class AccManagerPerformanceDTO
    {
        public int SaleID { get; set; }
        public string Season { get; set; }

        public decimal? ExpectationQnt40HC { get; set; }
        public decimal? ExpectationAmountUSD { get; set; }
        public decimal? ExpectationAmountEUR { get; set; }
        public decimal? ExpectationTotalEUR { get; set; }


        public decimal? PITotalQnt40HC { get; set; }
        public decimal? PITotalAmountUSD { get; set; }
        public decimal? PITotalAmountEUR { get; set; }
        public decimal? PITotalTotalEUR { get; set; }

        public decimal? PIConfirmedQnt40HC { get; set; }
        public decimal? PIConfirmedAmountUSD { get; set; }
        public decimal? PIConfirmedAmountEUR { get; set; }
        public decimal? PIConfirmedTotalEUR { get; set; }
        public decimal? PIConfirmedDelta6EUR { get; set; }
        public decimal? Delta6Percent { get; set; }
        public decimal? AVGDelta6Percent { get; set; }

        public decimal? CIQnt40HC { get; set; }
        public decimal? CIAmountUSD { get; set; }
        public decimal? CIAmountEUR { get; set; }
        public decimal? CITotalEUR { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class EurofarSeasonSpecPercentDTO
    {
        public int BreakdownID { get; set; }
        public int? ModelID { get; set; }
        public decimal? SeasonSpecPercent { get; set; }
    }
}

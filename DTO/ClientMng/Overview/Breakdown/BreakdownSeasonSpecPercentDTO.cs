using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Breakdown
{
    public class BreakdownSeasonSpecPercentDTO
    {
        public int BreakdownID { get; set; }
        public int? ModelID { get; set; }
        public decimal? SeasonSpecPercent { get; set; }
    }
}

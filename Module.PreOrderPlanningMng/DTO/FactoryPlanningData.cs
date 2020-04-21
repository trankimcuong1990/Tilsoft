using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningMng.DTO
{
    public class FactoryPlanningData
    {
        public int PrimaryID { get; set; }

        public int? Month { get; set; }

        public int? WeekIndex { get; set; }

        public string DisplayWeekStart { get; set; }

        public string DisplayWeekEnd { get; set; }

        public decimal? TotalCapacityByWeekly { get; set; }

        public decimal? TotalOfficalOrder { get; set; }

        public decimal? OfficialOrderScaledQnt { get; set; }

        public decimal? PreOrderQnt { get; set; }

        public decimal? PreOrderScaledQnt { get; set; }

        public decimal? PreProducedQnt { get; set; }

        public decimal? PreProducedScaledQnt { get; set; }

        public int FactoryPlanningID { get; set; }
    }
}

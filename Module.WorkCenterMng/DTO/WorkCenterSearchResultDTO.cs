using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class WorkCenterSearchResultDTO
    {
        public int? WorkCenterID { get; set; }

        public string WorkCenterUD { get; set; }

        public string WorkCenterNM { get; set; }

        public decimal? OperatingTime { get; set; }

        public decimal? DefaultCost { get; set; }

        public decimal? Capacity { get; set; }

        public int? ResponsibleBy { get; set; }

        public int? PlanningTime { get; set; }

        public int? WorkingTime { get; set; }

        public bool? IsVirtual { get; set; }

        public string Responsiblor { get; set; }

        public int PrimaryID { get; set; }
    }
}

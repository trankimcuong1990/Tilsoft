using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class ProductionTeam
    {
        public int ProductionTeamID { get; set; }
        public string ProductionTeamUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? CompanyID { get; set; }
        public decimal? OperatingTime { get; set; }
        public int? WorkCenterID { get; set; }
        public bool? IsDefault { get; set; }
    }
}

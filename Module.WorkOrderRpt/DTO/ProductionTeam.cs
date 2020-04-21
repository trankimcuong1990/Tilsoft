using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class ProductionTeam
    {
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
        public int ProductionTeamID { get; set; }
        public int OPSequenceDetailID { get; set; }
        public string ProductionTeamUD { get; set; }
        public string ProductionTeamNM { get; set; }
    }
}

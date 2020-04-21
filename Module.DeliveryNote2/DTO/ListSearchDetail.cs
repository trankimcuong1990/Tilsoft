using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class ListSearchDetail
    {
        public List<Support.DTO.WorkCenter> WorkCenters { get; set; }
        public List<Support.DTO.ProductionTeam> ProductionTeams { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryTeam
    {
        public int FactoryTeamID { get; set; }
        public string FactoryTeamUD { get; set; }
        public string FactoryTeamNM { get; set; }
        public List<FactoryTeamStep> FactoryTeamSteps { get; set; }
    }
}

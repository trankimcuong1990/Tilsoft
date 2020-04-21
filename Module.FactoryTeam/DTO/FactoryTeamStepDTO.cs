using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryTeam.DTO
{
    public class FactoryTeamStepDTO
    {
        public int FactoryTeamStepID { get; set; }
        public int? FactoryTeamID { get; set; }
        public int? FactoryStepID { get; set; }
        public int? StepIndex { get; set; }
    }
}

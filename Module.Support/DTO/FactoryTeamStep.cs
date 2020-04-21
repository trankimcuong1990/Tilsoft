using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryTeamStep
    {
        public int FactoryTeamStepID { get; set; }
        public int? FactoryTeamID { get; set; }
        public int? FactoryStepID { get; set; }
        public int? StepIndex { get; set; }
        public string FactoryStepNM { get; set; }
        public List<FactoryGoodsProcedureDetail> FactoryGoodsProcedureDetails { get; set; }

    }
}

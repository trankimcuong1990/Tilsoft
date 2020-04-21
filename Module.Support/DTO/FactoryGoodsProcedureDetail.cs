using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryGoodsProcedureDetail
    {
        public int FactoryGoodsProcedureDetailID { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public int? FactoryStepID { get; set; }
        public int? StepIndex { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
        public string FactoryStepNM { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryStep
    {
        public int FactoryStepID { get; set; }
        public string FactoryStepUD { get; set; }
        public string FactoryStepNM { get; set; }
        public List<FactoryGoodsProcedureDetail> FactoryGoodsProcedureDetails { get; set; }
    }
}

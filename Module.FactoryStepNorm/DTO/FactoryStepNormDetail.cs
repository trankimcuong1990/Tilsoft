using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStepNorm.DTO
{
    public class FactoryStepNormDetail
    {
        public int? FactoryStepNormDetailID { get; set; }
        public int? FactoryStepNormID { get; set; }
        public int? FactoryStepID { get; set; }
        public int? StepIndex { get; set; }
        public int? UnitID { get; set; }
        public decimal? TimeNorm { get; set; }
        public string FactoryStepUD { get; set; }
        public string FactoryStepNM { get; set; }
        public string UnitNM { get; set; }
    }
}

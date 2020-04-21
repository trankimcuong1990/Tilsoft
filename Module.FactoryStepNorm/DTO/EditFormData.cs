using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStepNorm.DTO
{
    public class EditFormData
    {
        public FactoryStepNorm Data { get; set; }
        public List<Module.Support.DTO.Unit> Units { get; set; }
        public List<Module.Support.DTO.FactoryStep> FactorySteps { get; set; }
    }
}

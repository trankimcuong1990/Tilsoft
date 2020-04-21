using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryGoodsProcedure.DTO
{
    public class EditFormDataDTO
    {
        public DTO.FactoryGoodsProcedureDTO Data { get; set; }
        public List<Support.DTO.FactoryStep> FactorySteps { get; set; }
    }
}

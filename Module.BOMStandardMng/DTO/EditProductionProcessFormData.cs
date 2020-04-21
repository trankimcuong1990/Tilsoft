using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class EditProductionProcessFormData
    {
        public ProductionProcessDTO Data { get; set; }
        public List<Support.DTO.OPSequence> OPSequences { get; set; }
    }
}

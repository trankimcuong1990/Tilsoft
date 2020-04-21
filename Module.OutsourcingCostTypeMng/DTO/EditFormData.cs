using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostTypeMng.DTO
{
    public class EditFormData
    {
        public DTO.OutsourcingCostTypeDTO Data { get; set; }
        public List<DTO.ProductionItemGroup> ProductionItemGroups { get; set; }
    }
}

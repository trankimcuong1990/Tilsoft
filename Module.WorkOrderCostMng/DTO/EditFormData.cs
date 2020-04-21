using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderCostMng.DTO
{
    public class EditFormData
    {
        public DTO.BOMDTO Data { get; set; }      
        public List<WorkOrderCostDTO> WorkOrderCosts { get; set; }
    }
}

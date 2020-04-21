using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingStandardCostFeeMng.DTO
{
    public class EditForm
    {
        public EditForm()
        {
            Data = new List<OutsourcingStandardCostFeeDTO>();
        }
        public List<DTO.OutsourcingStandardCostFeeDTO> Data { get; set; }
    }
}

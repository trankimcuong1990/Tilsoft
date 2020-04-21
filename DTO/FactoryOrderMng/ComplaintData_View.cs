using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class ComplaintData_View
    {
        public DTO.FactoryOrderMng.ClientComplaint_View Data { get; set; }
        public List<DTO.FactoryOrderMng.ConstantEntry> ComplaintTypes { get; set; }

        public List<DTO.FactoryOrderMng.ConstantEntry> ComplaintStatuses { get; set; }
    }
}

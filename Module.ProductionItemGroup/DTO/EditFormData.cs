using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemGroup.DTO
{
    public class EditFormData
    {
        public DTO.ProductionItemGroupDTO Data { get; set; }
        public List<DTO.UserOnProductionItemGroup> ListUser { get; set; }
    }
}

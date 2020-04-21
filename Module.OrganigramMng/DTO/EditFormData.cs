using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrganigramMng.DTO
{
    public class EditFormData
    {
        public List<DTO.Employee> Data { get; set; }
        public DTO.OrganizationChart OrganizationChart { get; set; }
    }
}

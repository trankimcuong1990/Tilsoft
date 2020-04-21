using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.UserGroup> UserGroups { get; set; }
        public List<DTO.NotYetMappedEmployee> Employees { get; set; }
    }
}

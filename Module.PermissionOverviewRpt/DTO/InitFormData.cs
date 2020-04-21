using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PermissionOverviewRpt.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.User> Users { get; set; }
        public List<Support.DTO.InternalCompany> Offices { get; set; }
        public List<Support.DTO.UserGroup> UserGroups { get; set; }
        public List<DTO.Module> Modules { get; set; }
    }
}

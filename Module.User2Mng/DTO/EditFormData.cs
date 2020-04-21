using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng.DTO
{
    public class EditFormData
    {
        public DTO.UserProfile Data { get; set; }

        public List<Support.DTO.InternalCompany> InternalCompanies { get; set; }
        public List<Support.DTO.InternalDepartment> InternalDepartments { get; set; }
        public List<Support.DTO.Location> Locations { get; set; }
        public List<Support.DTO.UserGroup> UserGroups { get; set; }
        public List<Support.DTO.Employee> Managers { get; set; }
        public List<Support.DTO.JobLevel> JobLevels { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
    }
}

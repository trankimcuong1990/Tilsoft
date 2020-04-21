using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO
{
    public class EditFormData
    {
        public Module.EmployeeMng.DTO.Employee Data { get; set; }
        public List<DTO.Director> Directors { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
        //public List<Module.Support.DTO.InternalCompany> Companies { get; set; }
        public List<Module.Support.DTO.InternalDepartment> Departments { get; set; }
        public List<Module.Support.DTO.JobLevel> JobLevels { get; set; }
        public List<Module.Support.DTO.Location> Locations { get; set; }
        public List<DTO.UnSelectedUser> Users { get; set; }
        public List<Module.Support.DTO.Factory> Factories { get; set; }
        public List<Module.Support.DTO.YesNo> YesNoValues { get; set; }
        public TilsoftUsage TilsoftUsage { get; set; }
        public List<AccountManagerTypeData> AccountManagerType { get; set; }
        public List<CompanyDTO> Companies { get; set; }
        public List<BranchDTO> Branches { get; set; }
        public List<Support.DTO.ConstantEntry> ResponsibleFor { get; set; }
    }
}

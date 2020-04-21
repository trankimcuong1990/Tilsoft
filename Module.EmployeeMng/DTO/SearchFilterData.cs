using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.JobLevel> JobLevels { get; set; }
        //public List<Support.DTO.InternalCompany> Companies { get; set; }
        public List<Support.DTO.InternalDepartment> Departments { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.YesNo> YesNoValues { get; set; }
        public List<Support.DTO.Location> Locations { get; set; }
        public List<CompanyDTO> Companies { get; set; }
        public List<BranchDTO> Branches { get; set; }
    }
}

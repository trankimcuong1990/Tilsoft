using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.Employee> Employees { get; set; }
        public List<Support.DTO.InternalCompany> InternalCompanies { get; set; }
        public List<DTO.Module> Modules { get; set; }
        public List<Support.DTO.Location> Locations { get; set; }
    }
}

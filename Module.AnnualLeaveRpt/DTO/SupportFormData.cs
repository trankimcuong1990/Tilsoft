using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveRpt.DTO
{
    public class SupportFormData
    {
        public List<EmployeeDTO> EmployeeDTOs { get; set; }
        public List<CompanyDTO> CompanyDTOs { get; set; }
    }
}

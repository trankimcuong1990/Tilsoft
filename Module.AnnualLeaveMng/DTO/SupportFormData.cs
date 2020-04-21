using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveMng.DTO
{
    public class SupportFormData
    {
        public List<EmployeeDTO> EmployeeDTOs { get; set; }
        public List<CompanyDTO> CompanyDTOs { get; set; }
        public List<AnnualLeaveStatusDTO> AnnualLeaveStatusDTOs { get; set; }
    }
}

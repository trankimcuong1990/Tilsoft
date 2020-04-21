using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO.Overview
{
    public class ManagementData
    {
        public string ManagerNote { get; set; }
        public List<Overview.EmployeeContractDTO> EmployeeContractDTOs { get; set; }
        public List<Overview.EmployeeFileDTO> EmployeeFileDTOs { get; set; }
    }
}

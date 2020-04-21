using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO.Overview
{
    public class EmployeeContractDTO
    {
        public int EmployeeContractID { get; set; }
        public string ValidFrom { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    }
}

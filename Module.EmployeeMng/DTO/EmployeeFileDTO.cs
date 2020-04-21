using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO
{
    public class EmployeeFileDTO
    {
        public int EmployeeFileID { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string NewFile { get; set; }
        public bool HasChanged { get; set; }
    }
}

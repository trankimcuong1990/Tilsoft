using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO
{
    public class EmployeeFactory
    {
        public int EmployeeFactoryID { get; set; }
        public int? EmployeeID { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
    }
}

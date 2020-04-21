using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ForwarderMng
{
    public class ForwarderPIC
    {
        public int ForwarderPICID { get; set; }
        public int? ForwarderID { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public int? UserID { get; set; }
    }
}

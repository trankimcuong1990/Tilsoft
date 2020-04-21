using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPermissionMng.DTO
{
    public class ClientPermissionDetailDTO
    {
        public int? ClientPermissionDetailID { get; set; }
        public int? ClientPermissionID { get; set; }
        public int? UserID { get; set; }
        public string EmployeeNM { get; set; }
        public int? ModuleID { get; set; }
        public string DisplayText { get; set; }
    }
}

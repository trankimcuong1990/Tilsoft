using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OnlineFileMng.DTO
{
    public class OnlineFilePermission
    {
        public int OnlineFilePermissionID { get; set; }
        public int UserID { get; set; }
        public string EmployeeNM { get; set; }
        public string InternalCompanyNM { get; set; }
    }
}

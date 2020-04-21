using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng.DTO
{
    public class NewAccountData
    {
        public string AspNetUserId { get; set; }
        public int UserGroupID { get; set; }
        public int? EmployeeID { get; set; }
        public bool IsActive { get; set; }
    }
}

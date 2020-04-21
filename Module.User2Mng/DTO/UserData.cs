using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng.DTO
{
    public class UserData
    {
        public int UserID { get; set; }
        public int UserGroupID { get; set; }
        public bool IsSuperUser { get; set; }
        public bool IsActivated { get; set; }
    }
}

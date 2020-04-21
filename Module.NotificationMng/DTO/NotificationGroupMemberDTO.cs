using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DTO
{
    public class NotificationGroupMemberDTO
    {
        public int NotificationGroupMemberID { get; set; }
        public int NotificationGroupID { get; set; }
        public int UserID { get; set; }
        public string Remark { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class NotificationMember
    {
        public int NotificationGroupMemberID { get; set; }
        public string NotificationGroupUD { get; set; }
        public string NotificationGroupNM { get; set; }
        public int UserID { get; set; }
        public string EmployeeNM { get; set; }
        public string Email1 { get; set; }
        public string InternalCompanyNM { get; set; }
    }
}

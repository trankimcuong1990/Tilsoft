using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMessageMng.DTO
{
    public class NotificationMessageDTO
    {
        public int NotificationMessageID { get; set; }
        public string NotificationMessageTag { get; set; }
        public string NotificationMessageTitle { get; set; }
        public string NotificationMessageContent { get; set; }
        public int UserID { get; set; }
        public bool IsSynced { get; set; }
        public string SyncedDate { get; set; }
    }
}

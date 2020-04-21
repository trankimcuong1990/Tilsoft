using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng.DTO
{
    public class NotificationGroupDTO
    {
        public NotificationGroupDTO()
        {
            NotificationGroupMemberData = new List<NotificationGroupMemberDTO>();
            NotificationGroupStatuses = new List<NotificationGroupStatusDTO>();
        }

        public int NotificationGroupID { get; set; }
        public string NotificationGroupUD { get; set; }
        public string NotificationGroupNM { get; set; }
        public string Description { get; set; }        
        public int? ModuleID { get; set; }
        public string DisplayText { get; set; }
        public string EmailTemplate { get; set; }

        public List<DTO.NotificationGroupMemberDTO> NotificationGroupMemberData { get; set; }
        public List<NotificationGroupStatusDTO> NotificationGroupStatuses { get; set; }
        
    }
}

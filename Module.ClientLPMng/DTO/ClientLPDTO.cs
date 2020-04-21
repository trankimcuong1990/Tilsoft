using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng.DTO
{
    public class ClientLPDTO
    {
        public int ClientLPID { get; set; }
        public int? ClientID { get; set; }
        public int? LPManagingTeamID { get; set; }
        public bool? IsAutoUpdateSimilarLP { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string LPManagingTeamNM { get; set; }
        public string UpdaterName { get; set; }
        public List<ClientLPNotificationDTO> ClientLPNotificationDTOs { get; set; }
    }
}

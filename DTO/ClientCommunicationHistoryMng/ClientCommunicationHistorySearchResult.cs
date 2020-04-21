using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientCommunicationHistoryMng
{
    public class ClientCommunicationHistorySearchResult
    {
        public int ClientCommunicationHistoryID { get; set; }
        public string ClientNM { get; set; }
        public string EurofarContactPerson { get; set; }
        public string ClientContactPerson { get; set; }
        public string CommunicationRemark { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}

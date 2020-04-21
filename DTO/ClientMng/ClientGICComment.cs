using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientGICComment
    {
       
        public int? ClientContactHistoryID { get; set; }
       
        public int? ClientID { get; set; }
       
        public string ContactDate { get; set; }
        
        public string ContactTime { get; set; }
        
        public string ContactType { get; set; }
       
        public string CommunicationContent { get; set; }

        public string ContactDateFormated { get; set; }
    }
}

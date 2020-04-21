using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class ClientContactHistory
    {
        [Display(Name = "ClientContactHistoryID")]
        public int? ClientContactHistoryID { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "ContactDate")]
        public string ContactDate { get; set; }

        [Display(Name = "ContactTime")]
        public string ContactTime { get; set; }

        [Display(Name = "ContactType")]
        public string ContactType { get; set; }

        [Display(Name = "CommunicationContent")]
        public string CommunicationContent { get; set; }

        public string ContactDateFormated { get; set; }

        public bool HasChanged { get; set; }
    }
}
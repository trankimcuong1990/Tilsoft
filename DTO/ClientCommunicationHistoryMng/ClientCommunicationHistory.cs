using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientCommunicationHistoryMng
{
    public class ClientCommunicationHistory
    {
        public int ClientCommunicationHistoryID { get; set; }

        [Required]
        [Display(Name = "Client ID")]
        public Nullable<int> ClientID { get; set; }

        [Required]
        [Display(Name = "Eurofar Contact Person")]
        public string EurofarContactPerson { get; set; }

        [Required]
        [Display(Name = "Client Contact Person")]
        public string ClientContactPerson { get; set; }

        [Required]
        [Display(Name = "Communication Remark")]
        public string CommunicationRemark { get; set; }

        [Required]
        [Display(Name = "Uodator Name")]
        public string UpdatorName { get; set; }

        [Required]
        [Display(Name = "Date Updated")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}

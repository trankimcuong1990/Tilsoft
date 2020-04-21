using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DocumentClientMng
{
    public class DocumentClientSearchUpdate
    {
        [Display(Name = "DocumentClientID")]
        public int DocumentClientID { get; set; }

        [Display(Name = "DateEmailToClient")]
        public DateTime? DateEmailToClient { get; set; }

        [Display(Name = "DateSendToClient")]
        public DateTime? DateSendToClient { get; set; }

        [Display(Name = "DateContainerDelivery")]
        public DateTime? DateContainerDelivery { get; set; }

        [Display(Name = "UpdatedBy")]
        public int UpdatedBy { get; set; }

        public string DateEmailToClientFormated { get; set; }

        public string DateSendToClientFormated { get; set; }

        public string DateContainerDeliveryFormated { get;set; }

        public string DHLTrackingNo { get; set; }

        public int IsEdit { get; set; }
    }
}

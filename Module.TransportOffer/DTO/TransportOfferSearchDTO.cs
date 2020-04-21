using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer.DTO
{
    public class TransportOfferSearchDTO
    {
        public int? TransportOfferID { get; set; }
        public string ForwarderNM { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public int? ForwarderID { get; set; }
    }
}

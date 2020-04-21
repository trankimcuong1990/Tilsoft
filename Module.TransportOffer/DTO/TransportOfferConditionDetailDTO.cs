using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer.DTO
{
    public class TransportOfferConditionDetailDTO
    {
        public int? TransportOfferConditionDetailID { get; set; }
        public int? TransportOfferID { get; set; }
        public int? TransportConditionItemID { get; set; }
        public string Condition20DC { get; set; }
        public string Condition40DC { get; set; }
        public string Condition40HC { get; set; }
        public int? PODID { get; set; }
        public int? POLID { get; set; }
        public string Remark { get; set; }
        public string TransportConditionItemNM { get; set; }
    }
}

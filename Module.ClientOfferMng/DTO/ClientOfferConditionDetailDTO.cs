using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng.DTO
{
    public class ClientOfferConditionDetailDTO
    {
        public int? ClientOfferConditionDetailID { get; set; }
        public int? ClientOfferID { get; set; }
        public string Condition20DC { get; set; }
        public string Condition40DC { get; set; }
        public string Condition40HC { get; set; }
        public int? PODID { get; set; }
        public int? POLID { get; set; }
        public string Remark { get; set; }
        public int? ClientConditionItemID { get; set; }
        public string ClientConditionItemNM { get; set; }
    }
}

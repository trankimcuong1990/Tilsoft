using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng.DTO
{
    public class ShippingInstruction
    {
        public int PrimaryID { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public int? PODID { get; set; }
        public string PoDName { get; set; }
        public string NotifyInfo { get; set; }
        public string ConsigneeInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer.DTO
{
    public class TransportOfferCostDetailDTO
    {
        public int? TransportOfferCostDetailID { get; set; }
        public int? TransportOfferID { get; set; }
        public int? TransportCostItemID { get; set; }
        public string Currency { get; set; }
        public decimal? Cost20DC { get; set; }
        public decimal? Cost40DC { get; set; }
        public decimal? Cost40HC { get; set; }
        public int? PODID { get; set; }
        public int? POLID { get; set; }
        public string Remark { get; set; }
        public string TransportCostItemNM { get; set; }
        public int? TransportCostTypeID { get; set; }
    }
}

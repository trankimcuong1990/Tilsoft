using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class TransportCostItemDTO
    {
        public int TransportCostItemID { get; set; }
        public string TransportCostItemNM { get; set; }
        public int? TransportCostTypeID { get; set; }
        public string Currency { get; set; }
        public bool? IsDefault { get; set; }
        public int? TransportCostChargeTypeID { get; set; }
        public string TransportCostTypeNM { get; set; }
        public string TransportCostChargeTypeNM { get; set; }
    }
}

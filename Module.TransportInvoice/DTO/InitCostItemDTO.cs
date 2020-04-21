using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class InitCostItemDTO
    {
        public object KeyID { get; set; }
        public int? TransportCostItemID { get; set; }
        public string Currency { get; set; }
        public int? TransportCostChargeTypeID { get; set; }
        public decimal? Cost20DC { get; set; }
        public decimal? Cost40DC { get; set; }
        public decimal? Cost40HC { get; set; }
    }
}

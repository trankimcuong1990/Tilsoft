using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceContainerTransport
    {
        public int? ECommercialInvoiceContainerTransportID { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public int? ClientCostItemID { get; set; }
        public int? LoadingPlanID { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ETD { get; set; }
    }
}

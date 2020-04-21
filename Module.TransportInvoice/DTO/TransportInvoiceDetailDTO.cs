using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class TransportInvoiceDetailDTO
    {
        public int? TransportInvoiceDetailID { get; set; }
        public int? TransportInvoiceID { get; set; }
        public int? TransportCostItemID { get; set; }
        public decimal? SubTotalAmount { get; set; }
        public string Remark { get; set; }
        public string Currency { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? AlreadyInvoiced { get; set; }
        public int? TransportCostChargeTypeID { get; set; }
        public List<TransportInvoiceContainerDetailDTO> TransportInvoiceContainerDetailDTOs { get; set; }

    }
}

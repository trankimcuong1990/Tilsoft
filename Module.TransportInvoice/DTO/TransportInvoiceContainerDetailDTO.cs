using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class TransportInvoiceContainerDetailDTO
    {
        public int? TransportInvoiceContainerDetailID { get; set; }
        public int? TransportInvoiceDetailID { get; set; }
        public int? LoadingPlanID { get; set; }
        public decimal? Amount { get; set; }
        public string Remark { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? AlreadyInvoiced { get; set; }

    }
}

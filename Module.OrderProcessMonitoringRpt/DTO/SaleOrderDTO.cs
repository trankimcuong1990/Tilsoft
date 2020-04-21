using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrderProcessMonitoringRpt.DTO
{
    public class SaleOrderDTO
    {
        public int SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Season { get; set; }
        public string OrderType { get; set; }
        public string LDS { get; set; }
        public string FileLocation { get; set; }
        public string TrackingStatusNM { get; set; }
        public int? OfferID { get; set; }
        public bool IsSelected { get; set; }

        public List<DTO.FactoryOrderDTO> FactoryOrderDTOs { get; set; }
        public List<DTO.ECommercialInvoiceDTO> ECommercialInvoiceDTOs { get; set; }
        public List<DTO.LoadingPlanDTO> LoadingPlanDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrderProcessMonitoringRpt.DTO
{
    public class OfferDTO
    {
        public int OfferID { get; set; }
        public string OfferUD { get; set; }
        public string OfferDate { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string EmployeeNM { get; set; }
        public string TrackingStatusNM { get; set; }

        public List<DTO.SaleOrderDTO> SaleOrderDTOs { get; set; }
    }
}

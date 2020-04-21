using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrderProcessMonitoringRpt.DTO
{
    public class FactoryOrderDTO
    {
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string OrderDate { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProductionStatus { get; set; }
        public string TrackingStatusNM { get; set; }
        public int? SaleOrderID { get; set; }
        public bool IsSelected { get; set; }
    }
}

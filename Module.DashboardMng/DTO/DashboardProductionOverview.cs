using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardProductionOverview
    {
        public int? FactoryOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string OrderDate { get; set; }
        public string LDS1 { get; set; }
        public string LDS2 { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string CreatedDate { get; set; }
        public string ShipmentType { get; set; }
        public decimal? Turnover { get; set; }
        public int? SaleOrderID { get; set; }
    }
}

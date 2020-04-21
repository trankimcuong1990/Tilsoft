using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class FactoryOrderDetailOverView
    {
        public int FactoryOrderDetailID { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public string LDS { get; set; }
        public string ETD { get; set; }
        public int? OrderQnt { get; set; }
        public int? ShippedQnt { get; set; }
        public int? FactoryID { get; set; }
        public int? FactoryOrderID { get; set; }
        public string FactoryUD { get; set; }
        public string Season { get; set; }
        public int? ClientID { get; set; }
        public int? TotalQnt { get; set; }
        public int? SaleOrderID { get; set; }
        public List<DTO.SaleOrderMng.FactoryOrderDetailQCReportOverView> factoryOrderDetailQCReportOverViews { get; set; }
    }
}

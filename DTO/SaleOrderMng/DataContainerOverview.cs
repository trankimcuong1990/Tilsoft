using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class DataContainerOverview
    {
        public SaleOrderOverview SaleOrderData { get; set; }
        public List<DTO.SaleOrderMng.OrderType> OrderTypes { get; set; }
        public List<DTO.SaleOrderMng.CostType> CostTypes { get; set; }
        public List<DTO.SaleOrderMng.PaymentStatus> PaymentStatuses { get; set; }
        public List<DTO.Support.ReportTemplate> ReportTemplates { get; set; }
        public List<DTO.SaleOrderMng.FactoryOrderDetailOverView> FactoryOrderDetailOverViews { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class DataContainer
    {
        public SaleOrder SaleOrderData { get; set; }
        public List<Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.PaymentTerm> PaymentTerms { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.SaleOrderMng.OrderType> OrderTypes { get; set; }
        public List<Support.ProductStatus> ProductStatuses { get; set; }
        public List<DTO.SaleOrderMng.CostType> CostTypes { get; set; }
        public List<DTO.SaleOrderMng.PaymentStatus> PaymentStatuses { get; set; }
        public List<DTO.Support.VATPercent> VATPercents { get; set; }
        public List<DTO.Support.ReportTemplate> ReportTemplates { get; set; }
    } 
}

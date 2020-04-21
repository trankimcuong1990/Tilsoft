using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class DataContainer
    {
        public SaleOrder SaleOrderData { get; set; }
        public List<Support.DTO.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.DTO.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<DTO.OrderType> OrderTypes { get; set; }
        public List<DTO.ProductStatus> ProductStatuses { get; set; }
        public List<DTO.CostType> CostTypes { get; set; }
        public List<DTO.PaymentStatus> PaymentStatuses { get; set; }
        public List<DTO.VATPercent> VATPercents { get; set; }
        public List<Support.DTO.ReportTemplate> ReportTemplates { get; set; }
    }
}

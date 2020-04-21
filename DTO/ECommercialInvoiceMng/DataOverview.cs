using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class DataOverview
    {
        public ECommercialInvoiceOverview ECommercialInvoiceData { get; set; }
        public List<Support.ReportTemplate> ReportTemplates { get; set; }
        public List<ClientCostItem> ClientCostItems { get; set; }
        public List<ContainerTransport> ContainerTransports { get; set; }
        public List<ClientOfferCostItem> ClientOfferCostItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class DataContainer
    {
        public ECommercialInvoice ECommercialInvoiceData { get; set; }
        public List<DeliveryTerm> DeliveryTerms { get; set; }
        public List<PaymentTerm> PaymentTerms { get; set; }
        public List<TurnOverLedgerAccount> TurnOverLedgerAccounts { get; set; }
        public List<DTO.Support.VATPercent> VATPercents { get; set; }
        public List<Support.Currency> Currency { get; set; }
        public List<CostType> CostTypes { get; set; }
        public List<Support.Season> Seasons { get; set; }
        public List<Support.ReportTemplate> ReportTemplates { get; set; }
        public List<ClientCostItem> ClientCostItems { get; set; } // is only use for invoice with type = 5 (container transport invoice)
        public List<ContainerTransport> ContainerTransports { get; set; } // is only use for invoice with type = 5 (container transport invoice)
        public List<ClientOfferCostItem> ClientOfferCostItems { get; set; } // is only use for invoice with type = 5 (container transport invoice)       
    }
}

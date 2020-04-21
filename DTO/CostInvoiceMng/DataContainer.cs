using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CostInvoiceMng
{
    public class DataContainer
    {
        public CostInvoice CostInvoiceData { get; set; }
        public List<Support.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.PaymentTerm> PaymentTerms { get; set; }
        public List<Support.Currency> Currency { get; set; }
        public List<DTO.CostInvoiceMng.CostType> CostTypes { get; set; }
    }
}

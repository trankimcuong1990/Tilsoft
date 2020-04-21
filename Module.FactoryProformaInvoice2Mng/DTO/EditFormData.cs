using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng.DTO
{
    public class EditFormData
    {
        public DTO.FactoryProformaInvoice Data { get; set; }
        public List<Support.DTO.DeliveryTerm> DeliveryTerms { get; set; }
        public List<Support.DTO.PaymentTerm> PaymentTerms { get; set; }
    }
}
